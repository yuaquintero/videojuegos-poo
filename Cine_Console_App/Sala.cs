using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CineAPP
{
    public class Sala
    {
        //atributos
        int numSala;
        Funcion funcion;
        char[,] mapaOcupacion =new char[10,11];

        public char[,] MapaOcupacion { get => mapaOcupacion;}
        public Funcion Funcion { get => funcion;}

        public Sala() { }
        public Sala(int numSala, Funcion funcion)
        {
            this.numSala= numSala;
            this.funcion = funcion;
            LlenarMatriz();
        }
        /// <summary>
        /// Metodo que sirve para inicializar el mapa de ocupación con todos los ascientos vacios
        /// </summary>
        void LlenarMatriz()
        {
            for(int i=0; i<10;i++)
            {
                for (int j = 0; j < 11; j++)
                    mapaOcupacion[i, j] = '-';
            }
        }

        public bool ReservarAsientos(char fila, int asiento)
        {
            fila=char.ToUpper(fila); 
            int filaInt=(int)fila-65;
            if (mapaOcupacion[filaInt,asiento-1]=='-')
            {
                mapaOcupacion[filaInt, asiento - 1] = '+';
                return true;
            }
            else { return false;}              
        }
        public bool CancelarAsientos(char fila, int asiento)
        {
            fila = char.ToUpper(fila);
            int filaInt = (int)fila - 65;
            if (mapaOcupacion[filaInt, asiento - 1] == '+')
            {
                mapaOcupacion[filaInt, asiento - 1] = '-';
                return true;
            }
            else { return false; }
        }
    }
}
