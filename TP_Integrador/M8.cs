using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class M8 : Operador
    {
        public M8(String Localizacion) : base(Localizacion)
        {
            this.BateriaMax = 12250;
            this.CargaMax = 250;
            this.velocidad = 110;

        }
    }
}
