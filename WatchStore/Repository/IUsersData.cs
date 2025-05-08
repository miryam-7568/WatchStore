using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUsersData
    {
        public User GetUserByIdFromDB(int id);
        public void Register(User user);
        public User Login(LoginUser loginUser);
        public User UpdateUser(int id, User userToUpdate);

    }
}
