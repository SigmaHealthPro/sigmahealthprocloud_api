using BAL.Repository;
using BAL.RequestModels;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace Web_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        private readonly ILogger<UserController> _logger;


        public UserController(IUnitOfWork unitOfWork,IConfiguration config,ILogger<UserController> logger)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _logger = logger;
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
    }
}
