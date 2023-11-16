using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class Mundi
    {
      

        /// El atributo matriz representa el laberinto  por medio de Vertices que en realidad le corresponden a la clase Nodo

        private Nodo[,] matriz;

        /// Matriz de distancias

        private int[,] distancia;

        /// cola donde se atenderá a cada uno de los nodos del laberinto

        private Queue<Nodo> cola;

        /// se asume que los nodos adyacentes a un vertice son 4, y estan dados por el que esta arriba(Norte), abajo(Sur), este(Derecha), oeste(izquieda)
        /// <b>dx</b> es la variacion en la columna

        private int[] dx = { -1, 0, 1, 0 };

        /// dy es la variacion en la fila para los vertices adyacentes, recuerde que ambos, tanto dy y dx combinados en la misma posicion forman el vertice adyacente aumentandole el valor
        /// almacenado en el vector

        private int[] dy = { 0, -1, 0, 1 };

        /// inicio es el vertice de partida
        /// fin es el vertice objetivo o salida

        private Nodo inicio, fin;

        /// n es el tamaño del mundo

        private int n;


        private List<Cuartel> cuarteles;

        private List<SitioReciclaje> sitiosReciclaje;
        /// Constructor 

        public Mundi()
        {
            Console.WriteLine("Introduzca el tamaño del Mundo");
            n = int.Parse(Console.ReadLine());
            matriz = new Nodo[n+2, n+2]; //se suma +2,para permitir realiazr consultas adyacentes y no desborde la matriz
            distancia = new int[n+2, n+2];//idem anterior caso
            cola = new Queue<Nodo>();
            cuarteles = new List<Cuartel>();
            sitiosReciclaje = new List<SitioReciclaje> ();

        }

       



        public void cargarMundi()
        {
            
            ///iniciamos en el indice 1, dejando el indice 0 para hacer consultas en las adyacencias
         

            for (int i = 1; i <= n; i++)
            {

                char tipo;
                //recorremos cada carecter de la cadena leida
                for (int j = 1; j <= n; j++)
                {
                    Nodo nodo = new Nodo();
                    Terreno tipoTerreno = terrenoRandom(i,j);
                    nodo.setTipo(tipoTerreno);
                    nodo.setFila(i);
                    nodo.setColumna(j);
                    nodo.setVisitado(false);

                    this.matriz[i, j] = nodo;
                }
            }

        }

        private Terreno terrenoRandom(int i, int j)  //devuelve una clase de terreno determinado de manera aleatori
        {                                            //recibe ademas 2 enteros, i y j, por si se debe crear un cuartel o sitiosreciclaje
            Random rnd = new Random();
            Terreno terreno;

            do
            {
                int tipo = rnd.Next(0, 9);
                terreno = creaTerreno(tipo,i,j);

            } while ((terreno is Cuartel && this.cuarteles.Count()>=3)||(terreno is SitioReciclaje && this.sitiosReciclaje.Count()>=5));

            if (terreno is Cuartel) this.cuarteles.Add((Cuartel)terreno);
            if (terreno is SitioReciclaje) this.sitiosReciclaje.Add((SitioReciclaje)terreno);
            return terreno;
        }

        private Terreno creaTerreno(int tipo,int i,int j) //metodo para modularizar la manera de crear un tipo de terreno a partir de 
        {                                                 //un numero random y 2 enteros necesarios para crear cuartel y sitioreciclaje
            Terreno terreno;
            switch (tipo)
            {
                case 0: terreno = new Baldio();
                    break;
                case 1: terreno = new Planicie();
                    break;
                case 2: terreno = new Bosque();
                    break;
                case 3: terreno = new Urbano();
                    break;
                case 4: terreno = new Vertedero();
                    break;
                case 5: terreno = new Lago();
                    break;
                case 6: terreno = new VertederoElectronico();
                    break;
                case 7: terreno = new Cuartel(i,j);
                    break;
                case 8: terreno = new SitioReciclaje(i,j);
                    break;
                    default: return null;
            } 

            return terreno;
        }


        public Stack<Nodo> devolverCamino(int inicioF, int inicioC, int finF, int finC) {

            setInicio(inicioF, inicioC);
            setFin(finF, finC);

            recorrido();
            Stack<Nodo> pila = new Stack<Nodo>();

            Nodo actual = fin;
            pila.Push(actual);
            while (actual != null)
            {

                actual = actual.getAnterior();
                if (actual != null)
                {
                    pila.Push(actual);
                }
            }

            return pila;
        }

        private void setInicio(int i, int j)
        {
            matriz[i, j].setFila(i);
            matriz[i, j].setColumna(j);
            this.inicio = matriz[i, j];
        }
        private void setFin(int i, int j)
        {
            matriz[i, j].setFila(i);
            matriz[i, j].setColumna(j);
            this.fin = matriz[i, j];
        }

        private void recorrido()
        {
            

            //iniciamos la distnaica del nodo inicio en cero
            distancia[inicio.getFila(), inicio.getColumna()] = 0;

            //marcamos el nodo como visitado
            matriz[inicio.getFila(), inicio.getColumna()].setVisitado(true);

            //ingresamo el nodo en la cola
            cola.Enqueue(inicio);

            //mientras que la cola no este vacia
            while (cola.Count != 0)
            {
                //sacamos un nodo de la cola (el primero)
                Nodo nodoActual = cola.Dequeue();
                //recorremos los nodos adyacentes al nodoActualS
                //se recorren 4 posibles nodos adycentes, arriba, abajo, derecha e izquierda
                for (int i = 0; i < 4; i++)
                {
                    //dy y dx nos ayudan a solamente sumar +1,-1 o 0 tanto en fila o columna para los cuatro vertices adycentes
                    Nodo nodoAdy = matriz[nodoActual.getFila() + dy[i], nodoActual.getColumna() + dx[i]];
                    // si el nodo no es null , no ha sido visitado... si es un camino valido o es la salida
                    if (nodoAdy != null && !nodoAdy.isVisitado() && (nodoAdy.getTipo() == 'X' || nodoAdy == this.fin))
                    {
                        //marcamos como visitado

                        nodoAdy.setVisitado(true);
                        //incrementamos su distancia
                        distancia[nodoAdy.getFila(), nodoAdy.getColumna()] = distancia[nodoActual.getFila(), nodoActual.getColumna()] + 1;
                        //guardamos la referencia a su nodo anterior para trazar la ruta
                        nodoAdy.setAnterior(nodoActual);
                        //ingresamo el nuevo nodo descubierto a la cola
                        cola.Enqueue(nodoAdy);

                        //si hemos llegado a la salida o al objetivo... no terminamos de recorrer TODO el grafo, y la paramos ahi
                        if (nodoAdy == this.fin)
                        {

                            break;
                        }
                    }
                }
            }
        }

        public void imprimirMundi()
        {
            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine();
                for (int j = 1; j <= n; j++)
                {
                    Console.Write((matriz[i, j]).getTipo());
                }
            }

        }

    }
}
