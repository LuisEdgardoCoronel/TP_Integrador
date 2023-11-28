using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class SitioReciclaje : Terreno
    {
        private int fila, columna;
        public SitioReciclaje(int f, int c) { 
            this.fila = f;
            this.columna = c;
        }

        public Localizacion getLocalizacion()
        {
            return new Localizacion(fila,columna);
        }

    }
}
