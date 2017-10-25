using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpresaEventoDominio;

namespace EmpresaEvento
{ 
    class Program
    {
        private static Empresa emp = Empresa.Instancia;
        private static Usuario logueado = null;

        static void Main(string[] args)
        {
            MenuPrincipal();
        }

        private static void MenuPrincipal()
        {
            string opcion = "";
            while (opcion != "0")
            {
                Console.Clear();
                Console.WriteLine("----- Bienvenido -----");
                Console.WriteLine("1 - Ingrese usuario");
                Console.WriteLine("2 - Registrarse");
                Console.WriteLine("0 - Salir");
                Console.WriteLine("----------------------");
                Console.Write("Seleccione una opción: ");
                opcion = Console.ReadLine().Trim();
                if (opcion == "1")
                {
                    Ingresar();
                }
                else if(opcion == "2")
                {
                    Registro();
                }
                else if (opcion != "0")
                {
                    Console.Clear();
                    Console.WriteLine("La opción ingresada no es correcta.\n");
                }
            }
        }


        private static void Ingresar()
        {
            string email = "";
            string pass = "";
            string opcion = "";
            while (opcion != "0") {
                Console.Clear();
                Console.WriteLine("----- Ingresar -----");
                Console.Write("Ingrese email: ");
                email = Console.ReadLine();
                Console.Write("Ingrese una pass: ");
                pass = Console.ReadLine();
                logueado = emp.BuscarUsuario(email);
                if (logueado != null && logueado.Pass == pass)
                {
                    UsuarioLogeado();
                    opcion = "0";
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("¡Datos erróneos!\n");
                    opcion = Salir();
                }
            }
            Console.WriteLine("------------------------------------");
        }

        #region MenuLogueado
        private static void UsuarioLogeado()
        {
            Console.Clear();
            string opcion = "";
            while (opcion != "0")
            {
                Console.WriteLine("\n----- Menú -----");
                Console.WriteLine("¡Bienvenido " + logueado.Email + "!");
                Console.WriteLine("1 - Listar todos los usuarios");
                Console.WriteLine("2 - Listar catálogo de servicios");
                Console.WriteLine("3 - Registrar un evento");
                Console.WriteLine("4 - Eventos de un organizador");
                Console.WriteLine("5 - Agregar más servicios a un evento");
                Console.WriteLine("0 - Salir");
                Console.Write("Seleccione una opción: ");
                opcion = Console.ReadLine().Trim();
                switch (opcion)
                {
                    case "1":
                        ListarUsuarios();
                        break;
                    case "2":
                        ListarServicios();
                        break;
                    case "3":
                        if (logueado is Organizador)
                        {
                            RegistroEvento();
                        } else
                        {
                            Console.Clear();
                            Console.WriteLine("Acceso denegado. Debe ser organizador.");
                        }
                        break;
                    case "4":
                        EventosOrganizador();
                        break;
                    case "5":
                        if (logueado is Organizador)
                        {
                            AgregarServicioEvento();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Acceso denegado. Debe ser organizador.");
                        }
                        break;
                    case "0":
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("La opción ingresada no es correcta.\n");
                        break;
                }
            }
        }
        #endregion

        #region Listados
        private static void ListarUsuarios()
        {
            Console.Clear();
            Console.WriteLine("----- Listado de Usuarios -----");
            foreach (Usuario u in emp.Usuarios)
            {
                Console.WriteLine(u.ToString());
                if (u.Rol == 0)
                {
                    Console.WriteLine("Rol: Admin\n");
                }
                else if(u.Rol == 1)
                {
                    Console.WriteLine("Rol: Organizador\n");
                }
            }
            Console.WriteLine("-------------------------------");
        }

        private static void ListarServicios()
        {
            Console.Clear();
            Console.WriteLine("----- Listado de Servicios -----");
            foreach (Servicio s in emp.Servicios)
            {
                Console.WriteLine(s.ToString());
            }
            Console.WriteLine("-------------------------------");
        }

