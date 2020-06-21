namespace Digraph.WinForms
{
    partial class DevForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.graphPanel1 = new Digraph.WinForms.GraphPanel();
            this.SuspendLayout();
            // 
            // graphPanel1
            // 
            this.graphPanel1.AutoScroll = true;
            this.graphPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphPanel1.Location = new System.Drawing.Point(0, 0);
            this.graphPanel1.Name = "graphPanel1";
            this.graphPanel1.Size = new System.Drawing.Size(800, 450);
            this.graphPanel1.TabIndex = 0;
            // 
            // DevForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.graphPanel1);
            this.Name = "DevForm";
            this.Text = "DevForm";
            this.ResumeLayout(false);

        }

        #endregion

        private GraphPanel graphPanel1;
    }
}

