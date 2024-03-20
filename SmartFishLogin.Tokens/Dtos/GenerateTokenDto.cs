using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Tokens.Dtos
{
    public class GenerateTokenDto
    {
        public string Audiencia { get; set; }
        public List<Claim> Claims { get; set; }
        public DateTime ExperiTimen { get; set; }
    }
}
