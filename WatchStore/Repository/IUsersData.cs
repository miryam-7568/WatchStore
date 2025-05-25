using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace Repository
{
    public interface IUsersData
    {
        public Task<User> GetUserByIdFromDB(int id);
        public Task<User> Register(User user);
        public Task<User> Login(LoginUserDto loginUser);
        public Task<User> UpdateUser(int id, User userToUpdate);

    }
}
