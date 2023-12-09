using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class MenuCuartel
    {
        private List<Cuartel> cuarteles;
        private short cuartel;
        Dictionary<short, Action> acciones;
        private Action sky;
        private MenuOperadores menuOp;

        public MenuCuartel(List<Cuartel> cuarteles) 
        {
            this.cuarteles = cuarteles;
             acciones = new Dictionary<short, Action>()
            {
                {1, () => cuarteles.ElementAt(cuartel).estadoLogico()},
                {2, () =>{
                                try
                                {
                                    Console.Write("Introduzca la coordenada X: ");
                                    int x = int.Parse(Console.ReadLine());
                                    Console.Write("\nIntroduzca la coordenada Y:");
                                    int y = int.Parse(Console.ReadLine());
                                    Localizacion localizacion = new Localizacion(x, y);
                                    cuarteles.ElementAt(cuartel).estadoLogico(localizacion);
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Error, Ingrese coordenadas numericas");
                                }
                            }
                },
                {3, () => cuarteles.ElementAt(cuartel).recallCuartel()},
                {4, () => cuarteles.ElementAt(cuartel).agregarOperador()},
                {5, () => cuarteles.ElementAt(cuartel).cargayDescargaFisica()},
                {6, () => cuarteles.ElementAt(cuartel).repararOperadores()},
                {7, () => menuOp = new MenuOperadores(cuarteles.ElementAt(cuartel))},
                {8, () =>   {
                                try
                                {
                                    Console.WriteLine("Ingrese el Id del operador que desea eliminar:");
                                    cuarteles.ElementAt(cuartel).mostrarOperadores();
                                    Console.Write("\nSu opcion: ");
                                    int remover = int.Parse(Console.ReadLine());
                                    cuarteles.ElementAt(cuartel).removerOperador(remover);
                                }
                                catch (FormatException)
                                {
                                    msjErrorID();
                                }
                            }
                },
                {9, () => cuarteles.ElementAt(cuartel).reemplazarBateria()},
                {10, () => {
                    sky = new SkyNet().menuCuartel;
                    sky();
                    } 
                 },
                {11, () => {
                    sky = new SkyNet().menuGuardarMundo;
                    sky();
                    } 
                 }
            };

            mostrarCuarteles();
            mostrarOpciones();
            
        }





        private void mostrarCuarteles()
        {
            do
            {
                try
                {
                    Console.WriteLine("Seleccione uno de los " + cuarteles.Count + " Cuarteles del mundo:\n");
                    for (int c = 0; c < cuarteles.Count; c++)
                    {
                        Console.Write($"Cuartel [{c}] en posicion: ");
                        cuarteles.ElementAt(c).mostrarLocalizacion();
                    }
                    Console.Write("\nSu opcion: ");
                    cuartel = short.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error, seleccione el numero del cuartel al que desea ingresar");
                    throw;
                }

            } while (cuartel < 0 || cuartel >= cuarteles.Count);
        }







        private void mostrarOpciones()
        {
            short opcionCuartel;
            Console.Clear();
            Console.WriteLine("\nSeleccione la operacion que desea realizar dentro del cuartel:\n");
            Console.WriteLine("1: Ver listado de operadores con su estado logico \n2: Ver listado de operadores con su estado logico en una localizacion\n3: Regresar operadores al cuartel" +
                "\n4: Agregar nuevo operador\n5: Enviar operadores por carga fisica\n6: Retornar operadores dañados\n7: Seleccionar un Operador" +
                "\n8: Remover Operador.\n9: Llamar a todos los operadores con bateria dañada al cuartel para reemplazarla\n10: Volver. \n11: Salir");
            Console.Write("\nSu opcion: ");
            opcionCuartel = short.Parse(Console.ReadLine());


            
            // Intentar obtener y ejecutar la acción
            try
            {
                Action accion = this.acciones[opcionCuartel];
                accion();
            }
            // Capturar la excepción si la opción no es válida
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Opción inválida. Intente de nuevo.");
            }
            Console.WriteLine("Pulse una tecla para continuar......");
            Console.ReadKey();
        }




        private void msjErrorID()
        {
            Console.WriteLine("Error, Por favor ingrese el numero del Id del operador");
        }

    }
}
