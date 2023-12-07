using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class MapaTerrestre : Mapa
    {
        public MapaTerrestre(Nodo[,] matriz, List<Cuartel> cuarteles, List<SitioReciclaje> sReciclaje, List<Localizacion> vertederos) : base(matriz, cuarteles, sReciclaje, vertederos)
        {

        }

        public Stack<Nodo> devolverCaminoDirecto(Localizacion ubiInicial, Localizacion ubiFinal)
        {


            setInicio(ubiInicial.filaProperty, ubiInicial.columnaProperty);
            setFin(ubiFinal.filaProperty, ubiFinal.columnaProperty);

            recorridoDirecto();
            Stack<Nodo> pila = new Stack<Nodo>();

            Nodo actual = fin;
            pila.Push(actual);
            while (actual != null)
            {

                actual = actual.anterior;
                if (actual != null)
                {
                    pila.Push(actual);
                }
            }

            return pila;
        }

        private void recorridoDirecto()
        {


            //iniciamos la distnaica del nodo inicio en cero
            //  distancia[inicio.fila, inicio.columna] = 0;

            //marcamos el nodo como visitado
            matriz[inicio.fila, inicio.columna].visitado = (true);

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
                    Nodo nodoAdy = matriz[nodoActual.fila + dy[i], nodoActual.columna + dx[i]];
                    // si el nodo no es null , no ha sido visitado... si es un camino valido o es la salida
                    if (nodoAdy != null && !nodoAdy.visitado && (!(nodoAdy.tipo is Lago) || nodoAdy == this.fin)) //si es lago no es camino valido
                    {
                        //marcamos como visitado

                        nodoAdy.visitado = (true);
                        //incrementamos su distancia
                        //  distancia[nodoAdy.fila, nodoAdy.columna] = distancia[nodoActual.fila, nodoActual.columna] + 1;
                        //guardamos la referencia a su nodo anterior para trazar la ruta
                        nodoAdy.anterior = nodoActual;
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

        public Stack<Nodo> devolverCaminoOptimo(Localizacion ubiInicial, Localizacion ubiFinal)
        {

            setInicio(ubiInicial.filaProperty, ubiInicial.columnaProperty);
            setFin(ubiFinal.filaProperty, ubiFinal.columnaProperty);

            recorridoOptimo();
            Stack<Nodo> pila = new Stack<Nodo>();

            Nodo actual = fin;
            pila.Push(actual);
            while (actual != null)
            {

                actual = actual.anterior;
                if (actual != null)
                {
                    pila.Push(actual);
                }
            }

            return pila;
        }

        private void recorridoOptimo()
        {


            //iniciamos la distnaica del nodo inicio en cero
            // distancia[inicio.fila, inicio.columna] = 0;

            //marcamos el nodo como visitado
            matriz[inicio.fila, inicio.columna].visitado = (true);

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
                    Nodo nodoAdy = matriz[nodoActual.fila + dy[i], nodoActual.columna + dx[i]];
                    // si el nodo no es null , no ha sido visitado... si es un camino valido o es la salida
                    if (nodoAdy != null && !nodoAdy.visitado && (caminoValido(nodoAdy.tipo) || nodoAdy == this.fin))
                    {
                        //marcamos como visitado

                        nodoAdy.visitado = (true);
                        //incrementamos su distancia
                        //  distancia[nodoAdy.fila, nodoAdy.columna] = distancia[nodoActual.fila, nodoActual.columna] + 1;
                        //guardamos la referencia a su nodo anterior para trazar la ruta
                        nodoAdy.anterior = nodoActual;
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

        private bool caminoValido(Terreno terreno) //para determinar si el camino es valido sin peligros para el operador terrestre
        {
            if (!(terreno is Vertedero) && !(terreno is VertederoElectronico) && !(terreno is Lago)) return true;
            else return false;
        }


    }
}
