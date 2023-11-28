﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class Prueba
    {
        private Mundi mundo;

        public Mundi mundoProperty { get; private set; }

        
        public List<Cuartel> CuartelesProperty { get; private set; }

 

        public Prueba()
        {
            mundo = new Mundi();
            CuartelesProperty = this.mundo.cuarteles;
            CuartelesProperty.ElementAt(0).agregarOperador();
            CuartelesProperty.ElementAt(1).agregarOperador();
            CuartelesProperty.ElementAt(2).agregarOperador();
            CuartelesProperty.ElementAt(2).agregarOperador();

        }


        public void mostrarInfoCuarteles()
        {
            Console.WriteLine("Mostrando info de cuarteles");
            if(this.mundo.cuarteles != null)
            {
                Console.WriteLine(this.mundo.cuarteles.Count());
                foreach (Cuartel c in mundo.cuarteles)
                {
                    Console.WriteLine("Cuartel en posicion[" + c.localizacion.filaProperty + "] [" + c.localizacion.columnaProperty + "]");
                    c.mostraroOperadores();
                }
        
            }
        }

        public void guardarInfo()
        {

            string path = Directory.GetCurrentDirectory(); 
            path += "\\data";
            string filename = "\\archivoPrueba.json";
            Directory.CreateDirectory(path);
            string data = JsonSerializer.Serialize(CuartelesProperty);

            File.WriteAllText(path + filename, data);
        }

        public void leerInfo()
        {
            string path = Directory.GetCurrentDirectory();
            path += "\\data";
            string filename = "\\archivoPrueba.json";
            string data = File.ReadAllText(path + filename);
            List<Cuartel> cuarteles;
            try
            {
                cuarteles = JsonSerializer.Deserialize<List<Cuartel>>(data);
                
                foreach (Cuartel c in cuarteles)
                {
                    Console.WriteLine("Cuartel en posicion[" + c.localizacion.filaProperty + "] [" + c.localizacion.columnaProperty + "]");
                    //c.mostraroOperadores();
                }

            }
            catch( Exception e)
            {
                Console.WriteLine("Error al leer el archivo." + e);
                
            }   
        }


    }

   

  
}
