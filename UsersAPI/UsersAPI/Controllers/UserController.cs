using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using users.Business.Interface;
using users.DataObjects.DataModels;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;
      

        public UserController(IUserBusiness userBusiness, IConfiguration configuration, ILogger<UserController> logger)
        {
            _userBusiness = userBusiness;
            _configuration = configuration;
            _logger = logger;
        }

     

        [HttpPost]
        [Route("CreateUser")]
        [ProducesResponseType(typeof(IList<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ApiResponse<User>>> CeateUser(User user)
        {
            try
            {
                var output = await _userBusiness.CreateUser(user);
                if (output == null)
                {
                    var obj = new
                    {
                        success = false
                    };
                    return Ok(output);
                }
                else
                {
                    var token = CreateToken(output.Data);
                    HttpContext.Response.Headers.Add("Auth", token.ToString());
                    return Ok(output);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetAllUsers")]
        [ProducesResponseType(typeof(IList<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ApiResponse<List<User>>>> GetAllUsers()
        {
            try
            {
                var response = await _userBusiness.GetAllUsers();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{UserId}")]
        [ProducesResponseType(typeof(IList<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ApiResponse<User>>> GetUsersById(int UserId)
        {
            try
            {
                var response = await _userBusiness.GetUsersById(UserId);                           
                 return Ok(response);           
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("{UserId}")]
        [ProducesResponseType(typeof(IList<User>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUsers(User user, int UserId)
        {
            try
            {
                var response = await _userBusiness.UpdateUsers(user, UserId);
                return Ok(response);               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{UserId}")]
        [ProducesResponseType(typeof(IList<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ApiResponse<User>>> DeleteUsers(int UserId)
        {
            try
            {
                var response = await _userBusiness.DeleteUsers(UserId);
                return Ok(response);             
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
        private string CreateToken(User users)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Role, users.UserRole),
                 new Claim(ClaimTypes.NameIdentifier, users.UserId.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        private List<String> GetTokenValues(string requestToken)
        {
            var token = new JwtSecurityTokenHandler();
            var valid = token.ReadJwtToken(requestToken);
            var role = "";
            var userId = "";
            foreach (var user in valid.Claims)
            {
                if (user.Type == ClaimTypes.Role)
                {
                    role = user.Value;

                }
                if (user.Type == ClaimTypes.NameIdentifier)
                {
                    userId = user.Value;
                }
            }
            return new List<String> { userId, role };
        }
    }
}

