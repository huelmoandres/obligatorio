using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEventoDominio
{
    public class Comun : Evento
    {
        #region Atributos
        private double duracion;
        private static double limpieza;
        #endregion

        #region Propiedades
        public double Duracion
        {
            get
            {
                return duracion;
            }

            set
            {
                duracion = value;
            }
        }

        public static double Limpieza
        {
            get
            {
                return limpieza;
            }
        }
        #endregion

        #region Constructor
        public Comun(DateTime fecha, byte turno, string descripcion, string cliente, int cantAsistentes, double duracion) : base(fecha, turno, descripcion, cliente, cantAsistentes)
        {
            this.Duracion = duracion;
        }
        #endregion

        #region Validaciones
        public static bool ControlAsistentes(int asistentes)
        {
            bool resultado = false;
            if (asistentes <= 10)
            {
                resultado = true;
            }
            return resultado;
        }
        #endregion
    }
}
