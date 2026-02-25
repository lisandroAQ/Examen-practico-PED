using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_practico_PED
{
    public class NodoCola
    {
        //Campos
        public string info;
        public NodoCola sig;

        //Metodos 
        public NodoCola(String Valor) 
        {
            info = Valor;
            sig = null;
        }
    }
}
