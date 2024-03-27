using SmartFishLogin.encryp.Dtos;
using SmartFishLogin.encryp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.encryp.CreatorFile
{
    public abstract class CreatorEncryp
    {
        public abstract IEncryp Encryp();
        public async Task<EncrypDto> Encryption(DataEncryp param)
        {
            var Ec = Encryp();
            return await Ec.Encryption(param);
        }
        public async Task<EncrypDto> DesEncryption(DataEncryp param)
        {
            var Ec = Encryp();
            return await Ec.DesEncryption(param);
        }
    }
}
