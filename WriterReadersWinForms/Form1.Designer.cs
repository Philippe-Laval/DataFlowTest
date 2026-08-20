namespace WriterReadersWinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox4 = new CheckBox();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(100, 57);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(102, 29);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Reader1";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(100, 110);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(102, 29);
            checkBox2.TabIndex = 1;
            checkBox2.Text = "Reader2";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(100, 162);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(102, 29);
            checkBox3.TabIndex = 2;
            checkBox3.Text = "Reader3";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(100, 215);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(86, 29);
            checkBox4.TabIndex = 3;
            checkBox4.Text = "Writer";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            timer1.Interval = 2500;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(checkBox4);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private System.Windows.Forms.Timer timer1;
    }
}
