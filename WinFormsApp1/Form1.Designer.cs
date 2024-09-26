namespace WinFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            drawGraphs = new Button();
            graphsPlace = new PictureBox();
            textBox2 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)graphsPlace).BeginInit();
            SuspendLayout();

            // textBox1
            textBox1.Location = new Point(12, 12);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(788, 23);
            textBox1.TabIndex = 0;

            // drawGraphs
            drawGraphs.BackColor = Color.Orange;
            drawGraphs.Location = new Point(842, 11);
            drawGraphs.Name = "drawGraphs";
            drawGraphs.Size = new Size(220, 23);
            drawGraphs.TabIndex = 1;
            drawGraphs.Text = "Отрисовать графики";
            drawGraphs.UseVisualStyleBackColor = false;
            drawGraphs.Click += button1_Click; // Замените на DrawGraphs_Click, если используете этот метод

            // graphsPlace
            graphsPlace.BackColor = SystemColors.Menu;
            graphsPlace.Location = new Point(12, 86);
            graphsPlace.Name = "graphsPlace";
            graphsPlace.Size = new Size(1064, 517);
            graphsPlace.TabIndex = 2;
            graphsPlace.TabStop = false;

            // textBox2
            textBox2.Location = new Point(12, 46);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(788, 23);
            textBox2.TabIndex = 3;

            // Form1
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(1088, 615);
            Controls.Add(textBox2);
            Controls.Add(graphsPlace);
            Controls.Add(drawGraphs);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)graphsPlace).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #region Поля
        private TextBox textBox1;
        private Button drawGraphs;
        private PictureBox graphsPlace;
        private TextBox textBox2;
        #endregion
    }
}
