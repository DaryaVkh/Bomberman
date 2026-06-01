using System.ComponentModel;
using System.Drawing;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace Bomberman
{
    partial class WinWindow
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
            Exit = new System.Windows.Forms.Button();
            InMainMenu = new System.Windows.Forms.Button();
            SuspendLayout();
            //
            // InMainMenu
            //
            InMainMenu.FlatStyle = FlatStyle.Flat;
            InMainMenu.Font = new Font("Snowcard Gotic", 15F);
            InMainMenu.Margin = new Padding(3, 2, 3, 2);
            InMainMenu.Name = "InMainMenu";
            InMainMenu.Size = new Size(280, 80);
            InMainMenu.TabIndex = 0;
            InMainMenu.Text = "Главное меню";
            InMainMenu.UseVisualStyleBackColor = true;
            InMainMenu.FlatAppearance.BorderColor = Color.FromArgb(156, 34, 93);
            InMainMenu.BackColor = Color.FromArgb(213, 100, 124); 
            InMainMenu.Click += new System.EventHandler(this.InMainMenu_Click);
            //
            // CloseButton
            // 
            Exit.FlatStyle = FlatStyle.Flat;
            Exit.Font = new Font("Snowcard Gotic", 15F);
            Exit.Margin = new Padding(3, 2, 3, 2);
            Exit.Name = "Exit";
            Exit.Size = new Size(280, 80);
            Exit.TabIndex = 0;
            Exit.Text = "Выход";
            Exit.UseVisualStyleBackColor = true;
            Exit.FlatAppearance.BorderColor = Color.FromArgb(156, 34, 93);
            Exit.BackColor = Color.FromArgb(213, 100, 124); 
            Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // WinWindow
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Image.FromFile(backgroundImage.FullName);
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(900, 600);
            ControlBox = false;
            Controls.Add(Exit);
            Controls.Add(InMainMenu);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "WinWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WinWindow";
            ResumeLayout(false);
            PerformLayout();
            
            Exit.Location = new Point(Width - Exit.Width - 20, 30);
            InMainMenu.Location = new Point(20, 30);
        }

        #endregion

        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button InMainMenu;
    }
}