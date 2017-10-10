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
    }
}
