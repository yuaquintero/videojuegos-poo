using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace BombermanGame
{
    /// <summary>
    /// Clase base para representar objetos gráficos en el juego. Contiene propiedades y métodos comunes
    /// para posicionar, dimensionar y gestionar la imagen de un objeto gráfico.
    /// </summary>
    public class ObjetoGrafico
    {
        // Variables de instancia
        protected int x;  // Posición X del objeto
        protected int y;  // Posición Y del objeto
        protected int h, w;  // Dimensiones del objeto
        private Point ubicacion;
        private string nombreRecurso;

        /// <summary>
        /// Obtiene la posición X del objeto.
        /// </summary>
        public int X { get => x; }

        /// <summary>
        /// Obtiene la posición Y del objeto.
        /// </summary>
        public int Y { get => y; }

        /// <summary>
        /// Obtiene o establece el <see cref="PictureBox"/> que representa la imagen del objeto.
        /// </summary>
        public PictureBox Imagen { get; set; }

        /// <summary>
        /// Obtiene el nombre del recurso de imagen asociado al objeto.
        /// </summary>
        protected string NombreRecurso { get => nombreRecurso; }

        /// <summary>
        /// Obtiene o establece la ubicación del objeto en el tablero como un <see cref="Point"/>.
        /// </summary>
        public Point Ubicacion { get => ubicacion; set => ubicacion = value; }

        /// <summary>
        /// Constructor que inicializa la posición, dimensiones y recurso gráfico del objeto.
        /// </summary>
        /// <param name="x">Posición X del objeto.</param>
        /// <param name="y">Posición Y del objeto.</param>
        /// <param name="ancho">Ancho del objeto.</param>
        /// <param name="alto">Alto del objeto.</param>
        /// <param name="nombre">Nombre del recurso de imagen del objeto en los recursos del proyecto.</param>
        public ObjetoGrafico(int x, int y, int ancho, int alto, string nombre)
        {
            this.x = x;
            this.y = y;
            Imagen = new PictureBox
            {
                Location = new Point(x, y),
                Size = new Size(ancho, alto),
                Image = (Image)Properties.Resources.ResourceManager.GetObject(nombre),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            ubicacion = new Point(x, y);
        }

        /// <summary>
        /// Constructor sin parámetros, permite crear un <see cref="ObjetoGrafico"/> sin inicializar propiedades.
        /// </summary>
        public ObjetoGrafico() { }

        /// <summary>
        /// Obtiene los límites del objeto gráfico como un <see cref="Rectangle"/> que representa su ubicación y tamaño.
        /// </summary>
        /// <returns>Un <see cref="Rectangle"/> con los límites de la imagen del objeto.</returns>
        public virtual Rectangle GetBounds()
        {
            return Imagen.Bounds;
        }

        /// <summary>
        /// Establece la posición del objeto gráfico en el tablero.
        /// </summary>
        /// <param name="x">Nueva posición X del objeto.</param>
        /// <param name="y">Nueva posición Y del objeto.</param>
        public void setPos(int x, int y)
        {
            this.x = x;
            this.y = y;
            Imagen.Location = new Point(x, y);
        }
    }
}

