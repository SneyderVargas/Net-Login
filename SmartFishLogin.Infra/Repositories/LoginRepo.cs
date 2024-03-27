using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartFishLogin.Core.Dtos;
using SmartFishLogin.Core.Helpers;
using SmartFishLogin.Core.Interfaces;
using SmartFishLogin.encryp.ClientCode;
using SmartFishLogin.encryp.CreatorFile;
using SmartFishLogin.encryp.Dtos;
using SmartFishLogin.Helpers;
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
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly DefaultDbContext _defaultDbContext;
        private readonly EncrypConfiguration _encrypConfiguration;

        public LoginRepo(IOptions<JwtConfiguration> jwtConfiguration, DefaultDbContext defaultDbContext, IOptions<EncrypConfiguration> encrypConfiguration)
        {
            _jwtConfiguration = jwtConfiguration.Value;
            _defaultDbContext = defaultDbContext;
            _encrypConfiguration = encrypConfiguration.Value;
        }

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
                    ExperiTimen = DateTime.Now.AddDays(1),
                    Key = _jwtConfiguration.Key
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

        public async Task<LoginResultDto> RegisterUser(RegisterUserRequestDto param)
        {
            try
            {
                // consultar que el usuario este registrado
                var User = await _defaultDbContext.userEntities.Where(a => a.Email == param.Email).ToListAsync();
                if (User.Count > 0)
                {
                    string mensajeModificado = $" Email registrado en el sistema <- (Clase: {GetType().Name}, Método : {nameof(Login)})";
                    throw new Exception(mensajeModificado);
                }
                var ClientSmartSifhEncryp = new ClientEncryp();
                var ConcreteCreatorSmartFishEncryp = new ConcreteCreatorEncryp();

                var parametrosEncryp = new DataEncryp
                {
                    Key = _encrypConfiguration.Key,
                    Password = param.Password
                };

                var ServisEncrypt = await ClientSmartSifhEncryp.Encryption(ConcreteCreatorSmartFishEncryp, parametrosEncryp);
                var parametrosDesEncryp = new DataEncryp
                {
                    Key = _encrypConfiguration.Key,
                    Password = ServisEncrypt.DataEncry
                };
                var ServisDesEncrypt = await ClientSmartSifhEncryp.DesEncryption(ConcreteCreatorSmartFishEncryp, parametrosDesEncryp);
                return null;
            }
            catch (Exception ex)
            {
                string mensajeModificado = $"{ex.Message} <- (Clase: {GetType().Name}, Método : {nameof(Login)})";
                throw new Exception(mensajeModificado);
            }
        }
    }
}
