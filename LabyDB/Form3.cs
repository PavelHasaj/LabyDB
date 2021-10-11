using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB
{
    public partial class Form3 : Form
    {
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
            Program.form4.Show();
            this.Hide();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\feedf\source\repos\LabyDB\LabyDB\SampleDatabase.mdf;Integrated Security=True");
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SampleDatabaseDataSet dataSet = new SampleDatabaseDataSet();

        private void DatabaseUpdate()
        {
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("Select * From Payments", connection);
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        // Добавление
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            SqlCommand comand = new SqlCommand("Insert Into Payments Values (@AbonentID, @MonthOfPayment, @Tariff, @NumberOfKilowatts)", connection);
            comand.Parameters.AddWithValue("@AbonentID", textBox1.Text);
            comand.Parameters.AddWithValue("@MonthOfPayment", Convert.ToDateTime(textBox2.Text));
            comand.Parameters.AddWithValue("@Tariff", Convert.ToDouble(textBox3.Text));
            comand.Parameters.AddWithValue("@NumberOfKilowatts", Convert.ToInt32(textBox4.Text));

            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();//вызов метода обновления dataGridView
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            DatabaseUpdate();
        }

        // Изменение
        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update Payments set MonthOfPayment=@MonthOfPayment, Tariff=@Tariff, NumberOfKilowatts=@NumberOfKilowatts Where AbonentID = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            command.Parameters.AddWithValue("@MonthOfPayment", textBox2.Text);
            command.Parameters.AddWithValue("@Tariff", textBox3.Text);
            command.Parameters.AddWithValue("@NumberOfKilowatts", textBox4.Text);

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }

        // Удаление
        private void button7_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Delete From Payments where AbonentID = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);

            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
        }

        //Считает сумму к оплате за месяц
        private void button2_Click(object sender, EventArgs e)
        {
            double b = 0;
            double c = 0;
            double umn = 0;
            int h = dataGridView1.CurrentRow.Index;
            int j = dataGridView1.CurrentRow.Index;
            b = Convert.ToDouble(dataGridView1[2, h].Value);
            c = Convert.ToDouble(dataGridView1[3, j].Value);
            umn = b * c;
            textBox5.Text = Convert.ToString(umn);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }
    }
}
