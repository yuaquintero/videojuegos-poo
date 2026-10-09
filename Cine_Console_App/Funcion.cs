using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineAPP
{
    public class Funcion
    {
        //atributos

        string nombrePelicula;
        string genero;
        string horario;
        string idioma;
        int duracion;
        public Funcion()
        {
        }
        public Funcion(string nombrePelicula, string genero, 
            string horario, string idioma, int duracion)
        {
            this.nombrePelicula = nombrePelicula;
            this.genero = genero;
            this.horario = horario;
            this.idioma = idioma;
            this.duracion = duracion;
        }

        public string NombrePelicula { get => nombrePelicula;}

        public string MostrarInformacion()
        {
            return "Nombre película: " + nombrePelicula + "\nGénero: " + genero +
                "\nHorario: " + horario + "\nIdioma:" + idioma + "\nDuración: "
                + duracion + " min";
        }
    }
}
