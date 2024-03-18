using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartFishLogin.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartFishLogin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogin _login;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogin login, ILogger<LoginController> logger)
        {
            _login = login;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] string d, CancellationToken ct)
        {
            try
            {

                var gt = Convert.ToInt32(10000000);

                var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.NameId, "sneyder")
                };

                var expiresDate = DateTime.Now.AddDays(1);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta Mi palabra secreta Mi palabra secreta Mi palabra secreta Mi palabra secreta"));
                var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescripcion = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = expiresDate,
                    SigningCredentials = credenciales,
                    Audience = "App"
                };

                var tokenManejador = new JwtSecurityTokenHandler();
                var token = tokenManejador.CreateToken(tokenDescripcion);

                return Ok(tokenManejador.WriteToken(token));
            }
            catch (Exception ex)
            {
                return BadRequest(null);
            }
        }
    }
}
