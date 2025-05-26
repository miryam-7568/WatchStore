using AutoMapper;
using DTOs;
using Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UsersServices
        : IUsersServices
    {
        IUsersData _usersData;
        private readonly IMapper _mapper;

        public UsersServices(IUsersData usersData, IMapper mapper)
        {
            this._usersData = usersData;
            _mapper = mapper;
        }
        private bool ValidatePasswordStrength(string password)
        {
            var zxcvbnResult = Zxcvbn.Core.EvaluatePassword(password);
            return zxcvbnResult.Score >= 3; 
        }

        public async Task<UserDto> GetUserById(int id)
        {
            User user = await _usersData.GetUserByIdFromDB(id);
            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto> Register(RegisterUserDto registerUserDto)
        {
            if (ValidatePasswordStrength(registerUserDto.Password))
            {
                User user = _mapper.Map<User>(registerUserDto);
                await _usersData.Register(user);
                UserDto userDto = _mapper.Map<UserDto>(user);
                return userDto;
            }
            else
            {
                throw new CustomApiException(400, "password not strong enough");
            }
        }
        public async Task<UserDto> Login(LoginUserDto loginUser)
        {
            User user= await _usersData.Login(loginUser);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUser(int id, RegisterUserDto userToUpdate)
        {
            if (ValidatePasswordStrength(userToUpdate.Password))
            {
                User user = _mapper.Map<User>(userToUpdate);
                User updatedUser = await _usersData.UpdateUser(id, user);
                return _mapper.Map<UserDto>(updatedUser);
            }
            else
            {
                throw new CustomApiException(400, "password not strong enough");
            }
        }

    }
}
