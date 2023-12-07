using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class PuertoDesconectado : IDanioOperador
    {
        public void ProducirDanio(Operador operador)
        {

            operador.CambiarEstadoBateria(EstadoBateria.PuertoDesconectado);
            Console.WriteLine($"El operador {operador} sufrió daños: Puerto desconectado");
        }
    }
}
