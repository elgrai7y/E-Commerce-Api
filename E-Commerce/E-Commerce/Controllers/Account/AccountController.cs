using E_Commerce.BLL;
using E_Commerce.Common;
using E_Commerce.Common.GeneralResult;
using E_Commerce.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_Commerce.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly JwtSettings _jwtSettings;

       


        public AccountController(IOptions< JwtSettings> jwtSettings, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;

        }

        [HttpPost("register")]
        public async Task<ActionResult<GeneralResult<RegisterDto>>> Register(RegisterDto user)
        {
            var newUser = new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.Email,
                Country = user.Country,
                City = user.City,
            };

            var createResult = await _userManager.CreateAsync(newUser, user.Password);
            if (!createResult.Succeeded)
            {
                return BadRequest(createResult.Errors);
            }


            //var roleName = "Admin";
            //if (!await _roleManager.RoleExistsAsync(roleName))
            //{
            //    var roleCreateResult = await _roleManager.CreateAsync(new ApplicationRole { Name = roleName });
            //    if (!roleCreateResult.Succeeded)
            //    {
            //        return BadRequest(roleCreateResult.Errors);
            //    }
            //}

            //var addToRoleResult = await _userManager.AddToRoleAsync(newUser, roleName);
            //if (!addToRoleResult.Succeeded)
            //{
            //    return BadRequest(addToRoleResult.Errors);
            //}

            return Ok(GeneralResult<ApplicationUser>.SuccessResult(newUser));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<GeneralResult<TokenDto>>> Login(LoginDto loginUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GeneralResult<TokenDto>.FailResult());
            }

            var user = await _signinManager.UserManager.FindByEmailAsync(loginUser.Email);
            if (user is null)
            {
                return Unauthorized(GeneralResult<TokenDto>.NotFound("Invalid email or password."));
            }
            var login = await _signinManager.PasswordSignInAsync(user, loginUser.Password, loginUser.RememberMe, false);
            if (login is null)
            {
                return Unauthorized(GeneralResult<TokenDto>.NotFound("Invalid email or password."));
            }

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier ,user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName!));
            claims.Add(new Claim(ClaimTypes.Email, user.Email!));

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDto = GenerateToken(claims);
            return Ok(GeneralResult<TokenDto>.SuccessResult(tokenDto));
        }
        private TokenDto GenerateToken(List<Claim> claims)
        {
            var keyFromConfig = _jwtSettings.SecretKey;
            var keyInBytes = Convert.FromBase64String(keyFromConfig);
            var key = new SymmetricSecurityKey(keyInBytes);
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiryDateTime = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes);

            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: expiryDateTime
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            var tokenDto = new TokenDto(token, _jwtSettings.DurationInMinutes);
            return tokenDto;
        }
    }
}
