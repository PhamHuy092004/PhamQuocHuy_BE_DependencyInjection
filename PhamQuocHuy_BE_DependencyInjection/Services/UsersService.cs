using PhamQuocHuy_BE_DependencyInjection.Model;
using System.Security.Principal;

namespace PhamQuocHuy_BE_DependencyInjection.Services
{
    public class UsersService : IUsersService
    {
        private static List<Users> userList = new List<Users>();
        private static int nextId = 1;
        public IEnumerable<Users> GetUsers()
        {
            return userList;
        }
        public Users AddUser(Users user)
        {
            user.Id = nextId++; 
            userList.Add(user);
            return user;
        }

      
    }
}
