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
            return Ok(usersService.GetUsers());
        }

        [HttpPost]
        public ActionResult<Users> AddUser(Users user)
        {
            var addedUser = usersService.AddUser(user);
            return CreatedAtAction(nameof(GetUsers), new { id = addedUser.Id }, addedUser);
        }
    }
}
