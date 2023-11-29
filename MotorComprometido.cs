using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class MotorComprometido : IDanioOperador
    {
        public void ProducirDanio(Operador operador)
        {
            double vel = operador.velocidad;
            operador.velocidad = (vel / 2);
        }
    }
}
