using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Data;
using Repository.Model;
using WebShop.Controllers;
using WebShop.UnitOfWork;
using Xunit;

public class ProductControllerTest
{
    private readonly IUnitOfWork _fakeUnitOfWork;
    private readonly ProductController _controller;

    public ProductControllerTest()
    {
        
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        var fakeDbContext = new MyDbContext(options);

        _fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var fakeProductRepository = A.Fake<IProductRepository>();

        A.CallTo(() => _fakeUnitOfWork.Products).Returns(fakeProductRepository);

        _controller = new ProductController(fakeDbContext, _fakeUnitOfWork, fakeProductRepository);
    }

    [Fact]
    public void GetProducts_ReturnsOkResult_WithListOfProducts()
    {
        // Arrange
        var fakeProducts = new List<Product>
    {
        new Product { Id = 1, Name = "Product1", Price = 100, Stock = 10 },
        new Product { Id = 2, Name = "Product2", Price = 200, Stock = 20 }
    };
        A.CallTo(() => _fakeUnitOfWork.Products.GetAll()).Returns(fakeProducts);

        // Act
        var result = _controller.GetProducts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result); 
        var returnedProducts = Assert.IsType<List<Product>>(okResult.Value); 
        Assert.Equal(2, returnedProducts.Count); 
    }


    [Fact]
    public void GetProduct_WithValidId_ReturnsOkResult_WithProduct()
    {
        // Arrange
        var fakeProduct = new Product { Id = 1, Name = "Product1", Price = 100, Stock = 10 };
        A.CallTo(() => _fakeUnitOfWork.Products.Get(1)).Returns(fakeProduct);

        // Act
        var result = _controller.GetProduct(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedProduct = Assert.IsType<Product>(okResult.Value); 
        Assert.Equal(1, returnedProduct.Id);
        Assert.Equal("Product1", returnedProduct.Name); 
    }

    [Fact]
    public void GetProduct_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        A.CallTo(() => _fakeUnitOfWork.Products.Get(99)).Returns(null);

        // Act
        var result = _controller.GetProduct(99);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}









