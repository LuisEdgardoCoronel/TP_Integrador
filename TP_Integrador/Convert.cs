using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    internal static class Convert
    {

       
        public static Nodo[,] convertListToMatrice(List<Nodo> lista) //metodo para convertir una lista a matriz, necesario
        {                                                      //ya que no se permite serializar matrices de objetos personalizados

            int n = lista.Count();
            Nodo[,] matriz2 = new Nodo[n + 2, n + 2];
            int c = 0;
            for (int i = 1; i <= Math.Sqrt(n); i++)
            {
                for (int j = 1; j <= Math.Sqrt(n); j++)
                {


                    matriz2[i, j] = lista.ElementAt(c);
                    c++;

                }
            }
            return matriz2;
        }

        public static List<Nodo> convertMatriceToList(Nodo[,] matriz) //convierte una matriz en una lista
        {
            List<Nodo> lista = matriz.Cast<Nodo>().ToArray().ToList();

            lista.RemoveAll(nodo => nodo == null); //remueve los nulos 
            return lista;

        }

    }
}

    
    

