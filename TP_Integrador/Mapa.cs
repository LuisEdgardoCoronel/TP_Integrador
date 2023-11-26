using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal abstract class Mapa
    {
        protected Nodo[,] matriz;
        protected int[,] distancia;
        /// cola donde se atenderá a cada uno de los nodos del mundo
        protected Queue<Nodo> cola;
        protected List<Cuartel> cuarteles;
        protected List<SitioReciclaje> sitiosReciclaje;

        public Mapa(Nodo[,] matriz, int n, List<Cuartel> cuarteles, List<SitioReciclaje> sReciclaje)
        {
            this.matriz = matriz;
            distancia = new int[n + 2, n + 2];
            cola = new Queue<Nodo>();
            this.cuarteles = cuarteles;
            this.sitiosReciclaje = sReciclaje;
        }

    }
}