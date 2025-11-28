using System.Drawing;
using System.Windows.Forms;

namespace Casino_Multicast
{
    partial class Slot2Form
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnSpin;
        private Label lblBalance;
        private Label lblWinMessage;
        private Timer timer1;
        private ComboBox comboBet;

        // PictureBox для трьох слотів
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;


        // Додаткові PictureBox для 5 випадкових картинок (легенда чи анімація)
        private PictureBox[] extraPictures = new PictureBox[5];

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Slot2Form
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "Slot2Form";
            this.Load += new System.EventHandler(this.Slot2Form_Load);
            this.ResumeLayout(false);

        }

        private PictureBox pictureBox6;
        private PictureBox pictureBox7;
        private PictureBox pictureBox8;
        private PictureBox pictureBox9;
        private PictureBox pictureBox10;
    }
}
