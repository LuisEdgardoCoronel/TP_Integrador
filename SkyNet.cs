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
        public Mundi mundoProperty { get;  set; }


        public SkyNet()
        {
            mundoProperty = new Mundi();
           
        }





        public void guardarInfo()
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
            string data = JsonSerializer.Serialize(mundoProperty,options);

            File.WriteAllText(path + filename, data);
        }

        public void leerInfo()
        {
            string path = Directory.GetCurrentDirectory();
            path += "\\data";
            string filename = "\\archivoPrueba.json";
            string data = File.ReadAllText(path + filename);
            try
            {
                  mundoProperty = JsonSerializer.Deserialize<Mundi>(data);

            }
            catch( Exception e)
            {
                Console.WriteLine("Error al leer el archivo." + e.ToString());
                
            }   
        }


    }

   

  
}
