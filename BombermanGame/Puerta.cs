using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanGame
{
    /// <summary>
    /// Representa una puerta en el juego, que puede ser activada para permitir al jugador avanzar o ganar.
    /// Hereda de <see cref="ObjetoGrafico"/> y utiliza la posición y tamaño definidos.
    /// </summary>
    internal class Puerta : ObjetoGrafico
    {
        // Indica si la puerta está activa o no
        private bool activa;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Puerta"/> en una ubicación específica,
        /// estableciendo su tamaño y la imagen correspondiente.
        /// </summary>
        /// <param name="ubicacion">La ubicación de la puerta en el tablero, como un <see cref="Point"/>.</param>
        public Puerta(Point ubicacion) : base(ubicacion.X, ubicacion.Y, 40, 40, "puerta")
        {
            this.activa = false;
        }

        /// <summary>
        /// Obtiene o establece el estado de activación de la puerta.
        /// </summary>
        public bool Activa { get => activa; set => activa = value; }
    }
}
