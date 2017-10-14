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
                            UsuarioLogeado();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("¡Contraseña incorrecta!\n");
                            opcion = Salir();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usuario no existente");
                        Console.WriteLine("1. Registrarse como administrador");
                        Console.WriteLine("2. Registrarse como organizador");
                        Console.WriteLine("0. Salir");
                        opcion = Console.ReadLine().Trim();
                        switch (opcion)
                        {
                            case "1":
                                Console.WriteLine(emp.AltaAdministrador(email, pass));
                                break;
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("¡Email no válido!\n");
                    opcion = Salir();
                }
            }
        }

        public static void UsuarioLogeado()
        {
            Console.WriteLine("¡Bienvenido!");
            string opcion = "";
            while (opcion != "0")
            {
                Console.WriteLine("1 - Ingrese usuario");
                Console.WriteLine("0 - Salir");
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

        public static String Salir()
        {
            Console.Write("¿Desea salir?\n0 - Salir\nCualquier tecla para continuar: ");
            String opcion = Console.ReadLine();
            return opcion;
        }
    }
}
