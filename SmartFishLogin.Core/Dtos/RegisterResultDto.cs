﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Core.Dtos
{
    public class RegisterResultDto<T>
    {
        public List<T> Result { get; set; }
    }
}