        private static void EventosOrganizador()
        {
            Console.Clear();
            Console.WriteLine("----- Eventos por Organizador -----");
            Console.Write("Ingrese mail del organizador: ");
            string email = Console.ReadLine();
            Usuario u = emp.BuscarUsuario(email);
            if (u != null && u is Organizador)
            {
                Organizador org = (Organizador)u;
                if (org.Eventos.Count() != 0)
                {
                    Console.WriteLine("Organizador: " + org.Nombre + "\n");
                    foreach (Evento e in org.Eventos)
                    {
                        Console.WriteLine("Código del evento: " + e.Id +
                                          "\nNombre del cliente: " + e.Cliente +
                                          "\nCosto Total: $" + e.CalcularTotal());
                        Console.WriteLine("------------------------------------");
                    }
                    Console.WriteLine("Monto total de eventos: $" + org.CostoTotalEventos());
                }
                else
                {
                    Console.WriteLine("No existen eventos creados por este organizador.");
                }
            }
            else
            {
                Console.WriteLine("No existe un organizdor con ese email.");
            }
            Console.WriteLine("-----------------------------------");
        }
        #endregion

        #region Registro
        private static void Registro()
        {
            Console.Clear();
            Console.WriteLine("----- Registro de Usuario -----");
            Console.WriteLine("1. Registrarse como administrador");
            Console.WriteLine("2. Registrarse como organizador");
            Console.WriteLine("0. Salir");
            string opcion = "";
            while (opcion != "0")
            {
                Console.Write("Seleccione una opción: ");
                opcion = Console.ReadLine().Trim();
                switch (opcion)
                {
                    case "1":
                        AltaAdmin();
                        opcion = "0";
                        break;
                    case "2":
                        AltaOrganizador();
                        opcion = "0";
                        break;
                    case "0":
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("La opción ingresada no es correcta.\n");
                        break;
                }
            }
        }

        public static void AltaAdmin()
        {
            Console.Clear();
            string pass = "";
            string email = "";
            bool agregue = false;
            Console.WriteLine("----- Registro de Administrador -----");
            Console.Write("Ingrese un email: ");
            email = Console.ReadLine().Trim();
            while (!agregue)
            {
                if (emp.BuscarUsuario(email) == null)
                {
                    while (!Usuario.ValidoEmail(email))
                    {
                        Console.WriteLine("El email no es correcto.");
                        Console.Write("Ingrese un email nuevamente: ");
                        email = Console.ReadLine().Trim();
                    }
                    agregue = true;
                }
                if (emp.BuscarUsuario(email) != null)
                {
                    Console.WriteLine("El email ya existe.");
                    Console.Write("Ingrese un email nuevamente: ");
                    email = Console.ReadLine().Trim();
                    agregue = false;
                }
            }
            Console.WriteLine("-----------------------------------------");
            Console.Write("Ingrese una nueva contraseña para este email: ");
            pass = Console.ReadLine().Trim();
            while (!Usuario.ValidoPass(pass))
            {
                Console.WriteLine("Contraseña inválida. Debe contener: " +
                                    "\n - Al menos uno de los siguientes caracteres ';' ',' '!' '.'" +
                                    "\n - Tener un mínimo de 8 caracteres y una mayúscula.");
                Console.Write("Ingrese una contraseña nuevamente: ");
                pass = Console.ReadLine().Trim();
            }
            emp.AltaAdministrador(email, pass);
            logueado = emp.BuscarUsuario(email);
            UsuarioLogeado();
        }

