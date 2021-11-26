using System;
using System.Windows.Forms;

namespace LabyDB {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form2_open_button(object sender, EventArgs e) {
            Program.form2.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
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
