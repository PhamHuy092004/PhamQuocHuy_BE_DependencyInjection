using PhamQuocHuy_BE_DependencyInjection.Model;
using System.Security.Principal;

namespace PhamQuocHuy_BE_DependencyInjection.Services
{
    public interface IUsersService
    {
        public IEnumerable<Users> GetUsers();
        public Users AddUser(Users users);
    }
}
