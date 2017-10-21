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
        private Usuario a1 = new Usuario("admin@eventos17.com", "Admin!99", 0);
        private Organizador o1 = new Organizador("org@eventos17.com", "Organizador!99", 1, "Sergio", "091383578", "18 de Julio 2237");
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

        public List<Evento> Eventos
        {
            get { return eventos; }
        }
        #endregion

        #region Constructor
        private Empresa() {
            usuarios.Add(a1);
            usuarios.Add(o1);
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
        public bool BuscarFechaEvento(DateTime fecha)
        {
            bool esta = false;
            int i = 0;
            while (i < eventos.Count && !esta)
            {
                if (eventos[i].Fecha == fecha) esta = true;
                i++;
            }
            return esta;
        }

        public Evento BuscarEvento(int id)
        {
            Evento eve = null;
            int i = 0;
            while (i < eventos.Count && eve == null)
            {
                if (eventos[i].Id == id) eve = eventos[i];
                i++;
            }
            return eve;
        }
        #endregion

        #region Altas
        public Usuario.ErroresAlta AltaAdministrador(string email, string pass)
        {
            Usuario.ErroresAlta resultado = Usuario.ErroresAlta.Ok;
            if(!Usuario.ValidoEmail(email))
            {
                resultado = Usuario.ErroresAlta.ErrorEmail;
            } else if(!Usuario.ValidoPass(pass))
            {
                resultado = Usuario.ErroresAlta.ErrorPass;
            }
            else if(BuscarUsuario(email) != null)
            {
                resultado = Usuario.ErroresAlta.UsuarioRepetido;
            }
            else
            {
                Usuario a = new Usuario(email, pass, 0);
                usuarios.Add(a);
            }

            return resultado;
        }
        public Organizador.ErroresAlta AltaOrganizador(string email, string pass, string nombre, string tel, string dir)
        {
            Organizador.ErroresAlta resultado = Usuario.ErroresAlta.Ok;
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
            else if (BuscarUsuario(email) != null)
            {
                resultado = Usuario.ErroresAlta.UsuarioRepetido;
            }
            else
            {
                Organizador o = new Organizador(email, pass, 1, nombre, tel, dir);
                usuarios.Add(o);
            }

            return resultado;
        }
        public Comun.ErroresAlta AltaComun(DateTime fec, byte tur, string des, string cli, int cAsis, double dur, Servicio s)
        {
            Comun.ErroresAlta resultado = Comun.ErroresAlta.Ok;
            if (!Comun.ValidoFecha(fec))
            {
                resultado = Comun.ErroresAlta.ErrorFecha;
            }
            else if (!Comun.ValidTurno(tur))
            {
                resultado = Comun.ErroresAlta.ErrorTurno;
            }
            else if (!Comun.ValidoVacio(des) || !Comun.ValidoVacio(cli))
            {
                resultado = Comun.ErroresAlta.ErrorVacio;
            }
            else if (!Comun.ValidoDuracion(dur))
            {
                resultado = Comun.ErroresAlta.ErrorDuracion;
            }
            else if (!Comun.ControlAsistentes(cAsis))
            {
                resultado = Comun.ErroresAlta.ErrorAsistentes;
            }
            else if(BuscarFechaEvento(fec))
            {
                resultado = Comun.ErroresAlta.FechaRepetida;
            }
            else
            {
                Comun c = new Comun(fec, tur, des, cli, cAsis, dur);
                Contrato con = new Contrato(s, 5);
                c.AltaContrato(con);
                eventos.Add(c);
            }

            return resultado;
        }
        #endregion
    }
}
