using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class M8 : Operador
    {
        public M8(String Localizacion) : base(Localizacion, TamañoBateria.grande, CargaOperador.Alta)
        {

            
            this.velocidad = 110;

        }
    }
}
