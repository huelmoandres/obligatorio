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
            bool resultado = Usuario.ValidoPass("Ass?11sadw");
            if(resultado)
            {
                Console.WriteLine("No válida");
            } else
            {
                Console.WriteLine("Válida");
            }
            Console.ReadKey();
        }
    }
}
