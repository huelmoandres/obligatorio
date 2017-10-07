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
        private char rol;
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
        public char Rol
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
        public Usuario(string email, string pass, char rol)
        {
            this.Email = email;
            this.Pass = pass;
            this.rol = rol;
        }
        #endregion
    }
}
