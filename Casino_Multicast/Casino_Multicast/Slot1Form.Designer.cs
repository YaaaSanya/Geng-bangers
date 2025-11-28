namespace Casino_Multicast
{
    partial class Slot1Form
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button btnSpin;
        private System.Windows.Forms.Label lblWinMessage;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.ComboBox comboBoxBet;
        private System.Windows.Forms.Timer timer1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Slot1Form
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "Slot1Form";
            this.Load += new System.EventHandler(this.Slot1Form_Load);
            this.ResumeLayout(false);

        }
    }
}
