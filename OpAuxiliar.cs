using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class OpAuxiliar
    {
        public static int IdStatic = 0;            //Atributo estatico,inicializado en 0
        public int Id { get; set; }
        public int CargaMax { get; set; }
        public int CargaActual { get; set; }
        public EstadoLogicoOp EstadoLogico { get; set; }
        public EstadoFisicoOp EstadoFisico { get; set; }
        public Bateria Bateria { get; set; }
        public Localizacion localizacion { get; set; }
        public Localizacion localizacionCuartel { get; set; }
        public double velocidad { get; set; }

        public string tipo { get; set; }
        public int cantKms { get; set; }
        public int cantEnergiaConsumida { get; set; }
        public int cantCargaTransportada { get; set; }
        public int cantInstrucciones { get; set; }
        public int cantDanios { get; set; }
        public Queue<Localizacion> ultimasUbicVisitadas { get; set; }

        /*
         Bateria Bateria, Localizacion localizacion,
                        Localizacion localizacionCuartel,
        , ,
        string tipo
         */
        [JsonConstructor]
        public OpAuxiliar(int Id, int CargaMax, int CargaActual, EstadoLogicoOp EstadoLogico,
                        EstadoFisicoOp EstadoFisico, Bateria Bateria,Localizacion localizacion, Localizacion localizacionCuartel
                         , double velocidad, string tipo,
                        int cantKms, int cantEnergiaConsumida, int cantCargaTransportada,
                        int cantInstrucciones, int cantDanios, Queue<Localizacion> ultimasUbicVisitadas)
        {
           this.Id = Id;
            this.CargaMax = CargaMax;
            this.CargaActual = CargaActual;
            this.EstadoLogico = EstadoLogico;
            this.EstadoFisico = EstadoFisico;
            this.Bateria = Bateria;
            this.localizacion = localizacion;
            this.localizacionCuartel = localizacionCuartel;
            this.velocidad = velocidad;
            this.tipo = tipo;
            this.cantKms = cantKms;
            this.cantEnergiaConsumida = cantEnergiaConsumida;
            this.cantCargaTransportada = cantCargaTransportada;
            this.cantInstrucciones = cantInstrucciones;
            this.cantDanios = cantDanios;
            this.ultimasUbicVisitadas = ultimasUbicVisitadas;

        }

        public OpAuxiliar(Localizacion localizacion)
        {    //Se crea Operador, recibiendo la localizacion del Cuartel donde se crea

            this.Id = IdStatic;                    //Cada vez que se cree un objeto Operador, se le asigana el Id automaticamente
            IdStatic++;                           //La variable estatica incrementa automaticamente para asignarle otro Id diferente
                                                  //al siguiente Operador que se cree
           this.localizacion = localizacion;             ///Indicamos la localizacion donde se crea el operador
            this.localizacionCuartel = this.localizacion; /// Asignamos ademas la localzacin de su cuartel correspondiente
            this.EstadoLogico = EstadoLogicoOp.BuenEstado;
            this.EstadoFisico = EstadoFisicoOp.BuenEstado;
            this.Bateria = new Bateria(TamañoBateria.grande);

            this.CargaActual = 0;
            this.cantKms = 0;
            this.cantEnergiaConsumida = 0;
            this.cantCargaTransportada = 0;
            this.cantInstrucciones = 0;
            this.cantDanios = 0;
            this.ultimasUbicVisitadas = new Queue<Localizacion>();
//        
           // this.CargaMax = (int)CargaOperador.Media;
            this.velocidad = 100;
            //this.CargaActual = this.CargaMax;

        }

        public void mostrar()
        {
            Console.WriteLine("Id: " + Id);
            Console.WriteLine("carga maxima: " + CargaMax);
            Console.WriteLine("carga actual: " + CargaActual);
            Console.WriteLine("estado logico: " + EstadoLogico);
            Console.WriteLine("estado fisico: " + EstadoFisico);
            Console.WriteLine("velociad: " + velocidad);
            Console.WriteLine("cantidad de km: " + cantKms);
            Console.WriteLine("cantidad de energia consumida: " + cantEnergiaConsumida);
            Console.WriteLine("cantidad de carga transportada: " + cantCargaTransportada);
            Console.WriteLine("cantidad de instrucciones: " + cantInstrucciones);
            Console.WriteLine("cantidad de danios: " + cantDanios);
            Console.WriteLine("tipo: " + tipo);
            Console.WriteLine("Bateria maxima: " +Bateria.bateriaMaxima);
            Console.WriteLine("Localizacion del operador: " + localizacion.filaProperty + " - " + localizacion.columnaProperty);
            Console.WriteLine("cantidad de ultimas localizaciones visitadas: " + ultimasUbicVisitadas.Count());
            Console.WriteLine("Localizacion de su cuartel: " + localizacionCuartel.filaProperty + " - " + localizacionCuartel.columnaProperty);
        }


    }  }