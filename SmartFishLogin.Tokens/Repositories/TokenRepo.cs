using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartFishLogin.Helpers;
using SmartFishLogin.Tokens.Dtos;
using SmartFishLogin.Tokens.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Tokens.Repositories
{
    internal class TokenRepo : ITokenFactory
    {
        public async Task<TokenDto> GenerateToken(GenerateTokenDto param)
        {
            try
            {

                var gt = Convert.ToInt32(10000000);

                var claims = param.Claims;

                var expiresDate = param.ExperiTimen;

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(param.Key));
                var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescripcion = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(param.Claims),
                    Expires = param.ExperiTimen,
                    SigningCredentials = credenciales,
                    Audience = param.Audiencia
                };

                var tokenManejador = new JwtSecurityTokenHandler();
                var token = tokenManejador.CreateToken(tokenDescripcion);

                //return Ok(tokenManejador.WriteToken(token));
                var returnToken = new TokenDto();
                returnToken.Token = tokenManejador.WriteToken(token);
                return returnToken;
            }
            catch (Exception ex)
            {
                string mensajeModificado = $"{ex.Message} <- (Clase: {GetType().Name}, Método : {nameof(TokenRepo)})";
                throw new Exception(mensajeModificado);
            }
        }
    }
}
