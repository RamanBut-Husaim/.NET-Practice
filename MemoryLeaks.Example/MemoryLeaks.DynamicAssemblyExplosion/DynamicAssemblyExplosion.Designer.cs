namespace MemoryLeaks.DynamicAssemblyExplosion
{
    partial class DynamicAssemblyExplosion
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
            this.btn_explode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_explode
            // 
            this.btn_explode.Location = new System.Drawing.Point(206, 73);
            this.btn_explode.Name = "btn_explode";
            this.btn_explode.Size = new System.Drawing.Size(190, 53);
            this.btn_explode.TabIndex = 0;
            this.btn_explode.Text = "Explode";
            this.btn_explode.UseVisualStyleBackColor = true;
            this.btn_explode.Click += new System.EventHandler(this.btn_explode_Click);
            // 
            // DynamicAssemblyExplosion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 208);
            this.Controls.Add(this.btn_explode);
            this.Name = "DynamicAssemblyExplosion";
            this.Text = "Dynamic Assembly Explosion";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_explode;
    }
}

