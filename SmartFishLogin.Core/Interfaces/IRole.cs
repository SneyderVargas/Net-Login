using SmartFishLogin.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Core.Interfaces
{
    public interface IRole
    {
        Task<LoginResultDto> Register(LoginRequestDto param);
    }
}
