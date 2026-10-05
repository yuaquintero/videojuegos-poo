using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BombermanGame
{ 
/// <summary>
/// Clase que representa una bomba en el juego, capaz de generar una explosión que
/// afecta a objetos dentro de su rango, incluyendo paredes y enemigos.
/// </summary>
internal class Bomba : ObjetoGrafico
{
    private int Contframes;
    private int tamFrame;
    private Rectangle rect;
    private Bitmap bmp;
    private int posX;
    private int tiempo = 0;

    /// <summary>Lista de las explosiones generadas por la bomba.</summary>
    public List<Explosion> explosiones = new List<Explosion>();

    /// <summary>Lista de objetos impactados por la explosión.</summary>
    public List<ObjetoGrafico> objetosImpactados = new List<ObjetoGrafico>();

    /// <summary>Rango de la explosión en bloques.</summary>
    public int rangoExplosion = 1;

    /// <summary>Tiempo en milisegundos antes de la detonación de la bomba.</summary>
    public int Tiempo { get => tiempo; set => tiempo = value; }

    /// <summary>
    /// Constructor de la clase Bomba.
    /// </summary>
    /// <param name="x">Posición en el eje X de la bomba.</param>
    /// <param name="y">Posición en el eje Y de la bomba.</param>
    public Bomba(int x, int y) : base(x, y, 38, 38, "Bomba")
    {
        tamFrame = 80;
        Contframes = 0;
        bmp = (Bitmap)Properties.Resources.Bomba;
        rect = new Rectangle(posX, 0, tamFrame, tamFrame);
        Imagen.Image = bmp.Clone(rect, bmp.PixelFormat);
    }

    /// <summary>
    /// Actualiza la animación de la bomba, cambiando el frame en el que se encuentra.
    /// </summary>
    public void Animation()
    {
        rect = new Rectangle(posX, 0, tamFrame, tamFrame);
        Imagen.Image = bmp.Clone(rect, bmp.PixelFormat);
        Contframes++;
        posX += tamFrame;
        if (Contframes == 2)
        {
            Contframes = 0;
            posX = tamFrame;
        }
    }

    /// <summary>
    /// Inicia la detonación de la bomba, generando explosiones en las direcciones 
    /// especificadas y verificando los objetos impactados.
    /// </summary>
    /// <param name="paredesSólidas">Lista de paredes sólidas en el juego.</param>
    /// <param name="paredesDestructibles">Lista de paredes destructibles en el juego.</param>
    /// <param name="enemigos">Lista de enemigos en el juego.</param>
    public void Detonar(List<ObjetoGrafico> paredesSólidas, List<ObjetoGrafico> paredesDestructibles, List<ObjetoGrafico> enemigos)
    {
        // Coloca la explosión inicial en la posición de la bomba
       
        Explosion explosion = new Explosion(new Point(x, y), 40, 40, "explosionCentro", 1, 80);
        explosiones.Add(explosion);
            // Genera explosiones en las cuatro direcciones
        RevisarDireccion(0, -1, paredesSólidas, paredesDestructibles, enemigos); // Arriba
        RevisarDireccion(0, 1, paredesSólidas, paredesDestructibles, enemigos);  // Abajo
        RevisarDireccion(-1, 0, paredesSólidas, paredesDestructibles, enemigos); // Izquierda
        RevisarDireccion(1, 0, paredesSólidas, paredesDestructibles, enemigos);  // Derecha
    }

    /// <summary>
    /// Genera una serie de explosiones en la dirección especificada y verifica colisiones con objetos.
    /// </summary>
    /// <param name="dx">Dirección en el eje X (0 para ninguna, 1 para derecha, -1 para izquierda).</param>
    /// <param name="dy">Dirección en el eje Y (0 para ninguna, 1 para abajo, -1 para arriba).</param>
    /// <param name="paredesSólidas">Lista de paredes sólidas.</param>
    /// <param name="paredesDestructibles">Lista de paredes destructibles.</param>
    /// <param name="enemigos">Lista de enemigos.</param>
    private void RevisarDireccion(int dx, int dy, List<ObjetoGrafico> paredesSólidas, List<ObjetoGrafico> paredesDestructibles, List<ObjetoGrafico> enemigos)
    {
        Explosion explosion;
     
            int nuevaX = x + (dx *  40);
            int nuevaY = y + (dy *  40);

            ObjetoGrafico objetoImpactado = VerificarColision(nuevaX, nuevaY, paredesSólidas, paredesDestructibles, enemigos);

            if (objetoImpactado != null)
            {
                objetosImpactados.Add(objetoImpactado);
                string animacion = DeterminarAnimacion(objetoImpactado);
                if (animacion != "ParedSolida")
                {
                    explosion = new Explosion(new Point(nuevaX, nuevaY), 40, 40, animacion, 5, (animacion == "explosionDestructible" ? 60 : 80));
                    explosiones.Add(explosion);
                }
              
            }
            else
            {
                string animacion = (dx != 0) ? "explosionHorizontal" : "explosionVertical";
                explosion = new Explosion(new Point(nuevaX, nuevaY), 40, 40, animacion, 1, 80);
                explosiones.Add(explosion);
            }
       
    }

    /// <summary>
    /// Verifica si la posición de la explosión colisiona con alguno de los objetos en las listas proporcionadas.
    /// </summary>
    /// <param name="nuevaX">Nueva posición en X de la explosión.</param>
    /// <param name="nuevaY">Nueva posición en Y de la explosión.</param>
    /// <param name="listasDeObjetos">Listas de objetos a verificar (paredes sólidas, destructibles y enemigos).</param>
    /// <returns>El objeto impactado, o null si no hay colisión.</returns>
    private ObjetoGrafico VerificarColision(int nuevaX, int nuevaY, params List<ObjetoGrafico>[] listasDeObjetos)
    {
        Rectangle nuevaPosicion = new Rectangle(nuevaX, nuevaY, 40, 40);

        foreach (var lista in listasDeObjetos)
        {
            foreach (var objeto in lista)
            {
                if (objeto.GetBounds().IntersectsWith(nuevaPosicion))
                {
                    return objeto;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Determina el tipo de animación para la explosión según el objeto impactado.
    /// </summary>
    /// <param name="objeto">El objeto impactado por la explosión.</param>
    /// <returns>El tipo de animación de la explosión.</returns>
    private string DeterminarAnimacion(ObjetoGrafico objeto)
    {
        if (objeto is ParedDestructible)
        {
            return "explosionDestructible";
        }
        else if (objeto is Enemigo)
        {
            return "explosionEnemigo";
        }
        else
        {
            return "ParedSolida";
        }
    }
  }
}