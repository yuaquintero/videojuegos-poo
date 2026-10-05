using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombermanGame
{
    public partial class VtnInfo : Form
    {
        public VtnInfo(int tipo_imagen)
        {
       
            InitializeComponent();

            
            if (tipo_imagen == 1)
            {
                pictureBox1.Image = Properties.Resources.Game_Over_Alt;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.you_Win;
            }


        }

        void SetImagen(int imagen)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
