using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace Bomberman
{
    public partial class StartWindow : Form
    {
        private static readonly string ImagesPath = Path.Combine(Program.SlnPath, "Images");
        private static readonly Size ButtonSize = new Size(300, 70);
        public static FileInfo volumeIcon = Window.Icons.GetFiles("Volume.png").First();
        public static FileInfo volumeMuteIcon = Window.Icons.GetFiles("VolumeMute.png").First();
        private static readonly string soundFile = Path.Combine(Program.SoundsPath, "menu.wav");
        private SoundPlayer player;

        public StartWindow()
        {
            InitializeComponent();
            if (File.Exists(soundFile))
            {
                player = new SoundPlayer(soundFile);
                player.Play();
            }
        }
        
        protected override void OnClosed(EventArgs e)
        {
            Environment.Exit(0);
        }

        private void CloseButton_Click(object sender, EventArgs e) => Application.Exit();

        private void NewGame_Click(object sender, EventArgs e)
        {
            player.Stop();
            Hide();
            var gameWindow = new Window(this, new DirectoryInfo(ImagesPath));
            var welcomeWindow = new WelcomeWindow(gameWindow);
            welcomeWindow.Show();
            gameWindow.timer.Stop();
            gameWindow.gameState.Pause();
        }

        private void Saving_Click(object sender, EventArgs e)
        {
            if (File.Exists(Program.SavePath))
            {
                player.Stop();
                var save = File.ReadAllText(Program.SavePath);
                if (int.TryParse(save, out var levelId))
                    for (int i = 0; i < levelId; i++)
                    {
                        Program.LevelsToPlay.Dequeue();
                        Game.Level++;
                    }
                Hide();
                var gameWindow = new Window(this, new DirectoryInfo(ImagesPath));
                gameWindow.Show();
            }
        }

        private void Rules_Click(object sender, EventArgs e)
        {
            var rules = new Rules(this);
            rules.Show();
            Hide();
        }

        private void VolumeButton_Click(object sender, EventArgs e)
        {
            if (Program.EnableSound)
            {
                VolumeButton.BackgroundImage = Image.FromFile(volumeMuteIcon.FullName);
                VolumeButton.BackgroundImageLayout = ImageLayout.Stretch;
                Program.EnableSound = false;
                player?.Stop();
            }
            else
            {
                VolumeButton.BackgroundImage = Image.FromFile(volumeIcon.FullName);
                VolumeButton.BackgroundImageLayout = ImageLayout.Stretch;
                Program.EnableSound = true;
                if (File.Exists(soundFile))
                {
                    player = new SoundPlayer(soundFile);
                    player.Play();
                }
            }
        }

        private void StartWindow_Load(object sender, EventArgs e)
        {
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