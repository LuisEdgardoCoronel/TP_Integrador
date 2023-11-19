using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class K9 : Operador
    { 
       
        public K9(Localizacion localizacion,MapaTerrestre mapa,String tipo) : base(localizacion)
        {
            this.Bateria = new Bateria(TamañoBateria.mediana);
            this.CargaMax = (int)CargaOperador.Media;
            this.velocidad = 100;
            this.mapa = mapa; ///Se le da un mapa a nuestro operador
            this.tipo = tipo;
        }
    }
}
