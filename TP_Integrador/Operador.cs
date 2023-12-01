using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class Operador
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
        public double cantEnergiaConsumida { get; set; }
        public int cantCargaTransportada { get; set; }
        public int cantInstrucciones { get; set; }
        public int cantDanios { get; set; }
        public Queue<Localizacion> ultimasUbicVisitadas { get; set; }

        [JsonConstructor]
        public Operador(int Id, int CargaMax, int CargaActual, EstadoLogicoOp EstadoLogico,
                        EstadoFisicoOp EstadoFisico, Bateria Bateria, Localizacion localizacion,
                        Localizacion localizacionCuartel, double velocidad,
                        string tipo, int cantKms, double cantEnergiaConsumida, int cantCargaTransportada,
                        int cantInstrucciones, int cantDanios, Queue<Localizacion> ultimasUbicVisitadas)
        {
            this.Id = Id;
            IdStatic = Id + 1;
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

        public Operador(Localizacion localizacion)
        {    //Se crea Operador, recibiendo la localizacion del Cuartel donde se crea

            this.Id = IdStatic;                    //Cada vez que se cree un objeto Operador, se le asigana el Id automaticamente
            IdStatic++;                          //La variable estatica incrementa automaticamente para asignarle otro Id diferente
                                                 //al siguiente Operador que se cree
            this.localizacion = localizacion;
            this.localizacionCuartel = this.localizacion; // Asignamos ademas la localizacion de su cuartel correspondiente
            this.EstadoLogico = EstadoLogicoOp.BuenEstado;
            this.EstadoFisico = EstadoFisicoOp.BuenEstado;
            this.CargaActual = 0;
            this.cantKms = 0;
            this.cantEnergiaConsumida = 0;
            this.cantCargaTransportada = 0;
            this.cantInstrucciones = 0;
            this.cantDanios = 0;
            this.ultimasUbicVisitadas = new Queue<Localizacion>();

        }

        /*
      * ------------------------------------------
      *         funciones movimiento
      * ------------------------------------------
      */

        public void moverse(Localizacion localizacion, Mapa mapa)
        {
            if (this.EstadoLogico != EstadoLogicoOp.StandBy)
            {
                bool continuarCamino = true;
                Stack<Nodo> camino = determinaCamino(localizacion, mapa); /// Aqui ya tendriamos la pila de los nodos Camino

                if (camino.Count() != 0 && localizacion != null)
                {
                    //////////////////////////////////////////////////////////////////////////////////////////////
                    ///
                    this.cantInstrucciones++;
                    while (camino.Count() != 0 && continuarCamino)
                    {


                        if (this.Bateria.bateriaActual > 1000 / velocidad)
                        {
                            Nodo nodo = camino.Pop();

                            this.localizacion.filaProperty = nodo.fila;
                            this.localizacion.columnaProperty = nodo.columna;
                            this.cantKms++;

                            //bateria
                            Bateria.DescargaPorMovimiento(this.velocidad);//descarga cada vez que se mueve
                            cantEnergiaConsumida += (this.Bateria.bateriaMaxima - (1000 / this.velocidad));

                            cargarUltimaLocalizacion(new Localizacion(nodo.fila, nodo.columna));


                            if (nodo.tipo is Vertedero)//vertedero
                            {

                                ProbabilidadesDeDanio(5);
                                this.cantDanios++;
                            }

                            if (nodo.tipo is VertederoElectronico)//si es vertedero electronico
                            {
                                Bateria.estado = EstadoBateria.CargaReducida;
                                Bateria.ReducirCarga();
                                this.cantDanios++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se puede realizar movimiento. Bateria insuficiente.");
                            continuarCamino = false;
                        }

                    }


                    /////////////////////////////////////////////////////////////////////////////////////////////
                }

                else Console.WriteLine("No es posible encontrar un camino.");
            }
            else Console.WriteLine("El operador se encuentra en STANDBY");
        }


        private Stack<Nodo> determinaCamino(Localizacion destino, Mapa mapa)
        {
            Stack<Nodo> camino;
            short tipoRuta = tipoDeRuta();
            if (tipoRuta == 1)
            {
                if (this.tipo == "Terrestre")
                    camino = ((MapaTerrestre)mapa).devolverCaminoDirecto(this.localizacion, destino);
                else camino = ((MapaAereo)mapa).devolverCaminoDirecto(this.localizacion, destino);
            }
            else
            {
                if (this.tipo == "Terrestre")
                    camino = ((MapaTerrestre)mapa).devolverCaminoOptimo(this.localizacion, destino);
                else camino = ((MapaAereo)mapa).devolverCaminoOptimo(this.localizacion, destino); ;
            }
            return camino;

        }

        private short tipoDeRuta()
        {
            short opcion;
            do
            {
                Console.WriteLine("Que tipo de ruta desea seguir?" +
                                   "\n1) Directa" +
                                    "\n2) Optima");
                Console.Write("\nSu opcion: ");
                opcion = short.Parse(Console.ReadLine());

            } while (opcion < 1 || opcion > 2);
            return opcion;
        }

        private void cargarUltimaLocalizacion(Localizacion localizacion)
        {
            if (this.ultimasUbicVisitadas.Count() >= 3)
            {
                this.ultimasUbicVisitadas.Dequeue();
                this.ultimasUbicVisitadas.Enqueue(localizacion);
            }
            else
            {
                this.ultimasUbicVisitadas.Enqueue(localizacion);
            }
        }


        /*
        * ------------------------------------------
        *        fin funciones movimiento
        * ------------------------------------------
        */


        /*
        * -----------------------------------
        *          funciones bateria
        * -----------------------------------
        */

        public void transferirBateria(Operador op2, int bateria)  //Transfiere una cantidad en Amh desde nuestro Operador a otro Operador op2
        {
            if (this.Bateria.bateriaActual > 0 && (this.localizacion.equals(op2.localizacion)) &&
                (this.Bateria.estado != EstadoBateria.PuertoDesconectado))
            {
                this.Bateria.DescargarBateria(bateria);
                op2.Bateria.CargarBateria(bateria);
                this.cantInstrucciones++;
            }
            else Console.WriteLine("No es posible realizar la transferencia de bateria porque no estan en la misma localizacion");
        }



        public void cargarBateriaEnCuartel()           //Indica al operador que se desplace hacia su cuartel y carga su bateria al maximo
        {
            if (this.EstadoLogico != EstadoLogicoOp.StandBy)
            {
                if (this.localizacion.equals(this.localizacionCuartel))
                {
                    if (this.Bateria.estado != EstadoBateria.PuertoDesconectado)//controlar si tiene el daño "puerto desconectado"
                    {
                        //volverAlCuartel();
                        this.Bateria.RecargarBateriaCompleta();
                    }
                    else Console.WriteLine("El operador se encuentra dañado, el Puerto de la bateria está desconectado");
                }
                else Console.WriteLine("Error, el operador no logró llegar al cuartel");
            }
            else Console.WriteLine("El operador se encuentra en STANDBY");
        }


        public void ReemplazarBateria()
        {
            if (Bateria.estado != EstadoBateria.BuenEstado)
            {
                TamañoBateria capacidadBateria = Bateria.tipoBateria;
                Bateria = new Bateria(capacidadBateria);
                this.cantInstrucciones++;
            }
            else
            {
                Console.WriteLine("La bateria se encuentra en buen estado!");
            }
        }


        public void CambiarEstadoBateria(EstadoBateria nuevoEstado)
        {
            Bateria.estado = nuevoEstado;
        }



        /*
         * -----------------------------------
         *         fin funciones bateria
         * -----------------------------------
         */

        //----------------------------
        //  carga fisica del operador
        //----------------------------

        public void RecargaCargaMax()
        {
            if (this.EstadoFisico != EstadoFisicoOp.ServoAtascado)
            {
                this.CargaActual = this.CargaMax;
                this.cantCargaTransportada += this.CargaMax;
                this.cantInstrucciones++;
            }
        }


        public void RecargarCargaFisica(int carga)
        {
            if (this.EstadoFisico != EstadoFisicoOp.ServoAtascado)
            {
                if (this.CargaActual < this.CargaMax && (this.CargaActual + carga) <= this.CargaMax)
                {
                    this.CargaActual += carga;
                    this.cantCargaTransportada += carga;
                    this.cantInstrucciones++;
                }
            }
            else Console.WriteLine("No es posible realizar la carga fisica. Servo Atascado.");
        }

        public void transferirCarga(Operador op2, int carga)  //Transfiere en kg la carga actual de nuestro Operador a otro Operador op2
        {
            if (this.EstadoFisico != EstadoFisicoOp.ServoAtascado)
            {
                if (this.localizacion.Equals(op2.localizacion))
                {
                    int c = op2.CargaActual + carga;

                    if (carga <= this.CargaActual && (c > op2.CargaMax || c < 0))
                    {
                        this.CargaActual -= carga;
                        op2.RecargarCargaFisica(carga);
                        this.cantInstrucciones++;
                    }
                    else Console.WriteLine("No es posible realizar la transferencia de carga");

                }
                else Console.WriteLine("No es posible realizar la transferencia de carga porque no estan en la misma localizacion");
            }
            else Console.WriteLine("No es posible la transferencia. Servo Atascado.");
        }

        public void DescargarEnCuartel()    //Descarga la carga actual en el cuartel
        {

            if (this.localizacion.equals(this.localizacionCuartel))
            {
                if (this.EstadoFisico != EstadoFisicoOp.ServoAtascado)
                {
                    this.CargaActual = 0;
                }
                else
                {
                    short opc;
                    do
                    {
                        Console.WriteLine("Se llego al cuartel, pero presenta servo atascado. Desea repararlo?");
                        Console.WriteLine("1 - Si \n 2 - No");
                        Console.Write("\nSu opcion: ");
                        opc = short.Parse(Console.ReadLine());
                        if (opc < 1 || opc > 2) Console.Write("Opcion erronea. Vuelva a intentar.");
                    } while (opc < 1 || opc > 2);
                    if (opc == 1)
                    {
                        RepararOperador();
                        this.CargaActual = 0;
                    }

                }
            }
            else Console.WriteLine("Error. No se logro llegar al cuartel.");
        }

        //----------------------------
        // fin carga fisica del operador
        //----------------------------

        /*
         * --------------------------------------------------
         *  funcion que produce de daño y que repara daños
         *  -------------------------------------------------
         */

        public void ProbabilidadesDeDanio(int porcentajeExito)
        {
            Random random = new Random();
            int aleatorio = random.Next(0, 100);

            if (aleatorio < porcentajeExito)
            {
                IDanioOperador danio = DanioOperador.DanioAleatorio();
                danio.ProducirDanio(this);
            }
        }

        public void RepararOperador()
        {
            this.EstadoFisico = EstadoFisicoOp.BuenEstado;

            this.Bateria.RecargarBateriaCompleta();
        }


        /*
        * --------------------------------------------------
        *  fin funciones que producen daños y que reparan daños
        *  -------------------------------------------------
        */

        // Otras funciones




        public void volverAlCuartel(Mapa mapa)      //Indica al Operador que se desplace a su Cuartel correspondiente
        {
            if (this.EstadoLogico != EstadoLogicoOp.StandBy)
            {
                if (this.localizacion.equals(this.localizacionCuartel)) /// pregunta si ya se encuentra en el cuartel
                {

                    moverse(this.localizacionCuartel, mapa);

                }
                else Console.WriteLine("Ya se encuentra en el cuartel.");
            }
            else Console.WriteLine("El operador se encuentra en STANDBY");

        }

        // FUNCIONES PARTE 2

        public void moverseVertederoCercano(Mapa mapa)
        {
            int min = 500;
            int i = 0, actual;
            bool bandera = true;
            Localizacion vertederoCercano = null;

            while (i < mapa.vertederos.Count() && bandera)
            {
                Localizacion vertedero = mapa.vertederos[i];

                actual = Math.Abs(this.localizacion.filaProperty - vertedero.filaProperty) + Math.Abs(this.localizacion.columnaProperty - vertedero.columnaProperty);
                if (actual < min)
                {
                    min = actual;
                    vertederoCercano = vertedero;
                }
                if (min == 1)
                {
                    bandera = false;
                }

                i++;
            }

            moverse(vertederoCercano, mapa);
            if (this.localizacion.equals(vertederoCercano))
            {
                this.cantCargaTransportada += (this.CargaMax - this.CargaActual);
                this.CargaActual = this.CargaMax;
            }
            else Console.WriteLine("El operador [" + this.Id + "] No logro llegar al vertedero mas cercano");
        }


        public void moverseSitioReciclajeCercano(Mapa mapa)
        {
            Localizacion sitioReciclajeCercano = null;
            bool bandera = false;                                   //se podria optimizar con Find o Contains (investigar metodo)
            int i = 0;
            while (i < mapa.vertederos.Count() && !bandera)
            {
                if (this.localizacion.equals(mapa.vertederos[i]))
                {
                    bandera = true;
                }
                i++;
            }

            if (bandera)
            {
                int min = 500, actual;
                i = 0;
                while (i < mapa.sitiosReciclaje.Count() && bandera)
                {
                    Localizacion sitioReciclaje = mapa.sitiosReciclaje[i].getLocalizacion();

                    actual = Math.Abs(this.localizacion.filaProperty - sitioReciclaje.filaProperty) + Math.Abs(this.localizacion.columnaProperty - sitioReciclaje.columnaProperty);
                    if (actual < min)
                    {
                        min = actual;
                        sitioReciclajeCercano = sitioReciclaje;
                    }
                    if (min == 1)
                    {
                        bandera = false;
                    }

                    i++;
                }
                moverse(sitioReciclajeCercano, mapa);
                if (this.localizacion.equals(sitioReciclajeCercano))
                {
                    this.CargaActual = 0;
                }
                else Console.WriteLine("El operador [" + this.Id + "] No logro llegar al sitio de Reciclaje mas cercano");

            }

        }

        public void mostrarInfodelOperador()
        {
            Console.WriteLine("Id: " + Id);
            Console.WriteLine("carga fisica maxima: " + CargaMax);
            Console.WriteLine("carga fisica actual: " + CargaActual);
            Console.WriteLine("estado logico: " + EstadoLogico);
            Console.WriteLine("estado fisico: " + EstadoFisico);
            Console.WriteLine("velociad: " + velocidad);
            Console.WriteLine("cantidad de km: " + cantKms);
            Console.WriteLine("cantidad de energia consumida: " + cantEnergiaConsumida);
            Console.WriteLine("cantidad de carga transportada: " + cantCargaTransportada);
            Console.WriteLine("cantidad de instrucciones: " + cantInstrucciones);
            Console.WriteLine("cantidad de danios: " + cantDanios);
            Console.WriteLine("tipo de operador: " + tipo);
            Console.WriteLine("Bateria maxima: " + Bateria.bateriaMaxima);
            Console.WriteLine("Bateria actual: " + Bateria.bateriaActual);
            Console.WriteLine("Localizacion del operador: " + localizacion.filaProperty + " - " + localizacion.columnaProperty);
            Console.WriteLine("cantidad de ultimas localizaciones visitadas: " + this.ultimasUbicVisitadas.Count());
            if (this.ultimasUbicVisitadas.Count() != 0)
            {
                foreach (Localizacion local in this.ultimasUbicVisitadas)
                {
                    Console.WriteLine("[" + local.filaProperty + "  - " + local.columnaProperty + "]");
                }
            }
            else Console.Write("El operador no tiene ultimas ubicaciones visitadas. No se movio.");
            Console.WriteLine("Localizacion de su cuartel: " + localizacionCuartel.filaProperty + " - " + localizacionCuartel.columnaProperty);
        }

    }

}
