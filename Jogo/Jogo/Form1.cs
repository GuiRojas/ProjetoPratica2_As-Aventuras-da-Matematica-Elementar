using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jogo
{   //TODO pegar os niveis do bd
    public partial class frmJogo : Form
    {
        ObjHeroi heroi;
        Bitmap heroiImg;

        ObjGame monstro;
        Bitmap monstroImg;

        ObjEstatico arvore;
        Bitmap arvoreImg;

        ObjNpc mestre;
        Bitmap mestreImg;

        Game game = new Game();
        ObjGame[] objsDoGame;

        public frmJogo()
        {
            InitializeComponent();
        }

        private void Jogo_Load(object sender, EventArgs e)
        {
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

        private void tmrMain_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
        
        private void frmJogo_Paint(object sender, PaintEventArgs e)
        {
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
                    RectangleF rectF1 = new RectangleF((mestre.X * Game.Tam) - text1.Length * 12 + Game.Tam, (mestre.Y * Game.Tam) - Game.Tam, text1.Length * 12, Game.Tam);
                    e.Graphics.DrawString(text1, font1, Brushes.Black, rectF1);
                    e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rectF1));

                }
            }
        }

        private void frmJogo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up && game.checkup(heroi))
            {
                heroi.goup();
            }

            if (e.KeyCode == Keys.Down && game.checkdown(heroi))
            {
                heroi.godown();
            }

            if (e.KeyCode == Keys.Left && game.checkleft(heroi))
            {
                heroi.goleft();
            }

            if (e.KeyCode == Keys.Right && game.checkright(heroi))
            {
                heroi.goright();
            }

            if (e.KeyCode == Keys.E && game.perto(heroi, mestre))
            {
                mestre.dialogo();
            }
        }

        private void frmJogo_KeyUp(object sender, KeyEventArgs e)
        {

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

        public ObjHeroi(int xN, int yN, Bitmap imgN) : base(xN, yN, imgN)
        {
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
        private int x {get; set;}
        private int y { get; set; }
        private Bitmap img { get; set; }

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
        private bool[][] grid = new bool[25][];//25x20
        public const int Tam = 32, Altura = 20, Largura = 25;

        public Game()
        {
            for (int i = 0; i < Largura; i++) {
                grid[i] = new bool[20];
                for (int j = 0; j < Altura; j++)
                {
                    grid[i][j] = true;
                }
            }
        }

        public bool perto (ObjGame obj1, ObjGame obj2)
        {
            if ((obj1.X == (obj2.X - 1) && obj1.Y == obj2.Y) ||//y igual a esquerda
                (obj1.X == (obj2.X + 1) && obj1.Y == obj2.Y) ||//y igual a direita
                (obj1.Y == (obj2.Y - 1) && obj1.X == obj2.X) ||//x igual a abaixo
                (obj1.Y == (obj2.Y + 1) && obj1.X == obj2.X))   //x igual acima
            {
                return true;
            }

            return false;
        }

        public void setOcupado(int xNovo, int yNovo)
        {
            if(xNovo < 0 && xNovo > Largura)
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
            if (obj.Y == Altura-1)
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
            if (obj.X == Largura-1)
                return false;

            return grid[obj.X + 1][obj.Y];
        }


    }
}
