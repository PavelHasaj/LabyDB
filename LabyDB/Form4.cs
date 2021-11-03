using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB
{
    public partial class Form4 : Form
    {
        SqlConnection connection = new SqlConnection(Program.GetConnectionString());
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SampleDatabaseDataSet dataSet = new SampleDatabaseDataSet();

        private void DatabaseUpdate()
        {
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand("Select * From Services ORDER BY Id_services ASC", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        void DataAdd()
        {
            //добавление записи
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            SqlCommand comand = new SqlCommand("Insert Into Services Values (@Id_services, @Name, @Cost)", connection);
            comand.Parameters.AddWithValue("@Id_services", Convert.ToInt64(textBox1.Text));
            comand.Parameters.AddWithValue("@Name", textBox2.Text);
            comand.Parameters.AddWithValue("@Cost", Convert.ToInt64(textBox3.Text));

            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();//вызов метода обновления dataGridView
        }

        void DataChange()
        {
            SqlCommand command = new SqlCommand("Update Services set Name=@Name, Cost=@Cost Where Id_services = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            command.Parameters.AddWithValue("@Name", textBox2.Text);
            command.Parameters.AddWithValue("@Cost", textBox3.Text);

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }

        void DataDelete()
        {
            SqlCommand command = new SqlCommand("Delete From Services where Id_services = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);

            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.form1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Program.form5.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Program.form6.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataDelete();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
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

        private void button8_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
        }

        public Form4()
        {
            InitializeComponent();
        }
    }
}
