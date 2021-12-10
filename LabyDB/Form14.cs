using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB{
    public partial class Form14 : Form{
        public Form14(){
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
            SqlCommand comand = new SqlCommand("Select * From Spare_parts ORDER BY Id_spare_parts ASC", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            dataGridView1.Columns[0].HeaderText = "Id запчасти";
            dataGridView1.Columns[1].HeaderText = "Наименование";
            dataGridView1.Columns[2].HeaderText = "Цена";

            Program.DeleteEmptyColumns(dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e){
            Program.form1.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e){
            Form13 form13 = new Form13();
            form13.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e){
            Form12 form12 = new Form12();
            form12.Show();
            this.Close();
        }

        private void Form14_Load(object sender, EventArgs e){
            DatabaseUpdate();
        }

        private void button8_Click(object sender, EventArgs e){
            DatabaseUpdate();
        }

        private void button4_Click(object sender, EventArgs e){
            for (int i = 0; i < dataGridView1.RowCount; i++){
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox5.Text)){
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        private void button2_Click(object sender, EventArgs e){
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

        private void button10_Click(object sender, EventArgs e){
            DatabaseUpdate();
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("Select * From Spare_parts where Cost>@Cost", connection);
            command_select.Parameters.AddWithValue("@Cost", Convert.ToInt32(textBox5.Text));
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand("SELECT Spare_parts.Id_spare_parts, Spare_parts.Name, COUNT (Service.Id) AS count FROM Service, Spare_parts WHERE Spare_parts.Id_spare_parts = Service.Id_spare_parts GROUP BY Spare_parts.Id_spare_parts, Spare_parts.Name ORDER BY count DESC", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            dataGridView1.Columns[0].HeaderText = "Id запчасти";
            dataGridView1.Columns[1].HeaderText = "Наименование";
            dataGridView1.Columns[3].HeaderText = "Кол-во заказов";

            Program.DeleteEmptyColumns(dataGridView1);
        }
    }
}
