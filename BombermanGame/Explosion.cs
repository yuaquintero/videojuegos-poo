using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanGame
{
    /// <summary>
    /// Clase que representa una explosión en el juego. Hereda de <see cref="ObjetoGrafico"/>.
    /// La explosión se representa como una serie de frames animados, y puede impactar otros objetos en su área.
    /// </summary>
    internal class Explosion : ObjetoGrafico
    {
        private int frames;
        private int currentFrame;
        private int frameDuration;
        private Bitmap explosionSprites;
        private Rectangle explosionArea;
        private string tipoExplosion;
        private int Contframes = 0;
        private int posx = 0;

        /// <summary>
        /// Obtiene el tipo de explosión, representado por el nombre del sprite asociado.
        /// </summary>
        public string TipoExplosion { get => tipoExplosion; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Explosion"/>.
        /// </summary>
        /// <param name="coordenada">Ubicación de la explosión en el juego, como un <see cref="Point"/>.</param>
        /// <param name="ancho">Ancho de la explosión.</param>
        /// <param name="alto">Alto de la explosión.</param>
        /// <param name="nombreSprite">Nombre del sprite que representa la explosión.</param>
        /// <param name="totalFrames">Cantidad total de frames para la animación.</param>
        /// <param name="tamFrame">Tamaño de cada frame en milisegundos.</param>
        public Explosion(Point coordenada, int ancho, int alto, string nombreSprite, int totalFrames, int tamFrame)
            : base(coordenada.X, coordenada.Y, ancho, alto, nombreSprite)
        {
            frames = totalFrames;
            currentFrame = 0;
            tipoExplosion = nombreSprite;
            frameDuration = tamFrame;
            explosionSprites = (Bitmap)Properties.Resources.ResourceManager.GetObject(nombreSprite);
            explosionArea = new Rectangle(posx, 0, frameDuration, frameDuration);
            Imagen.Image = explosionSprites.Clone(explosionArea, explosionSprites.PixelFormat);
        }

        /// <summary>
        /// Ejecuta un paso en la animación de la explosión, avanzando al siguiente frame.
        /// </summary>
        /// <returns><c>true</c> si la animación ha terminado; de lo contrario, <c>false</c>.</returns>
        public bool Animation()
    {
        explosionArea = new Rectangle(posx, 0, frameDuration, frameDuration);
        Imagen.Image = explosionSprites.Clone(explosionArea, explosionSprites.PixelFormat);
        Contframes++;
        posx += frameDuration;
        return Contframes == frames - 1;
    }

    /// <summary>
    /// Verifica si la explosión impacta algún objeto en una lista dada.
    /// </summary>
    /// <param name="objetos">Lista de objetos a verificar colisión.</param>
    /// <returns>El objeto impactado si hay colisión; de lo contrario, <c>null</c>.</returns>
    public ObjetoGrafico VerificarImpacto(List<ObjetoGrafico> objetos)
    {
        foreach (var obj in objetos)
        {
            if (obj.GetBounds().IntersectsWith(this.GetBounds()))
            {
                return obj;
            }
        }
        return null;
    }
}

    }
