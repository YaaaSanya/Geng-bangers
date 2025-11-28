namespace Casino_Multicast
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelSlot1;
        private System.Windows.Forms.PictureBox pictureBoxSlot1;
        private System.Windows.Forms.PictureBox pictureBoxSlot2;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label labelSlot2;
        private System.Windows.Forms.Label labelRoulette;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBoxRoulette;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}
