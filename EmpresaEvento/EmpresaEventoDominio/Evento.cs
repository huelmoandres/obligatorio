using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEventoDominio
{
    public abstract class Evento
    {
        #region Atributos
        private static int contador = 0;
        private int id;
        private DateTime fecha;
        private byte turno;
        private string descripcion;
        private string cliente;
        private List<Contrato> contratos;
        #endregion

        #region Propiedades
        public int Id
        {
            get
            {
                return id;
            }
        }

        public DateTime Fecha
        {
            get
            {
                return fecha;
            }

            set
            {
                fecha = value;
            }
        }

        public byte Turno
        {
            get
            {
                return turno;
            }

            set
            {
                turno = value;
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

        public string Cliente
        {
            get
            {
                return cliente;
            }

            set
            {
                cliente = value;
            }
        }

        public List<Contrato> Contratos
        {
            get
            {
                return contratos;
            }
        }
        #endregion

        #region Constructor
        public Evento(DateTime fecha, byte turno, string descripcion, string cliente)
        {
            this.Fecha = fecha;
            this.Turno = turno;
            this.Descripcion = descripcion;
            this.Cliente = cliente;
            this.id = contador + 1;
            contador++;
        }
        #endregion
    }
}
