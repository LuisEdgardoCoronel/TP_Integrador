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

        /// n es el tamaño del mundo

        private int n;

        private List<Cuartel> cuarteles;


        private List<SitioReciclaje> sitiosReciclaje;
        /// Constructor 

        public Mundi()
        {
            Console.WriteLine("Introduzca el tamaño del Mundo");
            this.n = int.Parse(Console.ReadLine());
            matriz = new Nodo[n+2, n+2]; //se suma +2,para permitir realiazr consultas adyacentes y no desborde la matriz     
            cuarteles = new List<Cuartel>();
            sitiosReciclaje = new List<SitioReciclaje> ();

        }

       



        public void cargarMundi()
        {
            
            ///iniciamos en el indice 1, dejando el indice 0 para hacer consultas en las adyacencias
         

            for (int i = 1; i <= n; i++)
            {

               
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

            MapaTerrestre mapaT = new MapaTerrestre(this.matriz, this.n, this.cuarteles, this.sitiosReciclaje); /// Creamos un mapa a partir de nuestro mundo terrestre y otro aereo
            MapaAereo mapaA = new MapaAereo(this.matriz, this.n, this.cuarteles, this.sitiosReciclaje);
            foreach (Cuartel c in this.cuarteles)
            {
                c.asignarMapas(mapaT,mapaA); //a cada cuartel le damos un mapa del mundo
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

        public Nodo[,] getMatrizNodos()
        {
            return this.matriz;
        }

        public int getTamanioMundo()
        {
            return this.n;
        }

        public List<Cuartel> getCuarteles()
        {
            return this.cuarteles;
        }

        public List<SitioReciclaje> GetSitioReciclajes()
        {
            return this.sitiosReciclaje;
        }

    }
}
