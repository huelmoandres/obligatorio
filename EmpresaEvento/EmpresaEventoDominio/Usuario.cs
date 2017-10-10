using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EmpresaEventoDominio
{
    public abstract class Usuario
    {
        #region Atributos
        private string email;
        private string pass;
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
        #endregion

        #region Constructor
        public Usuario(string email, string pass)
        {
            this.Email = email;
            this.Pass = pass;
        }
        #endregion

        public static bool ValidoPass(string pass)
        {
            bool resultado = false;
            if (pass.Contains(".") && pass.Contains(",") && pass.Contains("?")
                && pass.Contains(";") && pass.Length > 8)
            {
                int i = 0;
                bool encontro = false;
                while (i < pass.Length && !encontro)
                {
                    if (char.IsUpper(pass[i]))
                    {
                        encontro = true;
                        resultado = true;
                    }
                    i++;
                }
            }
            return resultado;
        }

        public static bool ValidoEmail(String email)
        {
            bool resultado = false;
            string formato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, formato))
            {
                resultado = true;
            }
            return resultado;
        }

        public enum ErroresAlta
        {
            Ok,
            ErrorEmail,
            ErrorPass
        }
    }
}
