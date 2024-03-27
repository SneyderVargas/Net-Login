using SmartFishLogin.encryp.CreatorFile;
using SmartFishLogin.encryp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.encryp.ClientCode
{
    public class ClientEncryp
    {
        public Task<EncrypDto> Encryption(CreatorEncryp creator, DataEncryp param)
        {
            return creator.Encryption(param);
        }
        public Task<EncrypDto> DesEncryption(CreatorEncryp creator, DataEncryp param)
        {
            return creator.DesEncryption(param);
        }
    }
}
