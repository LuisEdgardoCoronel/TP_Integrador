using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class MapaAereo : Mapa
    {
        private int[] dx = { -1, 0, 1, 0 };
        private int[] dy = { 0, -1, 0, 1 };
        private Nodo inicio, fin;

        public MapaAereo(Mundi mundo) : base(mundo)
        {

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

        public Stack<Nodo> devolverCaminoDirecto(Localizacion ubiInicial, Localizacion ubiFinal)
        {

            setInicio(ubiInicial.getFila(), ubiInicial.getColumna());
            setFin(ubiFinal.getFila(), ubiFinal.getColumna());

            recorridoDirecto();
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




        private void recorridoDirecto()
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
                    if (nodoAdy != null && !nodoAdy.isVisitado() && (nodoAdy == this.fin)) //todos los caminos son validos para los aereos
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

        public Stack<Nodo> devolverCaminoOptimo(Localizacion ubiInicial, Localizacion ubiFinal)
        {

            setInicio(ubiInicial.getFila(), ubiInicial.getColumna());
            setFin(ubiFinal.getFila(), ubiFinal.getColumna());

            recorridoOptimo();
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

        private void recorridoOptimo()
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
                    if (nodoAdy != null && !nodoAdy.isVisitado() && (caminoValido(nodoAdy.getTipo()) || nodoAdy == this.fin))
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

        private bool caminoValido(Terreno terreno) //para determinar si el camino es valido sin peligros para el operador terrestre
        {
            if (!(terreno is Vertedero) && !(terreno is VertederoElectronico)) return true;
            else return false;
        }


    }
}
