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
        bool esperando = false;
        System.Drawing.Text.PrivateFontCollection privateFonts = new System.Drawing.Text.PrivateFontCollection();
        public static FontFamily FONTE;

        public frmJogo()
        {
            InitializeComponent();
            privateFonts.AddFontFile(@"PressStart2P.ttf");
            FONTE = privateFonts.Families[0];

        }

        private void Jogo_Load(object sender, EventArgs e)
        {
            /*
            if (faseDoBD == 0)
                continuar.enabled = false;
            */
            //this.carregar();

            /////provisorio
            btnContinuar.Enabled = true;
            /////

            System.Drawing.Font font = new Font(privateFonts.Families[0], 24);
            System.Drawing.Font font2 = new Font(privateFonts.Families[0], 18);
            lbl1.Font = font;
            lbl2.Font = font;
            btnComecar.Font = font2;
            btnContinuar.Font = font2;
            btnSair.Font = font2;
            btnOnline.Font = font2;
        }

        private void carregar (int fase)
        {
            background = new Background(fase, this);// TODO pegar do bd a fase do usuario
            background.carregarEstadoEFase();
        }

        private void tmrMain_Tick(object sender, EventArgs e)
        {
            if (background != null)
                background.tick(sender, e);
            Invalidate();
        }
        
        private void frmJogo_Paint(object sender, PaintEventArgs e)
        {
            if (background != null)
                background.paint(sender, e);
        }

        private void frmJogo_KeyDown(object sender, KeyEventArgs e)
        {
            if (background != null)
                background.keyDown(sender, e);

            if (e.KeyCode == Keys.Enter && esperando)
            {
                this.Controls.Remove(lbl1);
                esperando = false;
                carregar(1);
            }
        }

        private void frmJogo_KeyUp(object sender, KeyEventArgs e)
        {
            if (background != null)
               background.keyUp(sender, e);
        }

        private void frmJogo_Load(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnComecar_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(lbl2);
            this.Controls.Remove(btnComecar);
            this.Controls.Remove(btnSair);
            this.Controls.Remove(btnContinuar);
            this.Controls.Remove(btnOnline);


            lbl1.Width = 600;
            lbl1.Height = 800;
            lbl1.Dock = DockStyle.None;
            
            lbl1.Location = new Point(100, 20);
            lbl1.MaximumSize = new Size(600, 0);
            lbl1.AutoSize = true;
            this.BackColor = Color.FromArgb(0,0,0);
            lbl1.BackColor = Color.FromArgb(0, 0, 0);
            this.BackgroundImage = null;
            lbl1.Font = new Font(privateFonts.Families[0], 18);
            lbl1.Text = "";
            string a = "  Seu nome é Shingetsu Kun, e é um samurai muito respeitado. Sua vida inteira você usou a força para resolver seus problemas, até agora.\n\n  Você volta de sua viagem e chega em sua vila. Está tudo diferente, vazio, destruído; você se depara com Senpaio, o sensei da matemática.\n\n\n\n\n\nUse as setas para se locomover e [E] para interagir com os outros personagens.\n[Enter] para continuar.";

            for (int i =0; i < a.Length; i++)
            {
                lbl1.Text += a.ElementAt(i);
                Thread.Sleep(10);
                this.Refresh();
            }
            esperando = true;
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(lbl2);
            this.Controls.Remove(lbl1);
            this.Controls.Remove(btnComecar);
            this.Controls.Remove(btnSair);
            this.Controls.Remove(btnContinuar);
            this.Controls.Remove(btnOnline);

            carregar(4);
        }
    }
}
