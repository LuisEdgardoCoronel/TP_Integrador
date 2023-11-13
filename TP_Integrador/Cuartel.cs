using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class Cuartel
    {
       private List<Operador> Operadores;
       private String localizacion;

   
      public Cuartel(String localizacion) {       // Constructor
          this.localizacion = localizacion;
          this.Operadores = new List<Operador>();
        }
      

      public void estado()                      //Muestra el estado actual de todos los operadores del cuartel
        {
            for(int i = 0; i < this.Operadores.Count; i++)
            {
                Operador op = this.Operadores[i];
                Console.WriteLine($"Operador con Id: [{op.getId}]. Estado = {op.getEstado}");
            }
        }

      public void estado (String Localizacion)  //Muestra el estado actual de los operadores que se encuentran en una det. localizacion
        {
              for (int i = 0;i < this.Operadores.Count;i++)
            {
                Operador op = this.Operadores[i];
                if (op.getLocalizacion().CompareTo(Localizacion) ==0)
                {
                    Console.WriteLine($"Operador con Id: [{op.getId}]. Estado = {op.getEstado}");
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

        public void enviarOperador(int Id, String localizacion)   //Envia a un operador especifico a una det. localizacion
        {
            int posicion = estaEnLista(Id);

            if (posicion >= 0)
            {
                this.Operadores[posicion].moverse(localizacion);
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
                this.Operadores[posicion].setEstado(EstadoOperador.StandBy);
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
                op.setCuartel(this.localizacion);
                this.Operadores.Add(op);
            }
            else Console.WriteLine("No se pudo crear el operador.");
        }

        public void agregarOperador(Operador op)          //Agrega un operador existente a las lista de operadores del Cuartel
        {
            op.setCuartel(this.localizacion);
            this.Operadores.Add(op);
        }

        private Operador cargarOperador()                //Metodo privado encargado de crear un nuevo Operador
        {
            Operador op = null;
            String localizacion;
            short tipo = tipoOperador();
            Console.WriteLine("Indique la localizacion actual del Operador: ");
            localizacion = Console.ReadLine();

            switch (tipo)
            {
                case 1: op = new UAV(localizacion);
                    break;
                case 2: op = new M8(localizacion);
                    break;
                case 3: op = new K9(localizacion);
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
                if (this.Operadores[i].getId() == Id) posicion = i; 
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







    }
}
