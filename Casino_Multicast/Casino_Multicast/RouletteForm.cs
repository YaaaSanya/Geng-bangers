using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Casino_Multicast
{
    public partial class RouletteForm : Form
    {
        private Image wheelImage;
        private Image pointerImage;
        private SoundPlayer spinSound;
        private SoundPlayer winSound;

        private float wheelAngle = 0f;
        private Timer spinTimer;
        private float spinSpeed;
        private float deceleration;
        private float elapsedSpinTime = 0f;
        private const float totalSpinTime = 6f;
        private Label lblDisclaimer;

        private Random rand = new Random();
        private int winningNumber = -1;

        private enum BetType
        {
            None,
            Number,
            FirstHalf,
            SecondHalf,
            Black,
            Red,
            Even,
            Odd
        }

        private BetType currentBetType = BetType.None;
        private int betNumber = -1;
        private decimal betAmount = 0m;

        private TextBox txtBetAmount;
        private TextBox txtNumberBet;
        private Label lblResult;
        private Label lblBalance;
        private Button btnSpin;

        private readonly User currentUser;

        public RouletteForm(User User)
        {
            InitializeComponent();

            currentUser = User ?? throw new ArgumentNullException(nameof(User));

            InitResources();
            InitTimer();

            this.DoubleBuffered = true;
            this.Text = "Рулетка";
            this.ClientSize = new Size(800, 600);

            SetupUI();
            UpdateBalanceLabel();
        }

        private void UpdateBalanceLabel()
        {
            lblBalance.Text = $"Баланс: {currentUser.Balance} грн";
        }

        private void SetupUI()
        {
            this.Controls.Clear();

            // Баланс
            lblBalance = new Label()
            {
                Text = $"Баланс: {currentUser.Balance} грн",
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.Black
            };
            this.Controls.Add(lblBalance);

            Label lblBetAmount = new Label()
            {
                Text = "Сума ставки:",
                Location = new Point(10, 45),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lblBetAmount);

            txtBetAmount = new TextBox()
            {
                Location = new Point(100, 42),
                Width = 80,
                Text = "10",
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(txtBetAmount);

            Label lblNumberBet = new Label()
            {
                Text = "Число (0-36):",
                Location = new Point(200, 45),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lblNumberBet);

            txtNumberBet = new TextBox()
            {
                Location = new Point(300, 42),
                Width = 50,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(txtNumberBet);

            lblResult = new Label()
            {
                Text = "Ставка не зроблена",
                Location = new Point(10, 75),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };
            this.Controls.Add(lblResult);
            lblDisclaimer = new Label()
            {
                Text = "P.s - рулетка просто візуал",
                AutoSize = true,
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = Color.Black,
                Padding = new Padding(4, 2, 4, 2),
                Location = new Point(this.ClientSize.Width - 10, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            // Щоб позиція була правильною при зміні розміру форми, змінимо Location після додавання в Controls:
            this.Controls.Add(lblDisclaimer);
            lblDisclaimer.Location = new Point(this.ClientSize.Width - lblDisclaimer.Width - 10, 10);


            // Панель ставок внизу
            Panel buttonPanel = new Panel()
            {
                Height = 160,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(30, 30, 30)
            };
            this.Controls.Add(buttonPanel);

            int btnWidth = 160;
            int btnHeight = 40;
            int gapX = 12;
            int gapY = 12;
            int startX = 10;
            int startY = 10;

            var buttons = new (string text, BetType type)[]
            {
        ("Число", BetType.Number),
        ("0-18 (перша половина)", BetType.FirstHalf),
        ("19-36 (друга половина)", BetType.SecondHalf),
        ("Чорне", BetType.Black),
        ("Червоне", BetType.Red),
        ("Парне", BetType.Even),
        ("Непарне", BetType.Odd)
            };

            for (int i = 0; i < buttons.Length; i++)
            {
                int col = i % 3;
                int row = i / 3;

                Button btn = CreateStyledButton(
                    buttons[i].text,
                    buttons[i].type,
                    startX + col * (btnWidth + gapX),
                    startY + row * (btnHeight + gapY),
                    btnWidth,
                    btnHeight
                );

                buttonPanel.Controls.Add(btn);
            }

            // Кнопка "Крутити" — справа
            btnSpin = new Button()
            {
                Text = "КРУТИТИ",
                Size = new Size(200, 50),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.Black,
                ForeColor = Color.Orange,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(buttonPanel.Width - 210, buttonPanel.Height - 60),
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom
            };
            btnSpin.FlatAppearance.BorderColor = Color.Orange;
            btnSpin.FlatAppearance.BorderSize = 2;
            btnSpin.Click += BtnSpin_Click;
            btnSpin.BringToFront();
            buttonPanel.Controls.Add(btnSpin);
        }
        private void BtnSpin_Click(object sender, EventArgs e)
        {
            if (spinTimer.Enabled)
            {
                MessageBox.Show("Колесо вже крутиться!");
                return;
            }

            if (currentBetType == BetType.None)
            {
                MessageBox.Show("Будь ласка, зробіть ставку!");
                return;
            }

            if (betAmount > currentUser.Balance)
            {
                MessageBox.Show("Недостатньо балансу для ставки!");
                return;
            }

            currentUser.Balance -= (int)betAmount;
            UserManager.SaveUsers();
            UpdateBalanceLabel();

            spinSpeed = rand.Next(30, 50);
            deceleration = spinSpeed / (totalSpinTime * (1000f / spinTimer.Interval));
            elapsedSpinTime = 0f;
            winningNumber = -1;

            spinSound?.Play();
            spinTimer.Start();

            btnSpin.Enabled = false;

            // Заблокуємо всі ставки і поля вводу
            SetBetControlsEnabled(false);
        }


        private void InitResources()
        {
            try
            {
                spinSound = new SoundPlayer(Properties.Resources.spin_sound);
                winSound = new SoundPlayer(Properties.Resources.winSound);
                wheelImage = Properties.Resources.roulette_wheel;
                pointerImage = Properties.Resources.roulette_pointer;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження ресурсів: " + ex.Message);

                wheelImage = new Bitmap(400, 400);
                using (Graphics g = Graphics.FromImage(wheelImage))
                {
                    g.Clear(Color.DarkGreen);
                }

                pointerImage = new Bitmap(50, 50);
                using (Graphics g = Graphics.FromImage(pointerImage))
                {
                    g.Clear(Color.Transparent);
                    Point[] triangle = {
                        new Point(25, 0),
                        new Point(45, 50),
                        new Point(5, 50)
                    };
                    g.FillPolygon(Brushes.Red, triangle);
                }
            }
        }
        private void SetBet(BetType betType)
        {
            if (!decimal.TryParse(txtBetAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Введіть коректну суму ставки!");
                return;
            }

            if (amount > currentUser.Balance)
            {
                MessageBox.Show("Недостатньо балансу!");
                return;
            }

            betAmount = amount;
            currentBetType = betType;

            if (betType == BetType.Number)
            {
                if (!int.TryParse(txtNumberBet.Text, out int numBet) || numBet < 0 || numBet > 36)
                {
                    MessageBox.Show("Введіть правильне число від 0 до 36!");
                    return;
                }
                betNumber = numBet;
            }
            else
            {
                betNumber = -1;
            }

            lblResult.Text = $"Ставка зроблена: {betType} на суму {betAmount} грн" +
                             (betType == BetType.Number ? $" (число {betNumber})" : "");
        }


        private void InitTimer()
        {
            spinTimer = new Timer();
            spinTimer.Interval = 20;
            spinTimer.Tick += SpinTimer_Tick;
        }

        private void SpinTimer_Tick(object sender, EventArgs e)
        {
            elapsedSpinTime += spinTimer.Interval / 1000f;

            wheelAngle += spinSpeed;
            wheelAngle %= 360;

            spinSpeed -= deceleration;
            if (spinSpeed < 0) spinSpeed = 0;

            if (elapsedSpinTime >= totalSpinTime || spinSpeed <= 0)
            {
                spinSpeed = 0;
                spinTimer.Stop();

                winningNumber = GetWinningNumberByAngle(wheelAngle);

                btnSpin.Enabled = true;

                decimal payout = CheckBet(winningNumber);

                if (payout > 0)
                {
                    currentUser.Balance += (int)payout;
                    UserManager.SaveUsers();
                    lblResult.Text = $"Виграш! Число: {winningNumber}. Виплата: {payout} грн";
                    winSound?.Play();
                }
                else
                {
                    lblResult.Text = $"Програш. Число: {winningNumber}. Ставка: {betAmount} грн";
                }
                UpdateBalanceLabel();
                SetBetControlsEnabled(true);
            }
            Invalidate();
        }

        private int GetWinningNumberByAngle(float angle)
        {
            // 37 секторів, кут кожного ≈ 9.73 градусів
            float adjustedAngle = (360 - angle + 360f / 37f / 2) % 360;
            int sector = (int)(adjustedAngle / (360f / 37));
            return sector;
        }

        private decimal CheckBet(int number)
        {
            if (number == 0)
            {
                if (currentBetType == BetType.Number && betNumber == 0)
                    return betAmount * 35m;
                return 0;
            }

            switch (currentBetType)
            {
                case BetType.Number:
                    if (betNumber == number) return betAmount * 35m;
                    break;
                case BetType.FirstHalf:
                    if (number >= 1 && number <= 18) return betAmount * 2m;
                    break;
                case BetType.SecondHalf:
                    if (number >= 19 && number <= 36) return betAmount * 2m;
                    break;
                case BetType.Black:
                    if (IsBlack(number)) return betAmount * 2m;
                    break;
                case BetType.Red:
                    if (IsRed(number)) return betAmount * 2m;
                    break;
                case BetType.Even:
                    if (number % 2 == 0) return betAmount * 2m;
                    break;
                case BetType.Odd:
                    if (number % 2 == 1) return betAmount * 2m;
                    break;
            }
            return 0;
        }

        private bool IsBlack(int number)
        {
            int[] blackNumbers = { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
            foreach (var n in blackNumbers)
                if (n == number) return true;
            return false;
        }

        private bool IsRed(int number)
        {
            int[] redNumbers = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
            foreach (var n in redNumbers)
                if (n == number) return true;
            return false;
        }
        private Button CreateStyledButton(string text, BetType betType, int x, int y, int width, int height)
        {
            Button btn = new Button()
            {
                Text = text,
                Size = new Size(width, height),
                Location = new Point(x, y),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.Black,
                ForeColor = Color.Orange,
                FlatStyle = FlatStyle.Flat
            };
            btn.FlatAppearance.BorderColor = Color.Orange;
            btn.FlatAppearance.BorderSize = 2;
            btn.Click += (s, e) => SetBet(betType);
            return btn;
        }
        private void SetBetControlsEnabled(bool enabled)
        {
            // Заблокуємо або розблокуємо кнопки ставок
            foreach (Control ctrl in Controls)
            {
                if (ctrl is Panel panel)
                {
                    foreach (Control child in panel.Controls)
                    {
                        if (child is Button btn && btn != btnSpin)
                        {
                            btn.Enabled = enabled;
                        }
                    }
                }
            }

            // Заблокуємо або розблокуємо текстові поля
            txtBetAmount.Enabled = enabled;
            txtNumberBet.Enabled = enabled;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Малюємо колесо в центрі і крутимо за годинниковою стрілкою
            g.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);
            g.RotateTransform(wheelAngle);
            g.TranslateTransform(-wheelImage.Width / 2, -wheelImage.Height / 2);
            g.DrawImage(wheelImage, 0, 0);
            g.ResetTransform();

            // Малюємо стрілку повернену проти годинникової на 90 градусів і трохи опущену
            g.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);
            g.RotateTransform(-90);  // проти годинникової на 90 градусів
            int pointerX = -pointerImage.Width / 2 + 185;
            int pointerY = -pointerImage.Height / 2; // трохи опустити
            g.DrawImage(pointerImage, pointerX, pointerY);
            g.ResetTransform();
        }

        private void RouletteForm_Load(object sender, EventArgs e)
        {

        }
    }
}
