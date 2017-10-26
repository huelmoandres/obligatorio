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
        public Usuario BuscarUsuario(string emailUsuario) //Busca un usuario por su mail y devuelve el usuario entero
        {
            Usuario usu = null;
            int i = 0;
            while (i < usuarios.Count && usu == null)
            {
                if (usuarios[i].Email == emailUsuario) usu = usuarios[i];
                i++;
            }
            return usu;
        }

        public Evento BuscarFechaEvento(DateTime fecha) // Busca eventos por fecha
        {
            Evento eve = null;
            int i = 0;
            while (i < eventos.Count && eve == null)
            {
                if (eventos[i].Fecha == fecha) eve = eventos[i];
                i++;
            }
            return eve;
        }

        public Evento BuscarEvento(int id) // Busca eventos por id
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

        public Servicio BuscarServicio(string nom) // Busca el servicio por nombre
        {
            Servicio s = null;
            int i = 0;
            while (i < servicios.Count && s == null)
            {
                if (servicios[i].Nombre == nom) s = servicios[i];
                i++;
            }
            return s;
        }
        #endregion

        #region Altas

        public Usuario.ErroresAlta AltaAdministrador(string email, string pass) // Le da de alta a un administrador y sino te devuelve el error correspondiente
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

        public Organizador.ErroresAlta AltaOrganizador(string email, string pass, string nombre, string tel, string dir) // Le da de alta a un organizador y sino te devuelve el error correspondiente
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

        public Comun.ErroresAlta AltaComun(DateTime fec, byte tur, string des, string cli, int cAsis, double dur, Servicio s, int personasServicio, string emailUsuario) // Le da de alta a un evento común
        {
            Comun.ErroresAlta resultado = Comun.ErroresAlta.Ok;
            if (!Comun.ValidoFecha(fec))
            {
                resultado = Comun.ErroresAlta.ErrorFecha;
            }
            else if (!Comun.ValidoTurno(tur))
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
            else if(BuscarFechaEvento(fec) != null)
            {
                resultado = Comun.ErroresAlta.FechaRepetida;
            }
            else if (!Contrato.ValidoCantPersonasServicio(personasServicio) || personasServicio > cAsis)
            {
                resultado = Comun.ErroresAlta.ServicioPersonas;
            }
            else if(s == null)
            {
                resultado = Comun.ErroresAlta.ServicioVacio;
            }
            else if(BuscarUsuario(emailUsuario) == null || BuscarUsuario(emailUsuario).Rol != 1)
            {
                resultado = Comun.ErroresAlta.ErrorUsuario;
            }
            else
            {
                Contrato con = new Contrato(s, personasServicio); // Se crea el contrato
                Comun c = new Comun(fec, tur, des, cli, cAsis, con, dur); // Se agrega a un evento comun
                eventos.Add(c); // Se agrega a la lista de los eventos
                Usuario u = BuscarUsuario(emailUsuario);
                Organizador o = u as Organizador;
                o.Eventos.Add(c); // Se agrega a la lista del organizador simultaneamente
            }
            return resultado;
        }

        public Premium.ErroresAlta AltaPremium(DateTime fec, byte tur, string des, string cli, int cAsis, Servicio s, int personasServicio, string emailUsuario) // Se le da de alta a un evento premium
        {
            Premium.ErroresAlta resultado = Premium.ErroresAlta.Ok;
            if (!Premium.ValidoFecha(fec))
            {
                resultado = Premium.ErroresAlta.ErrorFecha;
            }
            else if (!Premium.ValidoTurno(tur))
            {
                resultado = Premium.ErroresAlta.ErrorTurno;
            }
            else if (!Premium.ValidoVacio(des) || !Premium.ValidoVacio(cli))
            {
                resultado = Premium.ErroresAlta.ErrorVacio;
            }
            else if (!Premium.ControlAsistentes(cAsis))
            {
                resultado = Premium.ErroresAlta.ErrorAsistentes;
            }
            else if (BuscarFechaEvento(fec) != null)
            {
                resultado = Premium.ErroresAlta.FechaRepetida;
            }
            else if (!Contrato.ValidoCantPersonasServicio(personasServicio) || personasServicio > cAsis)
            {
                resultado = Premium.ErroresAlta.ServicioPersonas;
            }
            else if (s == null)
            {
                resultado = Premium.ErroresAlta.ServicioVacio;
            }
            else if (BuscarUsuario(emailUsuario) == null || BuscarUsuario(emailUsuario).Rol != 1)
            {
                resultado = Comun.ErroresAlta.ErrorUsuario;
            }
            else
            {
                Contrato con = new Contrato(s, personasServicio); // Mismo funcionamiento que con el evento común 
                Premium p = new Premium(fec, tur, des, cli, cAsis, con);
                eventos.Add(p);
                Usuario u = BuscarUsuario(emailUsuario);
                Organizador o = u as Organizador;
                o.Eventos.Add(p);
            }
            return resultado;
        }
        #endregion

        #region Agregaciones
        public Contrato.ErroresAlta AgregarServicioEvento(Evento e, Servicio s, int cantPersonas) // Se agrega un servicio a un evento ya creado
        {
            Contrato.ErroresAlta resultado = Contrato.ErroresAlta.Ok;
            if(!Contrato.ValidoCantPersonasServicio(cantPersonas))
            {
                resultado = Contrato.ErroresAlta.ErrorPersonas;
            }
            else if(!Contrato.ValidoServicioVacio(s))
            {
                resultado = Contrato.ErroresAlta.ErrorServicio;
            }
            else if(e.BuscarServicioEvento(s.Nombre))
            {
                resultado = Contrato.ErroresAlta.ErrorServicioExiste;
            }
            else if (e.CantAsistentes < cantPersonas)
            {
                resultado = Contrato.ErroresAlta.ErrorPersonas;
            }
            else if (e == null)
            {
                resultado = Contrato.ErroresAlta.ErrorEvento;
            } else
            {
                Contrato con = new Contrato(s, cantPersonas);
                e.Contratos.Add(con);
            }
            return resultado;
        }
        #endregion
    }
}
