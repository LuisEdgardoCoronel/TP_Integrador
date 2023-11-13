using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class Bateria
    {
        private int bateriaMaxima;
        private int bateriaActual;
        public EstadoBateria estado;



        public Bateria(TamañoBateria bateriaMaxima)
        {
            this.bateriaMaxima = (int)bateriaMaxima;
            this.bateriaActual = (int)bateriaMaxima; // Inicializar la carga actual como la capacidad máxima al principio
            this.estado = EstadoBateria.BuenEstado;
        }




        public int ObtenerCargaActual()
        {
            return bateriaActual;
        }





        public void CargarBateria(int cantidad)
        {
            if (cantidad > 0 && bateriaActual + cantidad <= bateriaMaxima)
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


        




        public void BateriaDañada()
        {
            this.estado = EstadoBateria.Dañada;
        }






        public EstadoBateria SetEstadoBateria()
        {
            return this.estado;
        }












    }

}
