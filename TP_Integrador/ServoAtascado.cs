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
            operador.EstadoFisico = EstadoFisicoOp.ServoAtascado;
            Console.WriteLine($"El operador {operador} sufrió daños: Servo Atascado");
        }
    }
}
