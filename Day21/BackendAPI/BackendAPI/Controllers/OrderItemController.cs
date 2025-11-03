using Microsoft.AspNetCore.Mvc;
using BackendAPI.Models;
using BackendAPI.Repository;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemRepository _repo;

        public OrderItemController(OrderItemRepository repo)
        {
            _repo = repo;
        }

        // GET ALL
        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _repo.GetAll();
            return Ok(items);
        }

        // GET BY ORDER ID
        [HttpGet("Order/{orderId}")]
        public IActionResult GetByOrderId(int orderId)
        {
            var items = _repo.GetByOrderId(orderId);
            return Ok(items);
        }

        // ADD
        [HttpPost]
        public IActionResult Add(OrderItem i)
        {
            _repo.Add(i);
            return Ok(new { message = "OrderItem Added Successfully!" });
        }

        // UPDATE
        [HttpPut]
        public IActionResult Update(OrderItem i)
        {
            _repo.Update(i);
            return Ok(new { message = "OrderItem Updated Successfully!" });
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            return Ok(new { message = "OrderItem Deleted Successfully!" });
        }
    }
}
