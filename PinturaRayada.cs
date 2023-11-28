using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class PinturaRayada : IDanioOperador
    {
        public void ProducirDanio(Operador operador)
        {
            Console.WriteLine($"El operador {operador} sufrió rayaduras en su pintura");
        }
    }
}