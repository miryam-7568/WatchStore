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
        public User GetUserById(int id);
        public void Register(User user);
        public User Login(LoginUser loginUser);
        public User UpdateUser(int id, User userToUpdate);

    }
}
