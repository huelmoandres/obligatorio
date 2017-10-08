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
            try
            {
                int numero = Convert.ToInt32(Console.ReadLine());
            } catch(FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
