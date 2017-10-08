using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEventoDominio
{
    public class Organizador : Usuario
    {
        #region Atributos
        private string nombre;
        private string telefono;
        private string direccion;
        #endregion

        #region Propiedades
        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }
        public string Telefono
        {
            get
            {
                return telefono;
            }

            set
            {
                telefono = value;
            }
        }
        public string Direccion
        {
            get
            {
                return direccion;
            }

            set
            {
                direccion = value;
            }
        }
        #endregion

        #region Constructor
        public Organizador(string email, string pass, byte rol, string nombre, string telefono, string direccion) : base(email, pass, rol)
        {
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Telefono = telefono;
        }
        #endregion
    }
}
