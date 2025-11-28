using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Casino_Multicast
{
    public partial class RegisterForm : Form

    {
        private Button btnBack;
        private Panel topPanel;
        private Button closeButton;
        private Button minimizeButton;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnRegister;

        public RegisterForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;

            InitializeCustomTitleBar();
            InitializeFormContent();
        }

        private void InitializeFormContent()
        {
            lblUsername = new Label
            {
                Text = "👤 Логін:",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(60, 100),
                BackColor = Color.Transparent
            };
            Controls.Add(lblUsername);

            txtUsername = new TextBox
            {
                Location = new Point(160, 100),
                Width = 200,
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(255, 255, 255),
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(txtUsername);

            lblPassword = new Label
            {
                Text = "🔒 Пароль:",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(60, 150),
                BackColor = Color.Transparent
            };
            Controls.Add(lblPassword);

            txtPassword = new TextBox
            {
                Location = new Point(160, 150),
                Width = 200,
                Font = new Font("Segoe UI", 11),
                BackColor = Color.White,
                ForeColor = Color.Black,
                UseSystemPasswordChar = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(txtPassword);

            btnRegister = new Button
            {
                Text = "🚀 Зареєструватися",
                Location = new Point(160, 210),
                Width = 200,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.DarkOrange,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += BtnRegister_Click;

            btnRegister.MouseEnter += (s, e) =>
            {
                btnRegister.BackColor = Color.OrangeRed;
            };
            btnRegister.MouseLeave += (s, e) =>
            {
                btnRegister.BackColor = Color.DarkOrange;
            };

            Controls.Add(btnRegister);
            btnBack = new Button
            {
                Text = "← Назад",
                Location = new Point(10, this.ClientSize.Height - 50),
                Width = 100,
                Height = 35,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Gray,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += BtnBack_Click;
            btnBack.MouseEnter += (s, e) => btnBack.BackColor = Color.DimGray;
            btnBack.MouseLeave += (s, e) => btnBack.BackColor = Color.Gray;
            Controls.Add(btnBack);
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartForm startForm = new StartForm();
            startForm.ShowDialog();
            this.Close();
        }
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string login = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заповніть всі поля!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UserManager.LoadUsers(); // Завантаження з users.json

            if (UserManager.IsUsernameTaken(login))
            {
                MessageBox.Show("Користувач з таким логіном вже існує!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Створення нового користувача
            User newUser = new User
            {
                Username = login,
                Password = password,
                Balance = 1000,
                RegistrationDate = DateTime.Now
            };

            UserManager.Users.Add(newUser);
            UserManager.SaveUsers(); // Запис у users.json

            MessageBox.Show("Реєстрація успішна!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.Close();
        }



        private void InitializeCustomTitleBar()
        {
            topPanel = new Panel
            {
                Height = 30,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(25, 25, 112)
            };
            topPanel.MouseDown += TopPanel_MouseDown;
            Controls.Add(topPanel);

            minimizeButton = new Button
            {
                Text = "─",
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(40, 30),
                Location = new Point(this.Width - 80, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            minimizeButton.FlatAppearance.BorderSize = 0;
            minimizeButton.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
            topPanel.Controls.Add(minimizeButton);

            closeButton = new Button
            {
                Text = "✖",
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(40, 30),
                Location = new Point(this.Width - 40, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Click += (s, e) => this.Close();
            topPanel.Controls.Add(closeButton);
        }

        [DllImport("user32.dll")] public static extern bool ReleaseCapture();
        [DllImport("user32.dll")] public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
