﻿using System;
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
        private static Usuario logeado = null;

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
                Console.WriteLine("2 - Registrarse");
                Console.WriteLine("0 - Salir");
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
                Console.Write("\nIngrese email: ");
                email = Console.ReadLine();
                Console.Write("Ingrese una pass: ");
                pass = Console.ReadLine();
                logeado = emp.BuscarUsuario(email);
                if (logeado != null && logeado.Pass == pass)
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
        }

        private static void Registro()
        {
            Console.Clear();
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

        private static void UsuarioLogeado()
        {
            Console.Clear();
            string opcion = "";
            while (opcion != "0")
            {
                Console.WriteLine("\n----- Menú -----");
                Console.WriteLine("¡Bienvenido " + logeado.Email + "!");
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
                        if (logeado is Organizador)
                        {
                            RegistroEvento();
                        } else
                        {
                            Console.Clear();
                            Console.WriteLine("Acceso denegado. Debe ser organizador.");
                        }
                        break;
                    case "4":
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

        private static void RegistroEvento()
        {
            if (logeado is Organizador)
            {
                Console.Clear();
                Organizador org = (Organizador)logeado;
                Console.Clear();
                Console.WriteLine("----- Registro de Eventos -----");
                Console.WriteLine("Nombre: " + org.Nombre +
                                  "\nTeléfono: " + org.Telefono +
                                  "\nDirección: " + org.Direccion +
                                  "\nFecha de registro: " + org.Fecha + "\n");
                bool agregue = false;
                DateTime fecha = FormatoFecha("Ingrese una fecha: ");
                while (!agregue)
                {
                    if (!Comun.ValidoFecha(fecha))
                    {
                        Console.Clear();
                        Console.WriteLine("Fecha antigua.");
                        fecha = FormatoFecha("Ingrese una fecha nuevamente: ");
                    }
                    else if (emp.BuscarFechaEvento(fecha))
                    {
                        Console.Clear();
                        Console.WriteLine("En esa fecha ya existe un evento.");
                        fecha = FormatoFecha("Ingrese una fecha nuevamente: ");
                    }
                    else agregue = true;
                }
                Console.Write("\nIngrese un turno: " +
                                  "\n1- Mañana" +
                                  "\n2- Tarde" +
                                  "\n3- Noche");
                byte turno = FormatoByte("\nIngrese la opción correcta: ");
                while (turno != 1 && turno != 2 && turno != 3)
                {
                    Console.Clear();
                    Console.WriteLine("Opción incorrecta.");
                    Console.WriteLine("\nIngrese un turno: " +
                                  "\n1- Mañana" +
                                  "\n2- Tarde" +
                                  "\n3- Noche");
                    turno = FormatoByte("Ingrese la opción correcta: ");
                }
                Console.Write("\nIngrese una descripción: ");
                string des = Console.ReadLine();
                while (!Evento.ValidoVacio(des))
                {
                    Console.Clear();
                    Console.Write("Descripción inválida. Ingrese una nuevamente: ");
                    des = Console.ReadLine().Trim();
                }
                Console.Write("\nIngrese el nombre del cliente: ");
                string cliente = Console.ReadLine();
                while (!Evento.ValidoVacio(cliente))
                {
                    Console.Clear();
                    Console.Write("Nombre inválido. Ingrese uno nuevamente: ");
                    cliente = Console.ReadLine().Trim();
                }
                Console.WriteLine("\nIngrese si es un evento común o premium: " +
                                  "\n1- Común" +
                                  "\n2- Premium");
                Console.Write("Ingrese la opción correcta: ");
                string tipo = Console.ReadLine().Trim();
                while (tipo != "1" && tipo != "2")
                {
                    Console.Clear();
                    Console.WriteLine("Opción incorrecta.");
                    Console.WriteLine("Ingrese si es un evento común o premium: " +
                                      "\n1- Común" +
                                      "\n2- Premium");
                    Console.Write("Ingrese la opción correcta: ");
                    tipo = Console.ReadLine().Trim();
                }
                int cantidadPersonas = FormatoEntero("\nIngrese cantidad de peronas que van a asistir: ");
                if (tipo == "1") //Común
                {
                    while (!Comun.ControlAsistentes(cantidadPersonas))
                    {
                        Console.Clear();
                        Console.WriteLine("Error. Recuerde que la cantidad de personas no pueden ser más de 10 en eventos comunes.");
                        cantidadPersonas = FormatoEntero("Ingrese cantidad de nuevo: ");
                    }
                    Console.Clear();
                    double duracion = FormatoDecimal("\nIngrese la duración del evento: ");
                    while (!Comun.ValidoDuracion(duracion))
                    {
                        Console.Clear();
                        Console.WriteLine("Error. Recuerde que la duración no puede ser más de 4 horas en eventos comunes.");
                        duracion = FormatoDecimal("Ingrese duración de nuevo: ");
                    }
                }
                else if (tipo == "2") // Premium
                {
                    while (cantidadPersonas < 0 && cantidadPersonas > 100)
                    {
                        Console.Clear();
                        Console.WriteLine("Error. Recuerde que la cantidad de personas no pueden ser más de 100 en eventos Premium.");
                        cantidadPersonas = FormatoEntero("Ingrese cantidad de nuevo: ");
                    }
                }
            }
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

        public static void AltaAdmin()
        {
            Console.Clear();
            string pass = "";
            string email = "";
            bool agregue = false;
            Console.Write("Ingrese un email: ");
            email = Console.ReadLine().Trim();
            while (!agregue)
            {
                if (emp.BuscarUsuario(email) == null)
                {
                    while (!Usuario.ValidoEmail(email))
                    {
                        Console.Clear();
                        Console.WriteLine("El email no es correcto.");
                        Console.Write("Ingrese un email nuevamente: ");
                        email = Console.ReadLine().Trim();
                    }
                    agregue = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("El email ya existe.");
                    Console.Write("Ingrese un email nuevamente: ");
                    email = Console.ReadLine().Trim();
                }
            }
            Console.Clear();
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
            emp.AltaAdministrador(email, pass);
            logeado = emp.BuscarUsuario(email);
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
            Console.Write("Ingrese un email: ");
            email = Console.ReadLine().Trim();
            while (!agregue)
            {
                if (emp.BuscarUsuario(email) == null)
                {
                    while (!Usuario.ValidoEmail(email))
                    {
                        Console.Clear();
                        Console.WriteLine("El email no es correcto.");
                        Console.Write("Ingrese un email nuevamente: ");
                        email = Console.ReadLine().Trim();
                    }
                    agregue = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("El email ya existe.");
                    Console.Write("Ingrese un email nuevamente: ");
                    email = Console.ReadLine().Trim();
                }
            }
            Console.Clear();
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
            emp.AltaOrganizador(email, pass, nombre, telefono, direccion);
            logeado = emp.BuscarUsuario(email);
            UsuarioLogeado();
        }

        public static DateTime FormatoFecha(string texto)
        {
            DateTime fecha = new DateTime();
            Console.WriteLine(texto);
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
            Console.WriteLine(texto);
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
            Console.WriteLine(texto);
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
            Console.WriteLine(texto);
            string strDur = Console.ReadLine();
            while (!double.TryParse(strDur, out num))
            {
                Console.Write("Selección no válida. Ingrese una nuevamente: ");
                strDur = Console.ReadLine();
            }
            return num;
        }
        public static string Salir()
        {
            Console.Write("¿Desea salir?\n0 - Salir\nCualquier tecla para continuar: ");
            string opcion = Console.ReadLine().Trim();
            return opcion;
        }
    }
}

