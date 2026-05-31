using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace Bomberman
{
    partial class StartWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            CloseButton = new Button();
            NewGame = new Button();
            Saving = new Button();
            Rules = new Button();
            VolumeButton = new Button();
            Entertainment = new Label();
            SuspendLayout();
            //
            // Entertainment
            //
            Entertainment.BackColor = Color.Transparent;
            Entertainment.Font = new Font("Kristen ITC", 11F, FontStyle.Bold);
            Entertainment.ForeColor = Color.Black;
            Entertainment.Name = "PauseText";
            Entertainment.Size = new Size(1000, 50);
            Entertainment.TabStop = true;
            Entertainment.Text = "© 2020   Sweet   Brioches   Entertainment";
            Entertainment.BorderStyle = BorderStyle.None;
            Entertainment.TextAlign = ContentAlignment.TopCenter;
            Entertainment.AllowDrop = false;
            Entertainment.Cursor = DefaultCursor;
            //
            // CloseButton
            // 
            CloseButton.FlatStyle = FlatStyle.Flat;
            CloseButton.Font = new Font("Snowcard Gotic", 15F);
            CloseButton.Margin = new Padding(3, 2, 3, 2);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = ButtonSize;
            CloseButton.TabIndex = 0;
            CloseButton.Text = "Выход";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.FlatAppearance.BorderColor = Color.DarkSlateBlue;
            CloseButton.BackColor = Color.SlateBlue; 
            CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // NewGame
            // 
            NewGame.FlatStyle = FlatStyle.Flat;
            NewGame.Font = new Font("Snowcard Gotic", 15F);
            NewGame.Margin = new Padding(3, 2, 3, 2);
            NewGame.Name = "NewGame";
            NewGame.Size = ButtonSize;
            NewGame.TabIndex = 1;
            NewGame.Text = "Новая игра";
            NewGame.UseVisualStyleBackColor = true;
            NewGame.FlatAppearance.BorderColor = Color.DarkSlateBlue;
            NewGame.BackColor = Color.SlateBlue;
            NewGame.Click += new System.EventHandler(this.NewGame_Click);
            //
            // Saving
            //
            Saving.FlatStyle = FlatStyle.Flat;
            Saving.Font = new Font("Snowcard Gotic", 15F); Saving.Margin = new Padding(3, 2, 3, 2);
            Saving.Name = "Saving";
            Saving.Size = ButtonSize;
            Saving.TabIndex = 2;
            Saving.Text = "Загрузка";
            Saving.UseVisualStyleBackColor = true;
            Saving.FlatAppearance.BorderColor = Color.DarkSlateBlue;
            Saving.BackColor = Color.SlateBlue;
            Saving.Click += new System.EventHandler(this.Saving_Click);
            //
            // Rules
            //
            Rules.FlatStyle = FlatStyle.Flat;
            Rules.Font = new Font("Snowcard Gotic", 15F);
            Rules.Margin = new Padding(3, 2, 3, 2);
            Rules.Name = "Rules";
            Rules.Size = ButtonSize;
            Rules.TabIndex = 4;
            Rules.Text = "Правила";
            Rules.UseVisualStyleBackColor = true;
            Rules.FlatAppearance.BorderColor = Color.DarkSlateBlue;
            Rules.BackColor = Color.SlateBlue;
            Rules.Click += new System.EventHandler(this.Rules_Click);
            //
            // VolumeButton
            //
            VolumeButton.Size = new Size(70, 70);
            if (Program.EnableSound)
                VolumeButton.BackgroundImage = Image.FromFile(StartWindow.volumeIcon.FullName);
            else
                VolumeButton.BackgroundImage = Image.FromFile(StartWindow.volumeMuteIcon.FullName);
            VolumeButton.BackgroundImageLayout = ImageLayout.Stretch;
            VolumeButton.FlatStyle = FlatStyle.Popup;
            VolumeButton.FlatAppearance.BorderColor = Color.DarkSlateGray;
            VolumeButton.TabStop = true;
            VolumeButton.Name = "VolumeButton";
            VolumeButton.Click += new System.EventHandler(this.VolumeButton_Click);
            // 
            // StartWindow
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Image.FromFile(Path.Combine(Program.SlnPath, "Resources", "MainMenu.jpg"));
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(2000, 1000);
            Controls.Add(NewGame);
            Controls.Add(CloseButton);
            Controls.Add(Saving);
            Controls.Add(Rules);
            Controls.Add(VolumeButton);
            Controls.Add(Entertainment);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "StartWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StartWindow";
            Load += new System.EventHandler(this.StartWindow_Load);
            ResumeLayout(false);

            NewGame.Location = new Point((Width - NewGame.Width) / 2,
                Height / 2 - ButtonSize.Height - 20);
            Saving.Location = new Point((Width - Saving.Width) / 2,
                NewGame.Location.Y + 55);
            Rules.Location = new Point((Width - Saving.Width) / 2,
                Saving.Location.Y + 55);
            CloseButton.Location = new Point((Width - Saving.Width) / 2, 
                Rules.Location.Y + 55);
            VolumeButton.Location = new Point(Width - VolumeButton.Width, 0);
            Entertainment.Location = new Point((Width - Entertainment.Width) / 2, Height - 18);
        }

        #endregion

        private Button CloseButton;
        private Button NewGame;
        private Button Saving;
        private Button Rules;
        private Label Entertainment;
        public static Button VolumeButton;
    }
}

