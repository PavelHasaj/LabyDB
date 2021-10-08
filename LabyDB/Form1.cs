using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
