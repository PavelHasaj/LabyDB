using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB
{
    public partial class Form4 : Form{
        //Строка подключения к БД
        SqlConnection connection = new SqlConnection(Program.GetConnectionString());
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SampleDatabaseDataSet dataSet = new SampleDatabaseDataSet();
        //Обновление
        private void DatabaseUpdate(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand("Select * From Services ORDER BY Id_services ASC", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }
        //Добавление записи
        void DataAdd(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            SqlCommand comand = new SqlCommand("Insert Into Services Values (@Id_services, @Name, @Cost)", connection);
            comand.Parameters.AddWithValue("@Id_services", Convert.ToInt64(textBox1.Text));
            comand.Parameters.AddWithValue("@Name", comboBox1.Text);
            comand.Parameters.AddWithValue("@Cost", Convert.ToInt64(textBox3.Text));

            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();//вызов метода обновления dataGridView
        }
        //Изменение записи
        void DataChange(){
            SqlCommand command = new SqlCommand("Update Services set Name=@Name, Cost=@Cost Where Id_services = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            command.Parameters.AddWithValue("@Name", comboBox1.Text);
            command.Parameters.AddWithValue("@Cost", textBox3.Text);

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }
        //Удаление записи
        void DataDelete(){
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
        //Кнопка выход на главную
        private void button5_Click(object sender, EventArgs e){
            Program.form1.Show();
            this.Hide();
        }
        //Кнопка назад
        private void button4_Click(object sender, EventArgs e){
            Program.form5.Show();
            this.Hide();
        }
        //Кнопка вперед
        private void button6_Click(object sender, EventArgs e){
            Program.form6.Show();
            this.Hide();
        }
        //Кнопка удалить
        private void button7_Click(object sender, EventArgs e){
            DataDelete();
        }

        //Объявление переменной
        int name;
        int cost;

        private void Form4_Load(object sender, EventArgs e){
            DatabaseUpdate();

            //Услуги
            comboBox1.Items.Add("Авто-электрика");
            comboBox1.Items.Add("Аргонная сварка");
            comboBox1.Items.Add("Замена приводных ремней");
            comboBox1.Items.Add("Замена расходников");
            comboBox1.Items.Add("Замена технических жидкостей");
            comboBox1.Items.Add("Заправка и ремонт кондиционеров");
            comboBox1.Items.Add("Компьютерная диагностика");
            comboBox1.Items.Add("Развал схождения 3D");
            comboBox1.Items.Add("Ремонт выхлопной системы");
            comboBox1.Items.Add("Ремонт двигателя");
            comboBox1.Items.Add("Ремонт подвески");
            comboBox1.Items.Add("Тонировка");
            comboBox1.Items.Add("Другое");
        }
        //Кнопка добавить
        private void button1_Click(object sender, EventArgs e){
            DataAdd();
        }
        //Кнопка изменить
        private void button3_Click(object sender, EventArgs e){
            DataChange();
        }
        //Кнопка обновить
        private void button8_Click(object sender, EventArgs e){
            DatabaseUpdate();
        }

        public Form4(){
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e){
            //Вывод номера и цены услуги
            if (comboBox1.SelectedIndex == 0){
                name = 1;
                cost = 500;
            }
            else if (comboBox1.SelectedIndex == 1){
                name = 2;
                cost = 1000;
            }
            else if (comboBox1.SelectedIndex == 2){
                name = 3;
                cost = 210;
            }
            else if (comboBox1.SelectedIndex == 3){
                name = 4;
                cost = 1200;
            }
            else if (comboBox1.SelectedIndex == 4){
                name = 5;
                cost = 500;
            }
            else if (comboBox1.SelectedIndex == 5){
                name = 6;
                cost = 4500;
            }
            else if (comboBox1.SelectedIndex == 6){
                name = 7;
                cost = 1000;
            }
            else if (comboBox1.SelectedIndex == 7){
                name = 8;
                cost = 500;
            }
            else if (comboBox1.SelectedIndex == 8){
                name = 9;
                cost = 2500;
            }
            else if (comboBox1.SelectedIndex == 9){
                name = 10;
                cost = 5000;
            }
            else if (comboBox1.SelectedIndex == 10){
                name = 11;
                cost = 5000;
            }
            else if (comboBox1.SelectedIndex == 11){
                name = 12;
                cost = 1000;
            }

            textBox1.Text = name.ToString();
            textBox3.Text = cost.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
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
    }
}
