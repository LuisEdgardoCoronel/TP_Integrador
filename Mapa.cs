using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal abstract class Mapa
    {
        protected Nodo[,] matriz { get; set; }
        public List<Nodo> terrenos { get; set; }
      //  public int[,] distancia { get; set; }
        /// cola donde se atenderá a cada uno de los nodos del mundo
        public Queue<Nodo> cola { get; set; }
        public List<Cuartel> cuarteles { get; set; }
        public List<SitioReciclaje> sitiosReciclaje { get; set; }
        public List<Localizacion> vertederos { get; set; }

        [JsonConstructor]
        public Mapa(List<Nodo> terrenos, int[,] distancia, Queue<Nodo> cola, List<Cuartel> cuarteles, List<SitioReciclaje> sitiosReciclaje, List<Localizacion> vertederos)
        {
            this.terrenos = terrenos;
            this.matriz = Convert.convertListToMatrice(terrenos);
           // this.distancia = distancia;
            this.cola = cola;
            this.cuarteles = cuarteles;
            this.sitiosReciclaje = sitiosReciclaje;
            this.vertederos = vertederos;
        }

        public Mapa(Nodo[,] matriz, int n , List<Cuartel> cuarteles, List<SitioReciclaje> sReciclaje, List<Localizacion> vertederos)
        {
            this.matriz = matriz;
           // distancia = new int[n + 2, n+ 2];
            cola = new Queue<Nodo>();
            this.cuarteles = cuarteles;
            this.sitiosReciclaje = sReciclaje;
            this.vertederos = vertederos;
        }

    }
}
