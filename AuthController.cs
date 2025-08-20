using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Teknorix.JobsApi.Infrastructure.Auth;

namespace Teknorix.JobsApi.WebApi.Controllers;

[ApiController]
[Route(""/api/auth"")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IAuthService _auth;

    public AuthController(IConfiguration config, IAuthService auth)
    {
        _config = config;
        _auth = auth;
    }

    /// <summary>Simple login to get a JWT. Users: admin/admin123 or viewer/viewer123</summary>
    [HttpPost(""login"")]
    public IActionResult Login([FromBody] LoginRequest req)
    {
        if (!_auth.ValidateUser(req.Username, req.Password, out var role))
            return Unauthorized(new { error = ""Invalid credentials"" });

        var jwtSection = _config.GetSection(""Jwt"");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection[""Key""]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSection[""Issuer""],
            audience: jwtSection[""Audience""],
            claims: new[] { new Claim(ClaimTypes.Name, req.Username), new Claim(ClaimTypes.Role, role) },
            expires: DateTime.UtcNow.AddHours(4),
            signingCredentials: creds
        );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }

    public record LoginRequest(string Username, string Password);
}
