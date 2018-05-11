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
    public class Background
    {
        public Background(int fase, frmJogo frm)
        {
            this.fase = fase;
            this.estado = 1;
            this.frm = frm;
            this.dificuldade = 1;//todo pegar do bd os bagulho

            carregarEstadoEFase();
        }

        protected int fase { get; set; }
        protected int estado { get; set; }
        protected frmJogo frm { get; set; }
        protected int dificuldade { get; set; }
        protected bool heroiGanhou;

        GameClass gameClass;
        
        SolidBrush brush_transicao = new SolidBrush(Color.Black);
        Rectangle rect_transicao = new Rectangle(0, 0, Game.Tam * Game.Largura, Game.Tam * Game.Altura);
        
        public int Estado
        {
            get
            {
                return this.estado;
            }
            set
            {
                this.estado = value;
            }
        }

        public int Fase
        {
            get
            {
                return this.fase;
            }
            set
            {
                this.fase = value;
            }
        }

        public frmJogo Frm
        {
            get
            {
                return this.frm;
            }
            set
            {
                this.frm = value;
            }
        }

        public int Dificuldade
        {
            get
            {
                return this.dificuldade;
            }
            set
            {
                this.dificuldade = value;
            }
        }

        public void carregarEstadoEFase()
        {
            frm.setTimerState(false);

            this.gameClass = null;

            switch (estado)
            {
                case 0:
                    {
                        this.gameClass = new Fim (this);
                    }
                    break;

                case 1:
                    {
                        switch (fase)
                        {
                            case 1:
                                {
                                    this.gameClass = new Mapa1(this);
                                }
                                break;

                            case 2:
                                {
                                    this.gameClass = new Mapa2(this);
                                }
                                break;

                            case 3:
                                {
                                    this.gameClass = new Mapa3(this);
                                }
                                break;

                            case 4:
                                {
                                    this.gameClass = new Mapa4(this);
                                }
                                break;
                        }
                    }
                    break;
                    
                case 2:
                    {
                        switch (fase)
                        {
                            case 1:
                                {
                                    this.gameClass = new Batalha1(this);
                                }
                                break;

                            case 2:
                                {
                                    this.gameClass = new Batalha2(this);
                                }
                                break;

                            case 3:
                                {
                                    this.gameClass = new Batalha3(this);
                                }
                                break;

                            case 4:
                                {
                                    this.gameClass = new Batalha4(this);
                                }
                                break;
                        }
                    }
                    break;
                    
                case 3:
                    {
                        switch (fase)
                        {
                            case 1:
                                {
                                    this.gameClass = new Mapa1_pos(this);
                                }
                                break;

                            case 2:
                                {
                                    this.gameClass = new Mapa2_pos(this);
                                }
                                break;

                            case 3:
                                {
                                    this.gameClass = new Mapa3_pos(this);
                                }
                                break;

                            case 4:
                                {
                                    this.gameClass = new Mapa4_pos(this);
                                }
                                break;
                        }
                    }
                    break;

                default:
                        MessageBox.Show("O jogo não pôde ser iniciado :(");
                    break;
            }

            gameClass.carregarGame();
            frm.setTimerState(true);
        }

        public async Task transicao (int est)
        {
            this.estado = 0;

            for (int i = 0; i <= 255; i += 7)
            {
                brush_transicao = new SolidBrush(Color.FromArgb(i, 0, 0, 0));
                frm.Refresh();
                Thread.Sleep(1);
            }

            this.estado = est;
            carregarEstadoEFase();
        }

        public void iniciarBatalha()
        {
            transicao(2);
        }

        public void terminarGame ()
        {
            transicao(0);
        }
        
        public void carregarGame ()
        {
            gameClass.carregarGame();
        }
        
        public void tick (object sender, EventArgs e)
        {
            gameClass.tick(sender, e);
        }

        public void paint (object sender, PaintEventArgs e)
        {
            gameClass.paint(sender, e);

            if (estado == 0)
            {
                e.Graphics.FillRectangle(brush_transicao, rect_transicao);
                e.Graphics.DrawRectangle(new Pen(Color.Black), rect_transicao);
            }//todo colocar no paint duma classe
        }
        
        public void keyDown(object sender, KeyEventArgs e)
        {
            gameClass.keyDown(sender, e);

            if (e.KeyCode == Keys.P)
            {
                frm.Close();
            }
        }

        public void keyUp (object sender, KeyEventArgs e)
        {
            gameClass.keyUp(sender, e);
        }

        public void playSound(string src)
        {
            new System.Threading.Thread(() => {
                var c = new System.Windows.Media.MediaPlayer();
                c.Open(new System.Uri(src, UriKind.RelativeOrAbsolute));
                Thread.Sleep(10);
                c.Play();
            }).Start();
        }
    }
    
    public interface GameClass
    {
        void keyUp(object sender, KeyEventArgs e);

        void keyDown(object sender, KeyEventArgs e);

        void paint(object sender, PaintEventArgs e);

        void tick(object sender, EventArgs e);

        void carregarGame();
    }

    public class Fim : GameClass
    {
        protected Background background;
        protected Label lbl;
        protected Label lbl2;
        protected Label lbl3;

        void GameClass.keyUp(object sender, KeyEventArgs e)
        {
            
        }

        void GameClass.keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                System.Diagnostics.Process.Start(Application.ExecutablePath);
                background.Frm.Close();

                //todo recomecar os niveis do bd
            }
        }

        void GameClass.paint(object sender, PaintEventArgs e)
        {
            
        }

        void GameClass.tick(object sender, EventArgs e)
        {
            
        }

        void GameClass.carregarGame()
        {
            background.playSound(@"sound.wav");
            background.Frm.BackColor = Color.Black;
            Thread.Sleep(1000);
            Label lbl = new Label();
            background.Frm.Controls.Add(lbl);
            lbl.Width = 500;
            lbl.Text = "Fim!";
            lbl.Location = new Point((background.Frm.Width - lbl.Width) / 2, 280);
            lbl.ForeColor = Color.White;
            lbl.BackColor = Color.Black;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Dock = DockStyle.None;
            lbl.Font = new Font(frmJogo.FONTE, 24);
            lbl.Height = 50;
            Label lbl2 = new Label();
            background.Frm.Controls.Add(lbl2);
            lbl2.Width = 500;
            lbl2.Text = "Jogo por João Lucas Alvarez e Guilherme Rojas";
            lbl2.Location = new Point((background.Frm.Width - lbl2.Width) / 2, 340);
            lbl2.ForeColor = Color.White;
            lbl2.BackColor = Color.Black;
            lbl2.TextAlign = ContentAlignment.MiddleCenter;
            lbl2.Dock = DockStyle.None;
            lbl2.Font = new Font(frmJogo.FONTE, 18);
            lbl2.Height = 50;
            Label lbl3 = new Label();
            background.Frm.Controls.Add(lbl3);
            lbl3.Width = 500;
            lbl3.Text = "[Enter] para voltar ao menu.";
            lbl3.Location = new Point((background.Frm.Width - lbl3.Width) / 2, 445);
            lbl3.ForeColor = Color.White;
            lbl3.BackColor = Color.Black;
            lbl3.TextAlign = ContentAlignment.MiddleCenter;
            lbl3.Dock = DockStyle.None;
            lbl3.Font = new Font(frmJogo.FONTE, 18);
            lbl3.Height = 50;
        }

        public Fim(Background background)
        {
            this.background = background;
        }
    }

    public class ObjNpc : ObjGame
    {
        protected bool mostrarTexto { get; set; }
        protected bool iniciaBataha { get; set; }
        protected string msg { get; set; }
        protected bool terminaGame { get; set; } = false;

        protected Queue<String> mensagens;

        public ObjNpc(int xN, int yN, Bitmap imgN, Queue<String> mensagensN, bool iniciaBataha) : base(xN, yN, imgN)
        {
            mensagens = mensagensN;
            this.iniciaBataha = iniciaBataha;
        }

        public ObjNpc(int xN, int yN, Bitmap imgN, Queue<String> mensagensN, bool iniciaBataha, bool terminaGame) : base(xN, yN, imgN)
        {
            mensagens = mensagensN;
            this.terminaGame = terminaGame;
        }

        public async Task dialogoAsync(Background b, Boolean bol)
        {
            mostrarTexto = true;
            if (mensagens.Count == 0)
            {
                if (bol)
                    b.iniciarBatalha();

                if (terminaGame)
                    b.terminarGame();
            }
            else
            {
                msg = mensagens.Dequeue();
            }
        }

        public bool MostrarTexto
        {
            get
            {
                return this.mostrarTexto;
            }

            set
            {
                this.mostrarTexto = value;
            }
        }

        public bool IniciaBatalha
        {
            get
            {
                return this.iniciaBataha;
            }

            set
            {
                this.iniciaBataha = value;
            }
        }

        public string Msg
        {
            get
            {
                return this.msg;
            }

            set
            {
                this.msg = value;
            }
        }
    }

    public class ObjEstatico : ObjGame
    {
        public ObjEstatico(int xN, int yN, Bitmap imgN) : base(xN, yN, imgN)
        {

        }
    }

    public class ObjHeroi : ObjGame
    {
        protected bool activeLeft { get; set; }
        protected bool activeRight { get; set; }
        protected bool activeUp { get; set; }
        protected bool activeDown { get; set; }

        public bool ActiveDown
        {
            get
            {
                return this.activeDown;
            }
            set
            {
                this.activeDown = value;
            }
        }

        public bool ActiveLeft
        {
            get
            {
                return this.activeLeft;
            }
            set
            {
                this.activeLeft = value;
            }
        }

        public bool ActiveRight
        {
            get
            {
                return this.activeRight;
            }
            set
            {
                this.activeRight = value;
            }
        }

        public bool ActiveUp
        {
            get
            {
                return this.activeUp;
            }
            set
            {
                this.activeUp = value;
            }
        }

        public ObjHeroi(int xN, int yN, Bitmap imgN) : base(xN, yN, imgN)
        {

        }

        public void mover(Game game, Background b)
        {
            if (this.activeUp && game.checkup(this))
            {
                goup();
            }

            if (this.activeDown && game.checkdown(this))
            {
                godown();
            }

            if (this.activeLeft && game.checkleft(this))
            {
                goleft();
            }

            if (this.activeRight && game.checkright(this))
            {
                goright();
            }

            if (this.activeRight || this.activeLeft || this.activeDown || this.activeUp)
            {
                b.playSound(@"passo.wav");
                Thread.Sleep(200);
            }
        }

        public void goup()
        {
            this.Y -= 1;
        }

        public void godown()
        {
            this.Y += 1;
        }

        public void goleft()
        {
            this.X -= 1;
        }

        public void goright()
        {
            this.X += 1;
        }
    }

    public class ObjGame
    {
        protected int x { get; set; }
        protected int y { get; set; }
        protected Bitmap img { get; set; }

        public ObjGame(int xN, int yN, Bitmap imgN)
        {
            this.x = xN;
            this.y = yN;
            this.img = imgN;
        }

        public int X
        {
            get
            {
                return this.x;
            }

            set
            {
                this.x = value;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }

            set
            {
                this.y = value;
            }
        }

        public Bitmap Img
        {
            get
            {
                return this.img;
            }

            set
            {
                this.img = value;
            }
        }
    }

    public class Game
    {
        protected bool[][] grid = new bool[25][];//25x20
        public const int Tam = 32, Altura = 20, Largura = 25, Speed = 1;

        public Game()
        {
            for (int i = 0; i < Largura; i++)
            {
                grid[i] = new bool[20];
                for (int j = 0; j < Altura; j++)
                {
                    grid[i][j] = true;
                }
            }
        }

        public static bool perto(ObjGame obj1, ObjGame obj2)
        {
            if ((obj1.X == (obj2.X - 1) && obj1.Y == obj2.Y) || //y igual a esquerda
                (obj1.X == (obj2.X + 1) && obj1.Y == obj2.Y) || //y igual a direita
                (obj1.Y == (obj2.Y - 1) && obj1.X == obj2.X) || //x igual a abaixo
                (obj1.Y == (obj2.Y + 1) && obj1.X == obj2.X))   //x igual acima
            {
                return true;
            }

            return false;
        }

        public static bool checarPosicao(ObjGame obj, int x, int y)
        {
            if (obj.X == x && obj.Y == y)
            {
                return true;
            }

            return false;
        }

        public void setOcupado(int xNovo, int yNovo)
        {
            if (xNovo < 0 && xNovo > Largura)
            {
                //throw exception
            }

            if (yNovo < 0 && yNovo > Altura)
            {
                //throw exception
            }

            grid[xNovo][yNovo] = false;
        }

        public void setLivre(int xNovo, int yNovo)
        {
            if (xNovo < 0 && xNovo > Largura)
            {
                //throw exception
            }

            if (yNovo < 0 && yNovo > Altura)
            {
                //throw exception
            }

            grid[xNovo][yNovo] = true;
        }

        public bool checkup(ObjGame obj)
        {
            if (obj.Y == 0)
                return false;

            return grid[obj.X][obj.Y - 1];
        }

        public bool checkdown(ObjGame obj)
        {
            if (obj.Y == Altura - 1)
                return false;

            return grid[obj.X][obj.Y + 1];
        }

        public bool checkleft(ObjGame obj)
        {
            if (obj.X == 0)
                return false;

            return grid[obj.X - 1][obj.Y];
        }

        public bool checkright(ObjGame obj)
        {
            if (obj.X == Largura - 1)
                return false;

            return grid[obj.X + 1][obj.Y];
        }
    }
}

public static class ModifyProgressBarColor
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
    static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
    public static void SetState(this ProgressBar pBar, int state)
    {
        SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
    }
}

class newLabel : System.Windows.Forms.Label
{
    public int RotateAngle { get; set; }
    public string NewText { get; set; }
    protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
    {
        Brush b = new SolidBrush(this.ForeColor);
        e.Graphics.TranslateTransform(this.Width / 2, this.Height / 2);
        e.Graphics.RotateTransform(this.RotateAngle);
        e.Graphics.DrawString(this.NewText, this.Font, b, 0f, 0f);
        base.OnPaint(e);
    }
}