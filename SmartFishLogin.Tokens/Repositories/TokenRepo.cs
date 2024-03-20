﻿using Microsoft.IdentityModel.Tokens;
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

                var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.NameId, "sneyder")
                };

                var expiresDate = DateTime.Now.AddDays(1);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta Mi palabra secreta Mi palabra secreta Mi palabra secreta Mi palabra secreta"));
                var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescripcion = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = expiresDate,
                    SigningCredentials = credenciales,
                    Audience = "App"
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
