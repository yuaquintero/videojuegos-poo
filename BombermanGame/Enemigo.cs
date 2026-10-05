using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BombermanGame
{
    /// <summary>
    /// Clase que representa un enemigo en el juego, que hereda de la clase <see cref="Personaje"/>.
    /// El enemigo se mueve de manera automática y cambia de dirección al encontrar una colisión.
    /// </summary>
    internal class Enemigo : Personaje
    {
        private int direccionActual = 1;
        private bool colision = true;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Enemigo"/> en una posición específica.
        /// </summary>
        /// <param name="x">La posición X inicial del enemigo.</param>
        /// <param name="y">La posición Y inicial del enemigo.</param>
        public Enemigo(int x, int y) : base(new Point(x, y), 35, 35, "Enemigo", 3, 80)
        {
        }

        /// <summary>
        /// Mueve el enemigo en una dirección aleatoria. Cambia de dirección al colisionar con una pared sólida o destructible.
        /// </summary>
        /// <param name="paredSolidos">Lista de paredes sólidas con las que puede colisionar.</param>
        /// <param name="paredLadrillos">Lista de paredes destructibles con las que puede colisionar.</param>
        public void Mover(List<ObjetoGrafico> paredSolidos, List<ObjetoGrafico> paredLadrillos)
        {
            // Generador de direcciones aleatorias basado en el tiempo actual
            var seed = Environment.TickCount;
            Random ri = new Random(seed);

            if (!colision)
            {
                // Genera una nueva dirección aleatoria entre 0 y 3 (arriba, abajo, izquierda, derecha)
                direccionActual = ri.Next(0, 4);
                colision = true;
            }
            else
            {
                // Combina ambas listas de paredes para detectar colisiones en todas las direcciones
                List<ObjetoGrafico> todasLasParedes = paredSolidos.Concat(paredLadrillos).ToList();
                switch (direccionActual)
                {
                    case 0:  // Arriba
                        colision = MoverUp(todasLasParedes);
                        break;
                    case 1:  // Abajo
                        colision = MoverDown(todasLasParedes);
                        break;
                    case 2:  // Izquierda
                        colision = MoverLeft(todasLasParedes);
                        break;
                    case 3:  // Derecha
                        colision = MoveRight(todasLasParedes);
                        break;
                }
            }
        }
    }
}
