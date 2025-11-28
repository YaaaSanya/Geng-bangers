using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Casino_Multicast
{
    public partial class LoginForm : Form
    {
        private Button closeButton;
        private Button minimizeButton;
        private Panel topPanel;
        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Dictionary<string, string> users = new Dictionary<string, string>();

        public LoginForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(30, 30, 60);
            this.ClientSize = new Size(600, 450);
            InitializeCustomTitleBar();
            InitializeLabels();
            InitializeInputs();
            LoadUsers();

        }

        private void LoadUsers()
        {
            if (File.Exists("users.txt"))
            {
                foreach (string line in File.ReadAllLines("users.txt"))
                {
                    var parts = line.Split(';');
                    if (parts.Length == 2 && !users.ContainsKey(parts[0]))
                        users[parts[0]] = parts[1];
                }
            }
        }

        private void InitializeLabels()
        {
            lblTitle = new Label
            {
                Text = "🎰 Вхід у казино",
                Font = new Font("Segoe UI", 27, FontStyle.Bold), // 18 → 27
                ForeColor = Color.Gold,
                Location = new Point(160, 60),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            Controls.Add(lblTitle);

            lblUsername = new Label
            {
                Text = "👤 Логін:",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 17, FontStyle.Bold), // 11 → 17
                Location = new Point(100, 170),
                AutoSize = true,
                BackColor = Color.Black
            };
            Controls.Add(lblUsername);

            lblPassword = new Label
            {
                Text = "🔒 Пароль:",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 17, FontStyle.Bold),
                Location = new Point(100, 240),
                AutoSize = true,
                BackColor = Color.Black
            };
            Controls.Add(lblPassword);
        }



        private void InitializeInputs()
        {
            txtUsername = new TextBox
            {
                Location = new Point(240, 165),
                Width = 250,
                Font = new Font("Segoe UI", 15),
                BackColor = Color.White,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(txtUsername);

            txtPassword = new TextBox
            {
                Location = new Point(240, 235),
                Width = 250,
                UseSystemPasswordChar = true,
                Font = new Font("Segoe UI", 15),
                BackColor = Color.White,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(txtPassword);

            btnLogin = new Button
            {
                Text = "🚀 Увійти",
                Location = new Point(240, 310),
                Width = 250,
                Height = 55,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.DarkOrange,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;

            btnLogin.MouseEnter += (s, e) => btnLogin.BackColor = Color.OrangeRed;
            btnLogin.MouseLeave += (s, e) => btnLogin.BackColor = Color.DarkOrange;
            btnLogin.Click += BtnLogin_Click;

            Controls.Add(btnLogin);
            btnBack = new Button
            {
                Text = "⬅ Назад",
                Location = new Point(240, 380), // Постав під кнопкою входу
                Width = 250,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Gray,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.MouseEnter += (s, e) => btnBack.BackColor = Color.DarkGray;
            btnBack.MouseLeave += (s, e) => btnBack.BackColor = Color.Gray;
            btnBack.Click += BtnBack_Click;

            Controls.Add(btnBack);
        }
        private Button btnBack;

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartForm startForm = new StartForm();
            startForm.ShowDialog();
            this.Close();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string login = txtUsername.Text.Trim();
            string pass = txtPassword.Text;

            UserManager.LoadUsers(); // Завантажуємо всіх користувачів
            User currentUser = UserManager.FindUser(login, pass); // Шукаємо за логіном і паролем

            if (currentUser != null)
            {
                this.Hide();

                Form1 mainForm = new Form1(currentUser);
                mainForm.ShowDialog();

                this.Close();
            }
            else
            {
                MessageBox.Show("❌ Невірний логін або пароль", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void InitializeCustomTitleBar()
        {
            topPanel = new Panel
            {
                Height = 30,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(20, 20, 40),
                Cursor = Cursors.SizeAll
            };
            topPanel.MouseDown += TopPanel_MouseDown;
            Controls.Add(topPanel);

            minimizeButton = new Button
            {
                Text = "─",
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(30, 30),
                Location = new Point(this.Width - 60, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            minimizeButton.FlatAppearance.BorderSize = 0;
            minimizeButton.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
            topPanel.Controls.Add(minimizeButton);

            closeButton = new Button
            {
                Text = "✕",
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(30, 30),
                Location = new Point(this.Width - 30, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Click += (s, e) => this.Close();
            topPanel.Controls.Add(closeButton);
        }


        // Дозволяє рухати форму
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                Color.Gold, 3, ButtonBorderStyle.Solid,
                Color.Gold, 3, ButtonBorderStyle.Solid,
                Color.Gold, 3, ButtonBorderStyle.Solid,
                Color.Gold, 3, ButtonBorderStyle.Solid);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
