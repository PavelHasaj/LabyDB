using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB
{
    public partial class Form8 : Form{
        //Строка подключения к БД
        SqlConnection connection = new SqlConnection(Program.GetConnectionString());
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SampleDatabaseDataSet dataSet = new SampleDatabaseDataSet();
        //Обновление
        private void DatabaseUpdate()
        {
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("Select * From users", connection);
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }
        //Добавление записи
        void DataAdd()
        {
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand("Insert Into users Values (@Id, @login, @pass)", connection);
            comand.Parameters.AddWithValue("@Id", Convert.ToInt64(textBox1.Text));
            comand.Parameters.AddWithValue("@login", textBox3.Text);
            comand.Parameters.AddWithValue("@pass", textBox4.Text);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
            DatabaseUpdate();//вызов метода обновления dataGridView
        }
        //Изменение записи
        void DataChange()
        {
            SqlCommand command = new SqlCommand("Update users set login=@login, pass=@pass Where Id = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            command.Parameters.AddWithValue("@login", textBox3.Text);
            command.Parameters.AddWithValue("@pass", textBox4.Text);
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
            DatabaseUpdate();
        }
        //Удаление записи
        void DataDelete()
        {
            SqlCommand command = new SqlCommand("Delete From users where Id = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
            DatabaseUpdate();
        }
        public Form8()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.form1.Show();
            this.Hide();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sampleDatabaseDataSet1.users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.sampleDatabaseDataSet1.users);
            DatabaseUpdate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataAdd();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataChange();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataDelete();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox2.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
        }
    }
}
