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

        public int filaProperty { get; private set; }
        public int columnaProperty { get; private set; }
        // probar
        public string nombreProperty { get; private set; }


        public Localizacion(int filaProperty, int columnaProperty, string nombreProperty)
        {
            this.columnaProperty = columnaProperty;
            this.filaProperty = filaProperty;
            this.nombreProperty = nombreProperty;
        }


        public Localizacion(int f, int c) {
        //    this.fila = f;
        //    this.columna = c;
            this.columnaProperty = c;
            this.filaProperty = f;
            this.nombreProperty = "ubicacion [" + f + "][" + c +"]";
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
