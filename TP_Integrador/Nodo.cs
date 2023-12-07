using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class Nodo
    {
        public int fila { get; set; }
        public int columna { get; set; }

        /// tipo de Vertic, 'I'->Punto de inicio(partida), 'S'->Punto final(Salida), 'P'->No se puede pasar,  'X'->CAMINO VALIDO
        public Terreno tipo { get; set; }

        /// Indica si el nodo ya ha sido visitado
        [JsonIgnore]
        public bool visitado { get; set; }

        /// Nodo anterior por el cual se debe seguir a la siguiente ruta

        [JsonIgnore]
        public Nodo anterior { get; set; }


        [JsonConstructor]
        public Nodo(int fila, int columna, Terreno tipo)
        {
            this.fila = fila;
            this.columna = columna;
            this.tipo = tipo;
            this.visitado = false;
            this.anterior = null;
        }

        public Nodo()
        {

        }

    }

     

}
