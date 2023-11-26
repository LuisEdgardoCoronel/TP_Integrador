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
<<<<<<< Updated upstream:TP_Integrador/Class1.cs
            operador.CambiarEstadoBateria(EstadoBateria.Perforada);
            
=======

            operador.GetBateria().SetEstadoBateria(EstadoBateria.Perforada);

>>>>>>> Stashed changes:TP_Integrador/BateriaPerforada.cs
        }
    }
}