        public static void AltaOrganizador()
        {
            Console.Clear();
            string pass = "";
            string nombre = "";
            string direccion = "";
            string telefono = "";
            string email = "";
            bool agregue = false;
            Console.WriteLine("----- Registro de Organizador -----");
            Console.Write("Ingrese un email: ");
            email = Console.ReadLine().Trim();
            while (!agregue)
            {
                if (emp.BuscarUsuario(email) == null)
                {
                    while (!Usuario.ValidoEmail(email))
                    {
                        Console.WriteLine("El email no es correcto.");
                        Console.Write("Ingrese un email nuevamente: ");
                        email = Console.ReadLine().Trim();
                    }
                    agregue = true;
                }
                if (emp.BuscarUsuario(email) != null)
                {
                    Console.WriteLine("El email ya existe.");
                    Console.Write("Ingrese un email nuevamente: ");
                    email = Console.ReadLine().Trim();
                    agregue = false;
                }
            }
            Console.WriteLine("-----------------------------------------");
            Console.Write("Ingrese una nueva contraseña para este email: ");
            pass = Console.ReadLine().Trim();
            while (!Usuario.ValidoPass(pass))
            {
                Console.WriteLine("Contraseña inválida. Debe contener: " +
                                  "\n - Al menos uno de los siguientes caracteres ';' ',' '!' '.'" +
                                  "\n - Tener un mínimo de 8 caracteres y una mayúscula.");
                Console.Write("Ingrese una contraseña nuevamente: ");
                pass = Console.ReadLine().Trim();
            }
            Console.WriteLine("-----------------------------------------");
            Console.Write("Ingrese un nombre: ");
            nombre = Console.ReadLine().Trim();
            while (!Usuario.ValidoNombre(nombre))
            {
                Console.Write("Nombre inválido. Ingrese uno nuevamente: ");
                nombre = Console.ReadLine().Trim();
            }
            Console.WriteLine("-----------------------------------------");
            Console.Write("Ingrese teléfono: ");
            telefono = Console.ReadLine().Trim();
            while (!Usuario.ValidoTel(telefono))
            {
                Console.Write("Teléfono inválido. Ingrese uno nuevamente: ");
                telefono = Console.ReadLine().Trim();
            }
            Console.WriteLine("-----------------------------------------");
            Console.Write("Ingrese dirección: ");
            direccion = Console.ReadLine().Trim();
            while (!Usuario.ValidoDir(direccion))
            {
                Console.Write("Dirección inválida. Ingrese una nuevamente: ");
                direccion = Console.ReadLine().Trim();
            }
            emp.AltaOrganizador(email, pass, nombre, telefono, direccion);
            logueado = emp.BuscarUsuario(email);
            UsuarioLogeado();
        }
        #endregion

