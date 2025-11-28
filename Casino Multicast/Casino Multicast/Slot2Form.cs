using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Casino_Multicast
{
    public partial class Slot2Form : Form
    {
        private readonly Random slot2Rand = new Random();
        private readonly List<Slot2Symbol> slot2Symbols = new List<Slot2Symbol>();
        private SoundPlayer spinSound2;
        private SoundPlayer winSound2;
        private int spin2Counter = 0;
        private User slot2User;
        private System.Windows.Forms.Button btnAutoSpin;


        private PictureBox[] slot2Boxes;
        private Slot2Symbol[] final2Symbols = new Slot2Symbol[5];

        private bool isBonus2Mode = false;
        private int bonus2SpinsLeft = 0;
        private int bonus2Multiplier = 0;
        private int bonus2Winnings = 0;
        private bool isAutoSpin = false;

        public Slot2Form(User user)
        {
            slot2User = user;
            InitializeComponent();
            LoadSlot2Symbols();
            LoadSlot2Sounds();
            SetupSlot2PictureBoxes();
            AddSlot2Legend();

            btnSpin.Click += BtnSpin2_Click;
            timer1.Tick += Timer1_Tick2;
            UpdateSlot2BalanceLabel();
            ShowRandomExtraPictures();

            btnAutoSpin = new System.Windows.Forms.Button();
            btnAutoSpin.Text = "Автоспін: Вимк.";
            btnAutoSpin.Location = new Point(400, 404);
            btnAutoSpin.Size = new Size(145, 45);
            btnAutoSpin.FlatStyle = FlatStyle.Flat;
            btnAutoSpin.FlatAppearance.BorderSize = 0;
            btnAutoSpin.BackColor = Color.FromArgb(52, 152, 219);
            btnAutoSpin.ForeColor = Color.White;
            btnAutoSpin.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnAutoSpin.Cursor = Cursors.Hand;
            btnAutoSpin.TabStop = false;

            btnAutoSpin.Paint += (s, e) =>
            {
                var button = s as System.Windows.Forms.Button;
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    int radius = 15;
                    var rect = new Rectangle(0, 0, button.Width, button.Height);
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();
                    button.Region = new Region(path);
                }
            };

            btnAutoSpin.MouseEnter += (s, e) =>
            {
                btnAutoSpin.BackColor = Color.FromArgb(41, 128, 185);
            };
            btnAutoSpin.MouseLeave += (s, e) =>
            {
                btnAutoSpin.BackColor = Color.FromArgb(52, 152, 219);
            };

            this.Controls.Add(btnAutoSpin);
            btnAutoSpin.Click += BtnAutoSpin_Click;
            btnAutoSpin.Enabled = true;

            lblWinMessage.Font = new Font("Segoe UI", 36, FontStyle.Bold);
            lblWinMessage.TextAlign = ContentAlignment.MiddleCenter;
            lblWinMessage.AutoSize = false;

            lblWinMessage.Size = new Size(this.ClientSize.Width, 100);
            lblWinMessage.Location = new Point(0, (this.ClientSize.Height - lblWinMessage.Height) / 2);

            lblWinMessage.Visible = false;
        }

        private void LoadSlot2Symbols()
        {
            slot2Symbols.Add(new Slot2Symbol { Name = "SymbolD", Image = Properties.Resources.Teamspirit, Multiplier = 250, Chance = 0.10 });
            slot2Symbols.Add(new Slot2Symbol { Name = "SymbolC", Image = Properties.Resources.navi, Multiplier = 50, Chance = 0.15 });
            slot2Symbols.Add(new Slot2Symbol { Name = "SymbolA", Image = Properties.Resources.og, Multiplier = 25, Chance = 0.20 });
            slot2Symbols.Add(new Slot2Symbol { Name = "SymbolB", Image = Properties.Resources.odium, Multiplier = 5, Chance = 0.15 });
            slot2Symbols.Add(new Slot2Symbol { Name = "SymbolE", Image = Properties.Resources.VS, Multiplier = 2, Chance = 0.20 });
            slot2Symbols.Add(new Slot2Symbol { Name = "Bonus2", Image = Properties.Resources.bonus, Multiplier = 0, Chance = 0.20 });
        }

        private void BtnAutoSpin_Click(object sender, EventArgs e)
        {
            isAutoSpin = !isAutoSpin;
            btnAutoSpin.Text = isAutoSpin ? "Автоспін: Вкл." : "Автоспін: Вимк.";

            if (isAutoSpin && !timer1.Enabled)
            {
                BtnSpin2_Click(null, null);
            }
        }

        private void LoadSlot2Sounds()
        {
            spinSound2 = new SoundPlayer(Properties.Resources.spin);
            winSound2 = new SoundPlayer(Properties.Resources.win);
        }

        private void SetupSlot2PictureBoxes()
        {
            slot2Boxes = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5 };
        }

        private void UpdateSlot2BalanceLabel()
        {
            lblBalance.Text = $"Баланс: {slot2User.Balance} монет";
        }

        private void SaveSlot2UserBalance()
        {
            var userInList = UserManager.Users.Find(u => u.Username == slot2User.Username);
            if (userInList != null)
                userInList.Balance = slot2User.Balance;

            UserManager.SaveUsers();
        }

        private void AddSlot2Legend()
        {
            int startX = 850;
            int startY = 30;
            int spacingY = 100;

            foreach (var symbol in slot2Symbols)
            {
                PictureBox pb = new PictureBox
                {
                    Image = symbol.Image,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(startX, startY),
                    Size = new Size(80, 80)
                };
                this.Controls.Add(pb);

                Label lbl = new Label
                {
                    Text = $"{symbol.Multiplier}x",
                    Font = new Font("Segoe UI", 20, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Location = new Point(startX + 90, startY + 20),
                    AutoSize = true
                };
                this.Controls.Add(lbl);

                startY += spacingY;
            }
        }


        private void BtnSpin2_Click(object sender, EventArgs e)
        {
            lblWinMessage.Visible = false;
            if (!isBonus2Mode && slot2User.Balance < GetCurrentBet())
            {
                MessageBox.Show("Недостатньо коштів!", "Слот 2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isAutoSpin = false;
                btnAutoSpin.Text = "Автоспін: Вимк.";
                btnSpin.Enabled = true;
                return;
            }

            if (!isBonus2Mode)
            {
                slot2User.Balance -= GetCurrentBet();
                SaveSlot2UserBalance();
                UpdateSlot2BalanceLabel();
            }

            spin2Counter = 20;
            timer1.Start();
            spinSound2.PlayLooping();

            btnSpin.Enabled = false;
            comboBet.Enabled = false;
        }

        private async void Timer1_Tick2(object sender, EventArgs e)
        {
            if (spin2Counter > 0)
            {
                for (int i = 0; i < 5; i++)
                    slot2Boxes[i].Image = GetRandomFakeSlot2Symbol().Image;
                spin2Counter--;
            }
            else
            {
                timer1.Stop();
                spinSound2.Stop();

                for (int i = 0; i < 5; i++)
                {
                    final2Symbols[i] = GetRandomSlot2Symbol(isBonus2Mode);
                    slot2Boxes[i].Image = final2Symbols[i].Image;
                }

                bool wasWin = false;

                if (isBonus2Mode)
                {
                    ProcessBonus2Spin();
                    wasWin = true;
                }
                else
                {
                    wasWin = HandleSlot2Results();
                }

                btnSpin.Enabled = true;
                comboBet.Enabled = true;

                if (wasWin)
                    await Task.Delay(1500);

                if (isAutoSpin)
                {
                    BtnSpin2_Click(null, null);
                }
            }
        }

        private void ProcessBonus2Spin()
        {
            int iterationWinnings = 0;
            Dictionary<string, int> symbolCounts = new Dictionary<string, int>();

            foreach (var symbol in final2Symbols)
            {
                if (symbol.Name.StartsWith("x"))
                {
                    bonus2Multiplier += int.Parse(symbol.Name.Substring(1));
                }
                else
                {
                    if (!symbolCounts.ContainsKey(symbol.Name))
                        symbolCounts[symbol.Name] = 0;
                    symbolCounts[symbol.Name]++;
                }
            }

            foreach (var kvp in symbolCounts)
            {
                if (kvp.Value >= 3)
                {
                    var symbol = slot2Symbols.Find(s => s.Name == kvp.Key);
                    if (symbol != null)
                    {
                        int winForSymbol = GetCurrentBet() * symbol.Multiplier * kvp.Value;
                        iterationWinnings += winForSymbol;
                    }
                }
            }

            bonus2Winnings += iterationWinnings;
            bonus2SpinsLeft--;

            string detailText = "";

            foreach (var kvp in symbolCounts)
            {
                if (kvp.Value >= 3)
                {
                    var symbol = slot2Symbols.Find(s => s.Name == kvp.Key);
                    if (symbol != null)
                    {
                        int singleWin = GetCurrentBet() * symbol.Multiplier * kvp.Value;
                        detailText += $"{kvp.Value}x = {singleWin}  ";
                    }
                }
            }

            detailText += $"Множник: x{bonus2Multiplier}  ";
            detailText += $"Поточний виграш: {bonus2Winnings} монет";

            lblWinMessage.Text = $"🎰 Бонус! Спінів: {bonus2SpinsLeft}  " + detailText;
            lblWinMessage.Visible = true;

            if (bonus2SpinsLeft <= 0)
            {
                int total = bonus2Winnings * Math.Max(1, bonus2Multiplier);
                slot2User.Balance += total;
                SaveSlot2UserBalance();
                UpdateSlot2BalanceLabel();

                lblWinMessage.Text = $"🎰 Бонус завершено! Загальний виграш: {total} монет";
                lblWinMessage.Visible = true;
                isBonus2Mode = false;
                bonus2Winnings = 0;
                bonus2Multiplier = 0;

                winSound2.Play();

                MessageBox.Show($"Вітаємо! Ви виграли {total} монет у бонусній грі!", "Бонус завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool HandleSlot2Results()
        {
            int bonusCount = 0;
            foreach (var symbol in final2Symbols)
                if (symbol.Name == "Bonus2") bonusCount++;

            if (bonusCount >= 3)
            {
                isBonus2Mode = true;
                bonus2SpinsLeft = 10;
                bonus2Winnings = 0;
                bonus2Multiplier = 0;

                lblWinMessage.Text = "🎁 Бонусна гра активована!";
                lblWinMessage.Visible = true;
                return true;
            }

            int maxCombo = 1, currentCombo = 1;
            string lastSymbol = final2Symbols[0].Name;

            for (int i = 1; i < 5; i++)
            {
                if (final2Symbols[i].Name == lastSymbol)
                {
                    currentCombo++;
                    maxCombo = Math.Max(maxCombo, currentCombo);
                }
                else
                {
                    currentCombo = 1;
                    lastSymbol = final2Symbols[i].Name;
                }
            }

            if (maxCombo >= 3)
            {
                int winnings = GetCurrentBet() * final2Symbols[0].Multiplier;
                slot2User.Balance += winnings;
                SaveSlot2UserBalance();
                UpdateSlot2BalanceLabel();

                lblWinMessage.Text = $"🎉 Виграш: {winnings} монет!";
                lblWinMessage.Visible = true;
                winSound2.Play();

                return true;
            }

            return false;
        }

        private Slot2Symbol GetRandomSlot2Symbol(bool forBonus = false)
        {
            double totalChance = 0;
            foreach (var symbol in slot2Symbols)
                totalChance += symbol.Chance;

            double randomValue = slot2Rand.NextDouble() * totalChance;

            double cumulative = 0;
            foreach (var symbol in slot2Symbols)
            {
                cumulative += symbol.Chance;
                if (randomValue < cumulative)
                {
                    if (forBonus && symbol.Name == "Bonus2")
                    {
                        int[] multipliers = new int[] { 1, 2, 3, 4, 5 };
                        int m = multipliers[slot2Rand.Next(multipliers.Length)];
                        return CreateMultiplierSymbol(m);
                    }
                    return symbol;
                }
            }
            return slot2Symbols[0];
        }

        private Slot2Symbol CreateMultiplierSymbol(int m)
        {
            Image img = null;
            switch (m)
            {
                case 2: img = Properties.Resources.x2; break;
                case 3: img = Properties.Resources.x3; break;
                case 5: img = Properties.Resources.x5; break;
                default: img = Properties.Resources.x2; break;
            }
            return new Slot2Symbol { Name = $"x{m}", Image = img, Multiplier = 0, Chance = 0 };
        }

        private Slot2Symbol GetRandomFakeSlot2Symbol()
        {
            return slot2Symbols[slot2Rand.Next(slot2Symbols.Count)];
        }

        private int GetCurrentBet()
        {
            if (comboBet.SelectedItem != null && int.TryParse(comboBet.SelectedItem.ToString(), out int bet))
                return bet;
            return 10; // дефолтне значення
        }

        private void ShowRandomExtraPictures()
        {
            var extraPics = new List<PictureBox> { pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10 };
            var randomIndices = new HashSet<int>();
            while (randomIndices.Count < 3)
            {
                randomIndices.Add(slot2Rand.Next(extraPics.Count));
            }

            foreach (var pic in extraPics)
                pic.Visible = false;

            foreach (int index in randomIndices)
                extraPics[index].Visible = true;
        }

        private void comboBoxBet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Slot2Form_Load(object sender, EventArgs e)
        {

        }
    }

    public class Slot2Symbol
    {
        public string Name { get; set; }
        public Image Image { get; set; }
        public int Multiplier { get; set; }
        public double Chance { get; set; }
    }
}
