using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal abstract class Operador
    {
        protected static int IdStatic=0;            //Atributo estatico,inicializado en 0
        protected int Id,CargaMax,CargaActual;
        protected EstadoLogicoOp EstadoLogico;
        protected EstadoFisicoOp EstadoFisico;
        protected Bateria Bateria;
        protected Localizacion localizacion;
        protected Localizacion localizacionCuartel;
        protected double velocidad;
        protected Mapa mapa;
        protected string tipo;
        protected int cantKms;
        protected int cantEnergiaConsumida;
        protected int cantCargaTransportada;
        protected int cantInstrucciones;
        protected int cantDanios;
        protected Queue<Localizacion> ultimasUbicVisitadas;

        public Operador(Localizacion localizacion) {    //Se crea Operador, recibiendo la localizacion del Cuartel donde se crea

            this.Id = IdStatic;                    //Cada vez que se cree un objeto Operador, se le asigana el Id automaticamente
            IdStatic++;                           //La variable estatica incrementa automaticamente para asignarle otro Id diferente
                                                  //al siguiente Operador que se cree
            this.localizacion = localizacion;             ///Indicamos la localizacion donde se crea el operador
            this.localizacionCuartel = this.localizacion; /// Asignamos ademas la localzacin de su cuartel correspondiente
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

        public void moverse(Localizacion localizacion)
        {
            bool continuarCamino = true;
            Stack<Nodo> camino = determinaCamino(localizacion); /// Aqui ya tendriamos la pila de los nodos Camino

            if (camino.Count() != 0 && localizacion!=null)
            {
                //////////////////////////////////////////////////////////////////////////////////////////////
                ///
                this.cantInstrucciones++;
                while (camino.Count() != 0 && continuarCamino)
                {


                    if (this.Bateria.getBateriaActual() > 1000 / velocidad)
                    {
                        Nodo nodo = camino.Pop();
                        this.localizacion.setFila(nodo.getFila());
                        this.localizacion.setColumna(nodo.getColumna());
                        this.cantKms++;
                        
                        Bateria.DescargaPorMovimiento(this.velocidad);//descarga cada vez que se mueve

                        cargarUltimaLocalizacion(new Localizacion(nodo.getFila(),nodo.getColumna()));


                         if (nodo.getTipo() is Vertedero)//vertedero
                        {

                            ProbabilidadesDeDanio(5);
                            this.cantDanios++;
                        }

                         if (nodo.getTipo() is VertederoElectronico)//si es vertedero electronico
                        {
                            Bateria.SetEstadoBateria(EstadoBateria.CargaReducida);
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


        private Stack<Nodo> determinaCamino(Localizacion destino)
        {
            Stack<Nodo> camino;
            short tipoRuta = tipoDeRuta();
            if (tipoRuta == 1)
            {
                if(this.tipo == "Terrestre")
                camino =((MapaTerrestre)mapa).devolverCaminoDirecto(this.localizacion, destino);
                else camino = ((MapaAereo)mapa).devolverCaminoDirecto(this.localizacion, destino);
            }
            else
            {
                if(this.tipo == "Terrestre")
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
                opcion = short.Parse(Console.ReadLine());

            } while (opcion < 1 && opcion > 2);
            return opcion;
        }

        private void cargarUltimaLocalizacion (Localizacion localizacion)
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
            if (this.Bateria.ObtenerCargaActual() > 0 && (this.localizacion.equals(op2.getLocalizacion())) &&
                (this.Bateria.GetEstadoBateria() != EstadoBateria.PuertoDesconectado)) ///Correcion: se cambio comparar ubicaciones
            {
                this.Bateria.DescargarBateria(bateria);
                op2.Bateria.CargarBateria(bateria);
                this.cantInstrucciones++;
            }
            else Console.WriteLine("No es posible realizar la transferencia de bateria porque no estan en la misma localizacion");
        }

        public void cargarBateriaEnCuartel()           //Indica al operador que se desplace hacia su cuartel y carga su bateria al maximo
        {
            if (this.Bateria.GetEstadoBateria() != EstadoBateria.PuertoDesconectado)//coontrolar si tiene el daño "puerto desconectado"
            {
                volverAlCuartel();
                this.Bateria.RecargarBateriaCompleta();
                
            }
            else
            {
                Console.WriteLine("El operador se encuentra dañado, el Puerto de la bateria está desconectado");
            }
        }


        public void ReemplazarBateria()
        {
            if (Bateria.estado != EstadoBateria.BuenEstado)
            {
                TamañoBateria capacidadBateria = Bateria.GetTamañoBateria();
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
           
                Bateria.SetEstadoBateria(nuevoEstado);
            
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



        public void transferirCargar(Operador op2, int carga)  //Transfiere en kg la carga actual de nuestro Operador a otro Operador op2
        {
            if(this.EstadoFisico != EstadoFisicoOp.ServoAtascado)
            {
                if (this.localizacion.Equals(op2.getLocalizacion())) ///se cambio la comparacion
                {
                    int c = op2.getCargaActual() + carga;

                    if (carga <= this.CargaActual && (c > op2.getCargaMaxima() || c < 0))
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
            if (this.EstadoFisico != EstadoFisicoOp.ServoAtascado)
            {
                volverAlCuartel();
                this.CargaActual = 0;
                
            }
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


        // getters and setters

        public EstadoFisicoOp getEstadoFisico()                      //Hacia abajo se encuentran algunos getters y setters necesarios
        {
            return this.EstadoFisico;
        }

        public void setEstadoFisico(EstadoFisicoOp estado)
        {
            this.EstadoFisico = estado;
        }

        public EstadoLogicoOp getEstadoLogico()                      //Hacia abajo se encuentran algunos getters y setters necesarios
        {
            return this.EstadoLogico;
        }

        public void setEstadoLogico(EstadoLogicoOp estado)
        {
            this.EstadoLogico = estado;
        }


        public void setCarga(int carga)
        {

            this.CargaActual = carga;

        }

        public int getCargaActual()
        {
            return this.CargaActual;
        }

        public int getCargaMaxima()
        {
            return this.CargaMax;
        }

        public Localizacion getLocalizacion()
        {
            return this.localizacion;
        }


        ///se elimino metodo para setear cuartel a un operador ya que no esncesario

        public int getId()
        {
            return this.Id;
        }

        public void SetVelocidad(double velocidad)
        {
            this.velocidad = velocidad;
        }


        public double GetVelocidad()
        {
            return this.velocidad;
        }

        public Bateria getBateria()
        {
            return this.Bateria;
        }

        // Otras funciones




        public void volverAlCuartel()      //Indica al Operador que se desplace a su Cuartel correspondiente
        {
            if (this.localizacion.equals(this.localizacionCuartel)) ///Mofificacion: pregunta si ya se encuentra en el cuartel
            {
                
                    moverse(this.localizacionCuartel); /// Falta modificar movimiento
                
            }
            else Console.WriteLine("Ya se encuentra en el cuartel.");

        }

        // FUNCIONES PARTE 2

        public void moverseVertederoCercano()
        {
            int min = 500;
            int i = 0,actual;
            bool bandera=true;
            Localizacion vertederoCercano = null;

            while (i < this.mapa.getVertederos().Count() && bandera)
            {
                Localizacion vertedero = this.mapa.getVertederos()[i];

                actual = Math.Abs(this.localizacion.getFila() - vertedero.getFila()) + Math.Abs(this.localizacion.getColumna() - vertedero.getColumna());
                if(actual < min)
                {
                    min = actual;
                    vertederoCercano = vertedero;
                }
                if(min == 1)
                {
                    bandera = false;
                }

                i++;
            }

            moverse(vertederoCercano);
            if (this.localizacion.equals(vertederoCercano))
            {
                this.CargaActual = this.CargaMax;
            }
            else Console.WriteLine("El operador [" + this.Id + "] No logro llegar al vertedero mas cercano");
        }


        public void moverseSitioReciclajeCercano()
        {
            Localizacion sitioReciclajeCercano = null;
            bool bandera = false;                                   //se podria optimizar con Find o Contains (investigar metodo)
            int i = 0;
            while(i<this.mapa.getVertederos().Count() && !bandera)
            {
                if (this.localizacion.equals(this.mapa.getVertederos()[i]))
                {
                    bandera = true;
                }
                i++;
            }
           
            if (bandera)
            {
                int min = 500, actual;
                i = 0;
                while (i < this.mapa.getSitiosReciclaje().Count() && bandera)
                {
                    Localizacion sitioReciclaje = this.mapa.getSitiosReciclaje()[i].getLocalizacion();

                    actual = Math.Abs(this.localizacion.getFila() - sitioReciclaje.getFila()) + Math.Abs(this.localizacion.getColumna() - sitioReciclaje.getColumna());
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
                moverse(sitioReciclajeCercano);
                if (this.localizacion.equals(sitioReciclajeCercano))
                {
                    this.CargaActual = 0;
                }
                else Console.WriteLine("El operador ["+ this.Id +"] No logro llegar al sitio de Reciclaje mas cercano");

            }

        }

    }


}
