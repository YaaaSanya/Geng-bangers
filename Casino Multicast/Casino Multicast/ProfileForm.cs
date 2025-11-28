using System;
using System.Drawing;
using System.Windows.Forms;

namespace Casino_Multicast
{
    public partial class ProfileForm : Form
    {
        private User currentUser;

        // Лейбли
        private Label lblUsername;
        private Label lblBalance;
        private Label lblRegistrationDate;
        private Label lblSlot1Plays;
        private Label lblSlot2Plays;
        private Label lblSlot3Plays;

        public ProfileForm(User user)
        {
            InitializeComponent();
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;

            if (user == null)
            {
                MessageBox.Show("Користувач не переданий!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            currentUser = user;

            this.BackColor = Color.FromArgb(25, 25, 25); // темна тема
            this.Size = new Size(400, 350);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Text = "Особистий кабінет";

            Font defaultFont = new Font("Segoe UI", 11F, FontStyle.Bold);
            Color textColor = Color.Gold;

            // 🔽 Створення лейблів
            lblUsername = new Label()
            {
                Font = defaultFont,
                ForeColor = textColor,
                Location = new Point(20, 30),
                AutoSize = true
            };

            lblBalance = new Label()
            {
                Font = defaultFont,
                ForeColor = Color.LawnGreen,
                Location = new Point(20, 65),
                AutoSize = true
            };

            lblRegistrationDate = new Label()
            {
                Font = defaultFont,
                ForeColor = textColor,
                Location = new Point(20, 100),
                AutoSize = true
            };

            lblSlot1Plays = new Label()
            {
                Font = defaultFont,
                ForeColor = Color.Orange,
                Location = new Point(20, 150),
                AutoSize = true
            };

            lblSlot2Plays = new Label()
            {
                Font = defaultFont,
                ForeColor = Color.Orange,
                Location = new Point(20, 185),
                AutoSize = true
            };

            lblSlot3Plays = new Label()
            {
                Font = defaultFont,
                ForeColor = Color.Orange,
                Location = new Point(20, 220),
                AutoSize = true
            };

            Random rnd = new Random();
            int choice = rnd.Next(1, 4); // від 1 до 3 включно

            switch (choice)
            {
                case 1:
                    pictureBox1.Visible = true;
                    break;
                case 2:
                    pictureBox2.Visible = true;
                    break;
                case 3:
                    pictureBox3.Visible = true;
                    break;
            }

            // 🔽 Тепер присвоюємо текст — після створення
            lblUsername.Text = $"Користувач: {currentUser.Username}";
            lblBalance.Text = $"Баланс: {currentUser.Balance} грн";
            lblRegistrationDate.Text = $"Дата реєстрації: {(currentUser.RegistrationDate == DateTime.MinValue ? "Невідомо" : currentUser.RegistrationDate.ToShortDateString())}";


            // 🔽 Додаємо на форму
            this.Controls.Add(lblUsername);
            this.Controls.Add(lblBalance);
            this.Controls.Add(lblRegistrationDate);

        }

        private void ProfileForm_Load(object sender, EventArgs e)
        {

        }
    }
}
