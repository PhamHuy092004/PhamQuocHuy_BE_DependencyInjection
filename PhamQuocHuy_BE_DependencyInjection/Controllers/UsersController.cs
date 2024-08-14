using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhamQuocHuy_BE_DependencyInjection.Model;
using PhamQuocHuy_BE_DependencyInjection.Services;
using System.Security.Principal;

namespace PhamQuocHuy_BE_DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IUsersService usersService;

        public UsersController(IUsersService usersService) =>
            this.usersService = usersService;

        [HttpGet]
        public ActionResult<IEnumerable<Users>> GetUsers()
        {
            var users = usersService.GetUsers();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public ActionResult<Users> GetUserById(int id)
        {
            var user = usersService.GetUsers().FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

       [HttpPost]
        public ActionResult<Users> AddUser(Users user)
        {
            if (ModelState.IsValid)
            {
                var addedUser = usersService.AddUser(user);
                return CreatedAtAction(nameof(GetUserById), new { id = addedUser.Id }, addedUser);
            }

            return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public ActionResult<Users> UpdateUser(int id, [FromBody] Users user)
        {
            if (id != user.Id)
            {
                return BadRequest("ID không tìm thấy");
            }

            var updatedUser = usersService.UpdateUser(id, user);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = usersService.DeleteUser(id);

            if (user != null)
            {
                return Ok(new { message = "Đã xóa thành công User.", user });
            }
            else
            {
                return NotFound(new { message = "User not found." });
            }
        }

    }
}
