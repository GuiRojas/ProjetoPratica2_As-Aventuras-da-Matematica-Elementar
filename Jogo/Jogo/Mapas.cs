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

        protected Bitmap barreira = new Bitmap(@"barreira1.png");
        protected bool pintarBarreira = true;

        protected String coords = "";

        public Mapa1(Background background)
        {
            this.background = background;

            game.setOcupado(6, 4); game.setOcupado(17, 2); game.setOcupado(18, 2); game.setOcupado(19, 2); game.setOcupado(10, 15); game.setOcupado(20, 2); game.setOcupado(21, 2); game.setOcupado(3, 4); game.setOcupado(3, 3); game.setOcupado(10, 8); game.setOcupado(15, 14); game.setOcupado(3, 2); game.setOcupado(4, 2); game.setOcupado(5, 2); game.setOcupado(6, 2); game.setOcupado(6, 3); game.setOcupado(5, 3); game.setOcupado(4, 3); game.setOcupado(4, 4); game.setOcupado(7, 15); game.setOcupado(7, 14); game.setOcupado(7, 13); game.setOcupado(6, 13); game.setOcupado(6, 12); game.setOcupado(6, 11); game.setOcupado(5, 11); game.setOcupado(5, 10); game.setOcupado(5, 9); game.setOcupado(6, 9); game.setOcupado(7, 9); game.setOcupado(8, 9); game.setOcupado(9, 9); game.setOcupado(10, 9); game.setOcupado(9, 8); game.setOcupado(8, 8); game.setOcupado(7, 8); game.setOcupado(6, 8); game.setOcupado(10, 9); game.setOcupado(11, 10); game.setOcupado(11, 11); game.setOcupado(11, 12); game.setOcupado(12, 13); game.setOcupado(12, 14); game.setOcupado(12, 15); game.setOcupado(11, 15); game.setOcupado(9, 15); game.setOcupado(8, 15); game.setOcupado(7, 15); game.setOcupado(14, 17); game.setOcupado(14, 16); game.setOcupado(15, 16); game.setOcupado(15, 15); game.setOcupado(16, 16); game.setOcupado(16, 17); game.setOcupado(15, 17); game.setOcupado(18, 15); game.setOcupado(20, 15); game.setOcupado(19, 15); game.setOcupado(20, 15); game.setOcupado(21, 15); game.setOcupado(21, 15); game.setOcupado(22, 15); game.setOcupado(21, 15); game.setOcupado(23, 15); game.setOcupado(23, 14); game.setOcupado(22, 14); game.setOcupado(21, 14); game.setOcupado(20, 14); game.setOcupado(19, 14); game.setOcupado(18, 14); game.setOcupado(18, 14); game.setOcupado(18, 12); game.setOcupado(18, 13); game.setOcupado(17, 12); game.setOcupado(17, 11); game.setOcupado(17, 10); game.setOcupado(18, 9); game.setOcupado(18, 8); game.setOcupado(19, 8); game.setOcupado(20, 8); game.setOcupado(21, 8); game.setOcupado(22, 8); game.setOcupado(22, 9); game.setOcupado(23, 8); game.setOcupado(23, 9); game.setOcupado(23, 11); game.setOcupado(23, 10); game.setOcupado(23, 11); game.setOcupado(23, 11); game.setOcupado(23, 12); game.setOcupado(23, 13); game.setOcupado(23, 14); game.setOcupado(23, 15); game.setOcupado(15, 11); game.setOcupado(15, 10); game.setOcupado(15, 8); game.setOcupado(15, 9); game.setOcupado(14, 9); game.setOcupado(14, 8); game.setOcupado(21, 3); game.setOcupado(20, 3); game.setOcupado(21, 3); game.setOcupado(23, 2); game.setOcupado(22, 2); game.setOcupado(22, 1); game.setOcupado(23, 1); game.setOcupado(23, 0); game.setOcupado(22, 0); game.setOcupado(17, 1); game.setOcupado(18, 1); game.setOcupado(19, 1); game.setOcupado(19, 1); game.setOcupado(19, 1); game.setOcupado(20, 1); game.setOcupado(16, 2); game.setOcupado(15, 2); game.setOcupado(15, 1); game.setOcupado(16, 1); game.setOcupado(16, 0); game.setOcupado(15, 0); game.setOcupado(16, 0);
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
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                heroi.ActiveUp = true;
            }

            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                heroi.ActiveDown = true;
            }

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                heroi.ActiveLeft = true;
            }

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
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
                    mestre.dialogoAsync(background, mestre.IniciaBatalha);
                }

                if (Game.perto(heroi, easterEgg))
                {
                    easterEgg.dialogoAsync(background, easterEgg.IniciaBatalha);
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

            if (pintarBarreira)
            {
                e.Graphics.DrawImage(barreira, 17 * Game.Tam, 2 * Game.Tam, 96, 32);
            }

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

        public virtual void carregarGame()
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
            mestre = new ObjNpc(5, 5, mestreImg, msgs, true);
            game.setOcupado(mestre.X, mestre.Y);

            Queue<String> msgsEasterEgg = new Queue<string>();
            msgsEasterEgg.Enqueue("É perigoso ir sozinho, eu até te daria uma espada...");
            msgsEasterEgg.Enqueue("... Mas é um jogo infantil.");
            easterEggImg = new Bitmap(@"heroi.png");
            easterEgg = new ObjNpc(18, 13, easterEggImg, msgsEasterEgg, false);
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
        protected ObjHeroi heroi;
        protected Bitmap heroiImg;

        Bitmap fundo;
        protected Bitmap barreira = new Bitmap(@"barreira2.png");
        protected bool pintarBarreira = true;

        protected ObjNpc aldeao;
        protected Bitmap aldeaoImg;

        protected ObjNpc aldeaoD;
        protected Bitmap aldeaoDImg;

        protected Game game = new Game();
        protected ObjGame[] objsDoGame;

        protected Background background;

        String coords = "";

        public Mapa2(Background background)
        {
            game.setOcupado(17, 4); game.setOcupado(18, 4); game.setOcupado(19, 3); game.setOcupado(20, 4); game.setOcupado(21, 4); game.setOcupado(22, 4); game.setOcupado(23, 3); game.setOcupado(23, 4); game.setOcupado(24, 3); game.setOcupado(15, 4); game.setOcupado(13, 1); game.setOcupado(14, 1); game.setOcupado(15, 2); game.setOcupado(12, 1); game.setOcupado(10, 0); game.setOcupado(1, 6); game.setOcupado(1, 5); game.setOcupado(1, 4); game.setOcupado(1, 3); game.setOcupado(3, 3); game.setOcupado(2, 3); game.setOcupado(4, 3); game.setOcupado(5, 3); game.setOcupado(6, 3); game.setOcupado(7, 3); game.setOcupado(0, 19); game.setOcupado(0, 18); game.setOcupado(0, 17); game.setOcupado(0, 16); game.setOcupado(0, 15); game.setOcupado(0, 14); game.setOcupado(0, 13); game.setOcupado(0, 12); game.setOcupado(1, 12); game.setOcupado(1, 11); game.setOcupado(1, 9); game.setOcupado(1, 10); game.setOcupado(2, 10); game.setOcupado(2, 9); game.setOcupado(2, 8); game.setOcupado(2, 7); game.setOcupado(3, 8); game.setOcupado(3, 7); game.setOcupado(8, 4); game.setOcupado(8, 3); game.setOcupado(9, 2); game.setOcupado(9, 1); game.setOcupado(11, 1); game.setOcupado(16, 3); game.setOcupado(14, 4); game.setOcupado(13, 5); game.setOcupado(12, 6); game.setOcupado(10, 6); game.setOcupado(12, 6); game.setOcupado(11, 6); game.setOcupado(10, 7); game.setOcupado(9, 8); game.setOcupado(8, 8); game.setOcupado(7, 9); game.setOcupado(7, 10); game.setOcupado(8, 10); game.setOcupado(9, 10); game.setOcupado(10, 9); game.setOcupado(11, 9); game.setOcupado(12, 9); game.setOcupado(13, 9); game.setOcupado(13, 7); game.setOcupado(13, 8); game.setOcupado(14, 8); game.setOcupado(15, 8); game.setOcupado(16, 8); game.setOcupado(16, 9); game.setOcupado(17, 9); game.setOcupado(18, 9); game.setOcupado(19, 9); game.setOcupado(20, 9); game.setOcupado(20, 10); game.setOcupado(20, 12); game.setOcupado(20, 11); game.setOcupado(19, 11); game.setOcupado(18, 12); game.setOcupado(17, 12); game.setOcupado(16, 13); game.setOcupado(15, 13); game.setOcupado(13, 13); game.setOcupado(15, 13); game.setOcupado(14, 13); game.setOcupado(12, 13); game.setOcupado(11, 13); game.setOcupado(10, 13); game.setOcupado(9, 13); game.setOcupado(8, 13); game.setOcupado(7, 13); game.setOcupado(6, 13); game.setOcupado(7, 13); game.setOcupado(5, 13); game.setOcupado(5, 14); game.setOcupado(5, 15); game.setOcupado(5, 17); game.setOcupado(4, 16); game.setOcupado(4, 17); game.setOcupado(5, 18); game.setOcupado(5, 19); game.setOcupado(4, 19); game.setOcupado(5, 19);
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

        public virtual void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                heroi.ActiveUp = true;
            }

            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                heroi.ActiveDown = true;
            }

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                heroi.ActiveLeft = true;
            }

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
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
                    aldeao.dialogoAsync(background, aldeao.IniciaBatalha);
                }

                if (Game.perto(heroi, aldeaoD))
                {
                    aldeaoD.dialogoAsync(background, aldeaoD.IniciaBatalha);
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
            if (pintarBarreira)
                e.Graphics.DrawImage(barreira, 13 * Game.Tam, 1 * Game.Tam, 256, 64);

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

        public virtual void carregarGame()
        {
            heroiImg = new Bitmap(@"heroi.png");
            heroi = new ObjHeroi(2, 19, heroiImg);

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
            aldeao = new ObjNpc(16, 10, aldeaoImg, msgs, true);
            game.setOcupado(aldeao.X, aldeao.Y);

            aldeaoDImg = new Bitmap(@"aldea_abatida.png");
            aldeaoD = new ObjNpc(4, 4, aldeaoDImg, msgsD, false);
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

    public class Mapa3
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

        protected Bitmap barreira = new Bitmap(@"barreira1.png");
        protected bool pintarBarreira = true;

        protected String coords = "";

        public Mapa1(Background background)
        {
            this.background = background;

            game.setOcupado(6, 4); game.setOcupado(17, 2); game.setOcupado(18, 2); game.setOcupado(19, 2); game.setOcupado(10, 15); game.setOcupado(20, 2); game.setOcupado(21, 2); game.setOcupado(3, 4); game.setOcupado(3, 3); game.setOcupado(10, 8); game.setOcupado(15, 14); game.setOcupado(3, 2); game.setOcupado(4, 2); game.setOcupado(5, 2); game.setOcupado(6, 2); game.setOcupado(6, 3); game.setOcupado(5, 3); game.setOcupado(4, 3); game.setOcupado(4, 4); game.setOcupado(7, 15); game.setOcupado(7, 14); game.setOcupado(7, 13); game.setOcupado(6, 13); game.setOcupado(6, 12); game.setOcupado(6, 11); game.setOcupado(5, 11); game.setOcupado(5, 10); game.setOcupado(5, 9); game.setOcupado(6, 9); game.setOcupado(7, 9); game.setOcupado(8, 9); game.setOcupado(9, 9); game.setOcupado(10, 9); game.setOcupado(9, 8); game.setOcupado(8, 8); game.setOcupado(7, 8); game.setOcupado(6, 8); game.setOcupado(10, 9); game.setOcupado(11, 10); game.setOcupado(11, 11); game.setOcupado(11, 12); game.setOcupado(12, 13); game.setOcupado(12, 14); game.setOcupado(12, 15); game.setOcupado(11, 15); game.setOcupado(9, 15); game.setOcupado(8, 15); game.setOcupado(7, 15); game.setOcupado(14, 17); game.setOcupado(14, 16); game.setOcupado(15, 16); game.setOcupado(15, 15); game.setOcupado(16, 16); game.setOcupado(16, 17); game.setOcupado(15, 17); game.setOcupado(18, 15); game.setOcupado(20, 15); game.setOcupado(19, 15); game.setOcupado(20, 15); game.setOcupado(21, 15); game.setOcupado(21, 15); game.setOcupado(22, 15); game.setOcupado(21, 15); game.setOcupado(23, 15); game.setOcupado(23, 14); game.setOcupado(22, 14); game.setOcupado(21, 14); game.setOcupado(20, 14); game.setOcupado(19, 14); game.setOcupado(18, 14); game.setOcupado(18, 14); game.setOcupado(18, 12); game.setOcupado(18, 13); game.setOcupado(17, 12); game.setOcupado(17, 11); game.setOcupado(17, 10); game.setOcupado(18, 9); game.setOcupado(18, 8); game.setOcupado(19, 8); game.setOcupado(20, 8); game.setOcupado(21, 8); game.setOcupado(22, 8); game.setOcupado(22, 9); game.setOcupado(23, 8); game.setOcupado(23, 9); game.setOcupado(23, 11); game.setOcupado(23, 10); game.setOcupado(23, 11); game.setOcupado(23, 11); game.setOcupado(23, 12); game.setOcupado(23, 13); game.setOcupado(23, 14); game.setOcupado(23, 15); game.setOcupado(15, 11); game.setOcupado(15, 10); game.setOcupado(15, 8); game.setOcupado(15, 9); game.setOcupado(14, 9); game.setOcupado(14, 8); game.setOcupado(21, 3); game.setOcupado(20, 3); game.setOcupado(21, 3); game.setOcupado(23, 2); game.setOcupado(22, 2); game.setOcupado(22, 1); game.setOcupado(23, 1); game.setOcupado(23, 0); game.setOcupado(22, 0); game.setOcupado(17, 1); game.setOcupado(18, 1); game.setOcupado(19, 1); game.setOcupado(19, 1); game.setOcupado(19, 1); game.setOcupado(20, 1); game.setOcupado(16, 2); game.setOcupado(15, 2); game.setOcupado(15, 1); game.setOcupado(16, 1); game.setOcupado(16, 0); game.setOcupado(15, 0); game.setOcupado(16, 0);
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
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                heroi.ActiveUp = true;
            }

            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                heroi.ActiveDown = true;
            }

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                heroi.ActiveLeft = true;
            }

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
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
                    mestre.dialogoAsync(background, mestre.IniciaBatalha);
                }

                if (Game.perto(heroi, easterEgg))
                {
                    easterEgg.dialogoAsync(background, easterEgg.IniciaBatalha);
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

            if (pintarBarreira)
            {
                e.Graphics.DrawImage(barreira, 17 * Game.Tam, 2 * Game.Tam, 96, 32);
            }

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

        public virtual void carregarGame()
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
            mestre = new ObjNpc(5, 5, mestreImg, msgs, true);
            game.setOcupado(mestre.X, mestre.Y);

            Queue<String> msgsEasterEgg = new Queue<string>();
            msgsEasterEgg.Enqueue("É perigoso ir sozinho, eu até te daria uma espada...");
            msgsEasterEgg.Enqueue("... Mas é um jogo infantil.");
            easterEggImg = new Bitmap(@"heroi.png");
            easterEgg = new ObjNpc(18, 13, easterEggImg, msgsEasterEgg, false);
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
}
