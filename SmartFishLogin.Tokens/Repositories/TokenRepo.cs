using SmartFishLogin.Tokens.Dtos;
using SmartFishLogin.Tokens.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Tokens.Repositories
{
    internal class TokenRepo : ITokenFactory
    {
        public Task<TokenDto> GenerateToken(GenerateTokenDto param)
        {
            throw new NotImplementedException();
        }
    }
}
