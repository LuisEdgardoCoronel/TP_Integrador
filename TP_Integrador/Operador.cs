﻿using System;
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
        protected String Localizacion,Cuartel;
        protected EstadoOperador Estado;
        protected double velocidad;

        public Operador(String Localizacion) {
            this.Id = IdStatic;                    //Cada vez que se cree un objeto Operador, se le asigana el Id automaticamente
            IdStatic++;                           //La variable estatica incrementa automaticamente para asignarle otro Id diferente
            this.Localizacion = Localizacion;     //al siguiente Operador que se cree
            this.Estado = EstadoOperador.Activo;
            this.CargaActual = 0;
            this.BateriaActual = this.BateriaMax;
        }

        public void moverse(String Localizacion)
        {
            //falta implementacion por falta de info sobre las distancia ente localizaciones
        }

        public void transferirBateria(Operador op2, int bateria)  //Transfiere una cantidad en Amh desde nuestro Operador a otro Operador op2
        {
            if (this.BateriaActual > 0 && (this.Localizacion.CompareTo(op2.getLocalizacion) == 0))
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

        public void cargarBateriaEnCuartel()           //Indica al operador que se desplace hacia su cuartel y carga su bateria al maximo
        {
            volverAlCuartel();
            this.BateriaActual = this.BateriaMax;
        }

        public EstadoOperador getEstado()                      //Hacia abajo se encuentran algunos getters y setters necesarios
        {
            return this.Estado;
        }

        public void setEstado(EstadoOperador estado)
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
