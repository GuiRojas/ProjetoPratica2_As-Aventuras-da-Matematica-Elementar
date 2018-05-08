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
        protected ListBox lbContasRelatorio = new ListBox();
        protected Queue<string> contas;
        protected int ganhou = 0;
        protected Label lblContinuar;
        protected Label lblResultadoFinal;

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
                    lblContinha.Text = this.gerarConta();
                }
                else if (lblResultado.Text != "" && podeAtacar)
                {
                    causarDano();
                }
                else if (ganhou > 0)
                {
                    background.Frm.Controls.Remove(lblResultadoFinal);
                    background.Frm.Controls.Remove(lblContinuar);
                    background.Frm.Controls.Remove(lbContasRelatorio);

                    if (ganhou == 1)
                        background.transicao(3);

                    if (ganhou == 2)
                        background.transicao(1);
                }
            }
        }

        public virtual String gerarConta()
        {
            return Conta.gerarSoma(background.Dificuldade);
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
            lblContinha.Font = new Font(frmJogo.FONTE, 18);
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
            lblResultado.Font = new Font(frmJogo.FONTE, 18);
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

            carregarImg(@"batalha1.png");

            contas = new Queue<string>();
        }

        protected virtual void carregarImg(string str)
        {
            batalhaImg = new Bitmap(@"batalha_1.png");
        }

        public void terminarBatalha()
        {

            background.Frm.Controls.Remove(lblContinha);
            background.Frm.Controls.Remove(lblResultado);
            background.Frm.Controls.Remove(pbTempo);
            background.Frm.Controls.Remove(pbVidaHeroi);
            background.Frm.Controls.Remove(pbVidaVilao);

            gerarRelatorio();

        }

        public void gerarRelatorio()
        {
            background.Frm.Controls.Add(lbContasRelatorio);
            lbContasRelatorio.Location = new Point(140, 100);
            lbContasRelatorio.Width = 520;
            lbContasRelatorio.Height = 400;
            lbContasRelatorio.Enabled = false;
            lbContasRelatorio.BackColor = Color.FromArgb(255, 224, 192);
            Font font = new Font(frmJogo.FONTE, 24);
            lbContasRelatorio.Font = font;
            this.batalhaImg = new Bitmap(@"relatorio.png");

            foreach (string conta in contas)
            {
                lbContasRelatorio.Items.Add(conta + " = " + Conta.resolver(conta).ToString());
            }

            Font font2 = new Font(frmJogo.FONTE, 18);

            lblResultadoFinal = new Label();
            lblResultadoFinal.Location = new Point(0, 20);
            lblResultadoFinal.Font = font2;
            lblResultadoFinal.Height = 50;
            lblResultadoFinal.Width = 800;
            lblResultadoFinal.AutoSize = false;
            lblResultadoFinal.TextAlign = ContentAlignment.MiddleCenter;
            lblResultadoFinal.Dock = DockStyle.None;
            lblResultadoFinal.BackColor = Color.FromArgb(83, 51, 34);
            lblResultadoFinal.ForeColor = Color.FromArgb(255, 255, 255);
            background.Frm.Controls.Add(lblResultadoFinal);

            if (ganhou == 1)
            {
                lblResultadoFinal.Text = "Você ganhou!";
            }
            else
            {
                lblResultadoFinal.Text = "Você perdeu.";
            }

            lblContinuar = new Label();
            lblContinuar.BackColor = Color.FromArgb(83, 51, 34);
            lblContinuar.ForeColor = Color.FromArgb(255, 255, 255);
            lblContinuar.Location = new Point(0, 550);
            lblContinuar.Height = 50;
            lblContinuar.Font = font2;
            lblContinuar.Text = "[Enter] para continuar";
            lblContinuar.Width = 800;
            lblContinuar.AutoSize = false;
            lblContinuar.TextAlign = ContentAlignment.MiddleCenter;
            lblContinuar.Dock = DockStyle.None;
            background.Frm.Controls.Add(lblContinuar);
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
            lblDaninho.Font = new Font(frmJogo.FONTE, 20);
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
                    if (pb.Equals(pbVidaHeroi))
                    {
                        ganhou = 2;
                    }
                    else
                    {
                        ganhou = 1;
                    }
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

            lblContinha.Text = this.gerarConta();
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
                receberDano(pbVidaVilao, 100);
                //todo tremer tela
            }
            else
            {
                int dano = 10 * background.Dificuldade + 10;
                receberDano(pbVidaHeroi, dano);
            }
        }

        public void batalha_tick(Object sender, EventArgs e)
        {
            if (pbTempo.Value == 0)
            {
                int dano = 10 * background.Dificuldade + 10;
                receberDano(pbVidaHeroi, dano);

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

        public override String gerarConta()
        {
            return Conta.gerarSubtracao(background.Dificuldade);
        }

        protected override void carregarImg(string str)
        {
            batalhaImg = new Bitmap(@"batalha_2.png");
        }
    }

    public class Batalha3 : Batalha1
    {
        Background background;

        public Batalha3(Background background) : base(background)
        {
            this.background = background;
        }

        public override String gerarConta()
        {
            return Conta.gerarMultiplicacao(background.Dificuldade);
        }

        protected override void carregarImg(string str)
        {
            batalhaImg = new Bitmap(@"batalha_3.png");
        }
    }

    public class Batalha4 : Batalha1
    {

        Background background;

        public Batalha4(Background background) : base(background)
        {
            this.background = background;
        }

        public override String gerarConta()
        {
            return Conta.gerarDivisao(background.Dificuldade);
        }
    }
}
