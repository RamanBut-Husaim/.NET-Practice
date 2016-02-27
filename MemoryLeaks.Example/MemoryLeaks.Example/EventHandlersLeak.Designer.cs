namespace MemoryLeaks.Example
{
    partial class EventHandlersLeak
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
            this.btn_allocate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_allocate
            // 
            this.btn_allocate.Location = new System.Drawing.Point(181, 38);
            this.btn_allocate.Name = "btn_allocate";
            this.btn_allocate.Size = new System.Drawing.Size(219, 72);
            this.btn_allocate.TabIndex = 0;
            this.btn_allocate.Text = "Allocate";
            this.btn_allocate.UseVisualStyleBackColor = true;
            this.btn_allocate.Click += new System.EventHandler(this.btn_allocate_Click);
            // 
            // EventHandlersLeak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 146);
            this.Controls.Add(this.btn_allocate);
            this.Name = "EventHandlersLeak";
            this.Text = "Event Handlers";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_allocate;
    }
}

