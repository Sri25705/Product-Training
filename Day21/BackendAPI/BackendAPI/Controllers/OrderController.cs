using Microsoft.AspNetCore.Mvc;
using BackendAPI.Models;
using BackendAPI.Repository;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _repo;

        public OrderController(OrderRepository repo)
        {
            _repo = repo;
        }

        // GET ALL
        [HttpGet]
        public ActionResult<List<Order>> GetOrders()
        {
            return Ok(_repo.GetOrders());
        }

        // GET BY ID
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _repo.GetOrderById(id);
            if (order == null)
                return NotFound($" Order with ID {id} not found");

            return Ok(order);
        }

        // POST

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            int orderId = _repo.AddOrder(order);

            if (orderId > 0)
                return Ok(orderId);

            return BadRequest("Failed to add order");
        }


        // PUT
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order order)
        {
            if (id != order.OrderId)
                return BadRequest("Order ID mismatch");

            bool result = _repo.UpdateOrder(order);

            if (result)
                return Ok(new { message = " Order updated successfully" });

            return NotFound($" Order with ID {id} not found");
        }

        // DELETE
        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder(int orderId)
        {
            bool result = _repo.DeleteOrder(orderId);

            if (!result)
                return NotFound(new { message = "Order not found" });

            return Ok(new { message = "Order deleted successfully" });
        }



        [HttpGet("details/{userId}")]
        public ActionResult<OrderDetailsDto> GetOrderDetails(int userId)
        {
            var result = _repo.GetOrderDetailsByUser(userId);

            if (result == null)
                return NotFound("No orders found for this user");

            return Ok(result);
        }

        [HttpPost("add-item")]
        public IActionResult AddOrderItem(OrderItem item)
        {
            bool result = _repo.AddOrderItem(item);

            if (result) return Ok(new { message = "Added to order" });

            return BadRequest("Failed");
        }

        [HttpGet("all-items/{userId}")]
        public IActionResult GetAllItems(int userId)
        {
            var result = _repo.GetAllItems(userId);

            if (result == null || result.Count == 0)
                return NotFound("No items found");

            return Ok(result);
        }


        [HttpGet("latest/{userId}")]
        public IActionResult GetLatestOrder(int userId)
        {
            int orderId = _repo.GetLatestOrderId(userId);

            if (orderId == 0)
                return NotFound("No order found");

            return Ok(orderId);
        }




    }
}
