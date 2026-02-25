using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_practico_PED
{
    public class Cola
    {
        //Atributos
        //Punteros que señalan el inicio y el final de nodos en cola
        NodoCola primero;
        NodoCola ultimo;
        int totalNodos; //Almacena todos los nodos existentes

        //Metodos
        public Cola()//Metodo Constructor
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

        public void VerContenido() 
        {
            //Recorre los nodos de la cola y los muestra en pantalla
            NodoCola aux; //permitira señalar a cada nodo dentro de cola
            if (EstaVacia())
            {
                Console.WriteLine("\nCola esta vacia, no tiene nodos");
            }
            else 
            {
                //Aplicacion de algoritmo FIFO (primero que entra primero que sale)
                Console.Write("\n PIMERO");
                aux = primero;
                do
                {
                    Console.Write("<- {0} ", aux.info);
                    aux = aux.sig;// se desplaza el puntero al siguiente nodo
                } while (aux != null);
                Console.WriteLine(" <- ULTIMO");
            }
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
    }
}
