﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class Cuartel : Terreno
    {
       public List<Operador> Operadores { get;  set; }
        public Localizacion localizacion { get;  set; }

     
        public MapaTerrestre mapaTerrestre { get;  set; }
 
        public MapaAereo mapaAereo { get;  set; }



        [JsonConstructor]
        public Cuartel (Localizacion localizacion, List<Operador> Operadores, MapaTerrestre mapaTerrestre, MapaAereo mapaAereo)
        {
            this.localizacion = localizacion;
            this.Operadores = Operadores;
            this.mapaAereo = mapaAereo;
            this.mapaTerrestre = mapaTerrestre;
        }



        public Cuartel(int fila,int columna) {       // Constructor
          this.localizacion = new Localizacion(fila,columna);
          this.Operadores = new List<Operador>();
           

        }

     public void asignarMapas(MapaTerrestre mT, MapaAereo mA)   //Le damos un mapa del mundo a nuestro cuartel, al momento de crear el mundo, se le asigna 
        {                                                      //a cada cuartel un mapa
            this.mapaTerrestre = mT;
            this.mapaAereo = mA;
        }
      

       

      public void estadoLogico()                      //Muestra el estado actual de todos los operadores del cuartel
        {
            for(int i = 0; i < this.Operadores.Count; i++)
            {
                Operador op = this.Operadores[i];
                Console.WriteLine($"Operador con Id: [{op.Id}]. Estado = {op.EstadoLogico}");
            }
        }

      public void estadoLogico(Localizacion localizacion)  //Muestra el estado actual de los operadores que se encuentran en una det. localizacion
        {
              for (int i = 0;i < this.Operadores.Count;i++)
            {
                Operador op = this.Operadores[i];
                if (op.localizacion.equals(localizacion)) ///Modificado comparacion de localizaciones
                {
                    Console.WriteLine($"Operador con Id: [{op.Id}]. Estado = {op.EstadoLogico}");
                }
            }
        }
        
     public void recallCuartel()              //Llama a todos los operadores al cuartel
        {
            for (int i = 0; i < this.Operadores.Count; i++)
            {
                Operador op = this.Operadores[i];
                op.volverAlCuartel(); 
            }
        }

        
        public void recallCuartel(int Id)   //Llama a un operador especifico al cuartel
        {
            int posicion = estaEnLista(Id);

            if (posicion >= 0)
            {
                this.Operadores[posicion].volverAlCuartel();
            }
            else Console.WriteLine("No se encontro Operador con Id ["+Id+"]");
        }

        public void enviarOperador(int Id, Localizacion localizacion)   //Envia a un operador especifico a una det. localizacion
        {                                                                         ///Recibe  el Id del operador, la ubiacion a mandaro y el mundo donde esta
            int posicion = estaEnLista(Id);

            if (posicion >= 0)
            {

                this.Operadores[posicion].moverse(localizacion);  ///Indicamos al operador que se mueva, el sera el encargado de trazar en us mapa la ruta


            }                                                        
            else
            {
                Console.WriteLine("No se encontro Operador con Id [" + Id + "]");
            }

        }

        public void standBy(int Id)                 //Establece el estado de un operador especificado por su Id, en StandBy
        {
            int posicion = estaEnLista(Id);

            if (posicion >= 0)
            {
                this.Operadores[posicion].EstadoLogico = EstadoLogicoOp.StandBy;
            }
            else
            {
                Console.WriteLine("No se encontro Operador con Id [" + Id + "]");
            }

        }



        public void agregarOperador()                     //Crea y agrega un nuevo operador a la lista de operadores del Cuartel
        {
            Operador op = cargarOperador();
            if (op != null)
            {
 
                this.Operadores.Add(op);
            }
            else Console.WriteLine("No se pudo crear el operador.");
        }

        
        private Operador cargarOperador()                //Metodo privado encargado de crear un nuevo Operador
        {
            Operador op = null;                                   
                                                                 
            short tipo = tipoOperador();
           

            switch (tipo)
            {
                case 1: op = new UAV(this.localizacion,mapaAereo,"Aereo");  ///Recibe la localizacion del cuartel. Idem para los demas
                    break;                                      
                case 2: op = new M8(this.localizacion,mapaTerrestre,"Terrestre");  ///Indicamos ademas el tipo de operador
                    break;
                case 3: op = new K9(this.localizacion, mapaTerrestre,"Terrestre");
                    break;
                default: Console.WriteLine("Error. No se eligio tipo de Operador.");
                    break;
            }

            return op; 

        }


        private short tipoOperador()                 //Metodo privado encargado de determinar el tipo de Operador que se desea crear
        {
            short tipo = 0;
            Console.WriteLine("Ingrese que tipo de operador desea crear: " +
                               "\n 1 - UAV" + 
                               "\n 2 - M8" +
                               "\n 3 - K9");
            tipo = short.Parse(Console.ReadLine());
            return tipo;
                        
        }

        private int estaEnLista(int Id)            //Metodo privado utilizado para determinar si un Operador se encuentra en la lista
        {                                          //del cuartel, si se encuentra: devuelve la posicion de dicho Operador
            int posicion = -1;                     //caso contrario devuelve -1

            for(int i = 0; i<this.Operadores.Count(); i++)
            {
                if (this.Operadores[i].Id == Id) posicion = i; 
            }
            return posicion;
        }
      
        public void removerOperador(int Id)             //Elimina un Operador especifico pasado por Id de la lista de Operadores del Cuartel
        {
            int posicion = estaEnLista(Id);

            if (posicion >= 0)
            {
                this.Operadores.RemoveAt(posicion);
            }
            else
            {
                Console.WriteLine("No se encontro Operador con Id [" + Id + "]");
            }

        }

       
        public void mostraroOperadores()
        {
            foreach(Operador op in this.Operadores)
            {
                Console.WriteLine("Operador " + op.Id);
            }
        }


        // NUEVAS FUNCIONALIDADES PARTE 2

        /*  Orden general: Todos los operadores que no estén ocupados
             actualmente deben dirigirse al vertedero más cercano y recoger su
             cantidad máxima de carga para traer al sitio de reciclaje más cercano.*/

        public void cargayDescargaFisica()
        {
            foreach(Operador op in this.Operadores)
            {
                if(op.CargaActual < op.CargaMax)
                {
                    op.moverseVertederoCercano();
                    op.moverseSitioReciclajeCercano();
                }
            }
        }

        public void repararOperadores()
        {
            foreach(Operador op in this.Operadores)
            {
                if(op.EstadoFisico != EstadoFisicoOp.BuenEstado)
                {
                    op.volverAlCuartel();
                    op.RepararOperador();
                }
            }

        }

        public void reemplazarBateria()
        {
            foreach (Operador op in this.Operadores)
            {
                if (op.Bateria.estado!=EstadoBateria.BuenEstado)
                {
                    op.volverAlCuartel();
                    op.ReemplazarBateria();
                }
            }
        }

    }
}
