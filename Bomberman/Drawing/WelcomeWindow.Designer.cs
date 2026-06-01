using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Bomberman
{
    partial class WelcomeWindow
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
            storyLine = new Queue<string>();
            storyLine.Enqueue("    Однажды Бомбермен случайно узнал правду: " +
                              "фабрикой тайно управляет безжалостная власть." +
                              "Оказалось, что бомбы, к производству " +
                              "которых он был причастен," +
                              "теперь используются для воплощения " +
                              "их злодейских планов.");
            storyLine.Enqueue("     Тогда он решил любой ценой остановить производство и сбежать." +
                              "Но коллеги отвернулись, " +
                              "а доносчик сдал его руководству." +
                              "Теперь весь завод настроен " +
                              "против Бомбермена.");
            storyLine.Enqueue("Ваша миссия — помочь Бомбермену добраться до пульта управления," +
                              "преодолев ловушки руководства и сопротивление бывших коллег. " +
                              "Успех навсегда остановит конвейер.");
            storyLine.Enqueue("       Уже сейчас за тобой начата погоня! Скорее убегай от разъярённых монстров в открытую дверь, вооружайся своими логикой и терпением, и вперед проходить испытания!");
            Next = new System.Windows.Forms.Button();
            Scip = new System.Windows.Forms.Button();
            Story = new Label();
            SuspendLayout();
            //
            // Story
            //
            Story.BackColor = Color.Transparent;
            Story.Font = new Font("Latin", 11.5F, FontStyle.Bold, GraphicsUnit.Point, ((byte) (0)));
            Story.ForeColor = Color.Black;
            Story.BorderStyle = BorderStyle.FixedSingle;
            Story.TabStop = true;
            Story.Name = "PauseText";
            Story.Size = new Size(600, 400);
            Story.TabStop = true;
            Story.Text = "  Знакомьтесь — Бомбермен.\r\n" +
                         "Сойдя с конвейера, он попал на фабрику бомб, " +
                         "где провёл всю жизнь, отлаживая производство.";
            Story.BorderStyle = BorderStyle.None;
            Story.TextAlign = ContentAlignment.MiddleLeft;
            Story.AllowDrop = false;
            Story.Cursor = DefaultCursor;
            //
            // Start
            //
            Next.FlatStyle = FlatStyle.Flat;
            Next.Font = new Font("Latin", 17F);
            Next.Margin = new Padding(3, 2, 3, 2);
            Next.Name = "Next";
            Next.Size = new Size(350, 80);
            Next.TabIndex = 0;
            Next.Text = "Далее";
            Next.UseVisualStyleBackColor = true;
            Next.FlatAppearance.BorderColor = Color.FromArgb(156, 34, 93);
            Next.BackColor = Color.FromArgb(213, 100, 124); 
            Next.Click += new System.EventHandler(this.Next_Click);
            //
            // Back
            //
            Scip.FlatStyle = FlatStyle.Flat;
            Scip.Font = new Font("Latin", 16F);
            Scip.Margin = new Padding(3, 2, 3, 2);
            Scip.Name = "Scip";
            Scip.Size = new Size(350, 80);
            Scip.TabIndex = 1;
            Scip.Text = "Пропустить";
            Scip.UseVisualStyleBackColor = true;
            Scip.FlatAppearance.BorderColor = Color.FromArgb(156, 34, 93);
            Scip.BackColor = Color.FromArgb(213, 100, 124); 
            Scip.Click += new System.EventHandler(this.Scip_Click);
            // 
            // WelcomeWindow
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            TransparencyKey = Color.SlateGray;
            BackgroundImage = Image.FromFile(background.FullName);
            BackgroundImageLayout = ImageLayout.Stretch;
            Size = new Size(900, 600);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "WelcomeWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WelcomeWindow";
            TopMost = true;
            Controls.Add(Story);
            Controls.Add(Next);
            Controls.Add(Scip);
            ResumeLayout(false);
            
            Story.Location = new Point(10, 10);
            Next.Location = new Point(Width - Scip.Width - 35, Height - Scip.Height - 20);
            Scip.Location = new Point(35, Height - Next.Height - 20);
        }

        #endregion

        private Queue<string> storyLine;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Button Scip;
        private Label Story;
    }
}