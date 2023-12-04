//using Castle.Core.Configuration;
//using Castle.Core.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NSubstitute;
using NSubstitute.Extensions;
using System.Net;
using users.Business.Implementation;
using users.Business.Interface;
using users.DataAccess.Interface;
using users.DataObjects.DataModels;
using UsersAPI.Controllers;
using Xunit;


namespace UsersApiTest
{
    public class UsersControllersShould : ControllerBase
    {
        private readonly IUserBusiness _usersBussiness;
        private readonly UserController _usersController;
        private readonly IConfiguration _configuration;
        private readonly IUserDataAccess _usersDataAccess;

        [Fact]
        public async Task CreateUser_ReturnsOkResult()
        {
            // Arrange
            var userBusiness = Substitute.For<IUserBusiness>();
            var controller = new UserController(userBusiness);
            var user = new User { 
             UserId = 1,
             UserName = "Test",
             UserEmail = "Test",
             UserRole = "Test"
            };

            // Act
            var result = await controller.CreateUser(user);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

        }
        [Fact]
        public async Task GetAllUsers_ReturnsOkResult()
        {
            // Arrange
            var userBusiness = Substitute.For<IUserBusiness>();
            var controller = new UserController(userBusiness);
         


            // Act
            var result = await controller.GetAllUsers();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
           // var okResult = result as OkObjectResult;
            //okResult.Should().NotBeNull();
        }

        [Fact]
        public async Task GetUsersById_ReturnsOkResult()
        {
            // Arrange
            var userBusiness = Substitute.For<IUserBusiness>();
            var controller = new UserController(userBusiness);

            // Act
            var result = await controller.GetUsersById(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateUsers_ReturnsOkResult()
        {
            // Arrange
            var userId = 1;
            var user = new User
            {
                UserId = 1,
                UserName = "Test",
                UserEmail = "Test",
                UserRole = "Admin"
            };
            var userBusiness = Substitute.For<IUserBusiness>();
            userBusiness.UpdateUsers(user, userId);
            var controller = new UserController(userBusiness);
         

            // Act
            var result = await controller.UpdateUsers(user, userId); 

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var userResult = okResult.Value as User;
            userResult.Should().NotBeNull();
            userResult.UserRole.Should().Be("Admin");

        }

        [Fact]
        public async Task DeleteUsers_ReturnsOkResult()
        {
            // Arrange
            var userId = 1; 
            var userBusiness = Substitute.For<IUserBusiness>();
            userBusiness.DeleteUsers(userId);
            var controller = new UserController(userBusiness);

            // Act
            var result = await controller.DeleteUsers(userId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
          
        }



    }
}





