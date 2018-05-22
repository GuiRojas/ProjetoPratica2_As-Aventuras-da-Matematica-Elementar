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
        protected System.Media.SoundPlayer sound { get; set; }
        bool esc = false;
        Button btnResumir;
        Button btnMenu;
        Bitmap fundo = new Bitmap(@"relatorio.png");

        public frmJogo(string str)
        {
            InitializeComponent();
            privateFonts.AddFontFile(@"PressStart2P.ttf");
            FONTE = privateFonts.Families[0];
            this.CenterToScreen();
            btnContinuar.Enabled = false;
        }

        public System.Media.SoundPlayer Sound
        {
            get
            {
                return this.sound;
            }
            set
            {
                this.sound = value;
            }
        }

        private void Jogo_Load(object sender, EventArgs e)
        {
            btnContinuar.Enabled = true;
            /*
            if (faseDoBD == 0)
                continuar.enabled = false;
            */
            //this.carregar();

            /////provisorio
            /////

            System.Drawing.Font font = new Font(privateFonts.Families[0], 24);
            System.Drawing.Font font2 = new Font(privateFonts.Families[0], 18);
            lbl1.Font = font;
            lbl2.Font = font;
            btnComecar.Font = font2;
            btnContinuar.Font = font2;
            btnSair.Font = font2;
            btnOnline.Font = font2;
            sound = new System.Media.SoundPlayer();

            btnMenu = new Button();
            btnMenu.Font = btnComecar.Font;
            btnMenu.Size = btnComecar.Size;
            btnMenu.Location = btnComecar.Location;
            btnMenu.FlatStyle = FlatStyle.Flat;
            btnMenu.Click += voltarAoMenu;
            btnMenu.Text = "Voltar ao menu";
            btnMenu.ForeColor = btnComecar.ForeColor;
            btnMenu.BackColor = btnComecar.BackColor;

            btnResumir = new Button();
            btnResumir.Font = btnComecar.Font;
            btnResumir.Size = btnComecar.Size;
            btnResumir.Location = btnContinuar.Location;
            btnResumir.FlatStyle = FlatStyle.Flat;
            btnResumir.Click += nSei;
            btnResumir.Text = "Resumir";
            btnResumir.ForeColor = btnComecar.ForeColor;
            btnResumir.BackColor = btnComecar.BackColor;
        }

        public void voltarAoMenu (object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            this.Close();
        }

        public void nSei (object sender, EventArgs e)
        {
            esc = false;
            this.Controls.Remove(btnResumir);
            this.Controls.Remove(btnMenu);
        }

        public void setSoundLoop (string str)
        {
            sound = new System.Media.SoundPlayer(str);
            sound.PlayLooping();
        }

        public void playSound (string src)
        {
            new System.Threading.Thread(() => {
                var c = new System.Windows.Media.MediaPlayer();
                c.Open(new System.Uri(src, UriKind.RelativeOrAbsolute));
                Thread.Sleep(50);
                c.Play();
            }).Start();
        }

        public void stopSound ()
        {
            sound.Stop();
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

            if (esc)
            {
                e.Graphics.DrawImage(fundo, 0, 0, Game.Largura * Game.Tam, Game.Altura * Game.Tam);
            }
                
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

            if (e.KeyCode == Keys.Escape)
            {
                this.Controls.Add(btnResumir);
                this.Controls.Add(btnMenu);
                esc = true;
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
            playSound("beep.wav");
            this.Close();
        }

        private void btnComecar_Click(object sender, EventArgs e)
        {
            playSound("beep.wav");
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
                //playSound("click.wav");
                Thread.Sleep(10);
                this.Refresh();
            }
            esperando = true;
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            playSound("beep.wav");
            this.Controls.Remove(lbl2);
            this.Controls.Remove(lbl1);
            this.Controls.Remove(btnComecar);
            this.Controls.Remove(btnSair);
            this.Controls.Remove(btnContinuar);
            this.Controls.Remove(btnOnline);

            carregar(4);
        }

        public void setTimerState (bool bol)
        {
            tmrPintar.Enabled = bol;
        }

        private void btnOnline_Click(object sender, EventArgs e)
        {
            playSound("beep.wav");
        }
    }
}
