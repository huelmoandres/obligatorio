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
        private static double limpieza = 300;
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
                return Comun.limpieza;
            }
            set
            {
                Comun.limpieza = value;
            }
        }
        #endregion

        #region Constructor
        public Comun(DateTime fecha, byte turno, string descripcion, string cliente, int cantAsistentes, Contrato contrato, double duracion) : base(fecha, turno, descripcion, cliente, cantAsistentes, contrato)
        {
            this.Duracion = duracion;
        }
        #endregion

        #region Validaciones
        public static bool ValidoDuracion(double dur)
        {
            bool resultado = false;
            if(dur > 0 && dur <= 4)
            {
                resultado = true;
            }
            return resultado;
        }

        public static bool ControlAsistentes(int asistentes)
        {
            bool resultado = false;
            if (asistentes > 0 && asistentes <= 10)
            {
                resultado = true;
            }
            return resultado;
        }
        #endregion

        #region Métodos
        public override double CalcularTotal()
        {
            double resultado = 0;
            foreach (Contrato c in Contratos)
            {
                resultado += c.SubTotal();
            }
            resultado += Comun.Limpieza;
            return resultado;
        }

        public override string ToString()
        {
            return base.ToString() + "\nDuración: " + duracion + " hs" +
                                     "\nCosto total del evento: $" + CalcularTotal();
        }
        #endregion
    }
}
