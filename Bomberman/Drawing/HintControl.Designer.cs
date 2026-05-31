using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Bomberman
{
    partial class HintControl
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

        #region Component Designer generated code
        private static string hint1 = "Расправляйся с монстрами при помощи бомб.\r\nПробел — установить заряд. Взрыв последует через 2 секунды.";
        private static string hint2 = "Подбирай бонусы, чтобы усилить радиус взрыва и увеличить лимит бомб.\r\nОтслеживай свои параметры на верхней панели.";
        private static string hint3 = "Подрывай динамит, поставив рядом бомбу.\r\nНо будь начеку: его взрыв не имеет ограничений по радиусу!";
        private static string hint4 = "Ставь бомбу рядом с блоком, чтобы сдвинуть его взрывом.\r\nБлок давит всё на своём пути: и врагов, и тебя.";
        private static string hint5 = "Активируй все кнопки блоками, чтобы открыть дверь.\r\nСработавшие кнопки не требуют постоянного удержания.";
        private void InitializeComponent()
        {
            OK = new Button();
            HintTitle = new Label();
            HintText = new Label();
            //
            // HintText
            //
            HintText.BackColor = Color.Transparent;
            HintText.Font = new Font("Latin", 13.5F, FontStyle.Bold, GraphicsUnit.Point, ((byte) (0)));
            HintText.ForeColor = Color.FromArgb(213, 100, 124); 
            HintText.BorderStyle = BorderStyle.FixedSingle;
            HintText.TabStop = true;
            HintText.Name = "PauseText";
            HintText.Size = new Size(350, 200);
            HintText.TabStop = true;
            HintText.BorderStyle = BorderStyle.None;
            HintText.TextAlign = ContentAlignment.MiddleLeft;
            HintText.AllowDrop = false;
            HintText.Cursor = DefaultCursor;
            //
            // HintTitle
            //
            HintTitle.BackColor = Color.Transparent;
            HintTitle.Font = new Font("Elephant", 32F, FontStyle.Bold,
                GraphicsUnit.Point, ((byte) (0)));
            HintTitle.ForeColor = Color.FromArgb(156, 34, 93);
            HintTitle.Name = "HintTitle";
            HintTitle.Size = new Size(400, 60);
            HintTitle.TabStop = true;
            HintTitle.Text = "ПОДСКАЗКА";
            HintTitle.BorderStyle = BorderStyle.None;
            HintTitle.TextAlign = ContentAlignment.TopCenter;
            HintTitle.AllowDrop = false;
            HintTitle.Cursor = DefaultCursor;
            //
            // Next
            //
            OK.FlatStyle = FlatStyle.Flat;
            OK.Font = new Font("Snowcard Gotic", 15F);
            OK.Margin = new Padding(3, 2, 3, 2);
            OK.Name = "OK";
            OK.Size = new Size(200, 40);
            OK.TabIndex = 0;
            OK.Text = "Продолжить";
            OK.UseVisualStyleBackColor = true;
            OK.FlatAppearance.BorderColor = Color.FromArgb(156, 34, 93);
            OK.BackColor = Color.FromArgb(213, 100, 124); 
            OK.Click += new System.EventHandler(this.OK_Click);
            //
            // Congrats
            //
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Image.FromFile(background.FullName);
            BackgroundImageLayout = ImageLayout.Stretch;
            Size = new Size(500, 300);
            Margin = new Padding(3, 4, 3, 4);
            Name = "HintControl";
            Controls.Add(OK);
            Controls.Add(HintTitle);
            Controls.Add(HintText);

            OK.Location = new Point(Width / 2 - OK.Width / 2, Height - OK.Height - 10);
            HintTitle.Location = new Point((Width - HintTitle.Width) / 2, 0);
            HintText.Location = new Point(Width / 2 - 100, 50);
        }

        #endregion

        private Button OK;
        private Label HintTitle;
        private static Label HintText;
    }
}