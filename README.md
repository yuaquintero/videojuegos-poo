# Proyectos de Videojuegos - Curso de POO

Este repositorio contiene una colección de videojuegos desarrollados durante el curso de **Programación Orientada a Objetos (POO)**.

## 🎮 Juegos Incluidos

### 1. BombermanGame
* **Descripción:** Recreación del clásico juego Bomberman aplicando conceptos de POO.
* **Tecnología:** C# (.NET) / Windows Forms.
* **Componentes clave:** Implementación mediante controles **PictureBox** para renderizar al personaje, bombas y obstáculos.
* **Diagrama:** [Ver Diagrama de Clases de Bomberman](./BombermanGame/BombermanDiagramaClases.png)

### 2. PacmanGame
* **Descripción:** Implementación del juego Pac-Man con movimiento de fantasmas y recolección de puntos.
* **Tecnología:** C# (.NET) / Windows Forms.
* **Componentes clave:** Uso de **PictureBox** para la gestión de colisiones del escenario, fantasmas y del jugador.
* **Diagrama:** [Ver Diagrama de Clases de Pacman](./PacmanGame/Diagrama_clases_pacman.png)

### 3. SpaceInvaders
* **Descripción:** Juego retro de naves espaciales con mecánicas de disparo y control de oleadas de enemigos.
* **Tecnología:** C# (.NET) / Windows Forms.
* **Componentes clave:** Renderizado gráfico avanzado mediante la clase **System.Drawing** (`Graphics`, `Pen`, `Brush`, etc.) sobre el lienzo en lugar de `PictureBox` estáticos.

---

## 🛠️ Conceptos de POO Aplicados

* **Herencia y Polimorfismo:** Para la gestión de personajes, enemigos y entidades del juego.
* **Encapsulamiento:** Protección de datos internos de las clases (puntaje, vidas, posiciones).
* **Abstracción:** Modelado del bucle principal del juego y renderizado de gráficos.

---

## 🚀 Cómo ejecutar los proyectos

1. Clona este repositorio:
   ```bash
   git clone [https://github.com/yuaquintero/videojuegos-poo.git](https://github.com/yuaquintero/videojuegos-poo.git)

2. Abre la solución (.sln) o las carpetas de los proyectos en Visual Studio o VS Code.

3. Compila y ejecuta la aplicación (tecla F5).
