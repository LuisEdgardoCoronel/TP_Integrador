using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class SitioReciclaje : Terreno
    {
        public int fila { get; set; }
        public int columna { get; set; }


        [JsonConstructor]
        public SitioReciclaje(int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
        }

        public Localizacion getLocalizacion()
        {
            return new Localizacion(fila, columna);
        }
    }
}
