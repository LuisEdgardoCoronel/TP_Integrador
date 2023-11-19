using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class M8 : Operador
    {
        
        public M8(Localizacion localizacion,MapaTerrestre mapa,String tipo) : base(localizacion)
        {
            this.Bateria = new Bateria(TamañoBateria.grande);
            this.CargaMax = (int)CargaOperador.Alta;
            this.velocidad = 110;
            this.mapa = mapa;
            this.tipo = tipo;
        }
        
    }
}
