using AutoMapper;
using Business;
using DTOs;
using Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UsersServicesUnitTest
    {
        [Fact]
        public async Task Register_StrongPassword_ReturnsUserDto()
        {
            // Arrange  
            var mockUsersData = new Mock<IUsersData>();
            var mockMapper = new Mock<AutoMapper.IMapper>();
            var loggerMock = new Mock<ILogger<UsersServices>>();

            var usersServices = new UsersServices(mockUsersData.Object, mockMapper.Object, loggerMock.Object);

            var registerUserDto = new RegisterUserDto("TestUser", "StrongP@ssw0rd!", "TestFirstName", "TestLastName");
            var user = new User { UserName = "TestUser", Password = "StrongP@ssw0rd!" };
            var userDto = new UserDto(1,"TestUser", "TestFirstName", "TestLastName");

            mockMapper.Setup(m => m.Map<User>(registerUserDto)).Returns(user);
            mockMapper.Setup(m => m.Map<UserDto>(user)).Returns(userDto);
            mockUsersData.Setup(u => u.Register(user)).ReturnsAsync(user);

            // Act  
            var result = await usersServices.Register(registerUserDto);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(userDto.UserName, result.UserName);
        }

        [Fact]
        public async Task Register_WeakPassword_ThrowsException()
        {
            // Arrange  
            var mockUsersData = new Mock<IUsersData>();
            var mockMapper = new Mock<AutoMapper.IMapper>();
            var loggerMock = new Mock<ILogger<UsersServices>>();
            var usersServices = new UsersServices(mockUsersData.Object, mockMapper.Object, loggerMock.Object);

            var registerUserDto = new RegisterUserDto
            (
                 "TestUser",
                 "12345", // Weak password  
                "Test",
                "User"
            );

            // Act & Assert  
            var exception = await Assert.ThrowsAsync<CustomApiException>(() => usersServices.Register(registerUserDto));
            Assert.Equal(400, exception.StatusCode);
            Assert.Equal("password not strong enough", exception.Message);
        }

        [Fact]
        public async Task UpdateUser_StrongPassword_ReturnsUpdatedUserDto()
        {
            // Arrange  
            var mockUsersData = new Mock<IUsersData>();
            var mockMapper = new Mock<AutoMapper.IMapper>();
            var loggerMock = new Mock<ILogger<UsersServices>>();
            var usersServices = new UsersServices(mockUsersData.Object, mockMapper.Object, loggerMock.Object);

            var userToUpdate = new RegisterUserDto
            (
                "UpdatedUser",
                "StrongP@ssw0rd!",
                 "Updated",
                 "User"
            );

            var user = new User { UserName = "UpdatedUser", Password = "StrongP@ssw0rd!" };
            var updatedUser = new User { UserName = "UpdatedUser", Password = "StrongP@ssw0rd!" };
            var updatedUserDto = new UserDto(2, "UpdatedUser", "a", "b");

            mockMapper.Setup(m => m.Map<User>(userToUpdate)).Returns(user);
            mockUsersData.Setup(u => u.UpdateUser(1, user)).ReturnsAsync(updatedUser);
            mockMapper.Setup(m => m.Map<UserDto>(updatedUser)).Returns(updatedUserDto);

            // Act  
            var result = await usersServices.UpdateUser(1, userToUpdate);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(updatedUserDto.UserName, result.UserName);
        }

        [Fact]
        public async Task UpdateUser_WeakPassword_ThrowsException()
        {
            // Arrange  
            var mockUsersData = new Mock<IUsersData>();
            var mockMapper = new Mock<AutoMapper.IMapper>();
            var loggerMock = new Mock<ILogger<UsersServices>>();
            var usersServices = new UsersServices(mockUsersData.Object, mockMapper.Object, loggerMock.Object);

            var userToUpdate = new RegisterUserDto
            (
                "UpdatedUser",
                 "12345", // Weak password  
                 "Updated",
                "User"
            );

            // Act & Assert  
            var exception = await Assert.ThrowsAsync<CustomApiException>(() => usersServices.UpdateUser(1, userToUpdate));
            Assert.Equal(400, exception.StatusCode);
            Assert.Equal("password not strong enough", exception.Message);
        }


        [Fact]
        public async Task Login_ValidUser_LogsInformation()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UsersServices>>();
            var usersDataMock = new Mock<IUsersData>();
            var mapperMock = new Mock<IMapper>();

            var user = new User { UserName = "testuser", Password = "pass" };
            var userDto = new UserDto(1, "testuser", "Test", "User");

            usersDataMock.Setup(x => x.Login(It.IsAny<LoginUserDto>()))
                         .ReturnsAsync(user);
            mapperMock.Setup(x => x.Map<UserDto>(user))
                      .Returns(userDto);

            var service = new UsersServices(usersDataMock.Object, mapperMock.Object, loggerMock.Object);

            // Act
            var result = await service.Login(new LoginUserDto("testuser", "pass"));

            // Assert
            Assert.NotNull(result);
            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString().Contains("logged in successfully")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Login_InvalidUser_LogsWarning()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UsersServices>>();
            var usersDataMock = new Mock<IUsersData>();
            var mapperMock = new Mock<IMapper>();

            usersDataMock.Setup(x => x.Login(It.IsAny<LoginUserDto>()))
                         .ReturnsAsync((User)null!);

            var service = new UsersServices(usersDataMock.Object, mapperMock.Object, loggerMock.Object);

            // Act
            var result = await service.Login(new LoginUserDto("nonexistent", "wrongpass"));

            // Assert
            Assert.Null(result);
            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString().Contains("Login failed")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }




    }
}



