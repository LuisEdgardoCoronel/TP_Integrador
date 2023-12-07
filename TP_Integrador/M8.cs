using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class M8 : Operador
    {

        [JsonConstructor]
        public M8(int Id, int CargaMax, int CargaActual, EstadoLogicoOp EstadoLogico,
                      EstadoFisicoOp EstadoFisico, Bateria Bateria, Localizacion localizacion,
                      Localizacion localizacionCuartel, double velocidad,
                      TipoOp tipo, int cantKms, int cantEnergiaConsumida, int cantCargaTransportada,
                      int cantInstrucciones, int cantDanios, Queue<Localizacion> ultimasUbicVisitadas) : base
          (Id, CargaMax, CargaActual, EstadoLogico, EstadoFisico, Bateria, localizacion, localizacionCuartel,
                          velocidad, tipo, cantKms, cantEnergiaConsumida, cantCargaTransportada, cantInstrucciones,
                          cantDanios, ultimasUbicVisitadas)
        {

        }

        public M8(Localizacion localizacion) : base(localizacion)
        {
            this.Bateria = new Bateria(TamañoBateria.grande);
            this.CargaMax = (int)CargaOperador.Alta;
            this.velocidad = 110;
            this.tipo = TipoOp.Terrestre;
            this.CargaActual = this.CargaMax;

        }

    }
}
