using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Casino_Multicast
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializePlaceholder()
        {
            // Для TextBox логіну
            txtUsername.Text = "Логін...";
            txtUsername.ForeColor = Color.Gray;
            txtUsername.GotFocus += RemovePlaceholder;
            txtUsername.LostFocus += AddPlaceholder;

            // Для TextBox паролю
            txtPassword.Text = "Пароль...";
            txtPassword.ForeColor = Color.Gray;
            txtPassword.GotFocus += RemovePlaceholder;
            txtPassword.LostFocus += AddPlaceholder;
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Логін..." || textBox.Text == "Пароль...")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
                if (textBox == txtPassword)
                    textBox.PasswordChar = '•'; // Приховати пароль
            }
        }

        private void AddPlaceholder(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox == txtUsername)
                    textBox.Text = "Логін...";
                else if (textBox == txtPassword)
                {
                    textBox.Text = "Пароль...";
                    textBox.PasswordChar = '\0'; // Показати текст
                }
                textBox.ForeColor = Color.Gray;
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RegisterForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "RegisterForm";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.ResumeLayout(false);

        }




        #endregion

    }
}