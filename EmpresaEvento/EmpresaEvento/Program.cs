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
        public Empresa emp = Empresa.Instancia;

        static void Main(string[] args)
        {
            //try
            //{
            //    int numero = Convert.ToInt32(Console.ReadLine());
            //} catch(FormatException e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //Console.ReadKey();

            string mail = "";
            string clave = "";

            while (mail != "0")
            {
                Console.Clear();
                Console.WriteLine("Ingrese usuario");
                Console.WriteLine("0 para salir");
                mail = Console.ReadLine().Trim();
                while(mail != "0")
                {
                    if(mail != "0")
                    {
                        if (Usuario.ValidoEmail(mail))
                        {
                            
                            Console.WriteLine("Ingrese contraseña");
                            clave = Console.ReadLine().Trim();
                            if(Usuario.ValidoPass(mail))
                            {
                                //Funcion con un despliegue de menu en donde puedas trabajar
                            }
                        }
                        else
                        {
                            Console.WriteLine("Vuelva a ingresar usuario o elige opcón 0 para salir");
                        }
                    }
                }
            }

        }
    }
}
