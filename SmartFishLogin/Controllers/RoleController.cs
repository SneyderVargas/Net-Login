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

        public RoleController(IRole role)
        {
            _role = role;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto param)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseErrorApi(ModelState));
                var result = await _role.Register(param);
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
