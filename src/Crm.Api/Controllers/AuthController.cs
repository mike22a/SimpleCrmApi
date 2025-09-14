using Crm.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto model)
    {
        var userExists = await _userManager.FindByEmailAsync(model?.Email ?? string.Empty);
        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status409Conflict,
                new { Status = "Error", Message = "User with this email already exists." });
        }

        var user = new ApplicationUser()
        {
            Email = model?.Email ?? string.Empty,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model?.Email ?? string.Empty,
            FirstName = model?.FirstName ?? string.Empty
        };

        var result = await _userManager.CreateAsync(user, model?.Password ?? string.Empty);

        if (!result.Succeeded)
        {
            return BadRequest(new { Status = "Error", Message = "User creation failed.", Errors = result.Errors });
        }

        await _userManager.AddToRoleAsync(user, "User");

        return Ok(new { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model?.Email ?? string.Empty);
        if (user != null && await _userManager.CheckPasswordAsync(user, model?.Password ?? string.Empty))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user?.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }


            // 1. Get key from configuration into a variable
            var jwtKey = _configuration["Jwt:Key"];

            // 2. Validate the key. If it's missing, the app cannot continue securely.
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT Key is not configured");
            }

            // 3. Use the validated key. The warning will now be gone.
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));


            var token = new JwtSecurityToken(
                issuer: _configuration["JWT_ISSUER"] ?? _configuration["Jwt:Issuer"],
                audience: _configuration["JWT_AUDIENCE"] ?? _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }
}