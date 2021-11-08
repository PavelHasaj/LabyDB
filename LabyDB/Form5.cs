using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB
{
    public partial class Form5 : Form{
        //Строка подключения к БД
        SqlConnection connection = new SqlConnection(Program.GetConnectionString());
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SampleDatabaseDataSet dataSet = new SampleDatabaseDataSet();
        //Обновление
        private void DatabaseUpdate(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand("Select * From Spare_parts ORDER BY Id_spare_parts ASC", connection);
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

            SqlCommand comand = new SqlCommand("Insert Into Spare_parts Values (@Id_spare_parts, @Name, @Cost)", connection);
            comand.Parameters.AddWithValue("@Id_spare_parts", Convert.ToInt64(textBox1.Text));
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
            SqlCommand command = new SqlCommand("Update Spare_parts set Name=@Name, Cost=@Cost Where Id_spare_parts = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
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
            SqlCommand command = new SqlCommand("Delete From Spare_parts where Id_spare_parts = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);

            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }

        //Объявление переменной
        int name;
        int cost;

        public Form5(){
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e){
            DatabaseUpdate();

            //Запчасти
            comboBox1.Items.Add("Свеча накаливания");
            comboBox1.Items.Add("Фильтр воздушный");
            comboBox1.Items.Add("Фильтр масляный");
            comboBox1.Items.Add("Фильтр салона, пылевой");
            comboBox1.Items.Add("Фильтр салона, угольный");
            comboBox1.Items.Add("Фильтр топливный");
            comboBox1.Items.Add("Щётка стеклоочистителя, задняя");
            comboBox1.Items.Add("Щётки стеклоочистителя, комплект");
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
        //Кнопка удалить
        private void button7_Click(object sender, EventArgs e){
            DataDelete();
        }
        //Кнопка обновить
        private void button8_Click(object sender, EventArgs e){
            DatabaseUpdate();
        }
        //Кнопка выход на главную
        private void button5_Click(object sender, EventArgs e){
            Program.form1.Show();
            this.Hide();
        }
        //Кнопка назад
        private void button4_Click(object sender, EventArgs e){
            Program.form3.Show();
            this.Hide();
        }
        //Кнопка вперед
        private void button6_Click(object sender, EventArgs e){
            Program.form4.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Вывод номера и цены запчасти
            if (comboBox1.SelectedIndex == 0){
                name = 1;
                cost = 1971;
            }
            else if (comboBox1.SelectedIndex == 1){
                name = 2;
                cost = 1294;
            }
            else if (comboBox1.SelectedIndex == 2){
                name = 3;
                cost = 711;
            }
            else if (comboBox1.SelectedIndex == 3){
                name = 4;
                cost = 1614;
            }
            else if (comboBox1.SelectedIndex == 4){
                name = 5;
                cost = 3873;
            }
            else if (comboBox1.SelectedIndex == 5){
                name = 6;
                cost = 1824;
            }
            else if (comboBox1.SelectedIndex == 6){
                name = 7;
                cost = 709;
            }
            else if (comboBox1.SelectedIndex == 7){
                name = 8;
                cost = 2832;
            }
            else if (comboBox1.SelectedIndex == 8){
                name = 0;
                cost = 0;
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

        private void button9_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("Select * From Spare_parts where Id_spare_parts>@Id_spare_parts", connection);
            command_select.Parameters.AddWithValue("@Id_spare_parts", Convert.ToInt32(textBox5.Text));
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }
    }
}
