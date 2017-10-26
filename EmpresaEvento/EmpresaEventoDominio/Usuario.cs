using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EmpresaEventoDominio
{
    public class Usuario
    {
        #region Atributos
        private string email;
        private string pass;
        private byte rol;
        #endregion

        #region Propiedades
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }
        public string Pass
        {
            get
            {
                return pass;
            }

            set
            {
                pass = value;
            }
        }

        public byte Rol
        {
            get
            {
                return rol;
            }
            set
            {
                rol = value;
            }
        }
        #endregion

        #region Constructor
        public Usuario(string email, string pass, byte rol)
        {
            this.Email = email;
            this.Pass = pass;
            this.Rol = rol;
        }
        #endregion

        #region Validaciones

        public static bool ValidoPass(string pass)
        {
            bool resultado = false;
            bool mayu = false;
            bool puntuacion = false;
            int i = 0;
            int contador = 0;
            while (i < pass.Length && (!mayu || !puntuacion || contador < 8))
            {
                if (pass[i] == '!' || pass[i] == '.' || pass[i] == ',' || pass[i] == '¡' || pass[i] == ';')
                {
                    puntuacion = true;
                }
                if (char.IsUpper(pass[i]))
                {
                    mayu = true;
                }
                contador++;
                i++;
            }
            if (contador >= 8 && mayu && puntuacion) resultado = true;
            return resultado;
        }

        public static bool ValidoEmail(string email)
        {
            bool resultado = false;
            bool arroba = false;
            bool punto = false;
            bool existe = false;
            int contArroba = 0;
            int contPunto = 0;
            if (email.Length >= 5) // Me aseguro que haya por lo menos 5 caracteres para poder tener una letra, un arroba, un punto y una letra depués del arroba y punto
            {
                if (email[0] != '@' && email[0] != ' ') // Verifico que mi primer caracter no sea arroba ni vacío
                {
                    int i = 1;
                    while (i < email.Length && email[i] != ' ' && !arroba) // Luego busco el arroba
                    {
                        if (email[i] == '@')
                        {
                            arroba = true;
                            contArroba++;
                        }
                        i++;
                    }
                    if (arroba) // Si lo encuentro
                    {
                        int j = i; // Empiezo a buscar el punto a partir de la posición inmediatamente después que encontré el arroba
                        if (j < email.Length && email[j] != '.') // corroboro que exista algo después del arroba y no sea igual a punto 
                        {
                            j++;
                            while (j < email.Length && email[j] != ' ' && !punto)
                            {
                                if (email[j] == '@')
                                {
                                    contArroba++;
                                }
                                if (email[j] == '.')
                                {
                                    punto = true;
                                    contPunto++;
                                }
                                j++;
                            }
                        }
                        if (punto) // Si encuentro el punto
                        {
                            int m = j; // Empiezo a buscar que no haya más puntos ni arrobas.
                            if (m < email.Length) // Busco que haya algo después del punto
                            {
                                while (m < email.Length && email[m] != ' ')
                                {
                                    if (email[m] == '@')
                                    {
                                        contArroba++;
                                    }
                                    if (email[m] == '.')
                                    {
                                        contPunto++;
                                    }
                                    m++;
                                }
                                existe = true;
                            }
                        }
                    }
                }
                if (contArroba == 1 && contPunto == 1 && existe) resultado = true;
            }
            return resultado;
        }

        public static bool ValidoNombre(string nombre)
        {
            bool resultado = false;
            nombre = nombre.Trim();
            if (nombre.Length >= 3)
            {
                int i = 0;
                bool noLetra = false;
                while (i < nombre.Length && !noLetra)
                {
                    if(!nombre[i].Equals(' '))
                    {
                        if(!char.IsLetter(nombre[i]))
                        {
                            noLetra = true;
                        }
                    }
                    i++;
                }
                if (!noLetra) resultado = true;
            }
            return resultado;

            /*
            bool resultado = false;
            string formato = "^[a-zA-Z]*$";
            if(nombre.Length >= 3 && Regex.IsMatch(nombre, formato))
            {
                resultado = true;
            }
            */
        }

        public static bool ValidoTel(string tel)
        {
            bool resultado = false;
            if (tel.Length >= 5)
            {
                resultado = true;
            }
            return resultado;
        }

        public static bool ValidoDir(string dir)
        {
            bool resultado = false;
            if (dir != "")
            {
                resultado = true;
            }
            return resultado;
        }
        
        public enum ErroresAlta
        {
            Ok,
            ErrorEmail,
            ErrorPass,
            ErrorNombre,
            ErrorTel,
            ErrorDir,
            UsuarioRepetido
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return "Email: " + this.Email +
                "\nContraseña: " + this.Pass;
        }
        #endregion
    }
}
