using System;
using System.Windows.Forms;

namespace LabyDB {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void Form2_open_button(object sender, EventArgs e) {
            Program.form2.Show();
            this.Hide();
        }

        private void Form3_open_button(object sender, EventArgs e)
        {
            Program.form3.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.form4.Show();
            this.Hide();
        }
    }
}
