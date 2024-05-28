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
    [Route("[controller]/[action]")]
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
                    return BadRequest(ResponseApi<string>.ResponseValidacion(ModelState));
                var (result, errors) = await _login.Login(param);
                if (errors.Count > 0)
                    return BadRequest(new ResponseErrorApi("NO se proceso la información validacion del sistema", errors));
                return Ok(ResponseApi<string>.ResponseLogin(result.Token));
            }
            catch (Exception ex)
            {
                _logger.LogError($"log error: {ex.Message}");
                return BadRequest(ResponseApi<string>.Response("No se proceso la información"));
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestDto param)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseErrorApi(ModelState));
                var (result, errors) = await _login.RegisterUser(param);
                if(errors.Count > 0)
                    return BadRequest(new ResponseErrorApi("NO se proceso la información validacion del sistema", errors));
                return Ok(ResponseApi.Response(false, result, null, null));
            }
            catch (Exception ex)
            {
                _logger.LogError($"log error: {ex.Message}");
                return BadRequest(new ResponseErrorApi("No se proceso la información error de sistema", null));
            }

        }
        [HttpPost]
        public async Task<IActionResult> RegisterCompany([FromBody] RegisterUserRequestDto param)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseErrorApi(ModelState));
                if (param.Password != param.PasswordRepeat)
                {
                    return BadRequest(new ResponseErrorApi("Las contraseñas no son iguales", null));
                }
                var result = await _login.RegisterUser(param);
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
