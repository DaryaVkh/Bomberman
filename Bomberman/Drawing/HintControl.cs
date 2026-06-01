using System.Windows.Forms;
using System;
using System.IO;
using System.Linq;

namespace Bomberman
{
    public partial class HintControl : UserControl
    {
        private Window game;
        private static FileInfo background = Window.Icons.GetFiles("Hint.jpg").First();
        
        public HintControl(Window game)
        {
            InitializeComponent();
            Hide();
            this.game = game;
        }

        private void OKClick(object sender, EventArgs e)
        {
            game.gameState.Unpause();
            game.timer.Start();
            Hide();
            game.Focus();
        }

        public void SetHint()
        {
            if (Game.Hint1)
            {
                HintText.Text = hint1;
                Game.Hint1 = false;
            }
            else if (Game.Hint2)
            {
                HintText.Text = hint2;
                Game.Hint2 = false;
            }
            else if (Game.Hint3)
            {
                HintText.Text = hint3;
                Game.Hint3 = false;
            }
            else if (Game.Hint4)
            {
                HintText.Text = hint4;
                Game.Hint4 = false;
            }
            else if (Game.Hint5)
            {
                HintText.Text = hint5;
                Game.Hint5 = false;
            }
            else if (Game.Hint6)
            {
                HintText.Text = hint6;
                Game.Hint6 = false;
            }
            else if (Game.Hint7)
            {
                HintText.Text = hint7;
                Game.Hint7 = false;
            }

            Game.IsHint = false;
        }
    }
}