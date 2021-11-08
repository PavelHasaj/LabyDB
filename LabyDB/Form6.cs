using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB
{
    public partial class Form6 : Form{
        //Строка подключения к БД
        SqlConnection connection = new SqlConnection(Program.GetConnectionString());
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SampleDatabaseDataSet dataSet = new SampleDatabaseDataSet();
        //Обновление
        private void DatabaseUpdate(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand("Select * From Service ORDER BY Id ASC", connection);
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

            SqlCommand comand = new SqlCommand("Insert Into Service Values (@Id, @State_number, @Id_services, @Id_spare_parts, @Ready_date, @Total_cost)", connection);
            comand.Parameters.AddWithValue("@Id", Convert.ToInt64(textBox1.Text));
            comand.Parameters.AddWithValue("@State_number", comboBox1.Text);
            comand.Parameters.AddWithValue("@Id_services", Convert.ToInt64(textBox3.Text));
            comand.Parameters.AddWithValue("@Id_spare_parts", Convert.ToInt64(textBox4.Text));
            comand.Parameters.AddWithValue("@Ready_date", textBox5.Text);
            comand.Parameters.AddWithValue("@Total_cost", Convert.ToInt64(textBox6.Text));

            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();//вызов метода обновления dataGridView
        }
        //Изменение записи
        void DataChange(){
            SqlCommand command = new SqlCommand("Update Service set State_number=@State_number, Id_services=@Id_services, Id_spare_parts=@Id_spare_parts, Ready_date=@Ready_date, Total_cost=@Total_cost Where Id = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            command.Parameters.AddWithValue("@Id_services", comboBox1.Text);
            command.Parameters.AddWithValue("@Id_services", Convert.ToInt64(textBox3.Text));
            command.Parameters.AddWithValue("@Id_spare_parts", Convert.ToInt64(textBox4.Text));
            command.Parameters.AddWithValue("@Ready_date", textBox5.Text);
            command.Parameters.AddWithValue("@Total_cost", Convert.ToInt64(textBox6.Text));

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }
        //Удаление записи
        void DataDelete(){
            SqlCommand command = new SqlCommand("Delete From Service where Id = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);

            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }

        public Form6(){
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e){
            DatabaseUpdate();
        }
        //Кнопка обновить
        private void button8_Click(object sender, EventArgs e){
            DatabaseUpdate();
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
        //Кнопка назад
        private void button4_Click(object sender, EventArgs e){
            Program.form4.Show();
            this.Hide();
        }
        //Кнопка выход на главную
        private void button5_Click(object sender, EventArgs e){
            Program.form1.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //Тут должна быть функция подсчета итоговой цены.
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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

        private void textBox2_Clear(object sender, EventArgs e)
        {
            textBox2.Clear();
        }
    }
}
