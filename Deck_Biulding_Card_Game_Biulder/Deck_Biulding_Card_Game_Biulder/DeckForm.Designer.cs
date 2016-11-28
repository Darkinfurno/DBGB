namespace Deck_Biulding_Card_Game_Biulder
{
    partial class DeckForm
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
            this.Left = new System.Windows.Forms.Button();
            this.Right = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Left
            // 
            this.Left.Location = new System.Drawing.Point(12, 159);
            this.Left.Name = "Left";
            this.Left.Size = new System.Drawing.Size(30, 30);
            this.Left.TabIndex = 0;
            this.Left.Text = "←";
            this.Left.UseVisualStyleBackColor = true;
            this.Left.Click += new System.EventHandler(this.Right_Click);
            // 
            // Right
            // 
            this.Right.Location = new System.Drawing.Point(1058, 159);
            this.Right.Name = "Right";
            this.Right.Size = new System.Drawing.Size(30, 30);
            this.Right.TabIndex = 1;
            this.Right.Text = "→";
            this.Right.UseVisualStyleBackColor = true;
            this.Right.Click += new System.EventHandler(this.Left_Click);
            // 
            // Deck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 360);
            this.Controls.Add(this.Right);
            this.Controls.Add(this.Left);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Name = "Deck";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Deck_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Left;
        private System.Windows.Forms.Button Right;
    }
}