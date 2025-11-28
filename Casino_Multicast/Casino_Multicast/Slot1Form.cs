using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Casino_Multicast
{
    public partial class Slot1Form : Form
    {
        private readonly Random rand = new Random();
        private readonly List<SlotSymbol> slotSymbols = new List<SlotSymbol>();
        private SoundPlayer spinSound;
        private SoundPlayer winSound;
        private int spinCount = 0;
        private User currentUser;

        private SlotSymbol[] finalSymbols = new SlotSymbol[3];

        public Slot1Form(User user)
        {
            currentUser = user;
            InitializeComponent();
            LoadSlotImages();
            LoadSounds();

            AddLegend();  // Додаємо легенду

            btnSpin.Click += BtnSpin_Click;
            timer1.Tick += Timer1_Tick;

            UpdateBalanceLabel();
        }

        private const int BetAmount = 100;

        private void UpdateBalanceLabel()
        {
            lblBalance.Text = $"Баланс: {currentUser.Balance} монет";
        }

        private void SaveUserBalance()
        {
            // Знаходимо користувача у списку та оновлюємо його баланс
            var userInList = UserManager.Users.Find(u => u.Username == currentUser.Username);
            if (userInList != null)
            {
                userInList.Balance = currentUser.Balance;
            }

            UserManager.SaveUsers();
        }

        private void AddLegend()
        {
            int startX = 700;  // Позиція по X (підкоригуй під розмір форми)
            int startY = 100;  // Початкова позиція по Y
            int spacingY = 90; // Відступ між рядками

            for (int i = 0; i < slotSymbols.Count; i++)
            {
                // PictureBox для символу
                PictureBox pb = new PictureBox();
                pb.Image = slotSymbols[i].Image;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Location = new Point(startX, startY + i * spacingY);
                pb.Size = new Size(60, 60);
                this.Controls.Add(pb);

                // Label для тексту множника
                Label lbl = new Label();
                lbl.Text = $"{slotSymbols[i].Multiplier}x";
                lbl.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                lbl.ForeColor = Color.Black;
                lbl.Location = new Point(startX + 70, startY + i * spacingY + 15);
                lbl.AutoSize = true;
                this.Controls.Add(lbl);
            }
        }

        private void LoadSlotImages()
        {
            slotSymbols.Add(new SlotSymbol { Name = "Rapire", Image = Properties.Resources.Rapire, Multiplier = 250, Chance = 0.15 });
            slotSymbols.Add(new SlotSymbol { Name = "Multicast", Image = Properties.Resources.Multicast, Multiplier = 50, Chance = 0.15 });
            slotSymbols.Add(new SlotSymbol { Name = "Antimage", Image = Properties.Resources.Antimage, Multiplier = 10, Chance = 0.25 });
            slotSymbols.Add(new SlotSymbol { Name = "Midas", Image = Properties.Resources.Midas, Multiplier = 5, Chance = 0.15 });
            slotSymbols.Add(new SlotSymbol { Name = "Runa", Image = Properties.Resources.Runa, Multiplier = 2, Chance = 0.30 });
        }

        private void LoadSounds()
        {
            spinSound = new SoundPlayer(Properties.Resources.spin);
            winSound = new SoundPlayer(Properties.Resources.win);
        }

        private void BtnSpin_Click(object sender, EventArgs e)
        {
            lblWinMessage.Visible = false; // ховаємо повідомлення при новому спіні

            int betAmount = GetCurrentBet();

            if (currentUser.Balance < betAmount)
            {
                MessageBox.Show("Недостатньо коштів!", "Слот", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            btnSpin.Enabled = false;
            comboBoxBet.Enabled = false;
            currentUser.Balance -= betAmount;
            SaveUserBalance();
            UpdateBalanceLabel();

            spinCount = 20;
            timer1.Start();
            spinSound.PlayLooping();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (spinCount > 0)
            {
                pictureBox1.Image = GetRandomFakeSymbol().Image;
                pictureBox2.Image = GetRandomFakeSymbol().Image;
                pictureBox3.Image = GetRandomFakeSymbol().Image;
                spinCount--;
            }
            else
            {
                timer1.Stop();
                spinSound.Stop();
                btnSpin.Enabled = true;
                comboBoxBet.Enabled = true;
                // Вибираємо фінальні символи тільки зараз
                finalSymbols[0] = GetRandomSymbol();
                finalSymbols[1] = GetRandomSymbol();
                finalSymbols[2] = GetRandomSymbol();

                pictureBox1.Image = finalSymbols[0].Image;
                pictureBox2.Image = finalSymbols[1].Image;
                pictureBox3.Image = finalSymbols[2].Image;

                // Перевірка на виграш (перевіряємо, чи всі символи однакові за ім'ям)
                if (finalSymbols[0].Name == finalSymbols[1].Name && finalSymbols[1].Name == finalSymbols[2].Name)
                {
                    int multiplier = finalSymbols[0].Multiplier;
                    int betAmount = GetCurrentBet();
                    int winnings = betAmount * multiplier;
                    currentUser.Balance += winnings;

                    SaveUserBalance();
                    UpdateBalanceLabel();

                    winSound.Play();
                    lblWinMessage.Text = $"Виграш! {winnings} монет";
                    lblWinMessage.Visible = true;
                }
            }
        }

        private int GetCurrentBet()
        {
            if (int.TryParse(comboBoxBet.SelectedItem?.ToString(), out int bet))
                return bet;
            return 100;
        }

        // Випадковий символ для фінального результату (з шансами)
        private SlotSymbol GetRandomSymbol()
        {
            double roll = rand.NextDouble();
            double cumulative = 0;

            foreach (var symbol in slotSymbols)
            {
                cumulative += symbol.Chance;
                if (roll < cumulative)
                    return symbol;
            }

            return slotSymbols[slotSymbols.Count - 1];
        }

        // Випадковий символ просто для анімації
        private SlotSymbol GetRandomFakeSymbol()
        {
            int index = rand.Next(slotSymbols.Count);
            return slotSymbols[index];
        }

        private void Slot1Form_Load(object sender, EventArgs e)
        {

        }
    }

    public class SlotSymbol
    {
        public string Name { get; set; }
        public Image Image { get; set; }
        public int Multiplier { get; set; }
        public double Chance { get; set; }
    }
}
