using System;
using System.Windows.Forms;

namespace LabyDB {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void abonentsForm_open_button(object sender, EventArgs e) {
            Program.abonentsForm.Show();
            this.Hide();
        }

        private void paymentsForm_open_button(object sender, EventArgs e)
        {
            Program.paymentsForm.Show();
            this.Hide();
        }

        private void FormExitButton_Click(object sender, EventArgs e) {
            Application.Exit();
        }
    }
}