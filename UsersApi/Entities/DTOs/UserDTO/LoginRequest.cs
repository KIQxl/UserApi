﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.UserDTO
{
    public struct LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
