using AutoMapper;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UsersServices> _logger;

        public UsersServices(IUsersData usersData, IMapper mapper, ILogger<UsersServices> logger)
        {
            this._usersData = usersData;
            _mapper = mapper;
            _logger = logger;
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
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public async Task<UserDto> Register(RegisterUserDto registerUserDto)
        {
            if (!IsValidEmail(registerUserDto.UserName))
            {
                throw new CustomApiException(400, "Invalid email format.");
            }
            if (string.IsNullOrWhiteSpace(registerUserDto.UserName))
            {
                throw new CustomApiException(400, "Username is required.");
            }
            if (registerUserDto.Password.Length < 6)
            {
                throw new CustomApiException(400, "Password must be at least 6 characters.");
            }

            if (!ValidatePasswordStrength(registerUserDto.Password))
            {
                throw new CustomApiException(400, "Password not strong enough.");
            }

            User user = _mapper.Map<User>(registerUserDto);
            await _usersData.Register(user);
            UserDto userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto?> Login(LoginUserDto loginUser)
        {
            try
            {
                User? user = await _usersData.Login(loginUser);

                if (user == null)
                {
                    _logger.LogWarning("Login failed for username: {UserName}. Invalid credentials or user not found.", loginUser.UserName);
                    return null;
                }

                _logger.LogInformation("User {UserName} logged in successfully", user.UserName);
                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed for username: {UserName} due to an unexpected error.", loginUser.UserName);
                return null;
            }
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
