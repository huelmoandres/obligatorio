﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEventoDominio
{
    public abstract class Evento
    {
        #region Atributos
        private static int contador = 0;
        private int id;
        private DateTime fecha;
        private byte turno;
        private string descripcion;
        private string cliente;
        private int cantAsistentes;
        private List<Contrato> contratos = new List<Contrato>();
        #endregion

        #region Propiedades
        public int Id
        {
            get
            {
                return id;
            }
        }

        public DateTime Fecha
        {
            get
            {
                return fecha;
            }

            set
            {
                fecha = value;
            }
        }

        public byte Turno
        {
            get
            {
                return turno;
            }

            set
            {
                turno = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public string Cliente
        {
            get
            {
                return cliente;
            }

            set
            {
                cliente = value;
            }
        }

        public List<Contrato> Contratos
        {
            get
            {
                return contratos;
            }
            set
            {
                contratos = value;
            }
        }

        public int CantAsistentes
        {
            get
            {
                return cantAsistentes;
            }

            set
            {
                cantAsistentes = value;
            }
        }
        #endregion

        #region Constructor
        public Evento(DateTime fecha, byte turno, string descripcion, string cliente, int cantAsistentes, Contrato c)
        {
            this.Fecha = fecha;
            this.Turno = turno;
            this.Descripcion = descripcion;
            this.Cliente = cliente;
            this.CantAsistentes = cantAsistentes;
            this.id = contador + 1;
            contador++;
            contratos.Add(c);
        }
        #endregion

        #region Validaciones

        public static bool ValidoFecha(DateTime fecha)
        {
            return fecha >= DateTime.Today;
        }

        public static bool ValidTurno(byte turno)
        {
            bool resultado = false;
            if (turno >= 1 && turno <= 3)
            {
                resultado = true;
            }
            return resultado;
        }

        public static bool ValidoVacio(string campo)
        {
            return campo != "";
        }

        public enum ErroresAlta{
            Ok,
            ErrorFecha,
            ErrorTurno,
            ErrorVacio,
            ErrorDuracion,
            ErrorAsistentes,
            FechaRepetida,
            ServicioPersonas
        }
        #endregion
    }
}
