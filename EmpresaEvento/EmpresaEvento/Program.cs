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
        public static Empresa emp = Empresa.Instancia;

        static void Main(string[] args)
        {
            Console.WriteLine("¡Bienvenido!");
            string opcion = "";
            while (opcion != "0")
            {
                Console.WriteLine("1 - Ingrese usuario");
                Console.WriteLine("0 - Salir");
                Console.Write("Seleccione una opción: ");
                opcion = Console.ReadLine().Trim();
                if (opcion == "1")
                {
                    Ingresar();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("La opción ingresada no es correcta.\n");
                }
            }
        }

        public static void Ingresar()
        {
            string email = "";
            string pass = "";
            string opcion = "";
            while (opcion != "0") {
                Console.Write("\nIngrese un email: ");
                email = Console.ReadLine();
                Console.Write("Ingrese una pass: ");
                pass = Console.ReadLine();
                if(Usuario.ValidoEmail(email))
                {
                    Usuario tengoUsuario = emp.BuscarUsuario(email);
                    if (tengoUsuario != null)
                    {
                        if (tengoUsuario.Pass == pass)
                        {
                            UsuarioLogeado(tengoUsuario);
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
                                    break;
                                case "2":
                                    AltaOrganizador(email);
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

        public static void UsuarioLogeado(Usuario logueado)
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
                        if(logueado is Organizador)
                        {
                            Console.Clear();
                            Console.WriteLine("Con acceso.\n");
                        } else
                        {
                            Console.Clear();
                            Console.WriteLine("Acceso denegado. Debe ser organizador.");
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("La opción ingresada no es correcta.\n");
                        break;
                }
            }

        }

        public static void AltaAdmin(string nuevoEmail)
        {
            Console.Clear();
            Console.WriteLine("Email: " + nuevoEmail);
            string pass = "";
            string opcion = "";
            while(opcion != "0")
            {
                Console.Write("\nIngrese una nueva contraseña para este email: ");
                pass = Console.ReadLine().Trim();
                if (emp.AltaAdministrador(nuevoEmail, pass) == Admin.ErroresAlta.Ok)
                {
                    emp.AltaAdministrador(nuevoEmail, pass);
                    opcion = "0";
                    UsuarioLogeado(emp.BuscarUsuario(nuevoEmail));
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
            string opcion = "";
            string nombre = "";
            string direccion = "";
            string telefono = "";
            while (opcion != "0")
            {
                Console.Write("\nIngrese una nueva contraseña para este email: ");
                pass = Console.ReadLine().Trim();
                Console.Write("Ingrese nombre: ");
                nombre = Console.ReadLine().Trim();
                Console.Write("Ingrese teléfono: ");
                telefono = Console.ReadLine().Trim();
                Console.Write("Ingrese dirección: ");
                direccion = Console.ReadLine().Trim();
                if (emp.AltaOrganizador(nuevoEmail, pass, nombre, telefono, direccion) == Admin.ErroresAlta.Ok)
                {
                    emp.AltaOrganizador(nuevoEmail, pass, nombre, telefono, direccion);
                    opcion = "0";
                    UsuarioLogeado(emp.BuscarUsuario(nuevoEmail));
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Datos no válidos.\n");
                    opcion = Salir();
                }
            }
        }

        public static String Salir()
        {
            Console.Write("¿Desea salir?\n0 - Salir\nCualquier tecla para continuar: ");
            String opcion = Console.ReadLine().Trim();
            return opcion;
        }
    }
}
