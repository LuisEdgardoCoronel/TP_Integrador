using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class BateriaPerforada : IDanioOperador
    {
        public void ProducirDanio(Operador operador)
        {
           
            operador.Bateria.estado =EstadoBateria.Perforada;

        }
    }
}
