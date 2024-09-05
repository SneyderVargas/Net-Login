using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartFishLogin.Core.Dtos;
using SmartFishLogin.Core.Interfaces;
using SmartFishLogin.Helpers;
using SmartFishLogin.Infra.Resx;
using SmartFishLogin.Resx;
using SmartFishLogin.Tokens.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace SmartFishLogin.Controllers
{
    [Route("/api/1.0.0/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccount _login;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccount login, ILogger<AccountController> logger)
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
                    return BadRequest(new ResponseApi<string>(true, ModelState, null, SecurityMsgRequest.ValSistem));
                var result = await _login.Login(param);
                if (result.Error.Count > 0)
                    return BadRequest(new ResponseApi<ResultDto<LoginResultDto, List<ErrorsListDto>>>(true,SecurityMsgRequest.ErrorSistem,result,SecurityMsgRequest.ValSistem));
                return Ok(new ResponseApi<LoginResultDto>(false,SecurityMsgRequest.Exito,result.Resul,null));
            }
            catch (Exception ex)
            {
                _logger.LogError($"log error: {ex.Message}");
                return BadRequest(new ResponseApi<string>(true, ex.Message, null, SecurityMsgRequest.DetailErrorSistem));
                
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestDto param)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResponseApi<string>(true, ModelState, null, SecurityMsgRequest.ValSistem));
                var result= await _login.RegisterUser(param);
                if(result.Error.Count > 0)
                    return BadRequest(new ResponseApi<ResultDto<LoginResultDto, List<ErrorsListDto>>>(true, SecurityMsgRequest.ErrorSistem, result, SecurityMsgRequest.ValSistem));
                return Ok(new ResponseApi<LoginResultDto>(false, SecurityMsgRequest.Exito, result.Resul, null));

            }
            catch (Exception ex)
            {
                _logger.LogError($"log error: {ex.Message}");
                return BadRequest(new ResponseApi<string>(true, ex.Message, null, SecurityMsgRequest.DetailErrorSistem));
            }

        }
    }
}
