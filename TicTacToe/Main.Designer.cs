namespace TicTacToe
{
    partial class Main
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
            this.mainView1 = new TicTacToe.MainView();
            this.SuspendLayout();
            // 
            // mainView1
            // 
            this.mainView1.BackColor = System.Drawing.Color.White;
            this.mainView1.Location = new System.Drawing.Point(0, 0);
            this.mainView1.Name = "mainView1";
            this.mainView1.PlayerOneMarker = "X";
            this.mainView1.PlayerTwoMarker = "0";
            this.mainView1.Size = new System.Drawing.Size(470, 470);
            this.mainView1.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 470);
            this.Controls.Add(this.mainView1);
            this.Name = "Main";
            this.Text = "TicTacToe";
            this.ResumeLayout(false);

        }

        #endregion

        private MainView mainView1;
    }
}

