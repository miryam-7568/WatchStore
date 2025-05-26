using Business;
using DTOs;
using Entities;
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
            var usersServices = new UsersServices(mockUsersData.Object, mockMapper.Object);

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
            var usersServices = new UsersServices(mockUsersData.Object, mockMapper.Object);

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
            var usersServices = new UsersServices(mockUsersData.Object, mockMapper.Object);

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
            var usersServices = new UsersServices(mockUsersData.Object, mockMapper.Object);

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
    }
}
