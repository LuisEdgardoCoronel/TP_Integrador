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
        protected String Localizacion,Cuartel;

        protected Bateria Bateria;//poner en private

        protected CargaOperador CapacidadCarga;

        protected EstadoOperador Estado;

        protected double velocidad;




        public Operador(String Localizacion, TamañoBateria capacidadBateria, CargaOperador CapacidadCarga) {
            this.Id = IdStatic;                    //Cada vez que se cree un objeto Operador, se le asigana el Id automaticamente
            IdStatic++;                           //La variable estatica incrementa automaticamente para asignarle otro Id diferente
            this.Localizacion = Localizacion;     //al siguiente Operador que se cree
            this.Estado = EstadoOperador.Activo;
            this.CargaActual = 0;
            this.Bateria = new Bateria(capacidadBateria);
            this.CargaMax = (int)CapacidadCarga;
        }

   

        /*
         * ------------------------------------------
         *         funciones movimiento
         * ------------------------------------------
         */


        public void moverse(String Localizacion)
        {
            //falta implementacion por falta de info sobre las distancia ente localizaciones


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


        public void volverAlCuartel()      //Indica al Operador que se desplace a su Cuartel correspondiente
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




        public void transferirBateria(Operador op2, int bateria)  //Transfiere una cantidad en Amh desde nuestro Operador a otro Operador op2
        {
            if (this.Bateria.ObtenerCargaActual() > 0 && 
                (this.Localizacion.CompareTo(op2.getLocalizacion) == 0)&&//misma localizacion?
                (this.Bateria.GetEstadoBateria() != EstadoBateria.PuertoDesconectado)//que no esté dañada
                )
            {
                this.Bateria.DescargarBateria(bateria);//descargar bateria de un operador
                op2.Bateria.CargarBateria(bateria);//cargar bateria del otro operador
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

        //ver esta funcion
        public void ReemplazarBateria(TamañoBateria capacidadBateria)
        {
            if (Bateria.estado != EstadoBateria.BuenEstado)
            {
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
            this.CargaActual = this.CargaMax;
        }


        public void RecargarCargaFisica(int carga)
        {
            if (this.CargaActual<this.CargaMax && (this.CargaActual+carga) <= this.CargaMax)
            {
                this.CargaActual += carga;
            }
        }



        
        public void TransferirCargar(Operador op2, int carga)  //Transfiere en kg la carga actual de nuestro Operador a otro Operador op2
        {
            if (this.Localizacion.CompareTo(op2.getLocalizacion) == 0)
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





        public void descargarEnCuartel()    //Descarga la carga actual en el cuartel
        {
            volverAlCuartel();
            this.CargaActual = 0;
        }

        //----------------------------
        // fin carga fisica del operador
        //----------------------------












        
        /*
         * -------------------------------
         *  funcion que produce de daño
         *  ------------------------------
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











        














        /*
         * 
         * getters y setters
         * 
         */


        public EstadoOperador getEstado()                      
        {
            return this.Estado;
        }

        public void setEstado(EstadoOperador estado)
        {
            this.Estado = estado;
        }



        public void setCarga(int carga) { 
            
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

        public String getLocalizacion()
        {
            return this.Localizacion;
        }


        public void setCuartel(String cuartel)
        {
            this.Cuartel = cuartel;
        }

        public int getId()
        {
            return this.Id;
        }

       
        
        










    }


}
