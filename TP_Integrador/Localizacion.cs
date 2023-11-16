using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class Localizacion
    {
        private int fila,columna;

        public Localizacion(int f, int c) {
            this.fila = f;
            this.columna = c;
        }

         public int getFila() { return fila; }

         public void setFila(int fila)
        {
            this.fila = fila;
        }
        public int getColumna() { return columna;}

        public void setColumna(int columna)
        {
            this.columna = columna;
        }

        public bool equals(Localizacion local2) ///metodo nuevo para comparar localizaciones
        {
            return (this.fila == local2.getFila() && this.columna == local2.getColumna());
        }



    }
}
