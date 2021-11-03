using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB
{
    public partial class Form3 : Form
    {
        SqlConnection connection = new SqlConnection(Program.GetConnectionString());
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SampleDatabaseDataSet dataSet = new SampleDatabaseDataSet();

        private void DatabaseUpdate()
        {
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand("Select * From Cars ORDER BY State_number ASC", connection);
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

            SqlCommand comand = new SqlCommand("Insert Into Cars Values (@Id_owner, @State_number, @Car_brand)", connection);
            comand.Parameters.AddWithValue("@Id_owner", Convert.ToInt64(textBox1.Text));
            comand.Parameters.AddWithValue("@State_number", textBox2.Text);
            comand.Parameters.AddWithValue("@Car_brand", textBox3.Text);

            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();//вызов метода обновления dataGridView
        }

        void DataChange()
        {
            SqlCommand command = new SqlCommand("Update Cars set Id_owner=@Id_owner, Car_brandr=@Car_brand Where State_number = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            command.Parameters.AddWithValue("@Id_owner", textBox2.Text);
            command.Parameters.AddWithValue("@Car_brand", textBox3.Text);

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }

        void DataDelete()
        {
            SqlCommand command = new SqlCommand("Delete From Cars where State_number = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);

            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }

        // Добавление
        private void button1_Click(object sender, EventArgs e)
        {
            DataAdd();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            DatabaseUpdate();
        }

        // Изменение
        private void button3_Click(object sender, EventArgs e)
        {
            DataChange();
        }

        // Удаление
        private void button7_Click(object sender, EventArgs e)
        {
            DataDelete();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
        }

        public Form3()
        {
            InitializeComponent();
        }

        //Переход назад
        private void button4_Click(object sender, EventArgs e)
        {
            Program.form2.Show();
            this.Hide();
        }

        //Переход на главную
        private void button5_Click(object sender, EventArgs e)
        {
            Program.form1.Show();
            this.Hide();
        }

        //Переход вперед
        private void button6_Click(object sender, EventArgs e)
        {
            Program.form5.Show();
            this.Hide();
        }
    }
}
