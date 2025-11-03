using Microsoft.AspNetCore.Mvc;
using BackendAPI.Models;
using BackendAPI.Repository;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repo;

        public UserController(UserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _repo.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _repo.GetById(id);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        // ✅ Modified
        [HttpPost]
        public IActionResult AddUser(User u)
        {
            bool added = _repo.Add(u);

            if (!added)
                return Conflict("Username already exists!");

            return Ok(new { message = "User added successfully!" });
        }

        [HttpPut]
        public IActionResult UpdateUser(User user)
        {
            var existing = _repo.GetById(user.Id);
            if (existing == null)
                return NotFound("User not found");

            _repo.Update(user);
            return Ok(new { message = "User updated successfully!" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            var user = _repo.Login(login.Name, login.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool removed = _repo.Delete(id);

            if (!removed)
                return NotFound("User not found");

            return Ok("User deleted");
        }
    }
}
