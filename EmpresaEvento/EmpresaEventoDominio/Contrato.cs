using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEventoDominio
{
    public class Contrato
    {
        #region Atributos
        private Servicio servicio;
        private int cantPersonas;
        private double precioPersonaEstablecido;
        #endregion

        #region Propiedades
        public Servicio Servicio
        {
            get
            {
                return servicio;
            }

            set
            {
                servicio = value;
            }
        }

        public int CantPersonas
        {
            get
            {
                return cantPersonas;
            }

            set
            {
                cantPersonas = value;
            }
        }
        #endregion

        #region Constructor
        public Contrato(Servicio servicio, int cantPersonas)
        {
            this.Servicio = servicio;
            this.CantPersonas = cantPersonas;
            this.precioPersonaEstablecido = servicio.PrecioPersona;
        }
        #endregion

        public double SubTotal()
        {
            return precioPersonaEstablecido * cantPersonas;
        }

        #region Validaciones
        public static bool ValidoCantPersonasServicio(int p)
        {
            return p > 0;
        }

        public static bool ValidoServicioVacio(Servicio s)
        {
            return s != null;
        }

        public enum ErroresAlta
        {
            Ok,
            ErrorPersonas,
            ErrorServicio,
            ErrorServicioExiste,
            ErrorEvento
        }
        #endregion
    }
}

