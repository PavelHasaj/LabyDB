﻿using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB {
    public partial class Form2 : Form {
        public Form2() {
            InitializeComponent();
        }
        //Строка подключения к БД
        SqlConnection connection = new SqlConnection(Program.GetConnectionString());
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SampleDatabaseDataSet dataSet = new SampleDatabaseDataSet();
        //Обновление
        private void DatabaseUpdate(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("Select * From Owner", connection);
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }
        //Добавление записи
        void DataAdd(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand("Insert Into Owner Values (@Id_owner, @FIO, @Phone_number, @Driver_license_number)", connection);
            comand.Parameters.AddWithValue("@Id_owner", Convert.ToInt64(textBox1.Text));
            comand.Parameters.AddWithValue("@FIO", textBox2.Text);
            comand.Parameters.AddWithValue("@Phone_number", textBox3.Text);
            comand.Parameters.AddWithValue("@Driver_license_number", textBox4.Text);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
            DatabaseUpdate();//вызов метода обновления dataGridView
        }
        //Изменение записи
        void DataChange(){
            SqlCommand command = new SqlCommand("Update Owner set FIO=@FIO, Phone_number=@Phone_number, Driver_license_number=@Driver_license_number Where Id_owner = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            command.Parameters.AddWithValue("@FIO", textBox2.Text);
            command.Parameters.AddWithValue("@Phone_number", textBox3.Text);
            command.Parameters.AddWithValue("@Driver_license_number", textBox4.Text);
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
            DatabaseUpdate();
        }
        //Удаление записи
        void DataDelete(){
            SqlCommand command = new SqlCommand("Delete From Owner where Id_owner = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
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
        //Кнопка добавить
        private void add_button(object sender, EventArgs e) {
            DataAdd();
        }
        //Кнопка изменить
        private void data_change_button(object sender, EventArgs e) {
            DataChange();
        }
        //Кнопка удалить
        private void data_delete_button(object sender, EventArgs e) {
            DataDelete();
        }
        //Кнопка вперед
        private void button6_Click(object sender, EventArgs e)
        {
            Program.form3.Show();
            this.Hide();
        }
        //Кнопка обновить
        private void button8_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
        }
        //Кнопка выход на главную
        private void button5_Click(object sender, EventArgs e)
        {
            Program.form1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox5.Text)) 
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        private void textBox5_Clear(object sender, EventArgs e)
        {
            textBox5.Clear();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("Select * From Owner where Id_owner>@Id_owner", connection);
            command_select.Parameters.AddWithValue("@Id_owner", Convert.ToInt32(textBox5.Text));
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }
    }
}
