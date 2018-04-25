using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace Jogo
{   //TODO pegar os niveis do bd
    public partial class frmJogo : Form
    {
        Background background;

        
        public frmJogo()
        {
            InitializeComponent();
        }

        private void Jogo_Load(object sender, EventArgs e)
        {
            background = new Background(2, this);// TODO pegar do bd a fase do usuario
            background.carregarGame();
        }

        private void tmrMain_Tick(object sender, EventArgs e)
        {
            background.tick(sender, e);
            Invalidate();
        }
        
        private void frmJogo_Paint(object sender, PaintEventArgs e)
        {
            background.paint(sender, e);
        }

        private void frmJogo_KeyDown(object sender, KeyEventArgs e)
        {
            background.keyDown(sender, e);
        }

        private void frmJogo_KeyUp(object sender, KeyEventArgs e)
        {
            background.keyUp(sender, e);
        }

        private void frmJogo_Load(object sender, EventArgs e)
        {

        }
    }
}
