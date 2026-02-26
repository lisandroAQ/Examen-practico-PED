using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_practico_PED
{
    public class ColaEnlazada
    {
        //Atributos
        //Punteros que señalan el inicio y el final de nodos en cola
        NodoCola primero;
        NodoCola ultimo;
        int totalNodos; //Almacena todos los nodos existentes

        //Metodos
        public ColaEnlazada()//Metodo Constructor
        {
            primero = ultimo = null;
            totalNodos = 0;
        }
        public int TotalNodos()
        {
            //Retorna valor del campo
            return this.totalNodos;
        }
        //Retorna true si la cola esta vacia
        public bool EstaVacia()
        {
            if (primero == null) return true; //Cola vacia
            else return false;//cola tiene al menos un nodo
        }

        public string MostrarTodos()
        {
            if (EstaVacia()) return "Cola vacía.";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== Lista de espera ===");
            NodoCola aux = primero;
            int turno = 1;

            while (aux != null)
            {
                if (turno == 1)
                    sb.AppendLine(turno + ". " + aux.info + "   <--- Siguiente en ser atendido");
                else
                    sb.AppendLine(turno + ". " + aux.info);

                aux = aux.sig;
                turno++;
            }

            return sb.ToString();
        }

        public void Encolar(NodoCola nodo) 
        {
            //Agrega un nodo al final de la cola (Entrada)
            if (EstaVacia())
                primero = ultimo = nodo;

            else 
            {
                ultimo.sig = nodo;
                ultimo = nodo;
            }
            totalNodos++; // incrementa conteo nodos existentes
        }
      

        public NodoCola Desencolar() 
        {
            NodoCola aux = null;
            if (!EstaVacia()) 
            {
                //procede a extraer nodo ubicado al inicio de la cola (salida)
                aux = primero;
                primero = primero.sig;
                totalNodos--; // reduce conteo de nodos existentes
            }
            return aux;
        }
        public NodoCola Primero() => primero;
        public void Limpiar()
        {
            primero = null;
            ultimo = null;
            totalNodos = 0;
        }
    }
}
