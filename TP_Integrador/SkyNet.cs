using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class SkyNet
    {
        public Mundi mundoProperty { get; set; }


        public SkyNet()
        {
            menuInicial();
        }

        public void menuInicial()
        {
            short opcion;

            try
            {
                do
                {
                    Console.WriteLine("Seleccione una de las opciones para iniciar el programa: \n1: Crear un nuevo mundo.\n2: Cargar un mundo existe. ");
                    Console.Write("\nSu opcion: ");

                    opcion = short.Parse(Console.ReadLine());
                    if (opcion < 1 || opcion > 2)
                    {
                        Console.WriteLine("Opcion erronea. Ingrese una opcion valida.");
                    }

                } while (opcion < 1 || opcion > 2);

                if (opcion == 1)
                {
                    Console.Clear();
                    this.mundoProperty = new Mundi();
                    asignarMapasACuarteles();

                }
                else
                {
                    leerInfo(); //funcion para desearealizar y leer de base de datos
                    asignarMapasACuarteles();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ingrese un valor correcto.");
                Console.WriteLine("Pulse una tecla para continuar.....");
                Console.ReadKey();
                Console.Clear();
                menuInicial();


            }


            short opc = 0;
            do
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Desea realizar una Operacion? " +
                                     "\n1) Si \n2) Cerrar Programa.  ");
                    Console.Write("\nSu opcion: ");
                    opc = short.Parse(Console.ReadLine());
                    if (opc == 1) menuCuartel();
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Error, por favor ingrese una de las dos opciones numericas.");
                    opc = 1;
                }

            } while (opc == 1);
            menuGuardarMundo();
        }




        private void asignarMapasACuarteles()
        {
            MapaAereo mapaAereo = new MapaAereo(mundoProperty.getMatrizNodo(), mundoProperty.cuarteles, mundoProperty.sitiosReciclaje, mundoProperty.vertederos);
            MapaTerrestre mapaTerrestre = new MapaTerrestre(mundoProperty.getMatrizNodo(), mundoProperty.cuarteles, mundoProperty.sitiosReciclaje, mundoProperty.vertederos);
            foreach (Cuartel c in mundoProperty.cuarteles)
            {
                c.asignarMapas(mapaTerrestre, mapaAereo);
            }
        }





        public void menuCuartel()
        {
           List<Cuartel> cuarteles = mundoProperty.cuarteles;
            MenuCuartel menu = new MenuCuartel(cuarteles);
            
            Console.Clear();
        }






        public void menuGuardarMundo()
        {
            try
            {
                short opc;
                Console.WriteLine("Desea guardar el mundo creado? \n1) Si \n2) No");
                opc = short.Parse(Console.ReadLine());
                if (opc == 1) guardarInfo();
                Console.WriteLine("Cerrando programa......");
            }
            catch
            {
                Console.WriteLine("Error, Ingrese una de las opciones numericas");
            }
        }


        


        private void guardarInfo()
        {

            string path = Directory.GetCurrentDirectory();
            path += "\\data";
            string filename = "\\skynet.json";
            Directory.CreateDirectory(path);
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // Hacer que la serialización sea insensible a mayúsculas y minúsculas
                                                    // Otras opciones...
            };
            string data = JsonSerializer.Serialize(mundoProperty, options);

            File.WriteAllText(path + filename, data);
        }









        private void leerInfo()
        {
            string path = Directory.GetCurrentDirectory();
            path += "\\data";
            string filename = "\\skynet.json";
            string data = File.ReadAllText(path + filename);
            try
            {
                mundoProperty = JsonSerializer.Deserialize<Mundi>(data);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error al leer el archivo." + e.ToString());

            }
        }


    }




}