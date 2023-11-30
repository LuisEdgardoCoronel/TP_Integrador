using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TP_Integrador
{
    internal class DanioOperador
    {
        private static List<IDanioOperador> Danios;

        static DanioOperador()
        {
            Danios = new List<IDanioOperador>
            {
                new MotorComprometido(),
                new ServoAtascado(),
                new BateriaPerforada(),
                new PuertoDesconectado(),
                new PinturaRayada()
            };
        }


        public static IDanioOperador DanioAleatorio()
        {
            Random randon = new();
            int indice = randon.Next(Danios.Count);
            return Danios[indice];
        }
    }
}