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

        #region Usuarios pre-cargados
        private Usuario a1 = new Admin("admin@eventos17.com", "Admin!99");
        #endregion
        #region Servicios pre-cargados
        private Servicio s1 = new Servicio("Cocina", "El servicio incluye plato principal y postre", 480);
        private Servicio s2 = new Servicio("Fotografía", "El servicio incluye cinco fotos por persona", 120);
        private Servicio s3 = new Servicio("Localidad", "El servicio incluye una localidad por persona", 50);
        #endregion
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
        private Empresa() {
            usuarios.Add(a1);
            servicios.Add(s1);
            servicios.Add(s2);
            servicios.Add(s3);
        }
        #endregion

        #region Buscadores
        public Usuario BuscarUsuario(string mailUsuario)
        {
            Usuario usu = null;
            int i = 0;
            while (i < usuarios.Count && usu == null)
            {
                if (usuarios[i].Email == mailUsuario) usu = usuarios[i];
                i++;
            }
            return usu;
        }

        PENEEEE
        public Evento BuscarEvento(DateTime fecha)
        {
            Evento eve = null;
            int i = 0;
            while (i < eventos.Count && eve == null)
            {
                if (eventos[i].Email == mailUsuario) usu = usuarios[i];
                i++;
            }
            return usu;
        }
        #endregion

        #region Altas
        public Admin.ErroresAlta AltaAdministrador(string email, string pass)
        {
            Admin.ErroresAlta resultado = Admin.ErroresAlta.Ok;
            if (!Admin.ValidoEmail(email))
            {
                resultado = Admin.ErroresAlta.ErrorEmail;
            } else if(!Admin.ValidoPass(pass))
            {
                resultado = Admin.ErroresAlta.ErrorPass;
            } else
            {
                Admin a = new Admin(email, pass);
                usuarios.Add(a);
            }

            return resultado;
        }
        public Organizador.ErroresAlta AltaOrganizador(string email, string pass, string nombre, string tel, string dir)
        {
            Organizador.ErroresAlta resultado = Admin.ErroresAlta.Ok;
            if (!Organizador.ValidoEmail(email))
            {
                resultado = Organizador.ErroresAlta.ErrorEmail;
            }
            else if (!Organizador.ValidoPass(pass))
            {
                resultado = Organizador.ErroresAlta.ErrorPass;
            }
            else if(!Organizador.ValidoNombre(nombre))
            {
                resultado = Organizador.ErroresAlta.ErrorNombre;
            }
            else if (!Organizador.ValidoTel(tel))
            {
                resultado = Organizador.ErroresAlta.ErrorTel;
            }
            else if (!Organizador.ValidoDir(dir))
            {
                resultado = Organizador.ErroresAlta.ErrorDir;
            }
            else
            {
                Organizador o = new Organizador(email, pass, nombre, tel, dir);
                usuarios.Add(o);
            }

            return resultado;
        }
        #endregion
    }
}
