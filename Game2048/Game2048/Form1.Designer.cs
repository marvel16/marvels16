namespace Game2048
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
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
        private void InitializeComponent( )
        {
			this.label9 = new System.Windows.Forms.Label();
			this.scorebox = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(97, 9);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(85, 30);
			this.label9.TabIndex = 10;
			this.label9.Text = "SCORE";
			// 
			// scorebox
			// 
			this.scorebox.AutoSize = true;
			this.scorebox.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.scorebox.Location = new System.Drawing.Point(182, 8);
			this.scorebox.Name = "scorebox";
			this.scorebox.Size = new System.Drawing.Size(0, 30);
			this.scorebox.TabIndex = 11;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(406, 380);
			this.Controls.Add(this.scorebox);
			this.Controls.Add(this.label9);
			this.Name = "Form1";
			this.Text = "2048 by Marvel";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_pressed);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label scorebox;

	}
}

