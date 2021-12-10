using System;
using System.IO;
using System.Windows.Forms;

namespace LabyDB
{
    public partial class Form10 : Form{

        public Form10()
        {
            InitializeComponent();
        }
        string Path = Directory.GetCurrentDirectory() + @"nacenka.txt";

        private void button5_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Path, textBox1.Text);
            this.Close();
            Program.form7.Show();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            if (File.Exists(Path))
            {
                textBox1.Text = File.ReadAllText(Path);
            }
            else
            {
                MessageBox.Show("Файл наценка не существует");
            }
        }
    }
}
