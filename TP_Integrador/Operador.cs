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
<<<<<<< Updated upstream
        protected String Localizacion,Cuartel;

        protected Bateria Bateria;//poner en private

        protected CargaOperador CapacidadCarga;

        protected EstadoOperador Estado;

=======
        protected EstadoLogicoOp EstadoLogico;
        protected EstadoFisicoOp EstadoFisico;
        protected Bateria Bateria;
        protected Localizacion localizacion;
        protected Localizacion localizacionCuartel;
>>>>>>> Stashed changes
        protected double velocidad;




        public Operador(String Localizacion) {
            this.Id = IdStatic;                    //Cada vez que se cree un objeto Operador, se le asigana el Id automaticamente
            IdStatic++;                           //La variable estatica incrementa automaticamente para asignarle otro Id diferente
<<<<<<< Updated upstream
            this.Localizacion = Localizacion;     //al siguiente Operador que se cree
            this.Estado = EstadoOperador.BuenEstado;
=======
                                                  //al siguiente Operador que se cree
            this.localizacion = localizacion;             ///Indicamos la localizacion donde se crea el operador
            this.localizacionCuartel = this.localizacion; /// Asignamos ademas la localzacin de su cuartel correspondiente
            this.EstadoLogico = EstadoLogicoOp.Activo;
            this.EstadoFisico = EstadoFisicoOp.BuenEstado;
>>>>>>> Stashed changes
            this.CargaActual = 0;
        }

   

        /*
         * ------------------------------------------
         *         funciones movimiento
         * ------------------------------------------
         */


        public void moverse(String Localizacion)
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


        public void VolverAlCuartel()      //Indica al Operador que se desplace a su Cuartel correspondiente
        {
            if (this.Localizacion.CompareTo(this.Cuartel) != 0)
            {
                if (this.Cuartel != null)
                    moverse(this.Cuartel);
                else Console.WriteLine("No es posible. No tiene un Cuartel asignado.");
            }
            else Console.WriteLine("Ya se encuentra en el cuartel.");

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




        public void TransferirBateria(Operador op2, int bateria)  //Transfiere una cantidad en Amh desde nuestro Operador a otro Operador op2
        {
<<<<<<< Updated upstream
            if (this.Bateria.ObtenerCargaActual() > 0 && 
                (this.Localizacion.CompareTo(op2.getLocalizacion) == 0)&&//misma localizacion?
                (this.Bateria.GetEstadoBateria() != EstadoBateria.PuertoDesconectado)//que no esté dañada
                )
=======
            if (this.Bateria.ObtenerCargaActual() > 0 && (this.localizacion.equals(op2.GetLocalizacion())) &&
                (this.Bateria.GetEstadoBateria() != EstadoBateria.PuertoDesconectado)) ///Correcion: se cambio comparar ubicaciones
>>>>>>> Stashed changes
            {
                this.Bateria.DescargarBateria(bateria);//descargar bateria de un operador
                op2.Bateria.CargarBateria(bateria);//cargar bateria del otro operador
            }
            else Console.WriteLine("No es posible realizar la transferencia de bateria porque no estan en la misma localizacion");
        }




        public void CargarBateriaEnCuartel()           //Indica al operador que se desplace hacia su cuartel y carga su bateria al maximo
        {
            if (this.Bateria.GetEstadoBateria() != EstadoBateria.PuertoDesconectado)//coontrolar si tiene el daño "puerto desconectado"
            {
                VolverAlCuartel();
                this.Bateria.RecargarBateriaCompleta();
            }
            else
            {
                Console.WriteLine("El operador se encuentra dañado, el Puerto de la bateria está desconectado");
            }
        }

        //ver esta funcion
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
            if (Bateria != null)
            {
                Bateria.SetEstadoBateria(nuevoEstado);
            }
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
            if (EstadoFisico != EstadoFisicoOp.ServoAtascado)
            {
                this.CargaActual = this.CargaMax;
            }
        }


        public void RecargarCargaFisica(int carga)
        {
            if (EstadoFisico != EstadoFisicoOp.ServoAtascado)
            {
                if (this.CargaActual<this.CargaMax && (this.CargaActual+carga) <= this.CargaMax)
                {
                    this.CargaActual += carga;
                }
            }
        }


        //agregar la condicion de servo atascado a esta funcion
        public void TransferirCargar(Operador op2, int carga)  //Transfiere en kg la carga actual de nuestro Operador a otro Operador op2
        {
<<<<<<< Updated upstream
            if (this.Localizacion.CompareTo(op2.getLocalizacion) == 0)
            {
                int c = op2.getCargaActual() + carga;

                if (carga <= this.CargaActual && (c > op2.getCargaMaxima() || c < 0))
                {
                    this.CargaActual -= carga;
                    op2.setCarga(c);
=======
            if(this.EstadoFisico != EstadoFisicoOp.ServoAtascado)
            {
                if (this.localizacion.Equals(op2.GetLocalizacion())) ///se cambio la comparacion
                {
                    int c = op2.GetCargaActual() + carga;

                    if (carga <= this.CargaActual && (c > op2.GetCargaMaxima() || c < 0))
                    {
                        this.CargaActual -= carga;
                        op2.SetCarga(c);
                    }
                    else Console.WriteLine("No es posible realizar la transferencia de carga");

>>>>>>> Stashed changes
                }
                else Console.WriteLine("No es posible realizar la transferencia de carga");

            }
            else Console.WriteLine("No es posible realizar la transferencia de carga porque no estan en la misma localizacion");
        }



        public void DescargarEnCuartel()    //Descarga la carga actual en el cuartel
        {
            if (EstadoFisico != EstadoFisicoOp.ServoAtascado)
            {
                VolverAlCuartel();
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
<<<<<<< Updated upstream
            Estado = EstadoOperador.BuenEstado;
=======
            this.EstadoFisico = EstadoFisicoOp.BuenEstado;
>>>>>>> Stashed changes

            //Bateria.RecargarBateriaCompleta();
        }







        














        /*
         * 
         * getters y setters
         * 
         * 
         */


<<<<<<< Updated upstream
        public EstadoOperador getEstado()                      
=======
        // getters and setters

        public EstadoLogicoOp GetEstadoLogico()                      //Hacia abajo se encuentran algunos getters y setters necesarios
>>>>>>> Stashed changes
        {
            return this.EstadoLogico;
        }

        public void SetEstadoLogico(EstadoLogicoOp estado)
        {
            this.EstadoLogico = estado;
        }


        public EstadoFisicoOp GetEstadoFisico()                      //Hacia abajo se encuentran algunos getters y setters necesarios
        {
            return this.EstadoFisico;
        }

        public void SetEstadoFisico(EstadoFisicoOp estado)
        {
            this.EstadoFisico = estado;
        }

<<<<<<< Updated upstream
        public void SetVelocidad(double velocidad)
=======



        public void SetCarga(int carga)
>>>>>>> Stashed changes
        {
            this.velocidad= velocidad;
        }


        public double GetVelocidad()
        {
            return this.velocidad;
        }

        public void setCarga(int carga) { 
            
          this.CargaActual = carga;
          
        }

        public int GetCargaActual()
        {
            return this.CargaActual;
        }

        public int GetCargaMaxima()
        {
            return this.CargaMax;
        }

<<<<<<< Updated upstream
        public String getLocalizacion()
=======
        public Localizacion GetLocalizacion()
>>>>>>> Stashed changes
        {
            return this.Localizacion;
        }


        public void setCuartel(String cuartel)
        {
            this.Cuartel = cuartel;
        }

        public int GetId()
        {
            return this.Id;
        }

<<<<<<< Updated upstream
       
        
        



=======
        public void SetVelocidad(double velocidad)
        {
            this.velocidad = velocidad;
        }


        public double GetVelocidad()
        {
            return this.velocidad;
        }

        public Bateria GetBateria()
        {
            return this.Bateria;
        }

        // Otras funciones
>>>>>>> Stashed changes




<<<<<<< Updated upstream
=======
        public void VolverAlCuartel()      //Indica al Operador que se desplace a su Cuartel correspondiente
        {
            if (this.localizacion.equals(this.localizacionCuartel)) ///Mofificacion: pregunta si ya se encuentra en el cuartel
            {
                
                    moverse(this.localizacionCuartel); /// Falta modificar movimiento
                
            }
            else Console.WriteLine("Ya se encuentra en el cuartel.");
>>>>>>> Stashed changes



    }


}
