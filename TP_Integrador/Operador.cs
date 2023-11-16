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
        protected int Id,BateriaMax,CargaMax,CargaActual,BateriaActual;
        protected String Estado;
        protected Localizacion localizacion;
        protected Localizacion localizacionCuartel;
        protected double velocidad;

        public Operador(Localizacion localizacion) {    //Se crea Operador, recibiendo la localizacion del Cuartel donde se crea

            this.Id = IdStatic;                    //Cada vez que se cree un objeto Operador, se le asigana el Id automaticamente
            IdStatic++;                           //La variable estatica incrementa automaticamente para asignarle otro Id diferente
                                                  //al siguiente Operador que se cree
            this.localizacion = localizacion;             ///Indicamos la localizacion donde se crea el operador
            this.localizacionCuartel = this.localizacion; /// Asignamos ademas la localzacin de su cuartel correspondiente
            this.Estado = "Disponible";
            this.CargaActual = 0;
            this.BateriaActual = this.BateriaMax;
        }

        public void moverse(Localizacion Localizacion)
        {
            //falta implementacion por falta de info sobre las distancia ente localizaciones
        }

        public void transferirBateria(Operador op2, int bateria)  //Transfiere una cantidad en Amh desde nuestro Operador a otro Operador op2
        {
            if (this.BateriaActual > 0 && (this.localizacion.equals(op2.getLocalizacion()))) ///Correcion: se cambio comparar ubicaciones
            {
                if (bateria <= this.BateriaActual)
                {
                    this.BateriaActual -= bateria;
                    op2.setBateria(bateria);
                }
                else Console.WriteLine("No es posible realizar la transferencia de bateria.");
            }
            else Console.WriteLine("No es posible realizar la transferencia de bateria porque no estan en la misma localizacion");
        }

        public void transferirCargar(Operador op2, int carga)  //Transfiere en kg la carga actual de nuestro Operador a otro Operador op2
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

        public void descargarEnCuartel()    //Descarga la carga actual en el cuartel
        {
            volverAlCuartel();
            this.CargaActual = 0;
        }

        public void volverAlCuartel()      //Indica al Operador que se desplace a su Cuartel correspondiente
        {
            if (this.localizacion.equals(this.localizacionCuartel)) ///Mofificacion: pregunta si ya se encuentra en el cuartel
            {
                
                    moverse(this.localizacionCuartel);
                
            }
            else Console.WriteLine("Ya se encuentra en el cuartel.");

        }

        public void cargarBateriaEnCuartel()           //Indica al operador que se desplace hacia su cuartel y carga su bateria al maximo
        {
            volverAlCuartel();
            this.BateriaActual = this.BateriaMax;
        }

        public String getEstado()                      //Hacia abajo se encuentran algunos getters y setters necesarios
        {
            return this.Estado;
        }

        public void setEstado(String estado)
        {
            this.Estado = estado;
        }

        
        
        public void setBateria(int bateria)
        {
            int bat = this.BateriaActual + bateria;

            if(bat > this.BateriaMax)
            {
                this.BateriaActual = this.BateriaMax;
            }
            else
            {
                this.BateriaActual = bat;
            }
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

        public Localizacion getLocalizacion()
        {
            return this.localizacion;
        }


        ///se elimino meotodo para setear cuartel a un operador ya que no esncesario

        public int getId()
        {
            return this.Id;
        }

    }
}
