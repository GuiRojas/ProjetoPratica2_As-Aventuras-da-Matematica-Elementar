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
using System.Reflection;

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

        }

        private void checarAreas()
        {
            //todo checar todos os objs da classe OjbGame

            


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

            if (heroi.Y == 0)
            {
                ocupUp = true;
            }
            else
            {
                ocupUp = false;
            }

            if (heroi.Y == this.Height - heroi.Height)
            {
                ocupDown = true;
            }
            else
            {
                ocupDown = false;
            }
        }

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
