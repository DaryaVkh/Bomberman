using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bomberman
{
    partial class Rules
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
            Back = new System.Windows.Forms.Button();
            //
            // Back
            //
            Back.FlatStyle = FlatStyle.Flat;
            Back.Font = new Font("Snowcard Gotic", 15F, FontStyle.Bold);
            Back.Margin = new Padding(3, 2, 3, 2);
            Back.Name = "Back";
            Back.Size = new Size(200, 40);
            Back.TabIndex = 0;
            Back.Text = "Назад";
            Back.UseVisualStyleBackColor = true;
            Back.FlatAppearance.BorderColor = Color.FromArgb(156, 34, 93);
            Back.BackColor = Color.FromArgb(213, 100, 124); 
            Back.Click += new System.EventHandler(this.Back_Click);
            //
            // Rules
            //
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Image.FromFile(background.FullName);
            BackgroundImageLayout = ImageLayout.Stretch;
            Size = new Size(900, 500);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Rules";
            Controls.Add(Back);
            StartPosition = FormStartPosition.CenterScreen;
            
            Back.Location = new Point((Width - Back.Width) / 2, Height - Back.Height - 5);
        }

        #endregion

        private System.Windows.Forms.Button Back;
    }
}