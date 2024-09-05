using Microsoft.AspNetCore.Mvc;
using SmartFishLogin.Core.Dtos;
using SmartFishLogin.Core.Interfaces;
using SmartFishLogin.Helpers;

namespace SmartFishLogin.Controllers
{
    [Route("[controller]/[action]")]

    public class RoleController : Controller
    {
        private readonly IRole _role;
        private readonly ILogger<AccountController> _logger;

        public RoleController(IRole role, ILogger<AccountController> logger)
        {
            _role = role;
            _logger = logger;
        }

        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody] LoginRequestDto param)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(new ResponseErrorApi(ModelState));
        //        var (result, errors) = await _role.Register(param);
        //        if (errors.Count > 0)
        //            return BadRequest(new ResponseErrorApi("NO se proceso la información validacion del sistema", errors));
        //        return Ok(ResponseApi<string>.ResponseLogin(null));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"log error: {ex.Message}");
        //        return BadRequest(ResponseApi<string>.Response("No se proceso la información"));
        //    }

        //}
    }
}
