using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
<<<<<<< HEAD
using System.Reflection;
=======
>>>>>>> parent of 2e8c7da... a

namespace Jogo
{
    public partial class Form1 : Form
    {
        ObjGame heroi = new ObjGame(1, 1, 32,32);
        ObjGame npc = new ObjGame(100, 100, 30, 32);
        Bitmap imgHeroi = new Bitmap("Z:/pratica profissional/pratica2/Jogo/heroi.png");
        Bitmap imgNpc = new Bitmap("Z:/pratica profissional/pratica2/Jogo/npc.png");

        bool goup;
        bool godown;
        bool goleft;
        bool goright;
        bool ocupLeft = false;
        bool ocupRight = false;
        bool ocupUp = false;
        bool ocupDown = false;

        int speed = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD
=======
            heroiImg = new Bitmap(@"heroi.png");
            heroi = new ObjHeroi(0, 0, heroiImg);

            monstroImg = new Bitmap(@"monstro.png");
            monstro = new ObjGame(5, 5, monstroImg);
            game.setOcupado(monstro.X, monstro.Y);

            arvoreImg = new Bitmap(@"arvore.png");
            arvore = new ObjEstatico(0, 3, arvoreImg);
            game.setOcupado(arvore.X, arvore.Y);

            //Queue<String> msgs = ["eae","beleza","suave"];

            mestreImg = new Bitmap(@"heroi.png");
            mestre = new ObjNpc(16, 4, mestreImg);
            game.setOcupado(mestre.X, mestre.Y);
        }
>>>>>>> parent of 2e8c7da... a

        }

        private void checarAreas()
        {
<<<<<<< HEAD
            //todo checar todos os objs da classe OjbGame

=======
            //TODO loop por todas os objetos de ObjGame
            e.Graphics.DrawImage(heroi.Img, heroi.X * Game.Tam, heroi.Y * Game.Tam, Game.Tam, Game.Tam);
            e.Graphics.DrawImage(monstro.Img, monstro.X * Game.Tam, monstro.Y * Game.Tam, Game.Tam, Game.Tam);
            e.Graphics.DrawImage(arvore.Img, arvore.X * Game.Tam, arvore.Y * Game.Tam, Game.Tam, Game.Tam);
            e.Graphics.DrawImage(mestre.Img, mestre.X * Game.Tam, mestre.Y * Game.Tam, Game.Tam, Game.Tam);

            if (mestre.MostrarTexto)
            {
                string text1 = "aaaaaaaaaaaaaaaaaaaaa.";
                using (Font font1 = new Font("Lucida Console", 12, FontStyle.Bold, GraphicsUnit.Point))
                {
                    Rectangle rectF1 = new Rectangle((mestre.X * Game.Tam) - text1.Length * 12 + Game.Tam, (mestre.Y * Game.Tam) - Game.Tam, text1.Length * 12, Game.Tam);
                    SolidBrush branco = new SolidBrush(Color.White);
                    e.Graphics.FillRectangle(branco, rectF1);
                    e.Graphics.DrawString(text1, font1, Brushes.Black, rectF1);
                    e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rectF1));

                }
            }
        }

        private void frmJogo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                heroi.ActiveUp = true;
            }
>>>>>>> parent of 2e8c7da... a
            


            if ( heroi.X == 0)
            {
                ocupLeft = true;
            }
            else
            {
                ocupLeft = false;
            }

            if (heroi.X == this.Width - heroi.Width)
            {
                ocupRight = true;
            }
            else
            {
                ocupRight = false;
            }
<<<<<<< HEAD

            if (heroi.Y == 0)
            {
                ocupUp = true;
            }
            else
            {
                ocupUp = false;
            }
=======
        }
    }

    public class ObjNpc : ObjGame
    {
        private bool mostrarTexto { get; set; }

        private Queue<String> mensagens;

        public ObjNpc(int xN, int yN, Bitmap imgN) : base(xN, yN, imgN)
        {
            //Queue<String> mensagens = mensagensN;
        }

        public void dialogo()
        {
            mostrarTexto = true;
        }
>>>>>>> parent of 2e8c7da... a

            if (heroi.Y == this.Height - heroi.Height)
            {
                ocupDown = true;
            }
            else
            {
                ocupDown = false;
            }
        }
<<<<<<< HEAD

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
            if (e.KeyCode == Keys.Up)
=======
    }

    public class ObjEstatico : ObjGame
    {
        public ObjEstatico(int xN, int yN, Bitmap imgN) : base(xN, yN, imgN)
        {
        }
    }

    public class ObjHeroi : ObjGame
    {
        private bool activeLeft { get; set; }
        private bool activeRight { get; set; }
        private bool activeUp { get; set; }
        private bool activeDown { get; set; }

        public bool ActiveDown
        {
            get
>>>>>>> parent of 2e8c7da... a
            {
                goup = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = true;
            }

        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (goleft)
            {
                for(int i = 0; i < speed; i++)
                {
                    checarAreas();
                    if (!ocupLeft)
                        heroi.X --;
                }
            }

            if (goright)
            {
                for (int i = 0; i < speed; i++)
                {
                    checarAreas();
                    if (!ocupRight)
                        heroi.X ++;
                }
            }

            if (goup)
            {
                for (int i = 0; i < speed; i++)
                {
                    checarAreas();
                    if (!ocupUp)
                        heroi.Y --;
                }
            }

            if (godown)
            {
                for (int i = 0; i < speed; i++)
                {
                    checarAreas();
                    if (!ocupDown)
                        heroi.Y ++;
                }
            }
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(imgHeroi, heroi.X, heroi.Y, heroi.Width, heroi.Height);
            e.Graphics.DrawImage(imgNpc, npc.X, npc.Y, npc.Width, npc.Height);
        }
    }

    public class ObjGame
    {
        protected int x { set; get; }
        protected int y { set; get; }
        protected int width { set; get; }
        protected int height { set; get; }

        public ObjGame(int xInicial, int yInicial, int widthInicial, int heightInicial)
        {
            this.x = xInicial;
            this.y = yInicial;
            this.width = widthInicial;
            this.height = heightInicial;
        }

        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
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
    }
}
