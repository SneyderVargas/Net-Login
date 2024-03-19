using SmartFishLogin.Tokens.CreatorFile;
using SmartFishLogin.Tokens.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Tokens.ClientCode
{
    public class ClientToken
    {
        public Task<TokenDto> GenerateToken(CreatorToken creator, GenerateTokenDto param)
        {
            return creator.GenerateToken(param);
        }
    }
}
