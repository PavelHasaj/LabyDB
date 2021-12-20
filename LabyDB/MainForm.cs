using System;
using System.Windows.Forms;

namespace LabyDB {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void abonentsForm_open_button(object sender, EventArgs e) {
            AbonentsForm form = new AbonentsForm();
            form.Show();
            this.Hide();
        }

        private void paymentsForm_open_button(object sender, EventArgs e)
        {
            PaymentsForm form = new PaymentsForm();
            form.Show();
            this.Hide();
        }

        private void FormExitButton_Click(object sender, EventArgs e) {
            Application.Exit();
        }
    }
}