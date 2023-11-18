using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class Bateria
    {
        private TamañoBateria tipoBateria;
        private double bateriaMaxima;
        private double bateriaActual;
        public EstadoBateria estado;



        public Bateria(TamañoBateria bateriaMaxima)
        {
            this.tipoBateria = bateriaMaxima;
            this.bateriaMaxima = (double)bateriaMaxima;
            this.bateriaActual = (double)bateriaMaxima; // Inicializar la carga actual como la capacidad máxima al principio
            this.estado = EstadoBateria.BuenEstado;
        }




        public double ObtenerCargaActual()
        {
            return bateriaActual;
        }





        public void CargarBateria(int cantidad)
        {
            if ((cantidad > 0) && (bateriaActual + cantidad <= bateriaMaxima))
            {
                bateriaActual += cantidad;
            }
            else
            {
                Console.WriteLine("No se pudo cargar la batería. La cantidad es inválida o excede la capacidad máxima.");
            }
        }




        public void DescargarBateria(int cantidad)
        {
            if (cantidad > 0 && bateriaActual - cantidad >= 0)
            {
                bateriaActual -= cantidad;
            }
            else
            {
                Console.WriteLine("No se pudo descargar la batería. La cantidad es inválida o excede la carga actual.");
            }
        }




        public void RecargarBateriaCompleta()
        {
            bateriaActual = bateriaMaxima;
        }

        public void DescargaPorMovimiento(double velocidad)
        {
            if (estado == EstadoBateria.Perforada)
            {
                bateriaActual -= 1000 / (5 * velocidad);
            }
            else
            {
                bateriaActual -= 1000 / velocidad;
            }
            
        }


        //carga reducida por vertedero electronico
        public void ReducirCarga()
        {
            bateriaMaxima *= 0.2;
            if (bateriaActual > bateriaMaxima) bateriaActual = bateriaMaxima; 
        }





        public void SetEstadoBateria(EstadoBateria estado)
        {
            this.estado=estado;
        }




        public EstadoBateria GetEstadoBateria()
        {
            return this.estado;
        }



        public TamañoBateria GetTamañoBateria()
        {
            return this.tipoBateria;
        }


    }

}
