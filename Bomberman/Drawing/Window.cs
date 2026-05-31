using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace Bomberman
{
    public class Window : Form
    {
        private readonly Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        private static readonly string IconsPath = Path.Combine(Program.SlnPath, "Resources");
        public static readonly DirectoryInfo Icons = new DirectoryInfo(IconsPath);
        private static FileInfo bombIcon = Icons.GetFiles("BombIcon.png").First();
        private static FileInfo splashIcon = Icons.GetFiles("SplashIcon.png").First();
        public readonly GameState gameState;
        private readonly HashSet<Keys> pressedKeys = new HashSet<Keys>();
        private readonly HashSet<Keys> keysToRemove = new HashSet<Keys>();
        private int tickCount;
        public Timer timer = new Timer {Interval = 15};
        private StartWindow mainMenu;
        public static Pause pause;
        private CongratsControl congrats;
        private HintControl hint;
        public static int Bombs = 1;
        public static int Splash = 1;

        public Window(StartWindow startWindow, DirectoryInfo imagesDirectory = null)
        {
            mainMenu = startWindow;
            hint = new HintControl(this) {Size = new Size(500, 300), Dock = DockStyle.None};
            gameState = new GameState();
            ClientSize = new Size(
                GameState.ElementSize * Game.MapWidth,
                GameState.ElementSize * Game.MapHeight + GameState.ElementSize / 2);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            imagesDirectory ??= new DirectoryInfo("Images");
            foreach (var e in imagesDirectory.GetFiles("*.png"))
                bitmaps[e.Name] = (Bitmap) Image.FromFile(e.FullName);
            timer.Tick += TimerTick;
            timer.Start();
            pause = new Pause(mainMenu, this);
            hint.Location = new Point((Width - hint.Width) / 2, (Height - hint.Height) / 2);
            pause.Location = new Point((Width - hint.Width) / 2, (Height - hint.Height) / 2);
            Controls.Add(hint);
            Controls.Add(pause);
            Focus();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "Bomberman";
            DoubleBuffered = true;
        }

        protected override void OnClosed(EventArgs e)
        {
            Environment.Exit(0);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                timer.Stop();
                pause.Focus();
                pause.Show();
                gameState.Pause();
                Game.KeyPressed = Keys.None;
                pressedKeys.Clear();
            }
            else
            {
                pressedKeys.Add(e.KeyCode);
            }
        }
        
        protected override void OnKeyUp(KeyEventArgs e)
        {
            keysToRemove.Add(e.KeyCode);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(0, 23);
            e.Graphics.FillRectangle(Brushes.White, 0, 0, 
                GameState.ElementSize * Game.MapWidth,
                GameState.ElementSize * Game.MapHeight + GameState.ElementSize / 2);
            foreach (var a in gameState.Animations)
                e.Graphics.DrawImage(
                    bitmaps[a.Creature.GetImageFileName()],
                    a.Location.X,
                    a.Location.Y,
                    GameState.ElementSize,
                    GameState.ElementSize);
            e.Graphics.ResetTransform();
            e.Graphics.DrawString($"Level: {Game.Level} / {Program.AllLevels.Count - 1}", 
                new Font("Arial", 16, FontStyle.Bold), Brushes.Black, 0, 0);
            e.Graphics.DrawImage((Bitmap) Image.FromFile(bombIcon.FullName), (float)(GameState.ElementSize * 3.5 + 5), 0);
            e.Graphics.DrawString($"×{Bombs}", new Font("Arial", 16), 
                Brushes.Black, GameState.ElementSize * 4 - 3, 0);
            e.Graphics.DrawImage((Bitmap) Image.FromFile(splashIcon.FullName), GameState.ElementSize * 5 + 5, 0);
            e.Graphics.DrawString($"×{Splash}", new Font("Arial", 16), 
                Brushes.Black, (float)(GameState.ElementSize * 5.5), 0);
        }

        private void TimerTick(object sender, EventArgs args)
        {
            if (Game.IsOver)
            {
                var winWindow = new WinWindow(mainMenu);
                winWindow.Show();
                timer.Stop();
                Hide();
            }

            if (Game.IsRemoteControl)
            {
                timer.Stop();
                gameState.Pause();
                Game.KeyPressed = Keys.None;
                pressedKeys.Clear();
                congrats = new CongratsControl(this);
                congrats.Location = new Point((Width - hint.Width) / 2, (Height - hint.Height) / 2);
                Controls.Add(congrats);
                congrats.Focus();
                Game.IsRemoteControl = false;
            }
            
            if (Game.Hint1 || Game.Hint2 || Game.Hint3 || Game.Hint4 || Game.Hint5)
            {
                hint.Focus();
                timer.Stop();
                tickCount = 0;
                gameState.Pause();
                Game.KeyPressed = Keys.None;
                pressedKeys.Clear();
                hint.SetHintText();
                hint.Show();
            }

            Game.KeyPressed = pressedKeys.Contains(Keys.Space)
                ? Keys.Space
                : pressedKeys.Any()
                    ? pressedKeys.Min()
                    : Keys.None;
            
            if (tickCount == 0) gameState.BeginAct();
            foreach (var e in gameState.Animations)
                e.Location = new Point(e.Location.X + 4 * e.Command.DeltaX, e.Location.Y + 4 * e.Command.DeltaY);
            if (tickCount == 7)
                gameState.EndAct();
            tickCount++;
            if (tickCount == 8) tickCount = 0;
            Invalidate();
            
            pressedKeys.ExceptWith(keysToRemove);
            keysToRemove.Clear();
        }
        
        protected override CreateParams CreateParams 
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            } 
        }
    }
}