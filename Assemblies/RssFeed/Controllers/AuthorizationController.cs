using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RssFeed.DTOs.Requests;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RssFeed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _Logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthorizationController(IConfiguration configuration, ILogger<AuthorizationController> logger, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _Logger = logger;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserLoginRequestModel user)
        {
            var expectedUser = await _userManager.FindByNameAsync(user.UserName);
            if (expectedUser != null && await _userManager.CheckPasswordAsync(expectedUser, user.Password))
            {
                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, expectedUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = GetToken(authClaims);
                _Logger.LogInformation("User eas foumd and new token was generated");
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            _Logger.LogError("User was not found or credettials weren't correct");
            return StatusCode(StatusCodes.Status403Forbidden);
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(RegistrationRequestModel registrationModel)
        {
            var userExist = await _userManager.FindByEmailAsync(registrationModel.EmailAddress);
            if (userExist != null)
            {
                _Logger.LogInformation("user already exist");
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            IdentityUser user = new()
            {
                Email = registrationModel.EmailAddress,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registrationModel.UserName
            };

            var result = await _userManager.CreateAsync(user, registrationModel.Password);
            if (!result.Succeeded)
            {
                _Logger.LogError("user creation failed. Check details and try again");
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            _Logger.LogInformation("user was created");
            return Ok(StatusCodes.Status201Created);

        }


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSecurityKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
