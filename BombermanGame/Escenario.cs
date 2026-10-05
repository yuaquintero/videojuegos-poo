using BombermanGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BombermanGame
{
    public partial class Escenario : Form
    {
        Tablero tablero = new Tablero();
        List<ObjetoGrafico> paredesSolidas = new List<ObjetoGrafico>();
        List<ObjetoGrafico> paredesDestructibles = new List<ObjetoGrafico>();
        List<ObjetoGrafico> enemigos = new List<ObjetoGrafico>();
        List<ObjetoGrafico> vidas = new List<ObjetoGrafico>();

        Personaje personaje = null;
        Bomberman bomberman = null;
        List<Bomba> bombas = new List<Bomba>();
        Audio audio = null;
        int tiempo = 0;
        Puerta puerta = null;
        Llave llave = null;
        Point ubicacionPuerta;
        List<Explosion> Explosiones = new List<Explosion>();
        public Escenario()
        {
            InitializeComponent();
        }

        private void Escenario_Paint(object sender, PaintEventArgs e)
        {
            // tablero.DibujarTablero(e.Graphics);
        }
        /// <summary>
        /// Configura el escenario inicial del juego: muros sólidos, ladrillos y enemigos.
        /// </summary>
        private void CrearEscenario()
        {
            var CoorMuroSolido = tablero.CoorMurosSolidos;
            var CoorMuroLadrillo = tablero.CoorMurosLadrillos;
            var CoorEnemigos = tablero.CoorEnemigos;
            // Crea y agrega los muros sólidos al escenario
            foreach (var coor in CoorMuroSolido)
            {
                ParedSolida pared = new ParedSolida(coor);
                paredesSolidas.Add(pared);
                this.Controls.Add(pared.Imagen);
            }
            // Crea y agrega los muros de ladrillo destructibles

            foreach (var coor in CoorMuroLadrillo)
            {
                ParedDestructible paredDestructible = new ParedDestructible(coor);
                paredesDestructibles.Add(paredDestructible);
                this.Controls.Add(paredDestructible.Imagen);

            }
            // Crea y agrega los enemigos
            foreach (var coor in CoorEnemigos)
            {
                Enemigo enemigo = new Enemigo(coor.X, coor.Y);
                enemigos.Add(enemigo);
                this.Controls.Add(enemigo.Imagen);
            }
        }
        /// <summary>
        /// Carga el escenario, los elementos visuales y el sonido cuando el formulario se carga.
        /// </summary>

        private void Escenario_Load(object sender, EventArgs e)
        {
            CargarElementos();
            CrearEscenario();
            audio = new Audio(1);
            audio.ReproducirAudio();
            CargarVidas();
            CrearPuertaLlave();
            label1.Text = "Para mover el\n personaje";

        }
        /// <summary>
        /// Carga las vidas visuales del personaje en la interfaz.
        /// </summary>
        private void CargarVidas()
        {
            for (int i = 0; i < 5; i++)
            {
                ObjetoGrafico objVida = new ObjetoGrafico((10 + i * 30), 30, 20, 20, "vida");
                vidas.Add(objVida);
                this.panel1.Controls.Add(objVida.Imagen);
            }
        }
        /// <summary>
        /// Controla la animación del personaje y el movimiento automático de los enemigos en cada tick del temporizador.
        /// </summary>
        private void timerAnimacion_Tick(object sender, EventArgs e)
        {

            if (!bomberman.EstaMuriendo)
                bomberman.Animation();
            else
            {
                // Inicia animación de muerte y resetea la posición del personaje si la animación termina
                if (bomberman.AnimacionMuerte())
                {
                    audio = new Audio(5);
                    audio.ReproducirAudio();
                    System.Threading.Thread.Sleep(200);
                    bomberman.setPos(40, 40);
                }
            }
            // Mueve y anima a cada enemigo
            foreach (Enemigo ene in enemigos)
            {
                ene.Mover(paredesSolidas, paredesDestructibles);
                ene.Animation();

            }
            // Detecta colisiones con enemigos
            if (bomberman.EvaluarColision(enemigos) && !bomberman.EstaMuriendo)
            {
                BombermanMuere();
            }
        }
        /// <summary>
        /// Carga el personaje principal en la posición inicial.
        /// </summary>
        private void CargarElementos()
        {
            bomberman = new Bomberman(40, 40);
            this.Controls.Add(bomberman.Imagen);

        }
        /// <summary>
        /// Controla el movimiento del personaje con las teclas y la colocación de bombas.
        /// </summary>

        private void Escenario_KeyPress(object sender, KeyPressEventArgs e)
        {

            List<ObjetoGrafico> todasLasParedes = new List<ObjetoGrafico>();
            todasLasParedes.AddRange(paredesSolidas);
            todasLasParedes.AddRange(paredesDestructibles);
            char letra = e.KeyChar;
            letra = char.ToUpper(letra);

            switch (letra)
            {
                case 'W':
                    bomberman.MoverUp(todasLasParedes);
                    break;
                case 'S':
                    bomberman.MoverDown(todasLasParedes);
                    break;
                case 'D':
                    bomberman.MoveRight(todasLasParedes);
                    break;
                case 'A':
                    bomberman.MoverLeft(todasLasParedes);
                    break;
                case ' ':
                    // Coloca una bomba alineada a la cuadrícula de 40x40

                    int nuevaX = (bomberman.X / 40) * 40;  // Alinea a la cuadrícula
                    int nuevaY = (bomberman.Y / 40) * 40;
                    Bomba bomba = new Bomba(nuevaX, nuevaY);
                    bombas.Add(bomba);
                    this.Controls.Add(bomba.Imagen);
                    timerBomba.Enabled = true;
                    audio = new Audio(2);
                    audio.ReproducirAudio();
                    break;
                default:
                    break;

            }
        }
        /// <summary>
        /// Controla la detonación de las bombas, afectando las paredes, enemigos y generando explosiones.
        /// </summary>
        private void timerBomba_Tick(object sender, EventArgs e)
        {
            if (bombas.Count == 0)
            {
                timerBomba.Enabled = false;  // Desactivar timer solo cuando no quedan bombas
                return;
            }

            for (int i = bombas.Count - 1; i >= 0; i--)
            {
                var bomba = bombas[i];
                bomba.Animation();
                bomba.Tiempo++;
                // Si el tiempo de la bomba ha pasado, detona y crea explosiones

                if (bomba.Tiempo > 5)
                {
                    bomba.Detonar(paredesSolidas, paredesDestructibles, enemigos);
                    Explosiones = bomba.explosiones;

                    var ObjetosImpactados = bomba.objetosImpactados;
                    // Remueve la bomba visualmente

                    this.Controls.Remove(bomba.Imagen);
                    bombas.RemoveAt(i);  // Eliminar bomba

                    foreach (var exp in Explosiones)
                    {
                        this.Controls.Add(exp.Imagen);
                        audio = new Audio(3);
                        audio.ReproducirAudio();
                    }

                    // Maneja los objetos impactados por la explosión

                    foreach (var ObjImpactado in ObjetosImpactados)
                    {

                        if (ObjImpactado is ParedDestructible || ObjImpactado is Enemigo)
                        {
                            this.Controls.Remove(ObjImpactado.Imagen);
                            if (ObjImpactado is ParedDestructible)
                            {
                                bomberman.Puntaje += 1;
                                if (ObjImpactado.Ubicacion == puerta.Ubicacion)
                                {
                                    this.Controls.Add(puerta.Imagen);
                                    puerta.Activa = true;
                                }
                                // Agrega puerta o llave si es la ubicación de alguno

                                if (ObjImpactado.Ubicacion == llave.Ubicacion)
                                {
                                    this.Controls.Add(llave.Imagen);
                                    llave.Activa = true;
                                }
                                paredesDestructibles.Remove(ObjImpactado);
                            }
                            if (ObjImpactado is Enemigo)
                            {
                                enemigos.Remove(ObjImpactado);
                                bomberman.Puntaje += 20;

                            }

                            lblPuntaje.Text = "Puntaje : " + bomberman.Puntaje;
                        }
                    }
                    // Reproduce sonido y muestra mensaje de victoria si se activa la llave y puerta

                    if (llave.Activa && puerta.Activa)
                    {
                        audio = new Audio(4);
                        audio.ReproducirAudio();

                        if (tiempo < 120)
                        {
                            bomberman.Puntaje += 200;

                        }
                        System.Threading.Thread.Sleep(200);
                        VtnInfo vtnInfo = new VtnInfo(2);
                        vtnInfo.Show();
                        TerminarJuego();

                    }
                }
            }

        }
        /// <summary>
        /// Controla la desaparición de las explosiones en la pantalla después de un breve período.
        /// </summary>
        private void timerDetonacion_Tick(object sender, EventArgs e)
        {
            if (Explosiones.Count > 0)
            {
                for (int i = Explosiones.Count - 1; i >= 0; i--)
                {
                    var exp = Explosiones[i];

                    // Verificar si la explosión impacta a Bomberman y si no está en animación de muerte
                    if (bomberman.EvaluarColision(exp))
                    {
                        BombermanMuere();
                    }

                    // Control de animación y eliminación de la explosión
                    if (exp.TipoExplosion == "explosionDestructible" || exp.TipoExplosion == "explosionEnemigo")
                    {
                        // Llamada a la animación específica si es destructible o enemigo
                        bool animacionTerminada = exp.Animation();
                        if (animacionTerminada)
                        {
                            this.Controls.Remove(exp.Imagen);
                            Explosiones.RemoveAt(i);
                        }
                    }
                    else
                    {
                        // Explosiones sin animación que desaparecen después de un breve periodo
                        System.Threading.Thread.Sleep(100);
                        {
                            this.Controls.Remove(exp.Imagen);
                            Explosiones.RemoveAt(i);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Crea la puerta y la llave que apreceran debajo de uno de los muros destructibles,
        /// seleccionado de manera aleatoria dos posiciones de la lista de paredes destructibles
        /// </summary>

        void CrearPuertaLlave()
        {
            Random random = new Random();
            Point posicionPuerta = paredesDestructibles[random.Next(paredesDestructibles.Count)].Ubicacion;

            Point posicionLlave;

            while (true)
            {
                posicionLlave = paredesDestructibles[random.Next(paredesDestructibles.Count)].Ubicacion;
                if (posicionLlave != posicionPuerta)
                    break;
            }
            puerta = new Puerta(posicionPuerta);
            llave = new Llave(posicionLlave);

        }
        private void timerJuego_Tick(object sender, EventArgs e)
        {
            tiempo++;
            lblTiempo.Text = "Tiempo: " + tiempo.ToString();
        }

        /// <summary>
        /// Realiza el control de las vidas del personaje principal 
        /// </summary>

        void BombermanMuere()
        {
            // Iniciar la animación de muerte si es impactado y no está en proceso

            // Controlar el final de la animación
            bomberman.EstaMuriendo = true;
            bomberman.Vidas--;
            // Aquí podrías restar una vida o manejar el final del juego
            this.panel1.Controls.Remove(vidas[vidas.Count - 1].Imagen);  // Quitar la vida de la interfaz
            vidas.RemoveAt(vidas.Count - 1);  // Eliminar una vida visualmente
            if (bomberman.Vidas == 1)
            {
                VtnInfo vtnInfo = new VtnInfo(1);
                vtnInfo.Show();
                TerminarJuego();
            }

        }
        /// <summary>
        /// El juego termina cuando se ha logrado el objetivo del juego, encontrar la puerta y llave o cuando bomberman ha perdido 
        /// todas sus vidas. Se detienen todos los temporizadores
        /// </summary>

        void TerminarJuego()
        {
            timerAnimacion.Stop();
            timerDetonacion.Stop();
            timerBomba.Stop();
            timerJuego.Stop();

        }
    }
}
