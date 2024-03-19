using SmartFishLogin.Tokens.Dtos;
using SmartFishLogin.Tokens.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Tokens.CreatorFile
{
    public abstract class CreatorToken
    {
        public abstract ITokenFactory TokenFactory();
        public async Task<TokenDto> GenerateToken(GenerateTokenDto param)
        {
            var tk = TokenFactory();
            return await tk.GenerateToken(param);
        }
    }
}
