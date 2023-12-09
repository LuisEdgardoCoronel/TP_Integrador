using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class MenuOperadores
    {
        private Dictionary<short, Action> opciones;
        private List<Operador> operadores;
        private Action sky;
        private int indice, operador = 0;
        public MenuOperadores(Cuartel cuartel) 
        {
            this.operadores = cuartel.Operadores;
                opciones = new Dictionary<short, Action> {
                    {1,()=> {try
                            {
                                Console.Write("Introduzca la coordenada X: ");
                                int x = int.Parse(Console.ReadLine());
                                Console.Write("Introduzca la coordenada Y: ");
                                int y = int.Parse(Console.ReadLine());
                                Localizacion localizacion = new Localizacion(x, y);
                                cuartel.enviarOperador(operadores.ElementAt(operador).Id, localizacion);
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Error, Ingrese coordenadas numericas");
                            } }
                    },
                    {2,()=> cuartel.recallCuartel(operadores.ElementAt(operador).Id)},
                    {3,()=> cuartel.standBy(operadores.ElementAt(operador).Id)},
                    {4,()=> operadores.ElementAt(operador).ReemplazarBateria()},
                    {5,()=> {
                                int posicion = -1;
                                try
                                {
                                    Console.Write("Indique el Id del operador al que desea realizar la tranferencia de bateria: ");
                                    int id = int.Parse(Console.ReadLine());
                                    posicion = cuartel.estaEnLista(id);
                                }
                                catch (FormatException e)
                                {
                                    msjErrorID();
                                }
                                if (posicion != -1)
                                {
                                    try
                                    {
                                        Console.Write("Indique la cantidad de bateria que desea transferir: ");
                                        int cantidad = int.Parse(Console.ReadLine());
                                        operadores.ElementAt(operador).transferirBateria(operadores.ElementAt(posicion), cantidad);
                                    }
                                    catch (FormatException e)
                                    {
                                        Console.WriteLine("Error, Ingrese una cantidad numerica.");
                                    }
                                }
                                else Console.WriteLine("No se encontro operador con el ID indicado. No se transfirio bateria.");
                            }
                    },
                    {6,()=> {
                                int posicion2 = -1;
                                try
                                {
                                    Console.Write("Indique el Id del operador al que desea realizar la tranferencia de carga fisica: ");
                                    int id2 = int.Parse(Console.ReadLine());
                                    posicion2 = cuartel.estaEnLista(id2);
                                }
                                catch (FormatException) { msjErrorID(); }
                                if (posicion2 != -1)
                                {
                                    try
                                    {
                                        Console.Write("Indique la cantidad de carga fisica que desea transferir: ");
                                        int cantidad = int.Parse(Console.ReadLine());
                                        operadores.ElementAt(operador).transferirCarga(operadores.ElementAt(posicion2), cantidad);
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Error, ingrese una cantidad numerica");
                                    }
                                }
                                else Console.WriteLine("No se encontro operador con el ID indicado. No se transfirio de carga fisica.");
                            }
                    },
                    {7,()=> {
                            cuartel.recallCuartel(operadores.ElementAt(operador).Id);
                            operadores.ElementAt(operador).DescargarEnCuartel();
                            }
                    },
                    {8,()=> {
                            cuartel.recallCuartel(operadores.ElementAt(operador).Id);
                            operadores.ElementAt(operador).cargarBateriaEnCuartel();
                            }
                    },
                    {9,()=> operadores.ElementAt(operador).mostrarInfodelOperador()},
                    {10,()=> {
                            sky = new SkyNet().menuInicial; 
                            sky(); 
                             } 
                    }
                };
            
            if (operadores.Count > 0)
            {
                mostrarOperadores();

                mostrarOpciones();

            }
        }

        private void mostrarOpciones()
        {
            //acceder a un operador y las funciones que lista el menu
            short opcionOperador;
            Console.WriteLine("\nseleccione la operacion que desea realizar dentro del cuartel:\n");
            Console.WriteLine("1: Enviar operador a una localizacion\n2: Retornar operador al cuartel\n3: Cambiar Operador en StandBy\n4: Reemplazar bateria del operador" +
                             "\n5: Transferir una carga de batería a otro operador\n6: Transferir una carga Fisica a otro operador\n7: Volver al cuartel y transferir toda la carga física." +
                             "\n8: Volver al cuartel y cargar batería.\n9: Mostrar Informacion del Operador.\n10: Volver al menu del cuartel");
            opcionOperador = short.Parse(Console.ReadLine());



            try
            {
                Action action = opciones[opcionOperador];
                action();
            }
            catch (Exception)
            {
                Console.WriteLine("No se seleccionó niguna opcion..");
            }
            
        }




        private void mostrarOperadores() 
        {
            do
            {
                try
                {
                    Console.WriteLine("Seleccione el Id del operador al que desea acceder:");
                    for (int c = 0; c < operadores.Count; c++)
                    {
                        Console.WriteLine($"Operador con Id = [{operadores.ElementAt(c).Id}]");
                    }
                    Console.Write("\nSu opcion: ");
                    indice = short.Parse(Console.ReadLine());
                    //operador = cuartel.estaEnLista(indice);
                }
                catch (FormatException)
                {
                    msjErrorID();
                }

            } while (operador > operadores.Count || operador < 0);
        }



        private void msjErrorID()
        {
            Console.WriteLine("Error, Por favor ingrese el numero del Id del operador");
        }



    }
}
