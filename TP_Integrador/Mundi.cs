using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class Mundi
    {
        private const int max = 1000; /// valor maximo del mundo 

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

        /// Constructor 

        public Mundi()
        {
            matriz = new Nodo[max, max];
            distancia = new int[max, max];
            cola = new Queue<Nodo>();

        }

       



        public void cargarMundi()
        {
            Console.WriteLine("Introduzca el tamaño del Mundo");
            n = int.Parse(Console.ReadLine());


            ///iniciamos en el indice 1, dejando el indice 0 para hacer consultas en las adyacencias
            Random randy = new Random();

            for (int i = 1; i <= n; i++)
            {

                char tipo;
                //recorremos cada carecter de la cadena leida
                for (int j = 1; j <= n; j++)
                {
                    int rand = randy.Next(0, 2);
                    Nodo nodo = new Nodo();
                    if (rand == 0) tipo = 'X';
                    else tipo = 'P';
                    nodo.setTipo(tipo);
                    nodo.setFila(i);
                    nodo.setColumna(j);
                    nodo.setVisitado(false);

                    matriz[i, j] = nodo;
                }
            }

        }



        public Stack<Nodo> devolverCamino() {
            

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
            matriz[i, j].setTipo('I');
            this.inicio = matriz[i, j];
        }
        private void setFin(int i, int j)
        {
            matriz[i, j].setFila(i);
            matriz[i, j].setColumna(j);
            matriz[i, j].setTipo('S');
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
                    if (nodoAdy != null && !nodoAdy.isVisitado() && (nodoAdy.getTipo() == 'X' || nodoAdy.getTipo() == 'S'))
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
                        if (nodoAdy.getTipo() == 'S')
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
