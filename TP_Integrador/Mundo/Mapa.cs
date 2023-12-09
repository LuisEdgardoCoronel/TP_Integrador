using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal abstract class Mapa
    {
        public Nodo[,] matriz { get; set; }

        public Queue<Nodo> cola { get; set; }
        public List<Cuartel> cuarteles { get; set; }
        public List<SitioReciclaje> sitiosReciclaje { get; set; }
        public List<Localizacion> vertederos { get; set; }

        protected int[] dx { get; set; } = { -1, 0, 1, 0 };

        /// dy es la variacion en la fila para los vertices adyacentes, recuerde que ambos, tanto dy y dx combinados en la misma posicion forman el vertice adyacente aumentandole el valor
        /// almacenado en el vector
        protected int[] dy { get; set; } = { 0, -1, 0, 1 };

        /// inicio es el vertice de partida
        /// fin es el vertice objetivo o salida
        protected Nodo inicio { get; set; }
        protected Nodo fin { get; set; }



        public Mapa(Nodo[,] matriz, List<Cuartel> cuarteles, List<SitioReciclaje> sReciclaje, List<Localizacion> vertederos)
        {
            this.matriz = matriz;
            cola = new Queue<Nodo>();
            this.cuarteles = cuarteles;
            this.sitiosReciclaje = sReciclaje;
            this.vertederos = vertederos;
        }

        protected void setInicio(int i, int j) //setea la localizacion de partida
        {
            matriz[i, j].fila = i;
            matriz[i, j].columna = j;
            this.inicio = matriz[i, j];
        }
        protected void setFin(int i, int j)  //setea la localizacion de destino
        {
            matriz[i, j].fila = i;
            matriz[i, j].columna = j;
            this.fin = matriz[i, j];
        }
    }
}
