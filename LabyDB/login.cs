using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB
{
    public partial class login : Form{

        public login()
{
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e){
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e){
            if(textBox1.Text == "user1" && textBox2.Text == "12345"){
                Form1 s = new Form1();
                s.Show();
                this.Hide();
            }
            else{
                textBox1.Text = " ";
                textBox2.Text = " ";
                MessageBox.Show("Неправильный логин или пароль!");
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
