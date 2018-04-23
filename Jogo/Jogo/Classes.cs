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
        public Background(int fase, Form frm)
        {
            this.fase = fase;
            this.estado = 1;
            this.frm = frm;
            this.dificuldade = 2;//todo pegar do bd os bagulho

            carregarEstadoEFase();
        }

        protected int fase { get; set; }
        protected int estado { get; set; }
        protected Form frm { get; set; }
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

        public Form Frm
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
            switch (estado)
            {
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
                        }
                    }
                    break;
            }

            gameClass.carregarGame();
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
    }

    public interface GameClass
    {
        void keyUp(object sender, KeyEventArgs e);

        void keyDown(object sender, KeyEventArgs e);

        void paint(object sender, PaintEventArgs e);

        void tick(object sender, EventArgs e);

        void carregarGame();
    }

    public class Mapa1 : GameClass
    {
        protected ObjHeroi heroi;
        protected Bitmap heroiImg;

        protected Bitmap fundo;

        protected ObjNpc mestre;
        protected Bitmap mestreImg;

        protected ObjNpc easterEgg;
        protected Bitmap easterEggImg;

        protected Game game = new Game();
        protected ObjGame[] objsDoGame;

        protected Background background;

        protected String coords = "";

        public Mapa1(Background background)
        {
            this.background = background;
            game.setOcupado(17, 2); game.setOcupado(18, 2); game.setOcupado(19, 2); game.setOcupado(10, 15); game.setOcupado(20, 2); game.setOcupado(21, 2); game.setOcupado(3, 4); game.setOcupado(3, 3); game.setOcupado(10, 8); game.setOcupado(15, 14); game.setOcupado(3, 2); game.setOcupado(4, 2); game.setOcupado(5, 2); game.setOcupado(6, 2); game.setOcupado(6, 3); game.setOcupado(5, 3); game.setOcupado(4, 3); game.setOcupado(4, 4); game.setOcupado(7, 15); game.setOcupado(7, 14); game.setOcupado(7, 13); game.setOcupado(6, 13); game.setOcupado(6, 12); game.setOcupado(6, 11); game.setOcupado(5, 11); game.setOcupado(5, 10); game.setOcupado(5, 9); game.setOcupado(6, 9); game.setOcupado(7, 9); game.setOcupado(8, 9); game.setOcupado(9, 9); game.setOcupado(10, 9); game.setOcupado(9, 8); game.setOcupado(8, 8); game.setOcupado(7, 8); game.setOcupado(6, 8); game.setOcupado(10, 9); game.setOcupado(11, 10); game.setOcupado(11, 11); game.setOcupado(11, 12); game.setOcupado(12, 13); game.setOcupado(12, 14); game.setOcupado(12, 15); game.setOcupado(11, 15); game.setOcupado(9, 15); game.setOcupado(8, 15); game.setOcupado(7, 15); game.setOcupado(14, 17); game.setOcupado(14, 16); game.setOcupado(15, 16); game.setOcupado(15, 15); game.setOcupado(16, 16); game.setOcupado(16, 17); game.setOcupado(15, 17); game.setOcupado(18, 15); game.setOcupado(20, 15); game.setOcupado(19, 15); game.setOcupado(20, 15); game.setOcupado(21, 15); game.setOcupado(21, 15); game.setOcupado(22, 15); game.setOcupado(21, 15); game.setOcupado(23, 15); game.setOcupado(23, 14); game.setOcupado(22, 14); game.setOcupado(21, 14); game.setOcupado(20, 14); game.setOcupado(19, 14); game.setOcupado(18, 14); game.setOcupado(18, 14); game.setOcupado(18, 12); game.setOcupado(18, 13); game.setOcupado(17, 12); game.setOcupado(17, 11); game.setOcupado(17, 10); game.setOcupado(18, 9); game.setOcupado(18, 8); game.setOcupado(19, 8); game.setOcupado(20, 8); game.setOcupado(21, 8); game.setOcupado(22, 8); game.setOcupado(22, 9); game.setOcupado(23, 8); game.setOcupado(23, 9); game.setOcupado(23, 11); game.setOcupado(23, 10); game.setOcupado(23, 11); game.setOcupado(23, 11); game.setOcupado(23, 12); game.setOcupado(23, 13); game.setOcupado(23, 14); game.setOcupado(23, 15); game.setOcupado(15, 11); game.setOcupado(15, 10); game.setOcupado(15, 8); game.setOcupado(15, 9); game.setOcupado(14, 9); game.setOcupado(14, 8); game.setOcupado(21, 3); game.setOcupado(20, 3); game.setOcupado(21, 3); game.setOcupado(23, 2); game.setOcupado(22, 2); game.setOcupado(22, 1); game.setOcupado(23, 1); game.setOcupado(23, 0); game.setOcupado(22, 0); game.setOcupado(17, 1); game.setOcupado(18, 1); game.setOcupado(19, 1); game.setOcupado(19, 1); game.setOcupado(19, 1); game.setOcupado(20, 1); game.setOcupado(16, 2); game.setOcupado(15, 2); game.setOcupado(15, 1); game.setOcupado(16, 1); game.setOcupado(16, 0); game.setOcupado(15, 0); game.setOcupado(16, 0);
        }

        public void keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                heroi.ActiveUp = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                heroi.ActiveDown = false;
            }

            if (e.KeyCode == Keys.Left)
            {
                heroi.ActiveLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                heroi.ActiveRight = false;
            }
        }

        public virtual void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                heroi.ActiveUp = true;
            }

            if (e.KeyCode == Keys.Down)
            {
                heroi.ActiveDown = true;
            }

            if (e.KeyCode == Keys.Left)
            {
                heroi.ActiveLeft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                heroi.ActiveRight = true;
            }

            easterEgg.MostrarTexto = false;
            mestre.MostrarTexto = false;

            if (e.KeyCode == Keys.E)
            {
                //TODO loop pelos npc
                if (Game.perto(heroi, mestre))
                {
                    mestre.dialogoAsync(background, true);
                }

                if (Game.perto(heroi, easterEgg))
                {
                    easterEgg.dialogoAsync(background, false);
                }
            }

            if (e.KeyCode == Keys.Escape)
            {

            }

            if (e.KeyCode == Keys.Space)
            {
                coords += "game.setOcupado(" + heroi.X + ", " + heroi.Y + "); ";
            }

        }

        public void paint(object sender, PaintEventArgs e)
        {
            //TODO loop por todas os objetos de ObjGame
            e.Graphics.DrawImage(fundo, 0, 0, Game.Largura * Game.Tam, Game.Altura * Game.Tam);

            e.Graphics.DrawImage(heroi.Img, heroi.X * Game.Tam, heroi.Y * Game.Tam, Game.Tam, Game.Tam);
            e.Graphics.DrawImage(mestre.Img, mestre.X * Game.Tam, mestre.Y * Game.Tam, Game.Tam, Game.Tam);
            e.Graphics.DrawImage(easterEgg.Img, easterEgg.X * Game.Tam, easterEgg.Y * Game.Tam, 0, 0);

            if (mestre.MostrarTexto)
            {   //TODO transformar texto p/ classe
                texto(sender, e, mestre);
            }

            if (easterEgg.MostrarTexto)
            {   //TODO transformar texto p/ classe
                texto(sender, e, easterEgg);
            }
        }

        public void tick(object sender, EventArgs e)
        {
            heroi.mover(game);
        }
        
        public void carregarGame()
        {
            //texto
            //vc se chama shingetsu kun, um samurai muito respeitado
            //sua vida inteira vc usou a força sobre tudo, até agora.
            //vc chega na vila e ta tudo difrerente, vazia, e vc encontra o Senpaio, o mestre em matematica
            
            heroiImg = new Bitmap(@"heroi.png");
            heroi = new ObjHeroi(2, 17, heroiImg);

            fundo = new Bitmap(@"vila.png");

            Queue<String> msgs = new Queue<string>();
            msgs.Enqueue("Oh, nobre guerreiro samurai Shingetsu Kun.");
            msgs.Enqueue("O clan de Glau Xia destruiu tudo nosso.");
            msgs.Enqueue("Logo antes, antes de sua chegada, eles queimaram toda a vila...");
            msgs.Enqueue("e... roubaram a sua namorada, a Minna Chan.");
            msgs.Enqueue("A Glau Xia adora física, logo, só pode ser derrotada por um meio.");
            msgs.Enqueue("Sei que você quer vingança, mas para recuperar tudo isso, não será necessária a força de um samurai.");
            msgs.Enqueue("Mas a sabedoria de um matemático.");
            msgs.Enqueue("...");
            msgs.Enqueue("Entre no dojo, vamos conseguir sua vingança.");
            mestreImg = new Bitmap(@"npc.png");
            mestre = new ObjNpc(5, 5, mestreImg, msgs);
            game.setOcupado(mestre.X, mestre.Y);

            Queue<String> msgsEasterEgg = new Queue<string>();
            msgsEasterEgg.Enqueue("É perigoso ir sozinho, eu até te daria uma espada...");
            msgsEasterEgg.Enqueue("... Mas é um jogo infantil.");
            easterEggImg = new Bitmap(@"heroi.png");
            easterEgg = new ObjNpc(18, 13, easterEggImg, msgsEasterEgg);
        }

        public void texto(object sender, PaintEventArgs e, ObjNpc obj)
        {
            string text = obj.Msg;

            FontFamily fontFamily = new FontFamily("arial");

            try
            {
                fontFamily = new FontFamily("Gill Sans MT");
            }
            catch (Exception erro)
            {
                //TODO
            }


            using (Font font1 = new Font(fontFamily, 12, FontStyle.Bold, GraphicsUnit.Point))
            {
                SizeF a = e.Graphics.MeasureString(text, font1);
                Size sizeString = a.ToSize();

                int largura = sizeString.Width + 2;
                int altura = sizeString.Height + 2;//margem de erro
                int xTxt = (obj.X * Game.Tam) - largura + Game.Tam;
                int yTxt = (obj.Y * Game.Tam) - Game.Tam;

                /*int larguraTotal = Game.Tam * Game.Largura / 2;

                if (largura > larguraTotal)
                {
                    largura = larguraTotal;
                    xTxt += largura;

                    String[] linhas = text.Split(' ');
                    int qtd_linhas = linhas.Length;
                    altura += sizeString.Height * qtd_linhas;
                    yTxt -= altura;
                }*/

                if (xTxt < 0)
                {
                    xTxt = 0;
                }

                if (xTxt > Game.Tam * Game.Largura)
                {
                    xTxt = Game.Tam * Game.Largura - largura;
                }

                /*if (yTxt < 0)
                {
                    yTxt = 0;
                }

                if (yTxt > Game.Tam * Game.Altura)
                {
                    yTxt = Game.Tam * Game.Altura;
                }*/

                Rectangle rectF1 = new Rectangle(xTxt, yTxt, largura, altura);
                SolidBrush branco = new SolidBrush(Color.White);
                e.Graphics.FillRectangle(branco, rectF1);
                e.Graphics.DrawString(text, font1, Brushes.Black, rectF1);
                e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rectF1));
            }
        }
    }

    public class Mapa2 : GameClass
    {
        ObjHeroi heroi;
        Bitmap heroiImg;

        Bitmap fundo;

        ObjNpc aldeao;
        Bitmap aldeaoImg;

        ObjNpc aldeaoD;
        Bitmap aldeaoDImg;

        Game game = new Game();
        ObjGame[] objsDoGame;

        Background background;

        String coords = "";

        public Mapa2(Background background)
        {
            game.setOcupado(10, 0);game.setOcupado(1, 6); game.setOcupado(1, 5); game.setOcupado(1, 4); game.setOcupado(1, 3); game.setOcupado(3, 3); game.setOcupado(2, 3); game.setOcupado(4, 3); game.setOcupado(5, 3); game.setOcupado(6, 3); game.setOcupado(7, 3); game.setOcupado(0, 19); game.setOcupado(0, 18); game.setOcupado(0, 17); game.setOcupado(0, 16); game.setOcupado(0, 15); game.setOcupado(0, 14); game.setOcupado(0, 13); game.setOcupado(0, 12); game.setOcupado(1, 12); game.setOcupado(1, 11); game.setOcupado(1, 9); game.setOcupado(1, 10); game.setOcupado(2, 10); game.setOcupado(2, 9); game.setOcupado(2, 8); game.setOcupado(2, 7); game.setOcupado(3, 8); game.setOcupado(3, 7); game.setOcupado(8, 4); game.setOcupado(8, 3); game.setOcupado(9, 2); game.setOcupado(9, 1); game.setOcupado(11, 1); game.setOcupado(16, 3); game.setOcupado(14, 4); game.setOcupado(13, 5); game.setOcupado(12, 6); game.setOcupado(10, 6); game.setOcupado(12, 6); game.setOcupado(11, 6); game.setOcupado(10, 7); game.setOcupado(9, 8); game.setOcupado(8, 8); game.setOcupado(7, 9); game.setOcupado(7, 10); game.setOcupado(8, 10); game.setOcupado(9, 10); game.setOcupado(10, 9); game.setOcupado(11, 9); game.setOcupado(12, 9); game.setOcupado(13, 9); game.setOcupado(13, 7); game.setOcupado(13, 8); game.setOcupado(14, 8); game.setOcupado(15, 8); game.setOcupado(16, 8); game.setOcupado(16, 9); game.setOcupado(17, 9); game.setOcupado(18, 9); game.setOcupado(19, 9); game.setOcupado(20, 9); game.setOcupado(20, 10); game.setOcupado(20, 12); game.setOcupado(20, 11); game.setOcupado(19, 11); game.setOcupado(18, 12); game.setOcupado(17, 12); game.setOcupado(16, 13); game.setOcupado(15, 13); game.setOcupado(13, 13); game.setOcupado(15, 13); game.setOcupado(14, 13); game.setOcupado(12, 13); game.setOcupado(11, 13); game.setOcupado(10, 13); game.setOcupado(9, 13); game.setOcupado(8, 13); game.setOcupado(7, 13); game.setOcupado(6, 13); game.setOcupado(7, 13); game.setOcupado(5, 13); game.setOcupado(5, 14); game.setOcupado(5, 15); game.setOcupado(5, 17); game.setOcupado(4, 16); game.setOcupado(4, 17); game.setOcupado(5, 18); game.setOcupado(5, 19); game.setOcupado(4, 19); game.setOcupado(5, 19);
            this.background = background;
        }

        public void keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                heroi.ActiveUp = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                heroi.ActiveDown = false;
            }

            if (e.KeyCode == Keys.Left)
            {
                heroi.ActiveLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                heroi.ActiveRight = false;
            }
        }

        public void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                heroi.ActiveUp = true;
            }

            if (e.KeyCode == Keys.Down)
            {
                heroi.ActiveDown = true;
            }

            if (e.KeyCode == Keys.Left)
            {
                heroi.ActiveLeft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                heroi.ActiveRight = true;
            }


            aldeao.MostrarTexto = false;
            aldeaoD.MostrarTexto = false;

            if (e.KeyCode == Keys.E)
            {
                //TODO loop pelos npc
                if (Game.perto(heroi, aldeao))
                {
                    aldeao.dialogoAsync(background, true);
                }

                if (Game.perto(heroi, aldeaoD))
                {
                    aldeaoD.dialogoAsync(background, false);
                }
            }

            if (e.KeyCode == Keys.Escape)
            {

            }

            if (e.KeyCode == Keys.Space)
            {
                coords += "game.setOcupado(" + heroi.X + ", " + heroi.Y + ");";
            }

        }

        public void paint(object sender, PaintEventArgs e)
        {
            //TODO loop por todas os objetos de ObjGame
            e.Graphics.DrawImage(fundo, 0, 0, Game.Largura * Game.Tam, Game.Altura * Game.Tam);
            e.Graphics.DrawImage(heroi.Img, heroi.X * Game.Tam, heroi.Y * Game.Tam, Game.Tam, Game.Tam);
            e.Graphics.DrawImage(aldeao.Img, aldeao.X * Game.Tam, aldeao.Y * Game.Tam, Game.Tam, Game.Tam);
            e.Graphics.DrawImage(aldeaoD.Img, aldeaoD.X * Game.Tam, aldeaoD.Y * Game.Tam, Game.Tam, Game.Tam);

            if (aldeao.MostrarTexto)
            {   //TODO transformar texto p/ classe
                texto(sender, e, aldeao);
            }

            if (aldeaoD.MostrarTexto)
            {   //TODO transformar texto p/ classe
                texto(sender, e, aldeaoD);
            }
        }

        public void tick(object sender, EventArgs e)
        {
            heroi.mover(game);
        }
        
        public void carregarGame()
        {
            //texto
            //vc se chama shingetsu kun, um samurai muito respeitado
            //sua vida inteira vc usou a força sobre tudo, até agora.
            //vc chega na vila e ta tudo difrerente, vazia, e vc encontra o Senpaio, o mestre em matematica


            heroiImg = new Bitmap(@"heroi.png");
            heroi = new ObjHeroi(2, 17, heroiImg);

            fundo = new Bitmap(@"dark_forest.png");

            Queue<String> msgs = new Queue<string>();
            msgs.Enqueue("AAAAAAH.");
            msgs.Enqueue("Não me machuque novamente, eu imploro.");
            msgs.Enqueue("Vocês do clan de Glau Xia são muito do mal.");
            msgs.Enqueue("Destruiram minha casa, me bateram, cortaram minhas árvores.");
            msgs.Enqueue("Não se faça de desentendido, você sabe sim do que estou falando.");
            msgs.Enqueue("Quer saber?");
            msgs.Enqueue("Dessa vez vou te destruir com o poder da SUBTRAÇÃO.");

            Queue<String> msgsD = new Queue<string>();
            msgsD.Enqueue("Eles vieram aqui... acabaram com tudo.");


            aldeaoImg = new Bitmap(@"aldeao_abatido.png");
            aldeao = new ObjNpc(16, 10, aldeaoImg, msgs);
            game.setOcupado(aldeao.X, aldeao.Y);

            aldeaoDImg = new Bitmap(@"aldeao_abatido.png");
            aldeaoD = new ObjNpc(4, 4, aldeaoDImg, msgsD);
            game.setOcupado(aldeaoD.X, aldeaoD.Y);
        }

        public void texto(object sender, PaintEventArgs e, ObjNpc obj)
        {
            string text = obj.Msg;

            FontFamily fontFamily = new FontFamily("arial");

            try
            {
                fontFamily = new FontFamily("Gill Sans MT");
            }
            catch (Exception erro)
            {
                //TODO
            }


            using (Font font1 = new Font(fontFamily, 12, FontStyle.Bold, GraphicsUnit.Point))
            {
                SizeF a = e.Graphics.MeasureString(text, font1);
                Size sizeString = a.ToSize();

                int largura = sizeString.Width + 2;
                int altura = sizeString.Height + 2;//margem de erro
                int xTxt = (obj.X * Game.Tam) - largura + Game.Tam;
                int yTxt = (obj.Y * Game.Tam) - Game.Tam;

                /*int larguraTotal = Game.Tam * Game.Largura / 2;

                if (largura > larguraTotal)
                {
                    largura = larguraTotal;
                    xTxt += largura;

                    String[] linhas = text.Split(' ');
                    int qtd_linhas = linhas.Length;
                    altura += sizeString.Height * qtd_linhas;
                    yTxt -= altura;
                }*/

                if (xTxt < 0)
                {
                    xTxt = 0;
                }

                if (xTxt > Game.Tam * Game.Largura)
                {
                    xTxt = Game.Tam * Game.Largura - largura;
                }

                /*if (yTxt < 0)
                {
                    yTxt = 0;
                }

                if (yTxt > Game.Tam * Game.Altura)
                {
                    yTxt = Game.Tam * Game.Altura;
                }*/

                Rectangle rectF1 = new Rectangle(xTxt, yTxt, largura, altura);
                SolidBrush branco = new SolidBrush(Color.White);
                e.Graphics.FillRectangle(branco, rectF1);
                e.Graphics.DrawString(text, font1, Brushes.Black, rectF1);
                e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rectF1));
            }
        }
    }

    public class Mapa1_pos : Mapa1
    {
        public Background background;
        public Mapa1_pos (Background background):base(background)
        {
            this.background = background;
            this.game.setLivre(17, 2);
            this.game.setLivre(18, 2);
            this.game.setLivre(19, 2);
        }

        public override void keyDown(Object sender, KeyEventArgs e)
        {
            base.keyDown(sender, e);
            if (Game.checarPosicao(heroi, 17, 2) || Game.checarPosicao(heroi, 18, 2) || Game.checarPosicao(heroi, 19, 2))
            {
                //background.Estado = 1;
                //background.
                //background.carregarEstadoEFase();
                MessageBox.Show("acabou a fase");
            }
        }
    }

    public class Batalha1 : GameClass
    {
        Bitmap batalhaImg;
        ProgressBar pbVidaHeroi = new ProgressBar();
        ProgressBar pbVidaVilao = new ProgressBar();
        ProgressBar pbTempo = new ProgressBar();
        Label lblContinha = new Label();
        Label lblResultado = new Label();
        System.Windows.Forms.Timer tmrBatalha;
        Background background;
        protected bool podeAtacar = true;

        public Batalha1 (Background background)
        {
            this.background = background;
        }

        public void keyUp()
        {

        }

        public void keyDown(Object sender, KeyEventArgs e)
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
        }
        
        public void terminarBatalha()
        {
            //todo relatorio
            
            background.Frm.Controls.Remove(lblContinha);
            background.Frm.Controls.Remove(lblResultado);
            background.Frm.Controls.Remove(pbTempo);
            background.Frm.Controls.Remove(pbVidaHeroi);
            background.Frm.Controls.Remove(pbVidaVilao);
            background.transicao(3);
        }

        public void receberDano(ProgressBar pb, int dano)
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
                    break;
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

            tmrBatalha.Start();
        }

        public void causarDano()
        {
            pbTempo.Value = pbTempo.Maximum;

            if (Conta.resolver(lblContinha.Text).ToString() == lblResultado.Text)
            {
                int dano = Convert.ToInt32((10));
                receberDano(pbVidaVilao, 10);
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

    public class Batalha2 : GameClass
    {
        Bitmap batalhaImg;
        ProgressBar pbVidaHeroi = new ProgressBar();
        ProgressBar pbVidaVilao = new ProgressBar();
        ProgressBar pbTempo = new ProgressBar();
        Label lblContinha = new Label();
        Label lblResultado = new Label();
        System.Windows.Forms.Timer tmrBatalha;
        Background background;

        public Batalha2(Background background)
        {
            this.background = background;
        }

        public void keyUp()
        {

        }

        public void keyDown(Object sender, KeyEventArgs e)
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
                else if (lblResultado.Text != "")
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
        }

        public void terminarBatalha()
        {
            //todo relatorio

            background.Estado = 3;
            background.carregarGame();
        }

        public void receberDano(ProgressBar pb, int dano)
        {
            ModifyProgressBarColor.SetState(pb, 2);
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
                    //terminarBatalha();
                    return;
                }
                else
                {
                    Application.DoEvents();
                    Thread.Sleep(20);
                    i++;
                }
            }

            ModifyProgressBarColor.SetState(pb, 1);
            background.Frm.Controls.Remove(lblDaninho);

            lblContinha.Text = Conta.gerarSubtracao(background.Dificuldade);

            lblResultado.Text = "";
        }

        public void causarDano()
        {
            pbTempo.Value = pbTempo.Maximum;

            if (Conta.resolver(lblContinha.Text).ToString() == lblResultado.Text)
            {
                int dano = Convert.ToInt32((10));
                receberDano(pbVidaVilao, dano);
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
    
    public class ObjNpc : ObjGame
    {
        protected bool mostrarTexto { get; set; }
        protected string msg { get; set; }

        protected Queue<String> mensagens;

        public ObjNpc(int xN, int yN, Bitmap imgN, Queue<String> mensagensN) : base(xN, yN, imgN)
        {
            mensagens = mensagensN;
        }

        public async Task dialogoAsync(Background b, Boolean bol)
        {
            mostrarTexto = true;
            if (mensagens.Count == 0 && bol)
            {
               b.iniciarBatalha();
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

        public void mover(Game game)
        {
            Thread.Sleep(100);

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