using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class UAV : Operador
    {
       public UAV (Localizacion localizacion) : base(localizacion)
        {
            this.Bateria = new Bateria(TamañoBateria.pequeña);
            this.CargaMax = (int)CargaOperador.Baja;
            this.velocidad = 80;
            
        }

       
    }
    }
