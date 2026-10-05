using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;

namespace BombermanGame
{
    /// <summary>
    /// Representa el tablero de juego, incluyendo la generación de muros sólidos, muros destructibles y posiciones de enemigos.
    /// </summary>
    public class Tablero
    {
        // Atributos
        private List<Point> coorMurosSolidos = new List<Point>();    // Coordenadas de muros sólidos
        private List<Point> coorMurosLadrillos = new List<Point>();   // Coordenadas de muros destructibles
        private List<Point> coorEnemigos = new List<Point>();         // Coordenadas de los enemigos

        // Propiedades
        /// <summary>
        /// Obtiene las coordenadas de los muros sólidos en el tablero.
        /// </summary>
        public List<Point> CoorMurosSolidos { get => coorMurosSolidos; }

        /// <summary>
        /// Obtiene las coordenadas de los muros destructibles en el tablero.
        /// </summary>
        public List<Point> CoorMurosLadrillos { get => coorMurosLadrillos; }

        /// <summary>
        /// Obtiene las coordenadas de los enemigos en el tablero.
        /// </summary>
        public List<Point> CoorEnemigos { get => coorEnemigos; }

        // Constructor
        /// <summary>
        /// Constructor de la clase <see cref="Tablero"/> que inicializa el tablero con muros y enemigos.
        /// </summary>
        public Tablero()
        {
            InicializarTablero();
        }

        // Métodos
        /// <summary>
        /// Inicializa el tablero creando los contornos, muros sólidos internos, muros destructibles y posiciones de los enemigos.
        /// </summary>
        private void InicializarTablero()
        {
            // Crear contornos del tablero
            CrearLinea(0, 880, 0); // Línea horizontal superior
            CrearLinea(720, 920, 0);  // Línea horizontal inferior
            CrearLinea(0, 760, 1); // Línea vertical izquierda
            CrearLinea(880, 760, 1); // Línea vertical derecha

            // Crear muros sólidos internos en una cuadrícula con paso de 80 píxeles
            for (int fila = 0; fila < 680; fila += 80)
            {
                for (int columna = 0; columna < 840; columna += 80)
                {
                    Point paredSolida = new Point(columna, fila);
                    coorMurosSolidos.Add(paredSolida);
                }
            }
            CrearParedesDestructibles();
            CrearCoordenadasEnemigos(8); // Generar 8 enemigos
        }

        /// <summary>
        /// Crea una línea de muros sólidos en una dirección especificada.
        /// </summary>
        /// <param name="inicio">Coordenada inicial.</param>
        /// <param name="fin">Coordenada final.</param>
        /// <param name="direccion">Dirección de la línea (0: horizontal, 1: vertical).</param>
        private void CrearLinea(int inicio, int fin, int direccion)
        {
            for (int i = 0; i < fin; i += 40)
            {
                Point coordenada;
                coordenada = direccion == 1 ? new Point(inicio, i) : new Point(i, inicio);
                coorMurosSolidos.Add(coordenada);
            }
        }

        /// <summary>
        /// Crea muros destructibles aleatoriamente en el tablero, evitando colocar en ubicaciones iniciales específicas.
        /// </summary>
        public void CrearParedesDestructibles()
        {
            Random random = new Random();

            for (int fila = 40; fila < 720; fila += 40)
            {
                for (int columna = 40; columna < 880; columna += 40)
                {
                    // Evitar las ubicaciones iniciales
                    if ((fila == 40 && columna == 80) || (fila == 40 && columna == 40) || (fila == 80 && columna == 40))
                        continue;

                    // Verificar si ya hay una pared sólida en esa posición
                    bool existePared = coorMurosSolidos.Any(coor => coor.X == columna && coor.Y == fila);

                    // Agregar un muro destructible con probabilidad de 1 en 3 si no hay una pared sólida
                    if (!existePared && random.Next(0, 3) == 0)
                    {
                        Point paredDestructible = new Point(columna, fila);
                        coorMurosLadrillos.Add(paredDestructible);
                    }
                }
            }
        }

        /// <summary>
        /// Genera posiciones aleatorias para una cantidad especificada de enemigos, evitando posiciones bloqueadas.
        /// </summary>
        /// <param name="cantEnemigos">Número de enemigos a colocar en el tablero.</param>
        public void CrearCoordenadasEnemigos(int cantEnemigos)
        {
            int fila, columna;
            int tamañoCelda = 40;
            Random random = new Random();
            int contEnemigos = 0;

            while (contEnemigos < cantEnemigos)
            {
                fila = random.Next(0, 17);
                columna = random.Next(0, 21);
                Point posicionActual = new Point(columna * tamañoCelda, fila * tamañoCelda);

                // Evitar ubicaciones ocupadas por paredes y ubicaciones iniciales
                if (coorMurosLadrillos.Contains(posicionActual) || coorMurosSolidos.Contains(posicionActual))
                    continue;
                else if ((fila == 40 && columna == 80) || (fila == 40 && columna == 40) || (fila == 80 && columna == 40))
                    continue;

                coorEnemigos.Add(posicionActual);
                contEnemigos++;
            }
        }
    }
}
