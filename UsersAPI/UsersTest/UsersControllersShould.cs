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
    //public class UsersControllersShould : ControllerBase
    //{
    //    private readonly IUserBusiness _usersBussiness;
    //    private readonly UserController _usersController;
    //    private readonly IConfiguration _configuration;
    //    private readonly IUserDataAccess _usersDataAccess;

    //    public UsersControllersShould()
    //    {
    //        _usersBussiness = Substitute.For<IUserBusiness>();
    //        _usersDataAccess = Substitute.For<IUserDataAccess>();
    //        _usersController = Substitute.For<UserController>();
    //        _usersController = new UserController(_usersBussiness, _configuration);

    //    }
    //[Fact]
    //public async Task CreateUser_WithValidUser_ShouldReturnOkWithToken()
    //{
    //    // Arrange
    //    var validUser = new Users
    //    {
    //        UserId = 1,
    //        UserName = "john_doe",
    //        UserEmail = "john.doe@example.com",
    //        // Add other properties as needed
    //    };
    //    _usersBussiness.CreateUser(validUser).Returns(validUser);

    //    // Act
    //    var result = await _usersController.createUser(validUser);

    //    // Assert
    //    result.Should().BeOfType<ActionResult<Users>>();
    //    result.Data.Should().BeOfType<OkObjectResult>();
    //    var okResult = (OkObjectResult)result.Data;
    //    okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
    //    okResult.Value.Should().NotBeNull();
    //    okResult.Value.Should().BeOfType<Users>();
    //    _usersController.ControllerContext.HttpContext.Response.Headers.ContainsKey("Auth").Should().BeTrue();
    //    var authToken = _usersController.ControllerContext.HttpContext.Response.Headers["Auth"].ToString();
    //    authToken.Should().NotBeNullOrEmpty();
    //}
    //[Fact]
    //public async Task UpdateUsers_WithValidUser_ShouldReturnOk()
    //{
    //    // Arrange
    //    int userId = 1;
    //    var user = new Users
    //    {
    //        UserId = userId,
    //        UserName = "updated_user",
    //        UserEmail = "updated.user@example.com",
    //    };
    //    _usersBussiness.UpdateUsers(user,userId); 

    //    // Act
    //    var result = await _usersController.UpdateUsers(user).Returns(true);

    //    // Assert
    //    result.Should().BeOfType<ActionResult<Users>>();
    //    result.Return.Should().BeOfType<OkObjectResult>();
    //    var okResult = (OkResult)result;
    //    okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
    //}

    //[Fact]
    //public async Task UpdateUser_WithInvalidUser_ShouldReturnBadRequest()
    //{
    //    // Arrange
    //    int userId = 1;
    //    var updatedUser = new Users
    //    {
    //        UserId = userId,
    //        UserName = "updated_user",
    //        UserEmail = "updated.user@example.com",
    //    };
    //    _usersBusiness.updateUsers(updatedUser, userId).Returns(0); // Assuming false indicates failed update

    //    // Act
    //    var result = await _usersController.updateUser(userId, updatedUser);

    //    // Assert
    //    result.Should().BeOfType<BadRequestResult>();
    //    var badRequestResult = (BadRequestResult)result;
    //    badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    //}
    public class UserControllerTests
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;

        public UserControllerTests()
        {
            _userBusiness = Substitute.For<IUserBusiness>();
            _configuration = Substitute.For<IConfiguration>();
            _logger = Substitute.For<ILogger<UserController>>();
        }

        //[Fact]
        //public async Task CreateUser_ReturnsOkObjectResult_WithValidInput()
        //{
        //    // Arrange
        //    var controller = new UserController(_userBusiness, _configuration, _logger);
        //    var user = new User { 
        //        UserId = 1,
        //        UserName = "Test",
        //        UserEmail = "Test", 
        //        UserRole  = "Test"

        //    };
        //    _userBusiness.CreateUser(Arg.Any<User>()).Returns(Task.FromResult(new ApiResponse<User> { Data = user }));

        //    // Act
        //    var result = await controller.CeateUser(user);

        //    // Assert
        //    result.Should().BeOfType<ActionResult<ApiResponse<User>>>()
        //   .Which.Value.Should().NotBeNull().And.BeOfType<ApiResponse<User>>()
        //   .Which.Data.Should().BeEquivalentTo(user);

        //}
        [Fact]
        public async Task GetAllUsers_ReturnsOkObjectResult_WithValidData()
        {
            // Arrange
            var controller = new UserController(_userBusiness, _configuration, _logger);
            var userList = new List<User> { /* Add user objects here */ };
            _userBusiness.GetAllUsers().Returns(Task.FromResult(new ApiResponse<List<User>> { Data = userList }));

            // Act
            var result = await controller.GetAllUsers();

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<ApiResponse<List<User>>>()
                .Which.Data.Should().BeEquivalentTo(userList);
        }

        [Fact]
        public async Task GetUsersById_ReturnsOkObjectResult_WithValidData()
        {
            // Arrange
            var controller = new UserController(_userBusiness, _configuration, _logger);
            var userId = 1;
            var user = new User {new User { Id = 1, FirstName = "John", LastName = "Doe" },
                new User { Id = 2, FirstName = "Jane", LastName = "Doe" }
            };
            _userBusiness.GetUsersById(userId).Returns(Task.FromResult(new ApiResponse<User> { Data = user }));

            // Act
            var result = await controller.GetUsersById(userId);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<ApiResponse<User>>()
                .Which.Data.Should().BeEquivalentTo(user);
        }

        // Add more HttpGet tests as needed
    }

}





    