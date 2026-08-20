using System.Threading.Tasks.Dataflow;


/*
 * UI Thread
 * 
Because the incrementProgress and decrementProgress dataflow blocks 
act on the user interface, it is important that these actions occur 
on the user-interface thread. To accomplish this, during construction 
these objects each provide an ExecutionDataflowBlockOptions object that 
has the TaskScheduler property set to TaskScheduler.FromCurrentSynchronizationContext. 
The TaskScheduler.FromCurrentSynchronizationContext method creates a TaskScheduler 
object that performs work on the current synchronization context. 
Because the Form1 constructor is called from the user-interface thread, 
the actions for the incrementProgress and decrementProgress dataflow 
blocks also run on the user-interface thread.
 */

namespace CancellationWinForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            toolStrip1 = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            toolStripButton2 = new ToolStripButton();
            toolStripProgressBar1 = new ToolStripProgressBar();
            toolStripProgressBar2 = new ToolStripProgressBar();
            toolStripProgressBar3 = new ToolStripProgressBar();
            toolStripProgressBar4 = new ToolStripProgressBar();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripButton2, toolStripProgressBar1, toolStripProgressBar2, toolStripProgressBar3, toolStripProgressBar4 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(800, 34);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(146, 29);
            toolStripButton1.Text = "Add Work Items";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton2.Enabled = false;
            toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(67, 29);
            toolStripButton2.Text = "Cancel";
            toolStripButton2.Click += toolStripButton2_Click;
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(100, 29);
            // 
            // toolStripProgressBar2
            // 
            toolStripProgressBar2.Name = "toolStripProgressBar2";
            toolStripProgressBar2.Size = new Size(100, 29);
            // 
            // toolStripProgressBar3
            // 
            toolStripProgressBar3.Name = "toolStripProgressBar3";
            toolStripProgressBar3.Size = new Size(100, 29);
            // 
            // toolStripProgressBar4
            // 
            toolStripProgressBar4.Name = "toolStripProgressBar4";
            toolStripProgressBar4.Size = new Size(100, 29);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(toolStrip1);
            Name = "Form1";
            Text = "Form1";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripProgressBar toolStripProgressBar2;
        private ToolStripProgressBar toolStripProgressBar3;
        private ToolStripProgressBar toolStripProgressBar4;
    }
}
