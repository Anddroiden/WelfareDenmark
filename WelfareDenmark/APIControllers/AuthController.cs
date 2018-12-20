using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WelfareDenmark.Models;

namespace WelfareDenmark.APIControllers {
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<AppSettings> _appSettings;

        public AuthController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, IOptions<AppSettings> appSettings) {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Login([FromBody] LoginDto model) {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded) return Unauthorized();
            var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
            return Ok(new {Token = await GenerateJwtToken(appUser)});
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model) {
            var user = new ApplicationUser {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddClaimAsync(user, new Claim(PolicyConstants.IsPatient, "true"));
            var consultant = await _userManager.GetUserAsync(User);
            consultant.Patients.Add(user);
            await _userManager.UpdateAsync(consultant);
            if (!result.Succeeded) throw new ApplicationException("UNKNOWN_ERROR");
            return Ok();
        }
        
        private async Task<string> GenerateJwtToken(ApplicationUser user) {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(_userManager.Options.ClaimsIdentity.UserIdClaimType, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Value.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_appSettings.Value.JwtExpireDays);

            var token = new JwtSecurityToken(
                _appSettings.Value.JwtIssuer,
                _appSettings.Value.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}