using System;
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
        public List<Operador> Operadores { get; set; }
        public Localizacion localizacion { get; set; }

        private MapaTerrestre mapaTerrestre { get; set; }
        private MapaAereo mapaAereo { get; set; }


        [JsonConstructor]
        public Cuartel(List<Operador> Operadores, Localizacion localizacion)
        {
            this.Operadores = Operadores;
            this.localizacion = localizacion;
        }



        public Cuartel(int fila, int columna)
        {
            this.localizacion = new Localizacion(fila, columna);
            this.Operadores = new List<Operador>();
        }



        public void asignarMapas(MapaTerrestre mapaT, MapaAereo mapaA)
        {
            this.mapaTerrestre = mapaT;
            this.mapaAereo = mapaA;
        }



        public void estadoLogico()                      //Muestra el estado logico actual de todos los operadores del cuartel
        {
            if (this.Operadores.Count() != 0)
            {
                foreach (Operador op in this.Operadores)
                {
                    Console.WriteLine($"Operador {op.GetType().Name} con Id: [{op.Id}]. Estado = {op.EstadoLogico}");
                }
            }
            else Console.WriteLine("No hay Operadores en la lista.");
        }



        public void estadoLogico(Localizacion localizacion)  //Muestra el estado actual de los operadores que se encuentran en una det. localizacion
        {
            foreach (Operador op in this.Operadores)
            {
                if (op.localizacion.equals(localizacion))
                {
                    Console.WriteLine($"Operador {op.GetType().Name} con Id: [{op.Id}]. Estado = {op.EstadoLogico}");
                }
            }
        }




        public void recallCuartel()              //Llama a todos los operadores al cuartel
        {
            foreach (Operador op in this.Operadores)
            {   
                if (op.tipo == TipoOp.Terrestre)
                op.volverAlCuartel(this.mapaTerrestre);
                else op.volverAlCuartel(this.mapaAereo);
            }
        }





        public void recallCuartel(int Id)   //Llama a un operador especifico al cuartel
        {
            int posicion = estaEnLista(Id);

            if (posicion >= 0)
            {
                if (this.Operadores[posicion].tipo == TipoOp.Terrestre)
                    this.Operadores[posicion].volverAlCuartel(this.mapaTerrestre);
                else this.Operadores[posicion].volverAlCuartel(this.mapaAereo);
            }
            else Console.WriteLine("No se encontro Operador con Id [" + Id + "]");
        }





        public void enviarOperador(int Id, Localizacion localizacion)   //Envia a un operador especifico a una det. localizacion
        {
            int posicion = estaEnLista(Id);

            if (posicion >= 0)
            {
                if (this.Operadores[posicion].tipo == TipoOp.Terrestre)
                    this.Operadores[posicion].moverse(localizacion, this.mapaTerrestre);
                else this.Operadores[posicion].moverse(localizacion, this.mapaAereo);

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
            Dictionary<short, Func<Localizacion, Operador>> operadores = new Dictionary<short, Func<Localizacion, Operador>>()
            {
                {0, (localizacion) => new M8(localizacion)},
                {1, (localizacion) => new UAV(localizacion)},
                {2, (localizacion) => new K9(localizacion)}
            }; 

            short opcion = -1;
            do
            {
                Console.WriteLine("Elija el operador");
                try
                {
                    Console.WriteLine("0: M8\n1: UAV\n2: K9");
                    opcion = short.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("La respuesta debe ser numerica");
                }
            } while (opcion < 0 || opcion >= operadores.Count);

            Func<Localizacion, Operador> expresion = operadores[opcion];
            Localizacion localizacionOp = new Localizacion(this.localizacion.filaProperty,this.localizacion.columnaProperty);
            Operador operador = expresion(localizacionOp);
            this.Operadores.Add(operador);
            Console.WriteLine("El operador fue creado con exito!");

        }



        public int estaEnLista(int Id)            //si un Operador se encuentra en la lista
        {                                          //devuelve la posicion de dicho Operador
            return this.Operadores.FindIndex(x => x.Id == Id);//si no esta devuelve -1
        }





        public void removerOperador(int Id)             //Elimina un Operador especifico pasado por Id de la lista de Operadores del Cuartel
        {
            int cantidad = this.Operadores.RemoveAll(x => x.Id == Id);
            if (cantidad > 0) Console.WriteLine("Operador eliminado!");
            else Console.WriteLine("No se encontró Operador con Id [" + Id + "]");
        }





        public void mostrarOperadores() //muestra Id de los operadores de la lista del cuartel
        {
            foreach (Operador op in this.Operadores)
            {
                Console.WriteLine("Operador " + op.GetType().Name + " con Id = [" + op.Id + "]");
            }
        }


        // NUEVAS FUNCIONALIDADES PARTE 2



        public void cargayDescargaFisica()
        {
            foreach (Operador op in this.Operadores)
            {
                if (op.CargaActual < op.CargaMax) //operadores que no tengan su carga maxima van a moverse al vertedero
                {                                 //mas cercano para recoger su carga maxima y luego ir al sitio
                                                  //de reciclaje mas cercano para su descarga
                    if (op.tipo == TipoOp.Terrestre)
                    {
                        op.moverseVertederoCercano(mapaTerrestre);
                        op.moverseSitioReciclajeCercano(mapaTerrestre);
                    }
                    else
                    {
                        op.moverseVertederoCercano(mapaAereo);
                        op.moverseSitioReciclajeCercano(mapaAereo);
                    }

                }
            }
        }





        public void repararOperadores()           // va a llamar a todos los operadores a us respectivo cuartel
        {                                        //para que sean reparados, algunos podrian no llegar al cuartel por su bateria 
            foreach (Operador op in this.Operadores)
            {
                if (op.EstadoFisico != EstadoFisicoOp.BuenEstado)
                {
                    if (op.tipo == TipoOp.Terrestre)
                    {
                        op.volverAlCuartel(this.mapaTerrestre);

                    }
                    else
                    {
                        op.volverAlCuartel(this.mapaAereo);
                    }
                    if (op.localizacion.equals(op.localizacionCuartel))
                        op.RepararOperador();
                    else Console.WriteLine("No se pudo reparar. El operador no pudo llegar a su cuartel.");
                }
            }

        }




        public void reemplazarBateria()
        {
            foreach (Operador op in this.Operadores)
            {
                if (op.Bateria.estado != EstadoBateria.BuenEstado)
                {
                    if (op.tipo == TipoOp.Terrestre)
                    {
                        op.volverAlCuartel(this.mapaTerrestre);
                    }
                    else
                    {
                        op.volverAlCuartel(this.mapaAereo);
                    }
                    if (op.localizacion.equals(op.localizacionCuartel))
                        op.ReemplazarBateria();
                    else Console.WriteLine("No se pudo reemplazar bateria. El operador no pudo llegar a su cuartel.");

                }
            }
        }





        public void mostrarLocalizacion() //muestra localizacion del cuartel
        {
            Console.WriteLine($"[{localizacion.filaProperty} - {localizacion.columnaProperty}]");
        }

    }
}
