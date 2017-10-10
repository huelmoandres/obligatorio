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
            Ingresar();

            string opcion = "";

            while (opcion != "0")
            {
                Console.Clear();
                Console.WriteLine("1 - Ingrese usuario");
                Console.WriteLine("0 - para salir");
                opcion = Console.ReadLine().Trim();
                if(opcion != "1")
                {
                    
                }
            }

      
        }

        public static void Ingresar()
        {
            bool autenticado = false;
            string email = "";
            string pass = "";
            string opcion = "";

            while (!autenticado) {
                Console.Write("Ingrese un email: ");
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
                            autenticado = true;
                        }
                        else
                        {
                            Console.WriteLine("¡Contraseña incorrecta!");
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
                } else
                {
                        Console.Clear();
                        Console.WriteLine("¡Email no válido!\n");
                        Console.WriteLine("¿Desea salir? - s/n\n");
                        opcion = Console.ReadLine();
                }
            }
        }
    }
}
