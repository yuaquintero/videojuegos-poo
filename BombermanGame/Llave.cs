using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanGame
{
    // <summary>
    /// Clase que representa una llave en el juego, la cual puede estar activa o inactiva. 
    /// Hereda de la clase <see cref="ObjetoGrafico"/>.
    /// </summary>
    internal class Llave : ObjetoGrafico
    {
        private bool activa;

        /// <summary>
        /// Obtiene o establece el estado de la llave (activa o inactiva).
        /// </summary>
        public bool Activa
        {
            get => activa;
            set => activa = value;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Llave"/> en una ubicación específica.
        /// La llave se crea inactiva por defecto.
        /// </summary>
        /// <param name="ubicacion">Ubicación de la llave en el juego, especificada como un <see cref="Point"/> con coordenadas X e Y.</param>
        public Llave(Point ubicacion) : base(ubicacion.X, ubicacion.Y, 40, 40, "Llave")
        {
            activa = false;
        }
    }
}
