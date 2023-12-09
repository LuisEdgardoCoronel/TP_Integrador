using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class Bateria
    {
        public TamañoBateria tipoBateria { get; private set; }
        public double bateriaMaxima { get; private set; }
        public double bateriaActual { get; set; }
        public EstadoBateria estado { get; set; }


        [JsonConstructor]
        public Bateria(TamañoBateria tipoBateria, double bateriaMaxima, double bateriaActual, EstadoBateria estado)
        {
            this.tipoBateria = tipoBateria;
            this.bateriaMaxima = bateriaMaxima;
            this.bateriaActual = bateriaActual;
            this.estado = estado;



        }
        public Bateria(TamañoBateria bateriaMaxima)
        {
            this.tipoBateria = bateriaMaxima;
            this.bateriaMaxima = (double)bateriaMaxima;
            this.bateriaActual = (double)bateriaMaxima; // Inicializar la carga actual como la capacidad máxima al principio
            this.estado = EstadoBateria.BuenEstado;
        }



        public void CargarBateria(int cantidad)
        {
            if (cantidad > 0 && (this.bateriaActual + cantidad <= this.bateriaMaxima))
            {
                this.bateriaActual += cantidad;
            }
            else
            {
                Console.WriteLine("No se pudo cargar la batería. La cantidad es inválida o excede la capacidad máxima.");
            }
        }

        public void DescargarBateria(int cantidad)
        {
            if (cantidad > 0 && this.bateriaActual - cantidad >= 0)
            {
                this.bateriaActual -= cantidad;
            }
            else
            {
                Console.WriteLine("No se pudo descargar la batería. La cantidad es inválida o excede la carga actual.");
            }
        }

        public void RecargarBateriaCompleta()
        {
            this.bateriaActual = this.bateriaMaxima;
        }

        public void DescargaPorMovimiento(double velocidad)
        {

            if (estado == EstadoBateria.Perforada)
            {
                this.bateriaActual -= 1000 / (5 * velocidad);
            }
            else
            {
                this.bateriaActual -= 1000 / velocidad;
            }



        }


        //carga reducida por vertedero electronico
        public void ReducirCarga()
        {
            this.bateriaMaxima *= 0.2;
            if (this.bateriaActual > this.bateriaMaxima) this.bateriaActual = this.bateriaMaxima;
        }

    }

}
