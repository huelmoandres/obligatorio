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
        private DateTime fecha;
        private List<Evento> eventos;
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
        public DateTime Fecha
        {
            get
            {
                return fecha;
            }
        }

        public List<Evento> Eventos
        {
            get
            {
                return eventos;
            }
        }
        #endregion

        #region Constructor
        public Organizador(string email, string pass, byte rol, string nombre, string telefono, string direccion) : base(email, pass, rol)
        {
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Telefono = telefono;
            this.fecha = DateTime.Today;
            eventos = new List<Evento>();
        }
        #endregion

        #region Métodos
        public void AgregarEvento(Evento e)
        {
            eventos.Add(e);
        }

        public double CostoTotalEventos()
        {
            double resultado = 0;
            foreach (Evento e in eventos)
            {
                resultado += e.CalcularTotal();
            }
            return resultado;
        }

        public override string ToString()
        {
            return base.ToString() +
                "\nNombre: " + this.Nombre +
                "\nTeléfono: " + this.Telefono +
                "\nDirección: " + this.Direccion;
        }
        #endregion
    }
}
