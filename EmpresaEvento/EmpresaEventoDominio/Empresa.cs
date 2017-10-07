using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEventoDominio
{
    public class Empresa
    {
        private static Empresa instancia = new Empresa();
        private List<Usuario> usuarios = new List<Usuario>();
        private List<Servicio> servicios = new List<Servicio>();

        private Empresa(){}

        public Empresa Instancia
        {
            get { return instancia;  }
        }
    }
}
