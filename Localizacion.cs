using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal class Localizacion
    {
        

        public int filaProperty { get;  set; }
        public int columnaProperty { get;  set; }
     


        public Localizacion(int filaProperty, int columnaProperty)
        {
            this.columnaProperty = columnaProperty;
            this.filaProperty = filaProperty;
           
        }




        public bool equals(Localizacion local2) ///metodo nuevo para comparar localizaciones
        {
            return (this.filaProperty== local2.filaProperty && this.columnaProperty == local2.columnaProperty);
        }
        


    }
}
