using SmartFishLogin.encryp.Interfaces;
using SmartFishLogin.encryp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.encryp.CreatorFile
{
    public class ConcreteCreatorEncryp : CreatorEncryp
    {
        public override IEncryp Encryp()
        {
            return new EncrypRepo();
        }
    }
}
