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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Slot1Form));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnSpin = new System.Windows.Forms.Button();
            this.lblWinMessage = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.comboBoxBet = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(22, 133);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(260, 239);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Casino_Multicast.Properties.Resources.Multicast;
            this.pictureBox2.Location = new System.Drawing.Point(290, 133);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(260, 239);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Casino_Multicast.Properties.Resources.Multicast;
            this.pictureBox3.Location = new System.Drawing.Point(558, 133);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(261, 239);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // btnSpin
            // 
            this.btnSpin.BackColor = System.Drawing.Color.Purple;
            this.btnSpin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSpin.Location = new System.Drawing.Point(253, 404);
            this.btnSpin.Margin = new System.Windows.Forms.Padding(4);
            this.btnSpin.Name = "btnSpin";
            this.btnSpin.Size = new System.Drawing.Size(200, 82);
            this.btnSpin.TabIndex = 3;
            this.btnSpin.Text = "Крутити";
            this.btnSpin.UseVisualStyleBackColor = false;
            // 
            // lblWinMessage
            // 
            this.lblWinMessage.AutoSize = true;
            this.lblWinMessage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblWinMessage.ForeColor = System.Drawing.Color.Green;
            this.lblWinMessage.Location = new System.Drawing.Point(40, 320);
            this.lblWinMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWinMessage.Name = "lblWinMessage";
            this.lblWinMessage.Size = new System.Drawing.Size(0, 28);
            this.lblWinMessage.TabIndex = 4;
            this.lblWinMessage.Visible = false;
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblBalance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblBalance.Location = new System.Drawing.Point(40, 645);
            this.lblBalance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(156, 28);
            this.lblBalance.TabIndex = 5;
            this.lblBalance.Text = "Баланс: 0 монет";
            // 
            // comboBoxBet
            // 
            this.comboBoxBet.BackColor = System.Drawing.SystemColors.Highlight;
            this.comboBoxBet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBet.FormattingEnabled = true;
            this.comboBoxBet.Items.AddRange(new object[] {
            "5",
            "25",
            "100",
            "500",
            "1000",
            "10000"});
            this.comboBoxBet.Location = new System.Drawing.Point(461, 462);
            this.comboBoxBet.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxBet.Name = "comboBoxBet";
            this.comboBoxBet.Size = new System.Drawing.Size(159, 24);
            this.comboBoxBet.TabIndex = 6;
            // 
            // Slot1Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1195, 701);
            this.Controls.Add(this.comboBoxBet);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.lblWinMessage);
            this.Controls.Add(this.btnSpin);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Slot1Form";
            this.Text = "Слот 1";
            this.Load += new System.EventHandler(this.Slot1Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
