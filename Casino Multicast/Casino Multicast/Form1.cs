    using System;
    using System.Drawing;
    using System.Windows.Forms;

    namespace Casino_Multicast
    {
        public partial class Form1 : Form
        {
            private User currentUser;

            public Form1(User user)
            {
                InitializeComponent();

                currentUser = user ?? throw new ArgumentNullException(nameof(user));
                this.Text = $"Казино | {currentUser.Username}";

                UpdateBalanceLabel();

                // Hover ефект для кнопки профілю
                btnProfile.MouseEnter += (s, e) => btnProfile.BackColor = Color.OrangeRed;
                btnProfile.MouseLeave += (s, e) => btnProfile.BackColor = Color.DarkOrange;

                btnProfile.Click += btnProfile_Click_1;

                // Наведення на labelSlot1 для затемнення pictureBoxSlot1
                labelSlot1.MouseEnter += LabelSlot1_MouseEnter;
                labelSlot1.MouseLeave += LabelSlot1_MouseLeave;

            labelSlot2.MouseEnter += LabelSlot2_MouseEnter;
            labelSlot2.MouseLeave += LabelSlot2_MouseLeave;
            labelSlot2.Click += labelSlot2_Click;

            labelRoulette.MouseEnter += LabelRoulette_MouseEnter;
            labelRoulette.MouseLeave += LabelRoulette_MouseLeave;
            labelRoulette.Click += labelRoulette_Click;
        }

            private void UpdateBalanceLabel()
            {
                lblBalance.Text = $"Баланс: {currentUser.Balance} грн";
            }

            private void btnProfile_Click_1(object sender, EventArgs e)
            {
                ProfileForm profileForm = new ProfileForm(currentUser);
                profileForm.ShowDialog(); // модальне вікно
            }

            private void LabelSlot1_MouseEnter(object sender, EventArgs e)
            {
                pictureBoxSlot1.BackColor = Color.FromArgb(100, 0, 0, 0); // напівпрозорий чорний
            }

            private void LabelSlot1_MouseLeave(object sender, EventArgs e)
            {
                pictureBoxSlot1.BackColor = Color.Transparent;
            }

        private void labelSlot1_Click(object sender, EventArgs e)
        {
            Slot1Form slot1 = new Slot1Form(currentUser);
            slot1.FormClosed += Slot1_FormClosed; // підписуємось на закриття
            slot1.ShowDialog();
        }

        private void Slot1_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateBalanceLabel();
        }

        private void Form1_Load(object sender, EventArgs e)
            {
                // Можна додати додаткову ініціалізацію
            }

        private void pictureBoxSlot2_Click(object sender, EventArgs e)
        {

        }
        private void LabelSlot2_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxSlot2.BackColor = Color.FromArgb(100, 0, 0, 0); // напівпрозорий чорний
        }

        private void LabelSlot2_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxSlot2.BackColor = Color.Transparent;
        }


        private void labelSlot2_Click(object sender, EventArgs e)
        {
            Slot2Form slot2 = new Slot2Form(currentUser);
            slot2.FormClosed += Slot2_FormClosed; // підписуємось на закриття
            slot2.ShowDialog();
        }

        private void Slot2_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateBalanceLabel();
        }
        private void LabelRoulette_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxRoulette.BackColor = Color.FromArgb(100, 0, 0, 0); // затемнення
        }

        private void LabelRoulette_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxRoulette.BackColor = Color.Transparent;
        }

        private void labelRoulette_Click(object sender, EventArgs e)
        {
            RouletteForm rouletteForm = new RouletteForm(currentUser);
            rouletteForm.FormClosed += RouletteForm_FormClosed;
            rouletteForm.ShowDialog();
        }

        private void RouletteForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateBalanceLabel();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.FromArgb(128, 0, 0, 0); // напівпрозорий чорний фон
        }
    }
    }
