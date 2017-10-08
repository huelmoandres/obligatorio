using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEventoDominio
{
    public abstract class Usuario
    {
        #region Atributos
        private string email;
        private string pass;
        private byte rol;
        #endregion

        #region Propiedades
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }
        public string Pass
        {
            get
            {
                return pass;
            }

            set
            {
                pass = value;
            }
        }
        public byte Rol
        {
            get
            {
                return rol;
            }

            set
            {
                rol = value;
            }
        }
        #endregion

        #region Constructor
        public Usuario(string email, string pass, byte rol)
        {
            this.Email = email;
            this.Pass = pass;
            this.rol = rol;
        }
        #endregion

        public static bool ValidoPass(string pass)
        {
            bool resultado = false;
            if(pass.Length < 8)
            {
                resultado = true;
                
            }
            else if(!pass.Contains(".") && !pass.Contains(",") && !pass.Contains("?")
                && !pass.Contains(";"))
            {
                resultado = true;
            }

            int i = 0;
            bool bandera = false;
            while(i < pass.Length && !bandera)
            {
                if (char.IsUpper(pass[i]))
                {
                    bandera = true;
                } else
                {
                    resultado = true;
                }
                i++;
            }
            return resultado;
        }
    }
}
