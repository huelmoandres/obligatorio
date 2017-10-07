using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEventoDominio
{
    public class Servicio
    {
        #region Atributos
        private string nombre;
        private string descripcion;
        private double precioPersona;
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

        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public double PrecioPersona
        {
            get
            {
                return precioPersona;
            }

            set
            {
                precioPersona = value;
            }
        }
        #endregion

        #region Constructor
        public Servicio(string nombre, string descripcion, double precioPersona)
        {
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.PrecioPersona = precioPersona;
        }
        #endregion

    }
}
