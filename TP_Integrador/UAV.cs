using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class UAV : Operador
    {
       public UAV (String Localizacion) : base(Localizacion, TamañoBateria.pequeña)
        {
            
            this.CargaMax = 5;
            this.velocidad = 80;
            
        }


    }
    }

