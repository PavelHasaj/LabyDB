using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB {
    public partial class Form2 : Form {
        public Form2() {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(Program.GetConnectionString());
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SampleDatabaseDataSet dataSet = new SampleDatabaseDataSet();

        private void DatabaseUpdate() {
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("Select * From Abonents", connection);
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        void DataAdd() {
            //добавление записи
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            SqlCommand comand = new SqlCommand("Insert Into Abonents Values (@Nomer_licevogo_cheta, @FIO, @Adres)", connection);
            comand.Parameters.AddWithValue("@Nomer_licevogo_cheta", textBox1.Text);
            comand.Parameters.AddWithValue("@FIO", textBox2.Text);
            comand.Parameters.AddWithValue("@Adres", textBox3.Text);

            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();//вызов метода обновления dataGridView
        }

        void DataChange() {
            SqlCommand command = new SqlCommand("Update Abonents set FullName=@FullName, Adress=@Adress Where AbonentID = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            command.Parameters.AddWithValue("@FullName", textBox2.Text);
            command.Parameters.AddWithValue("@Adress", textBox3.Text);

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }

        void DataDelete() {
            SqlCommand command = new SqlCommand("Delete From Abonents where AbonentID = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);

            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }

        private void Form1_Load(object sender, EventArgs e) {
            DatabaseUpdate();
        }

        private void add_button(object sender, EventArgs e) {
            DataAdd();
        }

        private void data_change_button(object sender, EventArgs e) {
            DataChange();
        }

        private void data_delete_button(object sender, EventArgs e) {
            DataDelete();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Program.form3.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Program.form1.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.form1.Show();
            this.Hide();
        }
    }
}
