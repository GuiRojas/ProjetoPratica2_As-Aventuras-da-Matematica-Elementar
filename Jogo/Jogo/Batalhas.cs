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
using System.IO;
using System.Runtime.InteropServices;

namespace Jogo
{


    public class Batalha1 : GameClass
    {
        protected Bitmap batalhaImg;
        protected ProgressBar pbVidaHeroi = new ProgressBar();
        protected ProgressBar pbVidaVilao = new ProgressBar();
        protected ProgressBar pbTempo = new ProgressBar();
        protected Label lblContinha = new Label();
        protected Label lblResultado = new Label();
        protected System.Windows.Forms.Timer tmrBatalha;
        protected Background background;
        protected bool podeAtacar = true;
        protected ListBox lbContasRelatorio;
        protected Queue<string> contas;

        public Batalha1(Background background)
        {
            this.background = background;
        }

        public void keyUp()
        {

        }

        public virtual void keyDown(Object sender, KeyEventArgs e)
        {
            Char n = '\0';

            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                n = '1';
            }
            if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                n = '2';
            }
            if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {
                n = '3';
            }
            if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
            {
                n = '4';
            }
            if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5)
            {
                n = '5';
            }
            if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6)
            {
                n = '6';
            }
            if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7)
            {
                n = '7';
            }
            if (e.KeyCode == Keys.D8 || e.KeyCode == Keys.NumPad8)
            {
                n = '8';
            }
            if (e.KeyCode == Keys.D9 || e.KeyCode == Keys.NumPad9)
            {
                n = '9';
            }
            if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
            {
                n = '0';
            }

            lblResultado.Text += n;

            if (e.KeyCode == Keys.Back)
            {
                if (lblResultado.Text != "")
                {
                    String str = lblResultado.Text;
                    str = str.Remove(str.Length - 1);
                    lblResultado.Text = str;
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (lblResultado.Text == "inicio")
                {
                    tmrBatalha.Enabled = true;
                    tmrBatalha.Start();
                    lblResultado.Visible = true;
                    lblResultado.Text = "";
                    lblContinha.Text = Conta.gerarSoma(background.Dificuldade);
                    contas.Enqueue(lblContinha.Text);
                }
                else if (lblResultado.Text != "" && podeAtacar)
                {
                    causarDano();
                }
            }
        }

        public void paint(Object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(batalhaImg, 0, 0, Game.Largura * Game.Tam, Game.Altura * Game.Tam);
        }

        public void tick(object sender, EventArgs e)
        {

        }

        public void carregarGame()
        {
            background.Frm.Controls.Add(pbVidaVilao);
            pbVidaVilao.Maximum = 100;
            pbVidaVilao.Minimum = 0;
            pbVidaVilao.Enabled = true;
            pbVidaVilao.Location = new Point(75, 72);
            pbVidaVilao.Width = 320;
            pbVidaVilao.Height = 40;
            pbVidaVilao.Value = 100;

            background.Frm.Controls.Add(pbVidaHeroi);
            pbVidaHeroi.Maximum = 100;
            pbVidaHeroi.Minimum = 0;
            pbVidaHeroi.Enabled = true;
            pbVidaHeroi.Location = new Point(410, 360);
            pbVidaHeroi.Width = 320;
            pbVidaHeroi.Height = 40;
            pbVidaHeroi.Value = 100;

            background.Frm.Controls.Add(lblContinha);
            lblContinha.Width = 500;
            lblContinha.Text = "[Enter] para comecar!";
            lblContinha.Location = new Point((background.Frm.Width - lblContinha.Width) / 2, 470);
            lblContinha.ForeColor = Color.Black;
            lblContinha.BackColor = Color.White;
            lblContinha.TextAlign = ContentAlignment.MiddleCenter;
            lblContinha.Dock = DockStyle.None;
            lblContinha.Font = new Font("Segoe UI", 30);
            lblContinha.Height = 50;

            background.Frm.Controls.Add(lblResultado);
            lblResultado.Height = 70;
            lblResultado.Width = 350;
            lblResultado.Location = new Point((background.Frm.Width - lblResultado.Width) / 2, 530);
            lblResultado.Text = "inicio";
            lblResultado.AutoSize = false;
            lblResultado.TextAlign = ContentAlignment.MiddleCenter;
            lblResultado.Dock = DockStyle.None;
            lblResultado.BorderStyle = BorderStyle.FixedSingle;
            lblResultado.BackColor = Color.White;
            lblResultado.ForeColor = Color.Black;
            lblResultado.Font = new Font("Segoe UI", 30);
            lblResultado.Visible = false;

            background.Frm.Controls.Add(pbTempo);
            pbTempo.Width = 730;
            pbTempo.Height = 25;
            pbTempo.Location = new Point((background.Frm.Width - pbTempo.Width) / 2, 10);
            pbTempo.Maximum = 100;
            pbTempo.Minimum = 0;
            pbTempo.Value = 100;
            pbTempo.Enabled = true;
            ModifyProgressBarColor.SetState(pbTempo, 3);

            tmrBatalha = new System.Windows.Forms.Timer();
            tmrBatalha.Interval = 140 / this.background.Dificuldade;
            tmrBatalha.Tick += new EventHandler(batalha_tick);

            batalhaImg = new Bitmap(@"batalha.png");

            contas = new Queue<string>();
        }

        public void terminarBatalha()
        {
            gerarRelatorio();
        }

        public void gerarRelatorio ()
        {
            background.Frm.Controls.Add(lbContasRelatorio);

            foreach (string conta in contas){
                lbContasRelatorio.Items.Add(conta);
            }

            background.Frm.Controls.Remove(lblContinha);
            background.Frm.Controls.Remove(lblResultado);
            background.Frm.Controls.Remove(pbTempo);
            background.Frm.Controls.Remove(pbVidaHeroi);
            background.Frm.Controls.Remove(pbVidaVilao);
            background.transicao(3);
        }

        public virtual void receberDano(ProgressBar pb, int dano)
        {
            podeAtacar = false;

            ModifyProgressBarColor.SetState(pb, 2);
            tmrBatalha.Stop();
            newLabel lblDaninho = new newLabel();
            lblDaninho.Text = "-" + dano.ToString();
            lblDaninho.ForeColor = Color.Red;
            lblDaninho.BackColor = Color.White;
            lblDaninho.Font = new Font("Segoe UI", 20);
            lblDaninho.Height = pb.Height;
            lblDaninho.RotateAngle = -90;
            background.Frm.Controls.Add(lblDaninho);

            if (pb.Location.X > Convert.ToInt32(background.Frm.Width / 2))
            {
                lblDaninho.Location = new Point(pb.Location.X - lblDaninho.Width, pb.Location.Y);
            }
            else
            {
                lblDaninho.Location = new Point(pb.Location.X + pb.Width, pb.Location.Y);
            }

            int i = 0;
            while (i <= dano)
            {
                pb.Value--;

                if (pb.Value == 0)
                {
                    background.Frm.Controls.Remove(lblDaninho);
                    terminarBatalha();
                    return;
                }
                else
                {
                    Application.DoEvents();
                    Thread.Sleep(20);
                    i++;
                }
            }

            podeAtacar = true;

            ModifyProgressBarColor.SetState(pb, 1);

            lblContinha.Text = Conta.gerarSoma(background.Dificuldade);
            lblResultado.Text = "";
            background.Frm.Controls.Remove(lblDaninho);

            tmrBatalha.Start();
        }

        public void causarDano()
        {
            contas.Enqueue(lblContinha.Text);

            pbTempo.Value = pbTempo.Maximum;

            if (Conta.resolver(lblContinha.Text).ToString() == lblResultado.Text)
            {
                int dano = Convert.ToInt32((10));
                receberDano(pbVidaVilao, 50);
                //todo tremer tela
            }
            else
            {
                int dano = 5 * background.Dificuldade + 5;
                receberDano(pbVidaHeroi, dano);
            }
        }

        public void batalha_tick(Object sender, EventArgs e)
        {
            if (pbTempo.Value == 0)
            {
                receberDano(pbVidaHeroi, background.Dificuldade);
                pbTempo.Value = pbTempo.Maximum;
            }
            else
            {
                pbTempo.Value--;
            }
        }

        public void keyUp(object sender, KeyEventArgs e)
        {
            //
        }
    }

    public class Batalha2 : Batalha1
    {
        Background background;

        public Batalha2(Background background) : base(background)
        {
            this.background = background;
        }

        public override void keyDown(object sender, KeyEventArgs e)
        {
            Char n = '\0';

            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                n = '1';
            }
            if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                n = '2';
            }
            if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {
                n = '3';
            }
            if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
            {
                n = '4';
            }
            if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5)
            {
                n = '5';
            }
            if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6)
            {
                n = '6';
            }
            if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7)
            {
                n = '7';
            }
            if (e.KeyCode == Keys.D8 || e.KeyCode == Keys.NumPad8)
            {
                n = '8';
            }
            if (e.KeyCode == Keys.D9 || e.KeyCode == Keys.NumPad9)
            {
                n = '9';
            }
            if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
            {
                n = '0';
            }

            lblResultado.Text += n;

            if (e.KeyCode == Keys.Back)
            {
                if (lblResultado.Text != "")
                {
                    String str = lblResultado.Text;
                    str = str.Remove(str.Length - 1);
                    lblResultado.Text = str;
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (lblResultado.Text == "inicio")
                {
                    tmrBatalha.Enabled = true;
                    tmrBatalha.Start();
                    lblResultado.Visible = true;
                    lblResultado.Text = "";
                    lblContinha.Text = Conta.gerarSubtracao(background.Dificuldade);
                }
                else if (lblResultado.Text != "" && podeAtacar)
                {
                    causarDano();
                }
            }
        }

        public override void receberDano(ProgressBar pb, int dano)
        {
            base.receberDano(pb, dano);
            lblContinha.Text = Conta.gerarSubtracao(background.Dificuldade);
        }
    }

    public class Batalha3 : Batalha1
    {
        Background background;

        public Batalha3(Background background) : base(background)
        {
            this.background = background;
        }

        public override void keyDown(object sender, KeyEventArgs e)
        {
            Char n = '\0';

            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                n = '1';
            }
            if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                n = '2';
            }
            if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {
                n = '3';
            }
            if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
            {
                n = '4';
            }
            if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5)
            {
                n = '5';
            }
            if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6)
            {
                n = '6';
            }
            if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7)
            {
                n = '7';
            }
            if (e.KeyCode == Keys.D8 || e.KeyCode == Keys.NumPad8)
            {
                n = '8';
            }
            if (e.KeyCode == Keys.D9 || e.KeyCode == Keys.NumPad9)
            {
                n = '9';
            }
            if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
            {
                n = '0';
            }

            lblResultado.Text += n;

            if (e.KeyCode == Keys.Back)
            {
                if (lblResultado.Text != "")
                {
                    String str = lblResultado.Text;
                    str = str.Remove(str.Length - 1);
                    lblResultado.Text = str;
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (lblResultado.Text == "inicio")
                {
                    tmrBatalha.Enabled = true;
                    tmrBatalha.Start();
                    lblResultado.Visible = true;
                    lblResultado.Text = "";
                    lblContinha.Text = Conta.gerarMultiplicacao(background.Dificuldade);
                }
                else if (lblResultado.Text != "" && podeAtacar)
                {
                    causarDano();
                }
            }
        }

        public override void receberDano(ProgressBar pb, int dano)
        {
            base.receberDano(pb, dano);
            lblContinha.Text = Conta.gerarMultiplicacao(background.Dificuldade);
        }
    }

    public class Batalha4 : Batalha1
    {
        Background background;

        public Batalha4(Background background) : base(background)
        {
            this.background = background;
        }

        public override void keyDown(object sender, KeyEventArgs e)
        {
            Char n = '\0';

            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                n = '1';
            }
            if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                n = '2';
            }
            if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {
                n = '3';
            }
            if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
            {
                n = '4';
            }
            if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5)
            {
                n = '5';
            }
            if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6)
            {
                n = '6';
            }
            if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7)
            {
                n = '7';
            }
            if (e.KeyCode == Keys.D8 || e.KeyCode == Keys.NumPad8)
            {
                n = '8';
            }
            if (e.KeyCode == Keys.D9 || e.KeyCode == Keys.NumPad9)
            {
                n = '9';
            }
            if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
            {
                n = '0';
            }

            lblResultado.Text += n;

            if (e.KeyCode == Keys.Back)
            {
                if (lblResultado.Text != "")
                {
                    String str = lblResultado.Text;
                    str = str.Remove(str.Length - 1);
                    lblResultado.Text = str;
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (lblResultado.Text == "inicio")
                {
                    tmrBatalha.Enabled = true;
                    tmrBatalha.Start();
                    lblResultado.Visible = true;
                    lblResultado.Text = "";
                    lblContinha.Text = Conta.gerarDivisao(background.Dificuldade);
                }
                else if (lblResultado.Text != "" && podeAtacar)
                {
                    causarDano();
                }
            }
        }

        public override void receberDano(ProgressBar pb, int dano)
        {
            base.receberDano(pb, dano);
            lblContinha.Text = Conta.gerarDivisao(background.Dificuldade);
        }
    }
}
