using System;
using System.Windows.Forms;

namespace LabyDB
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            login form7 = new login();
            form7.Show();
            this.Hide();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button1, "Переход во вкладку для добавления владельца машины.");
            t.SetToolTip(button2, "Переход во вкладку для добавления автомобиля.");
            t.SetToolTip(button3, "Переход во вкладку для подсчета итоговой суммы.");
            t.SetToolTip(button4, "Переход во вкладку для добавления услуги.");
            t.SetToolTip(button5, "Переход во вкладку для добавление запчасти.");
            t.SetToolTip(button6, "Выход из приложения.");
            t.SetToolTip(button8, "Переход во вкладку настройки.");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();
            form10.Show();
            this.Hide();
        }
    }
}
