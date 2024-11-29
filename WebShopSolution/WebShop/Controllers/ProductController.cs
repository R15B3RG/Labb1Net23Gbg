using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Data;
using Repository.Model;
using WebShop.UnitOfWork;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly MyDbContext _context;
        private IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(MyDbContext context, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _context = context;
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), "UnitOfWork cannot be null");
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository), "ProductRepository cannot be null");
        }

        // Endpoint för att hämta alla produkter
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _unitOfWork.Products.GetAll();

            if (products == null || !products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _unitOfWork.Products.Get(id);

            if (product == null) return NotFound(); 

            return Ok(product); 
        }

        // Endpoint för att lägga till en ny produkt
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            if (product == null) return BadRequest("Product can not be null!");

            _context.Products.Add(product);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProduct), new {id = product.Id}, product);
        }

        [HttpPut]
        public ActionResult UpdateProduct(Product product, int id)
        {
            var updateProduct = _context.Products.FirstOrDefault(p => p.Id == id);

            if (updateProduct == null) return NotFound();

            updateProduct.Id = product.Id;
            updateProduct.Name = product.Name;
            updateProduct.Price = product.Price;
            updateProduct.Category = product.Category;
            updateProduct.CategoryId = product.CategoryId;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete]

        public ActionResult DeleteProduct(int id)
        {
            var deleteProduct = _context.Products.FirstOrDefault(p => p.Id == id);

            if (deleteProduct == null) return NotFound();

            _context.Products.Remove(deleteProduct);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
