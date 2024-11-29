using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Model;
using WebShop.Notifications;
using WebShop.UnitOfWork;
using Xunit;

public class UnitOfWorkTest
{
    private readonly DbContextOptions<MyDbContext> _options;

    public UnitOfWorkTest()
    {
        _options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "WebShopTestDb")
            .Options;
    }

    [Fact]
    public void ProductsRepository_CanAddAndRetrieveProduct()
    {
        // Arrange
        var fakeSubject = A.Fake<ProductSubject>();

        using (var context = new MyDbContext(_options))
        {
            var unitOfWork = new UnitOfWork(context, fakeSubject);
            var product = new Product { Name = "TestProduct", Price = 50, Stock = 5 };

            // Act
            unitOfWork.Products.Add(product);
            unitOfWork.Save();

            var retrievedProduct = unitOfWork.Products.Get(product.Id);

            // Assert
            Assert.NotNull(retrievedProduct);
            Assert.Equal("TestProduct", retrievedProduct.Name);
        }
    }

    [Fact]
    public void ProductsRepository_CanUpdateProductStock()
    {
        // Arrange
        var fakeSubject = A.Fake<ProductSubject>();

        using (var context = new MyDbContext(_options))
        {
            var unitOfWork = new UnitOfWork(context, fakeSubject);
            var product = new Product { Name = "TestProduct", Price = 50, Stock = 5 };
            unitOfWork.Products.Add(product);
            unitOfWork.Save();

            // Act
            unitOfWork.Products.UpdateProductStock(product, 10);
            unitOfWork.Save();

            var updatedProduct = unitOfWork.Products.Get(product.Id);

            // Assert
            Assert.NotNull(updatedProduct);
            Assert.Equal(15, updatedProduct.Stock);
        }
    }
}








