using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class K9 : Operador
    {
        public K9(String Localizacion) : base(Localizacion)
        {
            this.BateriaMax = 6500;
            this.CargaMax = 40;
            this.velocidad = 100;

        }
    }
}
