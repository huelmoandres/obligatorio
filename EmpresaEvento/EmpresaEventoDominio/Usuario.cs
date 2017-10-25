using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EmpresaEventoDominio
{
    public class Usuario
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
            this.Rol = rol;
        }
        #endregion

        #region Validaciones
        public static bool ValidoPass(string pass)
        {
            bool resultado = false;
            if ((pass.Contains(".") || pass.Contains(",") || pass.Contains("!")
                || pass.Contains(";")) && pass.Length >= 8)
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

        public static bool ValidoEmail(string email)
        {
            bool resultado = false;
            string formato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, formato))
            {
                resultado = true;
            }
            return resultado;
        }

        public static bool ValidoNombre(string nombre)
        {
            bool resultado = false;
            string formato = "^[a-zA-Z ]*$";
            if(nombre.Length >= 3 && Regex.IsMatch(nombre, formato))
            {
                resultado = true;
            }
            return resultado;
        }

        public static bool ValidoTel(string tel)
        {
            bool resultado = false;
            if (tel.Length >= 5)
            {
                resultado = true;
            }
            return resultado;
        }

        public static bool ValidoDir(string dir)
        {
            bool resultado = false;
            if (dir != "")
            {
                resultado = true;
            }
            return resultado;
        }
        
        public enum ErroresAlta
        {
            Ok,
            ErrorEmail,
            ErrorPass,
            ErrorNombre,
            ErrorTel,
            ErrorDir,
            UsuarioRepetido
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return "Email: " + this.Email +
                "\nContraseña: " + this.Pass;
        }
        #endregion
    }
}
