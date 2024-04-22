using BAL.Constant;
using BAL.Interfaces;
using BAL.Repository;
using BAL.Request;
using BAL.RequestModels;
using BAL.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        private readonly ILogger<UserController> _logger;
        private IUserService _userService;

        public UserController(IUnitOfWork unitOfWork,IConfiguration config,ILogger<UserController> logger, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _logger = logger;
            _userService = userService;
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate(string username, string password)
        {
            try
            {
                IActionResult response = Unauthorized();
                User usermodel = new User() { UserId = username, Password = password };
                Userloginmodel loginmod = _unitOfWork.Users.Authenticate(usermodel);
                if (loginmod != null)
                {
                    
                    var tokenString = GenerateJSONWebToken(usermodel);
                    response = Ok(new { token = tokenString, loginmod.id, loginmod.username,loginmod.firstName,loginmod.lastName,loginmod.email,loginmod.role,loginmod.gender,loginmod.birthdate,loginmod.position,loginmod.facility,loginmod.juridiction,loginmod.juridictionid,loginmod.organizationid,status = "Authorized",loginmod.image });                    
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while processing User Authenticate request.");

                // Return a 500 Internal Server Error with a generic message
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(1509),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        [Route("get-persons")]
        public async Task<IActionResult> GetPersons([FromBody] GetDataByCountRequest requestObject)

          => Ok(await _userService.GetPersons(requestObject).ConfigureAwait(true));

        [HttpPost]
        [Route("get-users")]
        public async Task<IActionResult> GetUsers([FromBody] GetDataByCountRequest requestObject)

          => Ok(await _userService.GetUsers(requestObject).ConfigureAwait(true));

        [HttpPost]
        [Route("get-user-by-id")]
        public async Task<IActionResult> GetUserById([FromForm] Guid Id)

         => Ok(await _userService.GetUserById(Id).ConfigureAwait(true));

        [HttpPost]
        [Route("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest userRequest)

        => Ok(await _userService.CreateUser(userRequest).ConfigureAwait(true));

        [HttpPost]
        [Route("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest userRequest)

         => Ok(await _userService.UpdateUser(userRequest).ConfigureAwait(true));

        [HttpPost]
        [Route("update-user-status")]
        public async Task<IActionResult> UpdateUserStatus([FromBody] UpdateUserStatusRequest userRequest)

         => Ok(await _userService.UpdateUserStatus(userRequest).ConfigureAwait(true));

        [HttpDelete]
        [Route("delete-user")]
        public async Task<IActionResult> DeleteUser([FromForm, Required] Guid Id)

         => Ok(await _userService.DeleteUser(Id).ConfigureAwait(true));
    }
}
