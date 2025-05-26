using DTOs;
using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UserRepositoryUnitTest
    {
        [Fact]
        public async Task GetUserByIdFromDB_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var user = new User { UserId = 1, UserName = "testuser", Password = "password" };
            var users = new List<User> { user };

            var mockContext = new Mock<ShopDB327742698Context>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var usersData = new UsersData(mockContext.Object);

            // Act
            var result = await usersData.GetUserByIdFromDB(1);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetUserByIdFromDB_ReturnsNull_WhenUserDoesNotExist()
        {
            // Arrange
            var users = new List<User>();

            var mockContext = new Mock<ShopDB327742698Context>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var usersData = new UsersData(mockContext.Object);

            // Act
            var result = await usersData.GetUserByIdFromDB(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Register_AddsUserToDatabase()
        {
            // Arrange
            var user = new User { UserId = 1, UserName = "newuser", Password = "password" };

            var mockContext = new Mock<ShopDB327742698Context>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(new List<User>());

            var usersData = new UsersData(mockContext.Object);

            // Act
            await usersData.Register(user);

            // Assert
            mockContext.Verify(x => x.Users.AddAsync(It.Is<User>(u => u == user), It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Register_ThrowsException_WhenUsernameAlreadyExists()
        {
            // Arrange
            var existingUser = new User { UserId = 1, UserName = "existinguser", Password = "password" };
            var users = new List<User> { existingUser };

            var mockContext = new Mock<ShopDB327742698Context>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var usersData = new UsersData(mockContext.Object);

            // Act & Assert
            await Assert.ThrowsAsync<CustomApiException>(() => usersData.Register(existingUser));
        }

        [Fact]
        public async Task Login_ReturnsUser_WhenCredentialsAreValid()
        {
            // Arrange
            var user = new User { UserId = 1, UserName = "testuser", Password = "password" };
            var users = new List<User> { user };

            var mockContext = new Mock<ShopDB327742698Context>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var usersData = new UsersData(mockContext.Object);

            var loginUser = new LoginUserDto ("testuser", "password");

            // Act
            var result = await usersData.Login(loginUser);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task Login_ReturnsNull_WhenCredentialsAreInvalid()
        {
            // Arrange
            var user = new User { UserId = 1, UserName = "testuser", Password = "password" };
            var users = new List<User> { user };

            var mockContext = new Mock<ShopDB327742698Context>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var usersData = new UsersData(mockContext.Object);

            var loginUser = new LoginUserDto ( "wronguser", "wrongpassword" );

            // Act
            var result = await usersData.Login(loginUser);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUser_UpdatesUserInDatabase()
        {
            // Arrange
            var user = new User { UserId = 1, UserName = "testuser", Password = "password" };

            var mockContext = new Mock<ShopDB327742698Context>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(new List<User> { user });

            var usersData = new UsersData(mockContext.Object);

            var updatedUser = new User { UserId = 1, UserName = "updateduser", Password = "newpassword" };

            // Act
            var result = await usersData.UpdateUser(1, updatedUser);

            // Assert
            mockContext.Verify(x => x.Update(It.Is<User>(u => u == updatedUser)), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(updatedUser, result);
        }
    }
}
