using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEventoDominio
{
    public class Premium : Evento
    {
        #region Atributos
        private static double aumento = 400;
        #endregion

        #region Propiedades
        public static double Aumento
        {
            get
            {
                return Premium.aumento;
            }
            set
            {
                Premium.aumento = value;
            }
        }
        #endregion

        #region Constructor
        public Premium(DateTime fecha, byte turno, string descripcion, string cliente, int cantAsistentes, Contrato contratos) : base(fecha, turno, descripcion, cliente, cantAsistentes, contratos)
        {
        }
        #endregion

        #region Validaciones
        public static bool ControlAsistentes(int asistentes)
        {
            bool resultado = false;
            if (asistentes > 0 && asistentes <= 100)
            {
                resultado = true;
            }
            return resultado;
        }
        #endregion

        #region Métodos
        public override double CalcularTotal() // Se calcula el total incluyendo precio de aumento y precio total por servicios brindados
        {
            double resultado = 0;
            foreach (Contrato c in Contratos)
            {
                resultado += c.SubTotal();
            }
            resultado += Premium.Aumento;
            return resultado;
        }
        public override string ToString()
        {
            return base.ToString() + "\nCosto total del evento: $" + CalcularTotal();
        }
        #endregion
    }
}
