using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
//el cuartel va a tener los mapas aereos y terrestres, y en cada operacion que invlucre moverse a un operador, se le
// a preguntar por el tipo, para mandarle el mapa correspondiente

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



        public void estadoLogico()                      //Muestra el estado actual de todos los operadores del cuartel
        {
            if (this.Operadores.Count() != 0)
            {
                for (int i = 0; i < this.Operadores.Count; i++)
                {
                    Operador op = this.Operadores[i];
                    Console.WriteLine($"Operador {op.GetType().Name} con Id: [{op.Id}]. Estado = {op.EstadoLogico}");
                }
            }
            else Console.WriteLine("No hay Operadores en la lista.");

        }

        public void estadoLogico(Localizacion localizacion)  //Muestra el estado actual de los operadores que se encuentran en una det. localizacion
        {
            for (int i = 0; i < this.Operadores.Count; i++)
            {
                Operador op = this.Operadores[i];
                if (op.localizacion.equals(localizacion))
                {
                    Console.WriteLine($"Operador {op.GetType().Name} con Id: [{op.Id}]. Estado = {op.EstadoLogico}");
                }
            }
        }

        public void recallCuartel()              //Llama a todos los operadores al cuartel
        {
            for (int i = 0; i < this.Operadores.Count(); i++)
            {
                Operador op = this.Operadores[i];
                if (op.tipo == "Terrestre")
                    op.volverAlCuartel(this.mapaTerrestre);
                else op.volverAlCuartel(this.mapaAereo);
            }
        }


        public void recallCuartel(int Id)   //Llama a un operador especifico al cuartel
        {
            int posicion = estaEnLista(Id);

            if (posicion >= 0)
            {
                if (this.Operadores[posicion].tipo == "Terrestre")
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
                if (this.Operadores[posicion].tipo == "Terrestre")
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
                case 1:
                    op = new UAV(this.localizacion);  ///Recibe la localizacion del cuartel. Idem para los demas y su tipo determinaod
                    break;
                case 2:
                    op = new M8(this.localizacion);
                    break;
                case 3:
                    op = new K9(this.localizacion);
                    break;
                default:
                    Console.WriteLine("Error. No se eligio tipo de Operador.");
                    break;

            }
            Console.WriteLine("Se creo el operador!");

            return op;

        }


        private short tipoOperador()   //Metodo privado encargado de determinar el tipo de Operador que se desea crear
        {
            short tipo = 0;

            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Ingrese que tipo de operador desea crear: " +
                                 "\n 1 - UAV" +
                                 "\n 2 - M8" +
                                 "\n 3 - K9");
                    Console.Write("\nSu opcion: ");
                    tipo = short.Parse(Console.ReadLine());
                    if (tipo < 1 || tipo > 3)
                        Console.WriteLine("Tipo de operador erroneo. Ingrese nuevamente una opcion valida.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ingrese un valor correcto.");
                    Console.WriteLine("Pulse una tecla para continuar.....");
                    Console.ReadKey();
                    Console.Clear();

                }

            } while (tipo < 1 || tipo > 3);

            return tipo;

        }

        public int estaEnLista(int Id)            //Metodo privado utilizado para determinar si un Operador se encuentra en la lista
        {                                          //del cuartel, si se encuentra: devuelve la posicion de dicho Operador
            int posicion = -1;                     //caso contrario devuelve -1

            for (int i = 0; i < this.Operadores.Count(); i++)
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
                Console.WriteLine("Operador eliminado!");
            }
            else
            {
                Console.WriteLine("No se encontro Operador con Id [" + Id + "]");
            }

        }


        public void mostrarOperadores() //muestra Id de los operadores de la lista del cuartel
        {
            foreach (Operador op in this.Operadores)
            {
                Console.WriteLine("Operador "+ op.GetType().Name + " con Id = [" + op.Id + "]");
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
                    if (op.tipo == "Terreste")
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
                    if (op.tipo == "Terreste")
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
                    if (op.tipo == "Terreste")
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
