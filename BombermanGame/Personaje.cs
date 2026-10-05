using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BombermanGame
{

    /// <summary>
    /// Representa un personaje en el juego que hereda de ObjetoGrafico, con capacidad de movimiento y animación.
    /// </summary>
    internal class Personaje: ObjetoGrafico
    {
        //atributos
        // Velocidad de movimiento del personaje
        int velocidad = 3;

        // Contador y límite de frames para la animación
        int Contframes;
        int frames;

        // Tamaño de cada frame en la animación
        int tamFrame;

        // Rectángulo que define la posición y tamaño de cada frame dentro del bitmap
        Rectangle rect;

        // Bitmap que contiene la imagen de la animación del personaje
        Bitmap bmp;

        // Coordenadas de la posición dentro del frame
        int posX = 0, posY = 0;

        /// <summary>
        /// Inicializa una nueva instancia de la clase Personaje con las coordenadas, tamaño, nombre de recurso de imagen y detalles de animación.
        /// </summary>
        /// <param name="coordenada">Posición inicial del personaje.</param>
        /// <param name="w">Ancho del personaje.</param>
        /// <param name="h">Altura del personaje.</param>
        /// <param name="nombre">Nombre del recurso de imagen del personaje.</param>
        /// <param name="Frames">Número total de frames en la animación.</param>
        /// <param name="TamFrame">Tamaño de cada frame en la animación.</param>
        public Personaje(Point coordenada, int w, int h, string nombre, int Frames, int TamFrame):
            base(coordenada.X, coordenada.Y,w,h,nombre)
        {
            frames = Frames;
            tamFrame = TamFrame;
            Contframes=0;
            bmp = (Bitmap)Properties.Resources.ResourceManager.GetObject(nombre);
            rect = new Rectangle(posX, posY, tamFrame, tamFrame);
            Imagen.BackColor = Color.Transparent;
            Imagen.Image = bmp.Clone(rect, bmp.PixelFormat);
        }
        /// <summary>
        /// Actualiza la imagen del personaje para simular animación mediante la selección del siguiente frame.
        /// </summary>
        public void Animation()
        {
            rect = new Rectangle(posX, posY, tamFrame, tamFrame);
            Imagen.Image = bmp.Clone(rect, bmp.PixelFormat);
            Contframes++;
            posX += tamFrame;
            if (Contframes == frames-1)
            {
                Contframes = 0;
                posX = tamFrame;
            }

        }
        /// <summary>
        /// Mueve el personaje hacia arriba si no colisiona con otros objetos.
        /// </summary>
        /// <param name="objetos">Lista de objetos gráficos a verificar para evitar colisiones.</param>
        /// <returns>Devuelve true si el movimiento fue exitoso, false si hubo colisión.</returns>     
        public bool MoverUp(List<ObjetoGrafico> objetos)
        {
            return Mover(0, -1, tamFrame * 3, objetos);

        }
        /// <summary>
        /// Mueve el personaje hacia abajo si no colisiona con otros objetos.
        /// </summary>
        /// <param name="objetos">Lista de objetos gráficos a verificar para evitar colisiones.</param>
        /// <returns>Devuelve true si el movimiento fue exitoso, false si hubo colisión.</returns>
        public bool MoverDown(List<ObjetoGrafico> objetos) { 
            return Mover(0, 1, tamFrame * 2, objetos);
        }
        /// <summary>
        /// Mueve el personaje hacia la izquierda si no colisiona con otros objetos.
        /// </summary>
        /// <param name="objetos">Lista de objetos gráficos a verificar para evitar colisiones.</param>
        /// <returns>Devuelve true si el movimiento fue exitoso, false si hubo colisión.</returns>
        public bool MoverLeft(List<ObjetoGrafico> objetos)
        {
            return Mover(-1, 0, 0, objetos);
        }
        /// <summary>
        /// Mueve el personaje hacia la derecha si no colisiona con otros objetos.
        /// </summary>
        /// <param name="objetos">Lista de objetos gráficos a verificar para evitar colisiones.</param>
        /// <returns>Devuelve true si el movimiento fue exitoso, false si hubo colisión.</returns>
        public bool MoveRight(List<ObjetoGrafico> objetos)
        {
            return Mover(1, 0, tamFrame, objetos);

        }
        /// <summary>
        /// Método auxiliar que maneja el movimiento del personaje en una dirección específica, verificando colisiones.
        /// </summary>
        /// <param name="dx">Dirección en el eje X.</param>
        /// <param name="dy">Dirección en el eje Y.</param>
        /// <param name="nuevaPosY">Nueva posición en Y del frame de animación.</param>
        /// <param name="objetos">Lista de objetos gráficos a verificar para evitar colisiones.</param>
        /// <returns>Devuelve true si el movimiento fue exitoso, false si hubo colisión.</returns>
        public bool Mover(int dx, int dy, int nuevaPosY, List<ObjetoGrafico> objetos)
        {
            Rectangle nuevaPosicion = this.GetBounds();
            nuevaPosicion.X += dx * velocidad;
            nuevaPosicion.Y += dy * velocidad;

            // Verificar si colisiona con algún objeto antes de mover
            if (!objetos.Any(o => o.GetBounds().IntersectsWith(nuevaPosicion)))
            {
                posY = nuevaPosY;
                setPos(nuevaPosicion.X, nuevaPosicion.Y);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Evalúa si el personaje colisiona con algún objeto de una lista de objetos gráficos.
        /// </summary>
        /// <param name="objetos">Lista de objetos gráficos a verificar.</param>
        /// <returns>Devuelve true si colisiona con algún objeto, de lo contrario false.</returns>
        public bool EvaluarColision(List<ObjetoGrafico> objetos)
        {
            foreach (var item in objetos)
            {
                if(item.GetBounds().IntersectsWith(this.GetBounds()))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Evalúa si el personaje colisiona con un objeto gráfico específico.
        /// </summary>
        /// <param name="objeto">Objeto gráfico a verificar.</param>
        /// <returns>Devuelve true si colisiona con el objeto, de lo contrario false.</returns>
        public bool EvaluarColision(ObjetoGrafico objeto)
        {
                if (objeto.GetBounds().IntersectsWith(this.GetBounds()))
                    return true;
            return false;
        }
    }
}
