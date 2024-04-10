using SmartFishLogin.Core.Dtos;
using SmartFishLogin.Core.Models;
using SmartFishLogin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Core.Interfaces
{
    public interface IRole
    {
        Task<(RegisterResultDto<RolesEntity>, List<ErrorsListDto>)> Register(LoginRequestDto param);
    }
}
