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
    public class Mapa1_pos : Mapa1
    {
        public Background background;
        public Mapa1_pos(Background background) : base(background)
        {
            this.background = background;
            this.game.setLivre(17, 2);
            this.game.setLivre(18, 2);
            this.game.setLivre(19, 2);
            this.game.setLivre(5, 5);
            pintarBarreira = false;
        }

        public override void carregarGame()
        {
            base.carregarGame();

            Queue<String> msgs = new Queue<string>();
            msgs.Enqueue("Retirei a madeira para você.");
            msgs.Enqueue("Siga com cuiadado.");
            mestreImg = new Bitmap(@"npc.png");
            mestre = new ObjNpc(16, 3, mestreImg, msgs, false);
            game.setOcupado(mestre.X, mestre.Y);

            heroi = new ObjHeroi(6, 5, heroiImg);
        }

        public override void keyDown(Object sender, KeyEventArgs e)
        {
            base.keyDown(sender, e);
            if (Game.checarPosicao(heroi, 17, 2) || Game.checarPosicao(heroi, 18, 2) || Game.checarPosicao(heroi, 19, 2))
            {
                background.Fase++;

                background.transicao(1);
            }
        }
    }

    public class Mapa2_pos : Mapa2
    {
        public Background background;
        public Mapa2_pos(Background background) : base(background)
        {
            this.background = background;
            game.setLivre(15, 4);
            game.setLivre(13, 1);
            game.setLivre(14, 1);
            game.setLivre(15, 2);
            game.setLivre(12, 1);
            pintarBarreira = false;
        }

        public override void carregarGame()
        {
            base.carregarGame();

            heroi = new ObjHeroi(15, 12, heroiImg);

            Queue<String> msgs = new Queue<string>();
            msgs.Enqueue("Sinto muito, pensei que você era do clã de Glau Xia.");
            msgs.Enqueue("Pedi para minha amiga retirar o crocodilo para você.");
            msgs.Enqueue("Siga sua jornada pelo deserto.");
            aldeaoImg = new Bitmap(@"aldeao_abatido.png");
            aldeao = new ObjNpc(15, 3, aldeaoImg, msgs, false);
            game.setOcupado(aldeao.X, aldeao.Y);

            msgs = new Queue<string>();
            msgs.Enqueue("Esse crocodilo é o bixão kkkj.");
            aldeaoDImg = new Bitmap(@"aldea_abatida.png");
            aldeaoD = new ObjNpc(11, 2, aldeaoDImg, msgs, false);
            game.setOcupado(aldeaoD.X, aldeaoD.Y);
        }

        public override void keyDown(Object sender, KeyEventArgs e)
        {
            base.keyDown(sender, e);
            if (Game.checarPosicao(heroi, 17, 0) || Game.checarPosicao(heroi, 18, 0) || Game.checarPosicao(heroi, 19, 1) || Game.checarPosicao(heroi, 20, 1) || Game.checarPosicao(heroi, 21, 1) || Game.checarPosicao(heroi, 24, 2) || Game.checarPosicao(heroi, 23, 2))
            {
                MessageBox.Show("aaa");
                //background.Fase++;

                //background.transicao(1);
            }
        }
    }

    public class Mapa3_pos : Mapa2
    {
        public Background background;
        public Mapa2_pos(Background background) : base(background)
        {
            this.background = background;
            game.setLivre(15, 4);
            game.setLivre(13, 1);
            game.setLivre(14, 1);
            game.setLivre(15, 2);
            game.setLivre(12, 1);
            pintarBarreira = false;
        }

        public override void carregarGame()
        {
            base.carregarGame();

            heroi = new ObjHeroi(15, 12, heroiImg);

            Queue<String> msgs = new Queue<string>();
            msgs.Enqueue("Sinto muito, pensei que você era do clã de Glau Xia.");
            msgs.Enqueue("Pedi para minha amiga retirar o crocodilo para você.");
            msgs.Enqueue("Siga sua jornada pelo deserto.");
            aldeaoImg = new Bitmap(@"aldeao_abatido.png");
            aldeao = new ObjNpc(15, 3, aldeaoImg, msgs, false);
            game.setOcupado(aldeao.X, aldeao.Y);

            msgs = new Queue<string>();
            msgs.Enqueue("Esse crocodilo é o bixão kkkj.");
            aldeaoDImg = new Bitmap(@"aldea_abatida.png");
            aldeaoD = new ObjNpc(11, 2, aldeaoDImg, msgs, false);
            game.setOcupado(aldeaoD.X, aldeaoD.Y);
        }

        public override void keyDown(Object sender, KeyEventArgs e)
        {
            base.keyDown(sender, e);
            if (Game.checarPosicao(heroi, 17, 0) || Game.checarPosicao(heroi, 18, 0) || Game.checarPosicao(heroi, 19, 1) || Game.checarPosicao(heroi, 20, 1) || Game.checarPosicao(heroi, 21, 1) || Game.checarPosicao(heroi, 24, 2) || Game.checarPosicao(heroi, 23, 2))
            {
                MessageBox.Show("aaa");
                //background.Fase++;

                //background.transicao(1);
            }
        }
    }
}
