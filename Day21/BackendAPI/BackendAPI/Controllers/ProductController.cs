using BackendAPI.Models;
using BackendAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _repo;

        public ProductController(ProductRepository repo)
        {
            _repo = repo;
        }

        // GET
        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            var products = _repo.GetAllProducts();
            return Ok(products);
        }

        // GET
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _repo.GetProductById(id);
            if (product == null)
                return NotFound($" Product with ID {id} not found");
            return Ok(product);
        }

        // POST
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            bool result = _repo.AddProduct(product);
            if (result)
                return Ok(new { message = " Product added successfully" });
            return BadRequest(" Failed to add product");
        }

        // PUT
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
                return BadRequest("Product ID mismatch");

            bool result = _repo.UpdateProduct(product);
            if (result)
                return Ok(new { message = " Product updated successfully" });

            return NotFound($" Product with ID {id} not found");
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            bool result = _repo.DeleteProduct(id);
            if (result)
                return Ok(new { message = "Product deleted successfully" });

            return NotFound($" Product with ID {id} not found");
        }
    }
}
