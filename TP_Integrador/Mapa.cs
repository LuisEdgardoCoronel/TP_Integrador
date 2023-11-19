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
        protected Queue<Nodo> cola;

        protected List<Cuartel> cuarteles;
        protected List<SitioReciclaje> sitiosReciclaje;

        public Mapa(Mundi mundo)
        {
            this.matriz = mundo.getMatrizNodos();
            distancia = new int[mundo.getTamanioMundo() + 2, mundo.getTamanioMundo() + 2];
            cola = new Queue<Nodo>();
            cuarteles = mundo.getCuarteles();
            sitiosReciclaje = mundo.GetSitioReciclajes();
        }

    }
}
