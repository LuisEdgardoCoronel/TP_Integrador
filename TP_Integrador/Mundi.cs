using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class Mundi
    {


        /// El atributo matriz representa el laberinto  por medio de Vertices que en realidad le corresponden a la clase Nodo

        private Nodo[,] matriz { get; set; }

        public List<Nodo> terrenos { get; set; }  // almacena los datos de matriz para poder serializar

        public int n { get; set; } //n es el tamaño del mundo

        public List<Cuartel> cuarteles { get; set; } //lista de cuarteles del mundo

        public List<SitioReciclaje> sitiosReciclaje { get; set; } //lista de sitios de reciclaje del mundo
        [JsonIgnore]
        public List<Localizacion> vertederos { get; set; } //lista de vertederos del mundo (se decidio guardar las
                                                           //localizaciones de los vertederos para facilitar la
                                                           //ejecucion de ciertos metodos)


        [JsonConstructor]
        public Mundi(List<Nodo> terrenos, int n, List<Cuartel> cuarteles, List<SitioReciclaje> sitiosReciclaje, List<Localizacion> vertederos)
        {
            this.n = n;
            this.terrenos = terrenos;
            this.cuarteles = cuarteles;
            this.sitiosReciclaje = sitiosReciclaje;
            this.vertederos = crearListaVertederos();
            this.matriz = Convert.convertListToMatrice(terrenos);
        }

        public Mundi()
        {
            Console.WriteLine("Introduzca el tamaño del Mundo");
            this.n = int.Parse(Console.ReadLine());
            matriz = new Nodo[n + 2, n + 2]; //se suma +2,para permitir realiazr consultas adyacentes y no desborde la matriz     
            cuarteles = new List<Cuartel>();
            sitiosReciclaje = new List<SitioReciclaje>();
            cargarMundi();
            terrenos = Convert.convertMatriceToList(this.matriz);
            vertederos = crearListaVertederos();
        }

        public void cargarMundi()
        {

            ///iniciamos en el indice 1, dejando el indice 0 para hacer consultas en las adyacencias


            for (int i = 1; i <= n; i++)
            {

                for (int j = 1; j <= n; j++)
                {
                    Nodo nodo = new Nodo();
                    Terreno tipoTerreno = terrenoRandom(i, j);
                    nodo.tipo = tipoTerreno;
                    nodo.fila = i;
                    nodo.columna = j;
                    nodo.visitado = false;

                    this.matriz[i, j] = nodo;

                }
            }

            cambiarTerreno(3, 5); // luego de cargar el terreno, se lo limpia para crear un cuartel(como maximo 3)
                                  // o un sitio de reciclaje (como maximo 5).

        }

        private Terreno terrenoRandom(int i, int j)  //devuelve una clase de terreno determinado de manera aleatori
        {                                            //recibe ademas 2 enteros, i y j, por si se debe crear un cuartel o sitiosreciclaje
            Random rnd = new Random();
            Terreno terreno;

            int tipo = rnd.Next(0, 7);
            terreno = creaTerreno(tipo, i, j);

            if (terreno is Vertedero) this.vertederos.Add(new Localizacion(i, j));

            return terreno;
        }


        private Terreno creaTerreno(int tipo, int i, int j) //metodo para modularizar la manera de crear un tipo de terreno a partir de 
        {                                                 //un numero random y 2 enteros necesarios para crear cuartel y sitioreciclaje
            Terreno terreno;
            switch (tipo)
            {
                case 0:
                    terreno = new Baldio();
                    break;
                case 1:
                    terreno = new Planicie();
                    break;
                case 2:
                    terreno = new Bosque();
                    break;
                case 3:
                    terreno = new Urbano();
                    break;
                case 4:
                    terreno = new Vertedero();
                    break;
                case 5:
                    terreno = new Lago();
                    break;
                case 6:
                    terreno = new VertederoElectronico();
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
                    Console.Write((matriz[i, j]).GetType().Name);
                }
            }

        }

        private void cambiarTerreno(int cantidadMaxCuartel, int cantidadMaxReciclaje) //cambia el terreno por un cuartel
                                                                                      // o por un sitio de reciclaje
        {

            crearCuartel();
            crearSitioReciclaje();
            Random rnd = new Random();

            for (int i = 0; i < cantidadMaxCuartel - 1; i++)
            {

                int posbilidad = rnd.Next(0, 2);

                if (posbilidad == 0)
                {
                    crearCuartel();

                }
            }

            for (int i = 0; i < cantidadMaxReciclaje - 1; i++)
            {

                int posbilidad = rnd.Next(0, 2);

                if (posbilidad == 0)
                {
                    crearSitioReciclaje();

                }
            }
        }

        private void crearCuartel()
        {
            Random rnd = new Random();
            int randomI = rnd.Next(1, this.n + 1);
            int randomJ = rnd.Next(1, this.n + 1);
            Terreno terreno = new Cuartel(randomI, randomJ);
            this.matriz[randomI, randomJ].tipo = terreno;
            this.cuarteles.Add((Cuartel)terreno);
        }

        private void crearSitioReciclaje()
        {
            Random rnd = new Random();
            int randomI = rnd.Next(1, this.n + 1);
            int randomJ = rnd.Next(1, this.n + 1);
            Terreno terreno = new SitioReciclaje(randomI, randomJ);
            this.matriz[randomI, randomJ].tipo = terreno;
            this.sitiosReciclaje.Add((SitioReciclaje)terreno);
        }

        public Nodo[,] getMatrizNodo()
        {
            return this.matriz;
        }



        private List<Localizacion> crearListaVertederos()
        {
            List<Localizacion> listaVertederos = new List<Localizacion>();

            foreach (Nodo nodo in this.terrenos)
            {
                if (nodo.tipo is Vertedero)
                {
                    listaVertederos.Add(new Localizacion(nodo.fila, nodo.columna));
                }
            }

            return listaVertederos;

        }
    }
}
