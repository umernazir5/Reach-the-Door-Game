namespace Game_app
{
    partial class MainMenu
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
            this.playbutton = new System.Windows.Forms.PictureBox();
            this.EnterYourCode = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.playbutton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnterYourCode)).BeginInit();
            this.SuspendLayout();
            // 
            // playbutton
            // 
            this.playbutton.BackColor = System.Drawing.Color.Transparent;
            this.playbutton.Image = global::Game_app.Properties.Resources.PlayButton;
            this.playbutton.Location = new System.Drawing.Point(521, 426);
            this.playbutton.Name = "playbutton";
            this.playbutton.Size = new System.Drawing.Size(227, 88);
            this.playbutton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playbutton.TabIndex = 0;
            this.playbutton.TabStop = false;
            this.playbutton.Click += new System.EventHandler(this.playbutton_Click);
            // 
            // EnterYourCode
            // 
            this.EnterYourCode.BackColor = System.Drawing.Color.Transparent;
            this.EnterYourCode.Image = global::Game_app.Properties.Resources.EnterYourCode;
            this.EnterYourCode.Location = new System.Drawing.Point(476, 520);
            this.EnterYourCode.Name = "EnterYourCode";
            this.EnterYourCode.Size = new System.Drawing.Size(306, 101);
            this.EnterYourCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.EnterYourCode.TabIndex = 1;
            this.EnterYourCode.TabStop = false;
            this.EnterYourCode.Click += new System.EventHandler(this.EnterYourCode_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Game_app.Properties.Resources.Mainmenu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.EnterYourCode);
            this.Controls.Add(this.playbutton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1280, 720);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainMenu";
            ((System.ComponentModel.ISupportInitialize)(this.playbutton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnterYourCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox playbutton;
        private System.Windows.Forms.PictureBox EnterYourCode;
    }
}