        #region Eventos
        private static void RegistroEvento()
        {
            if (logueado is Organizador)
            {
                Console.Clear();
                Organizador org = (Organizador)logueado;
                Console.Clear();
                Console.WriteLine("----- Registro de Eventos -----");
                Console.WriteLine("Nombre: " + org.Nombre +
                                  "\nTeléfono: " + org.Telefono +
                                  "\nDirección: " + org.Direccion +
                                  "\nFecha de registro: " + org.Fecha + "\n");
                string cliente = "", des = "", tipo = "";
                byte turno = 0;
                double duracion = 0;
                int cantidadPersonas = 0;
                bool agregue = false;
                DateTime fecha = FormatoFecha("Ingrese una fecha: ");
                while (!agregue)
                {
                    if (!Comun.ValidoFecha(fecha))
                    {
                        Console.WriteLine("Fecha antigua.");
                        fecha = FormatoFecha("Ingrese una fecha nuevamente: ");
                    }
                    else if (emp.BuscarFechaEvento(fecha) != null)
                    {
                        Console.WriteLine("En esa fecha ya existe un evento.");
                        fecha = FormatoFecha("Ingrese una fecha nuevamente: ");
                    }
                    else agregue = true;
                }
                Console.WriteLine("-----------------------------------------");
                Console.Write("Ingrese un turno: " +
                                  "\n1- Mañana" +
                                  "\n2- Tarde" +
                                  "\n3- Noche");
                turno = FormatoByte("\nIngrese opción: ");
                while (turno != 1 && turno != 2 && turno != 3)
                {
                    Console.WriteLine("Opción incorrecta.");
                    Console.WriteLine("Ingrese un turno: " +
                                  "\n1- Mañana" +
                                  "\n2- Tarde" +
                                  "\n3- Noche");
                    turno = FormatoByte("\nIngrese opción: ");
                }
                Console.WriteLine("-----------------------------------------");
                Console.Write("Ingrese una descripción: ");
                des = Console.ReadLine();
                while (!Evento.ValidoVacio(des))
                {
                    Console.Write("El campo descripción es obligatorio. Ingresar una nuevamente: ");
                    des = Console.ReadLine().Trim();
                }
                Console.WriteLine("-----------------------------------------");
                Console.Write("Ingrese el nombre del cliente: ");
                cliente = Console.ReadLine();
                while (!Evento.ValidoVacio(cliente))
                {
                    Console.Write("Nombre inválido. Ingrese uno nuevamente: ");
                    cliente = Console.ReadLine().Trim();
                }
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("Ingrese si es un evento común o premium: " +
                                  "\n1- Común" +
                                  "\n2- Premium");
                Console.Write("Ingrese opción: ");
                tipo = Console.ReadLine().Trim();
                while (tipo != "1" && tipo != "2")
                {
                    Console.WriteLine("Opción incorrecta.");
                    Console.WriteLine("Ingrese si es un evento común o premium: " +
                                      "\n1- Común" +
                                      "\n2- Premium");
                    Console.Write("Ingrese opción: ");
                    tipo = Console.ReadLine().Trim();
                }
                Console.WriteLine("-----------------------------------------");
                cantidadPersonas = FormatoEntero("Ingrese cantidad de peronas que van a asistir: ");
                if (tipo == "1") //Común
                {
                    while (!Comun.ControlAsistentes(cantidadPersonas))
                    {
                        Console.WriteLine("Error. Recuerde que la cantidad de personas no pueden ser más de 10 en eventos comunes.");
                        cantidadPersonas = FormatoEntero("Ingrese cantidad de nuevo: ");
                    }
                    Console.WriteLine("-----------------------------------------");
                    duracion = FormatoDecimal("Ingrese la duración del evento: ");
                    while (!Comun.ValidoDuracion(duracion))
                    {
                        Console.WriteLine("Error. Recuerde que la duración no puede ser más de 4 horas en eventos comunes.");
                        duracion = FormatoDecimal("Ingrese duración de nuevo: ");
                    }
                }
                else if (tipo == "2") // Premium
                {
                    while (!Premium.ControlAsistentes(cantidadPersonas))
                    {
                        Console.WriteLine("Error. Recuerde que la cantidad de personas no pueden ser más de 100 en eventos Premium.");
                        cantidadPersonas = FormatoEntero("Ingrese cantidad de nuevo: ");
                    }
                }
                Console.WriteLine("-----------------------------------------");
                ListarServicios();
                Console.Write("Ingrese un servicio por su nombre: ");
                string nombreS = Console.ReadLine().Trim();
                Servicio s = emp.BuscarServicio(nombreS);
                while (s == null)
                {
                    ListarServicios();
                    Console.Write("Nombre incorrecto. Vuelva a ingresarlo: ");
                    nombreS = Console.ReadLine().Trim();
                    s = emp.BuscarServicio(nombreS);
                }
                Console.WriteLine("-----------------------------------------");
                int personasServicio = FormatoEntero("Ingrese la cantidad de personas que recibirán ese servicio: ");
                while (personasServicio > cantidadPersonas || !Contrato.ValidoCantPersonasServicio(personasServicio))
                {
                    personasServicio = FormatoEntero("Error. Vuelva a ingresar la cantidad de personas: ");
                }
                if(tipo == "1")
                {
                    emp.AltaComun(fecha, turno, des, cliente, cantidadPersonas, duracion, s, personasServicio, logueado.Email);
                }
                else if(tipo == "2")
                {
                    emp.AltaPremium(fecha, turno, des, cliente, cantidadPersonas, s, personasServicio, logueado.Email);
                }
                DetalleEvento(fecha);
            }
        }

