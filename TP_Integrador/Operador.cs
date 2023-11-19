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
        protected EstadoOperador Estado;
        protected Bateria Bateria;
        protected Localizacion localizacion;
        protected Localizacion localizacionCuartel;
        protected double velocidad;

        public Operador(Localizacion localizacion) {    //Se crea Operador, recibiendo la localizacion del Cuartel donde se crea

            this.Id = IdStatic;                    //Cada vez que se cree un objeto Operador, se le asigana el Id automaticamente
            IdStatic++;                           //La variable estatica incrementa automaticamente para asignarle otro Id diferente
                                                  //al siguiente Operador que se cree
            this.localizacion = localizacion;             ///Indicamos la localizacion donde se crea el operador
            this.localizacionCuartel = this.localizacion; /// Asignamos ademas la localzacin de su cuartel correspondiente
            this.Estado = EstadoOperador.BuenEstado;
            this.CargaActual = 0;
          
        }

        /*
      * ------------------------------------------
      *         funciones movimiento
      * ------------------------------------------
      */

        public abstract void moverse(Stack<Nodo> camino);

        /// VER!

        /*    public void moverse(String Localizacion)
          {

              // pila = [[n1,m1],[n2,m2].....]; PILA

              if (this.Bateria.getBateriaActual() > 1000 / velocidad)
              {

                  Bateria.DescargaPorMovimiento(this.velocidad);//descarga cada vez que se mueve

                  if ()//vertedero
                  {

                      ProbabilidadesDeDanio(5);

                  }

                  if ()//si es vertedero electronico
                  {
                      Bateria.SetEstadoBateria(EstadoBateria.CargaReducida);
                      Bateria.ReducirCarga();
                  }
              }
              else Console.WriteLine("No se puede realizar movimiento. Bateria insuficiente.");



          }
           */


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
            if (Estado != EstadoOperador.ServoAtascado)
            {
                this.CargaActual = this.CargaMax;
            }
        }


        public void RecargarCargaFisica(int carga)
        {
            if (Estado != EstadoOperador.ServoAtascado)
            {
                if (this.CargaActual < this.CargaMax && (this.CargaActual + carga) <= this.CargaMax)
                {
                    this.CargaActual += carga;
                }
            }
        }



        public void transferirCargar(Operador op2, int carga)  //Transfiere en kg la carga actual de nuestro Operador a otro Operador op2
        {
            if(this.Estado != EstadoOperador.ServoAtascado)
            {
                if (this.localizacion.Equals(op2.getLocalizacion())) ///se cambio la comparacion
                {
                    int c = op2.getCargaActual() + carga;

                    if (carga <= this.CargaActual && (c > op2.getCargaMaxima() || c < 0))
                    {
                        this.CargaActual -= carga;
                        op2.setCarga(c);
                    }
                    else Console.WriteLine("No es posible realizar la transferencia de carga");

                }
                else Console.WriteLine("No es posible realizar la transferencia de carga porque no estan en la misma localizacion");
            }
            else Console.WriteLine("No es posible la transferencia. Servo Atascado.");


        }

        public void DescargarEnCuartel()    //Descarga la carga actual en el cuartel
        {
            if (Estado != EstadoOperador.ServoAtascado)
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
            this.Estado = EstadoOperador.BuenEstado;

            this.Bateria.RecargarBateriaCompleta();
        }


        /*
        * --------------------------------------------------
        *  fin funciones que producen daños y que reparan daños
        *  -------------------------------------------------
        */


        // getters and setters

        public EstadoOperador getEstado()                      //Hacia abajo se encuentran algunos getters y setters necesarios
        {
            return this.Estado;
        }

        public void setEstado(EstadoOperador estado)
        {
            this.Estado = estado;
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


    }


}
