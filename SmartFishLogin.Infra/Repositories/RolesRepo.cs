using SmartFishLogin.Core.Dtos;
using SmartFishLogin.Core.Interfaces;
using SmartFishLogin.Core.Models;
using SmartFishLogin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Infra.Repositories
{
    public class RolesRepo : IRole
    {
        private readonly DefaultDbContext _defaultDbContext;

        public RolesRepo(DefaultDbContext defaultDbContext)
        {
            _defaultDbContext = defaultDbContext;
        }

        public async Task<(RegisterResultDto<RolesEntity>, List<ErrorsListDto>)> Register(LoginRequestDto param)
        {
            try
            {
                //var data  = new BaseRepo<RoleEntity>(_defaultDbContext);
                //data.GetAll();
                return (null, null);
            }
            catch (Exception ex)
            {
                string mensajeModificado = $"{ex.Message} <- (Clase: {GetType().Name}, Método : {nameof(Register)})";
                throw new Exception(mensajeModificado);
            }
        }
    }
}
