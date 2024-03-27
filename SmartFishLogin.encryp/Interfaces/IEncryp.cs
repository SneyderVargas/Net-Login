using SmartFishLogin.encryp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.encryp.Interfaces
{
    public interface IEncryp
    {
        Task<EncrypDto> Encryption(DataEncryp param);
        Task<EncrypDto> DesEncryption(DataEncryp param);
    }
}
