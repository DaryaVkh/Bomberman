using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Bomberman
{
    partial class CongratsControl
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
            Next = new System.Windows.Forms.Button();
            //
            // Next
            //
            Next.FlatStyle = FlatStyle.Flat;
            Next.Font = new Font("Snowcard Gotic", 15F, FontStyle.Bold);
            Next.Margin = new Padding(3, 2, 3, 2);
            Next.Name = "Next";
            Next.Size = new Size(200, 40);
            Next.TabIndex = 0;
            Next.Text = "Продолжить";
            Next.UseVisualStyleBackColor = true;
            Next.FlatAppearance.BorderColor = Color.FromArgb(156, 34, 93);
            Next.BackColor = Color.FromArgb(213, 100, 124); 
            Next.Click += new System.EventHandler(this.Next_Click);
            //
            // Congrats
            //
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Image.FromFile(background.FullName);
            BackgroundImageLayout = ImageLayout.Stretch;
            Size = new Size(500, 300);
            Margin = new Padding(3, 4, 3, 4);
            Name = "WelcomeWindow";
            Controls.Add(Next);

            Next.Location = new Point(Width / 2 - Next.Width + 20, Height - Next.Height - 10);
        }

        #endregion

        private System.Windows.Forms.Button Next;
    }
}