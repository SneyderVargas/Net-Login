using SmartFishLogin.Core.Dtos;
using SmartFishLogin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Core.Interfaces
{
    public interface ILogin
    {
        Task<ResultDto<LoginResultDto, List<ErrorsListDto>>> Login(LoginRequestDto param);
        Task<ResultDto<LoginResultDto, List<ErrorsListDto>>> RegisterUser(RegisterUserRequestDto param);
    }
}
