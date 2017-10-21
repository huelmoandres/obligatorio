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
        private static double aumento;
        #endregion

        #region Propiedades
        public static double Aumento
        {
            get
            {
                return aumento;
            }
        }
        #endregion

        #region Constructor
        public Premium(DateTime fecha, byte turno, string descripcion, string cliente, int cantAsistentes) : base(fecha, turno, descripcion, cliente, cantAsistentes)
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
    }
}
