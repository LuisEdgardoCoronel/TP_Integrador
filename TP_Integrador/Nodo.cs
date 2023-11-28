using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class Nodo
    {
        private int fila;
        private int columna;

        /// tipo de Vertic, 'I'->Punto de inicio(partida), 'S'->Punto final(Salida), 'P'->No se puede pasar,  'X'->CAMINO VALIDO
        private Terreno tipo;

        /// Indica si el nodo ya ha sido visitado

        private bool visitado;

        /// Nodo anterior por el cual se debe seguir a la siguiente ruta

        private Nodo? anterior;

        public int getFila()
        {
            return fila;
        }

        public void setFila(int fila)
        {
            this.fila = fila;
        }

        public int getColumna()
        {
            return columna;
        }

        public void setColumna(int columna)
        {
            this.columna = columna;
        }

        public Terreno getTipo()
        {
            return tipo;
        }

        public void setTipo(Terreno terreno)
        {
            this.tipo = terreno;
        }

        public bool isVisitado()
        {
            return visitado;
        }

        public void setVisitado(bool visitado)
        {
            this.visitado = visitado;
        }

        public Nodo? getAnterior()
        {
            return anterior;
        }

        public void setAnterior(Nodo n)
        {
            this.anterior = n;
        }

    }

     

}
