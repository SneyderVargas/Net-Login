using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartFishLogin.Core.Dtos;
using SmartFishLogin.Core.Helpers;
using SmartFishLogin.Core.Interfaces;
using SmartFishLogin.Core.Models;
using SmartFishLogin.encryp.ClientCode;
using SmartFishLogin.encryp.CreatorFile;
using SmartFishLogin.encryp.Dtos;
using SmartFishLogin.Helpers;
using SmartFishLogin.Infra.Resx;
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
    public class AccountRepo : IAccount
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly DefaultDbContext _defaultDbContext;
        private readonly EncrypConfiguration _encrypConfiguration;

        public AccountRepo(IOptions<JwtConfiguration> jwtConfiguration, DefaultDbContext defaultDbContext, IOptions<EncrypConfiguration> encrypConfiguration)
        {
            _jwtConfiguration = jwtConfiguration.Value;
            _defaultDbContext = defaultDbContext;
            _encrypConfiguration = encrypConfiguration.Value;
        }

        public async Task<ResultDto<LoginResultDto, List<ErrorsListDto>>> Login(LoginRequestDto param)
        {
            var token = new LoginResultDto();
            List<ErrorsListDto> errors = new List<ErrorsListDto>();

            try
            {
                UserEntity User = await _defaultDbContext.userEntities.Where(a => a.Email == param.NameUser).FirstOrDefaultAsync();

                if (User == null)
                {
                    var errorDuplicateUser = new ErrorsListDto(SecurityMsg.errorAuth, SecurityMsg.verifyInfo);
                    errors.Add(errorDuplicateUser);

                    var r = new ResultDto<LoginResultDto, List<ErrorsListDto>>();
                    r.Resul = null;
                    r.Error = errors;
                    return r;
                }
                    

                var ClientSmartSifhEncryp = new ClientEncryp();
                var ConcreteCreatorSmartFishEncryp = new ConcreteCreatorEncryp();
                var parametrosEncryp = new DataEncryp
                {
                    Key = _encrypConfiguration.Key,
                    Password = param.PasswordUser
                };
                var ServisDesEncrypt = await ClientSmartSifhEncryp.Encryption(ConcreteCreatorSmartFishEncryp, parametrosEncryp);

                if (ServisDesEncrypt.DataEncry != User.Password)
                {
                    var errorDuplicateUser = new ErrorsListDto(SecurityMsg.errorAuth, SecurityMsg.verifyInfo);
                    errors.Add(errorDuplicateUser);
                }
                // validar que no exista ningun error
                if (errors.Count > 0)
                {
                    var r = new ResultDto<LoginResultDto, List<ErrorsListDto>>();
                    r.Resul = null;
                    r.Error = errors;
                    return  r;
                }
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

                var rr = new ResultDto<LoginResultDto, List<ErrorsListDto>>();
                rr.Resul = token;
                rr.Error = errors;
                return rr;
            }
            catch (Exception ex)
            {
                string mensajeModificado = $"{ex.Message} <- (Clase: {GetType().Name}, Método : {nameof(Login)})";
                throw new Exception(mensajeModificado);
            }
        }

        public async Task<ResultDto<LoginResultDto, List<ErrorsListDto>>> RegisterUser(RegisterUserRequestDto param)
        {
            List<ErrorsListDto> errors = new List<ErrorsListDto>();
            try
            {
                // consultar que el usuario este registrado
                var User = await _defaultDbContext.userEntities.Where(a => a.Email == param.Email).ToListAsync();
                if (User.Count > 0)
                {
                    var errorDuplicateUser = new ErrorsListDto(SecurityMsg.duplycateUser, SecurityMsg.verifyInfo);
                    errors.Add(errorDuplicateUser);
                }
                if (param.Password != param.PasswordRepeat)
                {
                    var errorDuplicateUser = new ErrorsListDto(SecurityMsg.equalPass, SecurityMsg.verifyInfo);
                    errors.Add(errorDuplicateUser);
                }
                // validar que no exista ningun error
                if (errors.Count > 0) 
                {
                    var r = new ResultDto<LoginResultDto, List<ErrorsListDto>>();
                    r.Resul = null;
                    r.Error = errors;
                    return r;
                }
                // ----------------------------------------------- bloque de creacion de constraseña ---------------------------------------------------------
                var ClientSmartSifhEncryp = new ClientEncryp();
                var ConcreteCreatorSmartFishEncryp = new ConcreteCreatorEncryp();

                var parametrosEncryp = new DataEncryp
                {
                    Key = _encrypConfiguration.Key,
                    Password = param.Password
                };

                var ServisEncrypt = await ClientSmartSifhEncryp.Encryption(ConcreteCreatorSmartFishEncryp, parametrosEncryp);

                //var parametrosDesEncryp = new DataEncryp
                //{
                //    Key = _encrypConfiguration.Key,
                //    Password = ServisEncrypt.DataEncry
                //};
                //var ServisDesEncrypt = await ClientSmartSifhEncryp.DesEncryption(ConcreteCreatorSmartFishEncryp, parametrosDesEncryp);

                // proceso de guardar el registro de usuarios.
                //BeginTransaction 001
                using (var transac = _defaultDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var user = new UserEntity
                        {
                            Email = param.Email,
                            Name = param.UserName,
                            Password = ServisEncrypt.DataEncry,
                            TypeUsers = "1",
                            Data = null,
                            LastLogin = DateTime.Now,
                            Active = 1,
                            CreateRegisterDate = DateTime.Now,
                            UpdateRegisterDate = DateTime.Now,
                            ActiveRegister = 1,
                            Tenancies = 1,
                            Steps = ""
                        };
                        await _defaultDbContext.userEntities.AddAsync(user);
                        await _defaultDbContext.SaveChangesAsync();
                        //var resDetPry = consecutivos
                        //        .Where(a => a.listaMensajesProceso != null)
                        //        .SelectMany(itemConse => itemConse.listaMensajesProceso
                        //            .Select(itemMensaje => new WSFelMensajesprocesoEntity
                        //            {
                        //                IdWsFelRespuestaenvio = felresdocumentpry.Where(a => a.Prefijo == requestDto.prefijo && a.Consecutivo == itemConse.consecutivo).Select(b => b.Id).FirstOrDefault(),
                        //                CodigoMensajeField = itemMensaje.codigoMensaje,
                        //                DescripcionMensajeField = itemMensaje.descripcionMensaje,
                        //                RechazoNotificacionField = itemMensaje.rechazoNotificacion
                        //            }).ToList()
                        //        ).ToList();

                        //await _defaultDbContext.WSFelMensajesprocesoEntities.AddRangeAsync(resDetPry);
                        //await _defaultDbContext.SaveChangesAsync();

                    }
                    catch (Exception ex)
                    {
                        transac.Rollback();
                        string mensajeModificado = $"{ex.Message} <- (Clase: {GetType().Name}, Método and BeginTransaction 001 : {nameof(RegisterUser)})";
                        throw new Exception(mensajeModificado);
                    }
                    transac.Commit();

                    var rr = new ResultDto<LoginResultDto, List<ErrorsListDto>>();
                    rr.Resul = null;
                    rr.Error = errors;
                    return rr;

                }
                var rrr = new ResultDto<LoginResultDto, List<ErrorsListDto>>();
                rrr.Resul = null;
                rrr.Error = errors;
                return rrr;
            }
            catch (Exception ex)
            {
                string mensajeModificado = $"{ex.Message} <- (Clase: {GetType().Name}, Método : {nameof(Login)})";
                throw new Exception(mensajeModificado);
            }
        }
    }
}
