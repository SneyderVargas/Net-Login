using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Core.Dtos
{
    public class ResultDto<T,Z>
    {
        public T Resul { get; set; }
        public Z Error { get; set; }


    }
}
