﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class UAV : Operador
    {
       public UAV (String Localizacion) : base(Localizacion)
        {
            this.BateriaMax = 4000;
            this.CargaMax = 5;
            this.velocidad = 80;
            
        }


    }
    }

