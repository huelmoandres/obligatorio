﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEventoDominio
{
    public class Admin : Usuario
    {
        #region Constructor
        public Admin(string email, string pass) : base(email, pass)
        {
        }
        #endregion
    }
}