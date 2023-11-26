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
<<<<<<< Updated upstream
            operador.CambiarEstadoBateria(EstadoBateria.PuertoDesconectado);
=======
           
            operador.GetBateria().SetEstadoBateria(EstadoBateria.PuertoDesconectado);
>>>>>>> Stashed changes

        }
    }
}
