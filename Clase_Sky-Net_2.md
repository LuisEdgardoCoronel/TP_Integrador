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

        private void menuInicial()
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
           

            short opc= 0;
            do
            {
                Console.Clear();
                try { 
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

        private void menuCuartel()
        {
            //cantidad de cuarteles
            List<Cuartel> cuarteles = mundoProperty.cuarteles;
            Console.Clear();

            //seleccionamos el cuartel
            short cuartel;
            do
            {

                Console.WriteLine("Seleccione uno de los " + cuarteles.Count() + " Cuarteles del mundo:\n");

                for (int c = 0; c < cuarteles.Count(); c++)
                {
                    Console.Write($"Cuartel [{c}] en posicion: ");
                    cuarteles.ElementAt(c).mostrarLocalizacion();

                }
                Console.Write("\nSu opcion: ");
                cuartel = short.Parse(Console.ReadLine());

            } while (cuartel < 0 || cuartel >= cuarteles.Count());

            //acceder a un cuartel y las funciones que lista el menu
            short opcionCuartel;
            Console.Clear();
            Console.WriteLine("\nSeleccione la operacion que desea realizar dentro del cuartel:\n");
            Console.WriteLine("1: Ver listado de operadores con su estado logico \n2: Ver listado de operadores con su estado logico en una localizacion\n3: Regresar operadores al cuartel" +
                "\n4: Agregar nuevo operador\n5: Enviar operadores por carga fisica\n6: Retornar operadores dañados\n7: Seleccionar un Operador" +
                "\n8: Remover Operador.\n9: Llamar a todos los operadores con bateria dañada al cuartel para reemplazarla\n10: Volver. \n11: Salir");
            Console.Write("\nSu opcion: ");
            opcionCuartel = short.Parse(Console.ReadLine());
            switch (opcionCuartel)
            {
                case 1:
                    cuarteles.ElementAt(cuartel).estadoLogico();

                    break;
                case 2:
                    try { 
                        Console.Write("Introduzca la coordenada X: ");
                        int x = int.Parse(Console.ReadLine());
                        Console.Write("\nIntroduzca la coordenada Y:");
                        int y = int.Parse(Console.ReadLine());
                        Localizacion localizacion = new Localizacion(x, y);
                        cuarteles.ElementAt(cuartel).estadoLogico(localizacion);
                    } catch (FormatException) 
                    {
                        Console.WriteLine("Error, Ingrese coordenadas numericas");
                    }
                    break;
                case 3:
                    cuarteles.ElementAt(cuartel).recallCuartel();
                    break;
                case 4:
                    cuarteles.ElementAt(cuartel).agregarOperador();
                    break;
                case 5:
                    cuarteles.ElementAt(cuartel).cargayDescargaFisica();
                    break;
                case 6:
                    cuarteles.ElementAt(cuartel).repararOperadores();
                    break;
                case 7:
                    menuOperador(cuarteles.ElementAt(cuartel));
                    break;
                case 8:
                    try { 
                        Console.WriteLine("Ingrese el Id del operador que desea eliminar:");
                        cuarteles.ElementAt(cuartel).mostrarOperadores();
                        Console.Write("\nSu opcion: ");
                        int remover = int.Parse(Console.ReadLine());
                        cuarteles.ElementAt(cuartel).removerOperador(remover);
                    } catch (FormatException e) 
                    {
                        msjErrorID();
                    }
                    break;
                case 9:
                    cuarteles.ElementAt(cuartel).reemplazarBateria();
                    break;
                case 10:
                    menuCuartel();
                    break;
                case 11:
                    menuGuardarMundo();
                    break;
                default:
                    Console.WriteLine("No se seleccionó niguna opcion.");
                    break;
            }
            Console.WriteLine("Pulse una tecla para continuar......");
            Console.ReadKey();
            
        }



        public void menuOperador(Cuartel cuartel)
        {
            //obtener total de operadores
            List<Operador> operadores = cuartel.Operadores;
            int indice, operador = 0;
            if (operadores.Count() > 0)
            {
                do
                {
                    try { 
                        Console.WriteLine("Seleccione el Id del operador al que desea acceder:");
                        for (int c = 0; c < operadores.Count(); c++)
                        {
                            Console.WriteLine($"Operador con Id = [{operadores.ElementAt(c).Id}]");
                        }
                        Console.Write("\nSu opcion: ");
                        indice = short.Parse(Console.ReadLine());
                        operador = cuartel.estaEnLista(indice);
                    } catch (FormatException e) 
                    {
                        msjErrorID();
                    }

                } while (operador > operadores.Count() || operador < 0);

                //acceder a un operador y las funciones que lista el menu
                short opcionOperador;
                Console.WriteLine("\nseleccione la operacion que desea realizar dentro del cuartel:\n");
                Console.WriteLine("1: Enviar operador a una localizacion\n2: Retornar operador al cuartel\n3: Cambiar Operador en StandBy\n4: Reemplazar bateria del operador" +
                                 "\n5: Transferir una carga de batería a otro operador\n6: Transferir una carga Fisica a otro operador\n7: Volver al cuartel y transferir toda la carga física." +
                                 "\n8: Volver al cuartel y cargar batería.\n9: Mostrar Informacion del Operador.\n10: Volver al menu del cuartel");
                opcionOperador = short.Parse(Console.ReadLine());
                switch (opcionOperador)
                {

                    case 1://creo que esto iba en el cuartel
                        try { 
                            Console.Write("Introduzca la coordenada X: ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write("Introduzca la coordenada Y: ");
                            int y = int.Parse(Console.ReadLine());
                            Localizacion localizacion = new Localizacion(x, y);
                            cuartel.enviarOperador(operadores.ElementAt(operador).Id, localizacion);
                        } catch (FormatException e) { 
                            Console.WriteLine("Error, Ingrese coordenadas numericas"); 
                        }
                        break;
                    case 2:
                        cuartel.recallCuartel(operadores.ElementAt(operador).Id);
                        break;
                    case 3:
                        cuartel.standBy(operadores.ElementAt(operador).Id);
                        break;
                    case 4:
                        operadores.ElementAt(operador).ReemplazarBateria();
                        break;
                    case 5:
                            int posicion = -1;
                        try { 
                            Console.Write("Indique el Id del operador al que desea realizar la tranferencia de bateria: ");
                            int id = int.Parse(Console.ReadLine());
                            posicion = cuartel.estaEnLista(id);
                        } catch (FormatException e) 
                        {
                            msjErrorID();
                        }
                        if (posicion != -1)
                        {
                            try { 
                                Console.Write("Indique la cantidad de bateria que desea transferir: ");
                                int cantidad = int.Parse(Console.ReadLine());
                                operadores.ElementAt(operador).transferirBateria(operadores.ElementAt(posicion), cantidad);
                            } catch (FormatException e) 
                            {
                                Console.WriteLine("Error, Ingrese una cantidad numerica.");
                            }
                        }
                        else Console.WriteLine("No se encontro operador con el ID indicado. No se transfirio bateria.");
                        break;
                    case 6:
                        int posicion2 = -1;
                        try { 
                            Console.Write("Indique el Id del operador al que desea realizar la tranferencia de carga fisica: ");
                            int id2 = int.Parse(Console.ReadLine());
                            posicion = cuartel.estaEnLista(id2);
                        } catch (FormatException e) { msjErrorID(); }
                        if (posicion2 != -1)
                        {
                            try { 
                                Console.Write("Indique la cantidad de carga fisica que desea transferir: ");
                                int cantidad = int.Parse(Console.ReadLine());
                                operadores.ElementAt(operador).transferirCarga(operadores.ElementAt(posicion2), cantidad);
                            } catch {
                                Console.WriteLine("Error, ingrese una cantidad numerica");
                            }
                        }
                        else Console.WriteLine("No se encontro operador con el ID indicado. No se transfirio de carga fisica.");
                        break;
                    case 7:
                        cuartel.recallCuartel(operadores.ElementAt(operador).Id);
                        operadores.ElementAt(operador).DescargarEnCuartel();
                        break;
                    case 8:
                        cuartel.recallCuartel(operadores.ElementAt(operador).Id);
                        operadores.ElementAt(operador).cargarBateriaEnCuartel();
                        break;
                    case 9:
                        operadores.ElementAt(operador).mostrarInfodelOperador();
                        break;
                    case 10:
                        menuCuartel();
                        break;
                    default:
                        Console.WriteLine("No se seleccionó niguna opcion.");
                        break;

                }
                Console.WriteLine("Pulse una tecla para continuar......");
                Console.ReadKey();
              
            }

        }






        private void menuGuardarMundo()
        {
            try { 
                short opc;
                Console.WriteLine("Desea guardar el mundo creado? \n1) Si \n2) No");
                opc = short.Parse(Console.ReadLine());
                if (opc == 1) guardarInfo();
                Console.WriteLine("Cerrando programa......");
            } catch {
                Console.WriteLine("Error, Ingrese una de las opciones numericas");
            }
        }


        private void msjErrorID() 
        {
            Console.WriteLine("Error, Por favor ingrese el numero del Id del operador");
        }


        private void guardarInfo()
        {

            string path = Directory.GetCurrentDirectory();
            path += "\\data";
            string filename = "\\archivoPrueba.json";
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
            string filename = "\\archivoPrueba.json";
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
