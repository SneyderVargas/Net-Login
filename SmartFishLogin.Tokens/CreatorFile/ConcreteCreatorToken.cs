using SmartFishLogin.Tokens.Interfaces;
using SmartFishLogin.Tokens.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Tokens.CreatorFile
{
    public class ConcreteCreatorToken : CreatorToken
    {
        public override ITokenFactory TokenFactory()
        {
            return new TokenRepo();
        }
    }
}
