using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace BombermanGame
{
    public class ParedSolida : ObjetoGrafico
    {
        public ParedSolida(Point coordenada)
            : base(coordenada.X, coordenada.Y, 40, 40, "MuroSolido") {
        
        
        }

    }


    public class ParedDestructible : ObjetoGrafico
    {
        public ParedDestructible(Point coordenada)
            : base(coordenada.X, coordenada.Y, 40, 40, "muro")
        {

        }
    }
}
