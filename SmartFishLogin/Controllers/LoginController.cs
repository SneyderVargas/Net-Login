using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartFishLogin.Core.Dtos;
using SmartFishLogin.Core.Interfaces;
using SmartFishLogin.Helpers;
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
        public async Task<IActionResult> Login([FromBody] LoginRequestDto param)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseErrorApi(ModelState));
                var result = await _login.Login(param);
                return Ok(ResponseApi.Response(false, result, null, null));
            }
            catch (Exception ex)
            {
                _logger.LogError($"log error: {ex.Message}");
                return BadRequest(ResponseApi.Response(true, null, "No se proceso la información", null));
            }
            
        }
    }
}
