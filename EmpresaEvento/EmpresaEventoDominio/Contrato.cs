﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEventoDominio
{
    public class Contrato
    {
        #region Atributos
        private Servicio servicio;
        private int cantPersonass;
        #endregion

        #region Propiedades
        public Servicio Servicio
        {
            get
            {
                return servicio;
            }

            set
            {
                servicio = value;
            }
        }

        public int CantPersonass
        {
            get
            {
                return cantPersonass;
            }

            set
            {
                cantPersonass = value;
            }
        }
        #endregion

        #region Constructor
        public Contrato(Servicio servicio, int cantPersonas)
        {
            this.Servicio = servicio;
            this.CantPersonass = cantPersonass;
        }
        #endregion
    }
}

