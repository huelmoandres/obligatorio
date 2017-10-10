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
        private List<Evento> eventos = new List<Evento>();
        #endregion

        #region Propiedades
        public static Empresa Instancia
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

        public bool BuscarUsuario(string mailUsuario)
        {
            bool encontre = false;
            int i = 0;
            while (i < usuarios.Count && !encontre)
            {
                if (usuarios[i].Email == mailUsuario) encontre = true;
                i++;
            }
            return encontre;
        }
    }
}
