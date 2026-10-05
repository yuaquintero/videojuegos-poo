using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using NAudio.Wave;

namespace BombermanGame
{
   

        /// <summary>
        /// Clase que maneja la reproducción de efectos de sonido en el juego.
        /// Utiliza el tipo de sonido especificado para reproducir el audio correspondiente.
        /// </summary>
        internal class Audio
        {
            private SoundPlayer player;
            private int tipo;

            /// <summary>
            /// Inicializa una nueva instancia de la clase <see cref="Audio"/> con un tipo específico de sonido.
            /// </summary>
            /// <param name="tipo">Especifica el tipo de sonido que se reproducirá:
            /// 1: Sonido de inicio del juego,
            /// 2: Sonido de colocar bomba,
            /// 3: Sonido de explosión,
            /// 4: Sonido de victoria,
            /// 5: Sonido de derrota.</param>
            public Audio(int tipo)
            {
                this.tipo = tipo;
            }

            /// <summary>
            /// Reproduce el sonido correspondiente al tipo especificado en el constructor.
            /// </summary>
            public void ReproducirAudio()
            {
                // Selección del archivo de sonido basado en el tipo especificado
                switch (tipo)
                {
                    case 1:
                        player = new SoundPlayer(Properties.Resources.bomberman_start);
                        break;
                    case 2:
                        player = new SoundPlayer(Properties.Resources.put);
                        break;
                    case 3:
                        player = new SoundPlayer(Properties.Resources.Explosicion);
                        break;
                    case 4:
                        player = new SoundPlayer(Properties.Resources.win);
                        break;
                    case 5:
                        player = new SoundPlayer(Properties.Resources.die);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(tipo), "Tipo de sonido no válido.");
                }

                player.Play();
            }
        } 
}
