using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEventoDominio
{
    public class Empresa
    {
        #region Atributos
        private static Empresa instancia = new Empresa();
        private List<Usuario> usuarios = new List<Usuario>();
        private List<Servicio> servicios = new List<Servicio>();
        #endregion

        #region Propiedades
        public Empresa Instancia
        {
            get { return instancia;  }
        }

        public List<Servicio> Servicios
        {
            get { return servicios; }
        }

        public List<Usuario> Usuarios
        {
            get { return usuarios; }
        }
        #endregion

        #region Constructor
        private Empresa() { }
        #endregion
    }
}
