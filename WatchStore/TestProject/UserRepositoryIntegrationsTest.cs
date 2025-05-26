using DTOs;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UserRepositoryIntegrationsTest : IClassFixture<DatabaseFixture>
    {
        private readonly ShopDB327742698Context _context;
        private readonly UsersData _usersData;

        public UserRepositoryIntegrationsTest(DatabaseFixture fixture)
        {
            _context = fixture.Context;
            _usersData = new UsersData(_context);
        }

        [Fact]
        public async Task Register_AddsUserToDatabase()
        {
            // Arrange
            var user = new User
            {
                UserName = "testuser",
                Password = "password123",
                FirstName = "Test",
                LastName = "User"
            };

            // Act
            var result = await _usersData.Register(user);

            // Assert
            var savedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == "testuser");
            Assert.NotNull(savedUser);
            Assert.Equal("Test", savedUser.FirstName);
        }

        [Fact]
        public async Task Login_ReturnsUser_WhenCredentialsAreValid()
        {
            // Arrange
            var user = new User
            {
                UserName = "validuser",
                Password = "validpassword",
                FirstName = "Valid",
                LastName = "User"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var loginDto = new LoginUserDto
            (
                 "validuser",
                 "validpassword"
            );

            // Act
            var result = await _usersData.Login(loginDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Valid", result.FirstName);
        }

        [Fact]
        public async Task UpdateUser_UpdatesUserInDatabase()
        {
            // Arrange
            var user = new User
            {
                UserName = "updatableuser",
                Password = "password",
                FirstName = "OldFirstName",
                LastName = "OldLastName"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var updatedUser = new User
            {
                UserName = "updatableuser",
                Password = "newpassword",
                FirstName = "NewFirstName",
                LastName = "NewLastName"
            };

            // Act
            var result = await _usersData.UpdateUser(user.UserId, updatedUser);

            // Assert
            var savedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == "updatableuser");
            Assert.NotNull(savedUser);
            Assert.Equal("NewFirstName", savedUser.FirstName);
        }
    }
}
