﻿using SmartFishLogin.Core.Dtos;
using SmartFishLogin.Core.Interfaces;
using SmartFishLogin.Tokens.ClientCode;
using SmartFishLogin.Tokens.CreatorFile;
using SmartFishLogin.Tokens.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Infra.Repositories
{
    public class LoginRepo : ILogin
    {
        public async Task<LoginResultDto> Login(LoginRequestDto param)
        {
            var token = new LoginResultDto();
            try
            {
                var ClientSmartFistTokens = new ClientToken();
                var ConcreteCreatorSmartFishLogin = new ConcreteCreatorToken();

                var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.NameId, "sneyder")
                };
                var ParamTokens = new GenerateTokenDto
                {
                    Audiencia = "Login",
                    Claims = claims,
                    ExperiTimen = DateTime.Now.AddDays(1)
                };
                var ServisToken = await ClientSmartFistTokens.GenerateToken(ConcreteCreatorSmartFishLogin, ParamTokens);
                token.Token = ServisToken.Token;
                return token;
            }
            catch (Exception ex)
            {
                string mensajeModificado = $"{ex.Message} <- (Clase: {GetType().Name}, Método : {nameof(Login)})";
                throw new Exception(mensajeModificado);
            }
        }
    }
}