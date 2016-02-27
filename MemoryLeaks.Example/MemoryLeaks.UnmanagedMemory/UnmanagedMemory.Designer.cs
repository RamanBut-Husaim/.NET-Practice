namespace MemoryLeaks.UnmanagedMemory
{
    partial class UnmanagedMemory
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
            this.btn_start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(216, 93);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(230, 59);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "Start Consumption";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // UnmanagedMemory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 246);
            this.Controls.Add(this.btn_start);
            this.Name = "UnmanagedMemory";
            this.Text = "Unamanged Memory";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_start;
    }
}

