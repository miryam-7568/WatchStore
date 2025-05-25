using DTOs;
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
        public Task<UserDto> GetUserById(int id);
        public Task<UserDto> Register(RegisterUserDto user);
        public Task<UserDto> Login(LoginUserDto loginUser);
        public Task<UserDto> UpdateUser(int id, RegisterUserDto userToUpdate);

    }
}
