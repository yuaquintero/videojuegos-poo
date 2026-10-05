using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BombermanGame
{
    /// <summary>
    /// Representa el personaje Bomberman, que hereda de la clase Personaje. Incluye propiedades adicionales como vidas y puntaje,
    /// además de una animación específica de muerte.
    /// </summary>
    internal class Bomberman : Personaje
    {
        // Número de vidas del personaje
        int vidas;

        // Puntaje acumulado del jugador
        int puntaje = 0;

        // SpriteSheet que contiene los frames de la animación de explosión
        private Bitmap explosionSprites;

        // Área del SpriteSheet que muestra el frame actual de la animación de explosión
        private Rectangle explosionArea;

        // Variables de control para la animación de muerte
        int xFrame = 0;
        int contFrame = 0;
        int tamFrame = 80;
        //propiedades
        // Estado del personaje que indica si está en proceso de morir
        private bool estaMuriendo = false;

        /// <summary>
        /// Inicializa una nueva instancia de la clase Bomberman con una posición inicial y configuraciones de animación.
        /// </summary>
        /// <param name="x">Posición X inicial de Bomberman.</param>
        /// <param name="y">Posición Y inicial de Bomberman.</param>
        public Bomberman(int x, int y)
            : base(new Point(x, y), 35, 35, "Bomber", 3, 80)
        {
            vidas = 6;
        }

        /// <summary>
        /// Obtiene o establece el puntaje acumulado por el jugador.
        /// </summary>
        public int Puntaje { get => puntaje; set => puntaje = value; }

        /// <summary>
        /// Obtiene o establece el estado de Bomberman, indicando si está en proceso de morir.
        /// </summary>
        public bool EstaMuriendo { get => estaMuriendo; set => estaMuriendo = value; }

        /// <summary>
        /// Obtiene o establece el número de vidas restantes de Bomberman.
        /// </summary>
        public int Vidas { get => vidas; set => vidas = value; }

        /// <summary>
        /// Ejecuta la animación de muerte de Bomberman. Avanza el frame de la animación hasta completarla.
        /// </summary>
        /// <returns>Devuelve true si la animación de muerte ha finalizado, false si continúa en proceso.</returns>
        public bool AnimacionMuerte()
        {
            // Carga el SpriteSheet de la animación de muerte
            explosionSprites = (Bitmap)Properties.Resources.BomberDie;

            // Define el área de recorte del frame actual en el SpriteSheet
            explosionArea = new Rectangle(xFrame, 0, tamFrame, tamFrame);

            // Actualiza la imagen de Bomberman con el frame actual de la animación
            Imagen.Image = explosionSprites.Clone(explosionArea, explosionSprites.PixelFormat);

            // Incrementa el contador de frames
            contFrame++;

            // Desplaza el área de recorte al siguiente frame en el SpriteSheet
            xFrame += tamFrame;

            // Verifica si se ha alcanzado el último frame de la animación
            if (contFrame == 7)  // Fin de la animación
            {
                estaMuriendo = false;
                contFrame = 0;
                xFrame = 0;
                return true;  // Indica que la animación de muerte ha finalizado
            }

            return false;  // La animación de muerte sigue en proceso
        }
    }
}