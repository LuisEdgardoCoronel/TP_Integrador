using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class ServoAtascado : IDanioOperador
    {
        public void ProducirDanio(Operador operador)
        {
            //no puede realizar carga y descarga fisica
        }
    }
}
