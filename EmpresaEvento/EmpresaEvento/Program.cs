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
                Console.WriteLine("----- Login -----");
                Console.WriteLine("1 - Ingrese usuario");
                Console.WriteLine("0 - Salir");
                Console.Write("Seleccione una opción: ");
                opcion = Console.ReadLine().Trim();
                if (opcion == "1")
                {
                    Ingresar();
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
                Console.Write("\nIngrese un email: ");
                email = Console.ReadLine();
                if (Usuario.ValidoEmail(email))
                {
                    Usuario tengoUsuario = emp.BuscarUsuario(email);
                    if (tengoUsuario != null)
                    {
                        Console.Write("Ingrese una pass: ");
                        pass = Console.ReadLine();
                        if (tengoUsuario.Pass == pass)
                        {
                            UsuarioLogeado(tengoUsuario);
                            opcion = "0";
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("¡Datos erróneos!\n");
                            opcion = Salir();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Email no registrado\n");
                        Console.WriteLine("1. Registrarse como administrador");
                        Console.WriteLine("2. Registrarse como organizador");
                        Console.WriteLine("0. Salir");
                        while (opcion != "0")
                        {
                            Console.Write("Seleccione una opción: ");
                            opcion = Console.ReadLine().Trim();
                            switch (opcion)
                            {
                                case "1":
                                    AltaAdmin(email);
                                    opcion = "0";
                                    break;
                                case "2":
                                    AltaOrganizador(email);
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
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("¡Datos erróneos!\n");
                    opcion = Salir();
                }
            }
        }

        private static void UsuarioLogeado(Usuario logueado)
        {
            string opcion = "";
            while (opcion != "0")
            {
                Console.WriteLine("\n----- Menú -----");
                Console.WriteLine("¡Bienvenido " + logueado.Email + "!");
                Console.WriteLine("1 - Listar todos los usuarios");
                Console.WriteLine("2 - Listar catálogo de servicios");
                Console.WriteLine("3 - Registrar un evento");
                Console.WriteLine("4 - Eventos de un organizador");
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
                            Organizador org = logueado as Organizador;
                            RegistroEvento(org);
                        } else
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

        private static void RegistroEvento(Organizador org)
        {
            Console.Clear();
            Console.WriteLine("Nombre: " + org.Nombre +
                              "Teléfono: " + org.Telefono +
                              "Dirección: " + org.Direccion +
                              "Fecha de registro: " + org.Fecha);

        }

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

        public static void AltaAdmin(string nuevoEmail)
        {
            Console.Clear();
            Console.WriteLine("Email: " + nuevoEmail);
            string pass = "";
            string opcion = "";
            while (opcion != "0")
            {
                Console.Write("\nIngrese una nueva contraseña para este email: ");
                pass = Console.ReadLine().Trim();
                if (emp.AltaAdministrador(nuevoEmail, pass) == Usuario.ErroresAlta.Ok)
                {
                    UsuarioLogeado(emp.BuscarUsuario(nuevoEmail));
                    opcion = "0";
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Contraseña incorrecta.\n");
                    opcion = Salir();
                }
            }
        }

        public static void AltaOrganizador(string nuevoEmail)
        {
            Console.Clear();
            Console.WriteLine("Email: " + nuevoEmail);
            string pass = "";
            string nombre = "";
            string direccion = "";
            string telefono = "";
            Console.Write("Ingrese una nueva contraseña para este email: ");
            pass = Console.ReadLine().Trim();
            while (!Usuario.ValidoPass(pass))
            {
                Console.Clear();
                Console.WriteLine("Contraseña inválida. Debe contener: " +
                                  "\n - Al menos uno de los siguientes caracteres ';' ',' '!' '.'" +
                                  "\n - Tener un mínimo de 8 caracteres y una mayúscula.");
                Console.Write("Ingrese una contraseña nuevamente: ");
                pass = Console.ReadLine().Trim();
            }
            Console.Clear();
            Console.Write("Ingrese un nombre: ");
            nombre = Console.ReadLine().Trim();
            while (!Usuario.ValidoNombre(nombre))
            {
                Console.Clear();
                Console.Write("Nombre inválido. Ingrese uno nuevamente: ");
                nombre = Console.ReadLine().Trim();
            }
            Console.Clear();
            Console.Write("Ingrese teléfono: ");
            telefono = Console.ReadLine().Trim();
            while (!Usuario.ValidoTel(telefono))
            {
                Console.Clear();
                Console.Write("Teléfono inválido. Ingrese uno nuevamente: ");
                telefono = Console.ReadLine().Trim();
            }
            Console.Clear();
            Console.Write("Ingrese dirección: ");
            direccion = Console.ReadLine().Trim();
            while (!Usuario.ValidoDir(direccion))
            {
                Console.Clear();
                Console.Write("Dirección inválida. Ingrese una nuevamente: ");
                direccion = Console.ReadLine().Trim();
            }
            emp.AltaOrganizador(nuevoEmail, pass, nombre, telefono, direccion);
            UsuarioLogeado(emp.BuscarUsuario(nuevoEmail));
        }

        public static void ValidoVacio(string mensaje, string opcion)
        {

        }

        public static string Salir()
        {
            Console.Write("¿Desea salir?\n0 - Salir\nCualquier tecla para continuar: ");
            string opcion = Console.ReadLine().Trim();
            return opcion;
        }
    }
}

