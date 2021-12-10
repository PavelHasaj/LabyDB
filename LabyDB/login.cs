using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace LabyDB
{
    public partial class login : Form{
        //Строка подключения к БД
        SqlConnection connection = new SqlConnection(Program.GetConnectionString());

        private void Login_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button2.PerformClick();
        }

        public login()
{
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Login_KeyUp);
            this.button2.Click += new System.EventHandler(this.button2_Click); 
        }

        private void button1_Click(object sender, EventArgs e){
            Application.Exit();
        }

        public SqlConnection getConnection()
        {
            return connection;
        }

        private void button2_Click(object sender, EventArgs e){
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            string loginUser = textBox1.Text;
            string passUser = textBox2.Text;

            login db = new login();

            DataTable table = new DataTable();

            SqlCommand command = new SqlCommand("SELECT * FROM users WHERE login = @uL AND pass = @uP", db.getConnection());

            command.Parameters.Add("@uL", SqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@uP", SqlDbType.VarChar).Value = passUser;

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                string Role = table.Rows[0][3].ToString();
                if (table.Rows.Count > 0)
                {

                    if (Role.ToString() == "Administrator")
                    {
                        Program.form1.Show();
                        this.Hide();
                    }

                    else if (Role.ToString() == "User")
                    {
                        Program.form9.Show();
                        this.Hide();
                    }
                }
            }
            else
            {
                MessageBox.Show("Логин или пароль неверный");
            }
        }

        private void login_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(textBox1, "Логин — позволяет пользователям войти в систему.");
            t.SetToolTip(textBox2, "Пароль — условное слово или произвольный набор знаков, состоящий из букв, цифр и других символов, и предназначенный для подтверждения личности или полномочий.");
            textBox2.UseSystemPasswordChar = true;
        }
    }
}
