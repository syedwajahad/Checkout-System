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
       
   
        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
      
        }
        [HttpPost]
        [Route("CreateUser")]
        [Authorize]
        public async Task<ActionResult> CreateUser(User user)
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
        [Authorize]
        public async Task<ActionResult> GetAllUsers()
        {
            var token = new JwtSecurityTokenHandler();
            var valid = token.ReadJwtToken(HttpContext.Request.Headers.Authorization.ToString().Split(" ")[1]);
            var roles = new List<String>() { "ADMIN" };

            var userRole = valid.Claims.ToList().Find((claim) => claim.Type == "role");
            var authorizedRoles = roles.Find((role) => role == userRole!.Value);
            if (authorizedRoles!.Length == 0)
            {
                return Unauthorized();
            }

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
        [Authorize]
        public async Task<ActionResult> GetUsersById(int UserId)
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
        [Authorize]
        public async Task<IActionResult> UpdateUsers(User user, int UserId)
        {
            var token = new JwtSecurityTokenHandler();
            var valid = token.ReadJwtToken(HttpContext.Request!.Headers!.Authorization!.ToString().Split(" ")[1]);
            var roles = new List<String>() { "ADMIN" };

            var userRole = valid.Claims.ToList().Find((claim) => claim.Type == "role");
            var authorizedRoles = roles.Find((role) => role == userRole!.Value);
            if (authorizedRoles!.Length == 0)
            {
                return Unauthorized();
            }
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
        [Authorize]
        public async Task<ActionResult> DeleteUsers(int UserId)
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
       
    }
}

