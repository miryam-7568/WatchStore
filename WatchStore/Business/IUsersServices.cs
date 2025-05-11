using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IUsersServices
    {
        public bool ValidatePasswordStrength(string password);
        public Task<User> GetUserById(int id);
        public Task Register(User user);
        public Task<User> Login(LoginUser loginUser);
        public Task<User> UpdateUser(int id, User userToUpdate);

    }
}
