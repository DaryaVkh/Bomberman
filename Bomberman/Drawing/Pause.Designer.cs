using System.ComponentModel;
using System.Drawing;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace Bomberman
{
    partial class Pause
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
            PauseText = new Label();
            BackInGame = new System.Windows.Forms.Button();
            Save = new System.Windows.Forms.Button();
            InMainMenu = new System.Windows.Forms.Button();
            Exit = new System.Windows.Forms.Button();
            VolumeButton = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // Pause
            // 
            PauseText.BackColor = Color.Transparent;
            PauseText.Font = new Font("Elephant", 42F, FontStyle.Bold,
                GraphicsUnit.Point, ((byte) (0)));
            PauseText.ForeColor = Color.Cyan;
            PauseText.Name = "PauseText";
            PauseText.Size = new Size(450, 200);
            PauseText.TabStop = true;
            PauseText.Text = "ПАУЗА";
            PauseText.BorderStyle = BorderStyle.None;
            PauseText.TextAlign = ContentAlignment.TopCenter;
            PauseText.AllowDrop = false;
            PauseText.Cursor = DefaultCursor;
            // 
            // BackInGame
            // 
            BackInGame.FlatStyle = FlatStyle.Flat;
            BackInGame.Name = "BackInGame";
            BackInGame.Size = new Size(350, 70);
            BackInGame.TabIndex = 0;
            BackInGame.Font = new Font("Snowcard Gotic", 15F);
            BackInGame.Text = "Продолжить";
            BackInGame.BackColor = Color.Cyan;
            BackInGame.FlatAppearance.BorderColor = Color.DarkCyan;
            BackInGame.UseVisualStyleBackColor = true;
            BackInGame.Click += new System.EventHandler(this.BackInGame_Click);
            //
            // Save
            //
            Save.FlatStyle = FlatStyle.Flat;
            Save.Name = "Save";
            Save.Size = new Size(350, 70);
            Save.TabIndex = 0;
            Save.Font = new Font("Snowcard Gotic", 15F);
            Save.Text = "Сохранить";
            Save.BackColor = Color.Cyan;
            Save.FlatAppearance.BorderColor = Color.DarkCyan;
            Save.UseVisualStyleBackColor = true;
            Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // InMainMenu
            // 
            InMainMenu.FlatStyle = FlatStyle.Flat;
            InMainMenu.Name = "InMainMenu";
            InMainMenu.Size = new Size(350, 70);
            InMainMenu.TabIndex = 1;
            InMainMenu.Font = new Font("Snowcard Gotic", 15F);
            InMainMenu.Text = "В главное меню";
            InMainMenu.BackColor = Color.Cyan;
            InMainMenu.FlatAppearance.BorderColor = Color.DarkCyan;
            InMainMenu.UseVisualStyleBackColor = true;
            InMainMenu.Click += new System.EventHandler(this.InMainMenu_Click);
            //
            // Exit
            //
            Exit.FlatStyle = FlatStyle.Flat;
            Exit.Name = "Exit";
            Exit.Size = new Size(350, 70);
            Exit.TabIndex = 2;
            Exit.Font = new Font("Snowcard Gotic", 15F);
            Exit.Text = "Выход";
            Exit.BackColor = Color.Cyan;
            Exit.FlatAppearance.BorderColor = Color.DarkCyan;
            Exit.UseVisualStyleBackColor = true;
            Exit.Click += new System.EventHandler(this.Exit_click);
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
            // Pause
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(120, Color.Black);
            ClientSize = new Size(3 * game.Width / 4, 
                PauseText.Height + BackInGame.Height + InMainMenu.Height + Exit.Height + Save.Height + 110);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Pause";
            Text = "Pause";
            Controls.Add(InMainMenu);
            Controls.Add(BackInGame);
            Controls.Add(Save);
            Controls.Add(Exit);
            Controls.Add(PauseText);
            Controls.Add(VolumeButton);
            ResumeLayout(false);
            
            PauseText.Location = new Point((Width - PauseText.Width) / 2, PauseText.Height / 5);
            BackInGame.Location = new Point((Width - BackInGame.Width) / 2, PauseText.Height);
            Save.Location = new Point((Width - InMainMenu.Width) / 2, BackInGame.Location.Y + 50);
            InMainMenu.Location = new Point((Width - Exit.Width) / 2, Save.Location.Y + 50);
            Exit.Location = new Point((Width - Exit.Width) / 2, InMainMenu.Location.Y + 50);
            VolumeButton.Location = new Point(Width - VolumeButton.Width, 0);
        }

        #endregion

        public System.Windows.Forms.Button Save;
        private Label PauseText;
        private System.Windows.Forms.Button InMainMenu;
        private System.Windows.Forms.Button BackInGame;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button VolumeButton;
    }
}