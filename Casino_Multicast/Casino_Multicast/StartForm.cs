using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Casino_Multicast
{
    public partial class StartForm : Form
    {
        private Button btnLogin;
        private Button btnRegister;
        private Label lblTitle;
        private Timer fadeInTimer;


        public StartForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.DarkSlateBlue;
            this.ClientSize = new Size(500, 350);

            InitializeUI();
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                       Color.FromArgb(48, 25, 52), // Топ
                       Color.FromArgb(30, 30, 80), // Низ
                       LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void InitializeUI()
        {
            this.Opacity = 0; // Стартова прозорість
            fadeInTimer = new Timer { Interval = 30 };
            fadeInTimer.Tick += (s, e) =>
            {
                if (this.Opacity < 1)
                    this.Opacity += 0.05;
                else
                    fadeInTimer.Stop();
            };
            fadeInTimer.Start();
            lblTitle = new Label
            {
                Text = "🎲 Вітаємо в Казино!",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.Gold,
                Location = new Point(80, 40),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            Controls.Add(lblTitle);

            btnLogin = new Button
            {
                Text = "🔐 Увійти",
                Location = new Point(150, 130),
                Size = new Size(200, 50),
                BackColor = Color.FromArgb(202, 103, 0),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderColor = Color.Gold;
            btnLogin.FlatAppearance.BorderSize = 2;
            btnLogin.Click += BtnLogin_Click;
            Controls.Add(btnLogin);
            btnLogin.MouseEnter += (s, e) => AnimateHover(btnLogin, Color.OrangeRed);
            btnLogin.MouseLeave += (s, e) => AnimateHover(btnLogin, Color.FromArgb(202, 103, 0));

            btnRegister = new Button
            {
                Text = "Зареєструватись",
                Location = new Point(150, 200),
                Size = new Size(200, 50),
                BackColor = Color.FromArgb(202, 103, 0),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderColor = Color.Gold;
            btnRegister.FlatAppearance.BorderSize = 2;
            btnRegister.Click += BtnRegister_Click;
            Controls.Add(btnRegister);
            btnLogin.MouseEnter += (s, e) => AnimateHover(btnLogin, Color.OrangeRed);
            btnLogin.MouseLeave += (s, e) => AnimateHover(btnLogin, Color.FromArgb(202, 103, 0));
        }
        void AnimateHover(Button btn, Color toColor)
        {
            Timer t = new Timer { Interval = 10 };
            int steps = 10;
            int step = 0;
            Color fromColor = btn.BackColor;

            t.Tick += (s, e) =>
            {
                float progress = (float)step / steps;
                int r = (int)(fromColor.R + (toColor.R - fromColor.R) * progress);
                int g = (int)(fromColor.G + (toColor.G - fromColor.G) * progress);
                int b = (int)(fromColor.B + (toColor.B - fromColor.B) * progress);

                btn.BackColor = Color.FromArgb(r, g, b);

                step++;
                if (step > steps) t.Stop();
            };
            t.Start();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.Close();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
            this.Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                Color.Gold, 4, ButtonBorderStyle.Solid,
                Color.Gold, 4, ButtonBorderStyle.Solid,
                Color.Gold, 4, ButtonBorderStyle.Solid,
                Color.Gold, 4, ButtonBorderStyle.Solid);
        }

        private void StartForm_Load(object sender, EventArgs e)
        {

        }
    }
}