        #region Agregaciones
        private static void AgregarServicioEvento()
        {
            if (emp.Eventos.Count() != 0)
            {
                Console.Clear();
                Console.WriteLine("----- Agregar Servicio a Evento -----");
                Console.WriteLine("Ingrese un evento por su código: ");
                foreach (Evento eve in emp.Eventos)
                {
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("Código de evento: " + eve.Id);
                    Console.WriteLine("Fecha de evento: " + eve.Fecha);
                    Console.WriteLine("---------------------------------");
                }
                int codigo = FormatoEntero("Ingrese el códigdo del evento: ");
                Evento e = emp.BuscarEvento(codigo);
                while (e == null)
                {
                    codigo = FormatoEntero("Código incorrecto. Vuelva a ingresarlo: ");
                    e = emp.BuscarEvento(codigo);
                }
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("Ingrese un servicio por su nombre: ");
                ListarServicios();
                Console.Write("Ingrese nombre del servicio: ");
                string nombreS = Console.ReadLine().Trim();
                Servicio s = emp.BuscarServicio(nombreS);
                while (s == null)
                {
                    Console.Clear();
                    ListarServicios();
                    Console.Write("Nombre incorrecto. Vuelva a ingresarlo: ");
                    nombreS = Console.ReadLine().Trim();
                    s = emp.BuscarServicio(nombreS);
                }
                Console.WriteLine("-----------------------------------------");
                if (!e.BuscarServicioEvento(s.Nombre))
                {
                    int personasServicio = FormatoEntero("Ingrese la cantidad de personas que recibirán ese servicio: ");
                    while (personasServicio > e.CantAsistentes || !Contrato.ValidoCantPersonasServicio(personasServicio))
                    {
                        personasServicio = FormatoEntero("Error. Vuelva a ingresar la cantidad de personas: ");
                    }
                    emp.AgregarServicioEvento(e, s, personasServicio);
                }
                else
                {
                    Console.WriteLine("Este evento ya cuenta con este servicio.");
                }

            }
            else
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------------");
                Console.Write("No hay eventos creados\n");
            }
            Console.WriteLine("-----------------------------------------");
        }
        #endregion

        #endregion

        #region Detalles
        public static void DetalleEvento(DateTime fecha)
        {
            Console.Clear();
            Evento e = emp.BuscarFechaEvento(fecha);
            if (e != null)
            {
                Console.WriteLine("----- Detalle del Evento -----");
                Console.WriteLine(e.ToString());
                Console.WriteLine("-------------------------------");
            }
        }
        #endregion

        #region Formatos
        public static DateTime FormatoFecha(string texto)
        {
            DateTime fecha = new DateTime();
            Console.Write(texto);
            string strFecha = Console.ReadLine();
            while (!DateTime.TryParse(strFecha, out fecha))
            {
                Console.Write("Formato de fecha no válido. Ingrese una nuevamente: ");
                strFecha = Console.ReadLine();
            }
            return fecha;
        }

        public static byte FormatoByte(string texto)
        {
            byte num = 0;
            Console.Write(texto);
            string strTurno = Console.ReadLine();
            while (!byte.TryParse(strTurno, out num))
            {
                Console.Write("Selección no válida. Ingrese una nuevamente: ");
                strTurno = Console.ReadLine();
            }
            return num;
        }

        public static int FormatoEntero(string texto)
        {
            int num = 0;
            Console.Write(texto);
            string strCant = Console.ReadLine();
            while (!int.TryParse(strCant, out num))
            {
                Console.Write("Selección no válida. Ingrese una nuevamente: ");
                strCant = Console.ReadLine();
            }
            return num;
        }

        public static double FormatoDecimal(string texto)
        {
            double num = 0;
            Console.Write(texto);
            string strDur = Console.ReadLine();
            while (!double.TryParse(strDur, out num))
            {
                Console.Write("Selección no válida. Ingrese una nuevamente: ");
                strDur = Console.ReadLine();
            }
            return num;
        }
        #endregion

        #region Salir
        public static string Salir()
        {
            Console.Write("¿Desea salir?\n0 - Salir\nCualquier tecla para continuar: ");
            string opcion = Console.ReadLine().Trim();
            return opcion;
        }
        #endregion
    }
}

