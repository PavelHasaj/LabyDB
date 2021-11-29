using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB{
    public partial class Form13 : Form{
        public Form13(){
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
            SqlCommand comand = new SqlCommand("Select * From Services ORDER BY Id_services ASC", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            dataGridView1.Columns[0].HeaderText = "Id услуги";
            dataGridView1.Columns[1].HeaderText = "Наименование";
            dataGridView1.Columns[2].HeaderText = "Цена";
        }

        private void button5_Click(object sender, EventArgs e){
            Program.form1.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e){
            Form15 form15 = new Form15();
            form15.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e){
            Form14 form14 = new Form14();
            form14.Show();
            this.Close();
        }

        private void Form13_Load(object sender, EventArgs e){
            DatabaseUpdate();
        }

        private void button9_Click(object sender, EventArgs e){
            DatabaseUpdate();
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("Select * From Services where Id_services>@Id_services", connection);
            command_select.Parameters.AddWithValue("@Id_services", Convert.ToInt32(textBox5.Text));
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
            SqlCommand command_select = new SqlCommand("Select * From Services where Cost>@Cost", connection);
            command_select.Parameters.AddWithValue("@Cost", Convert.ToInt32(textBox5.Text));
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
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

        private void button8_Click(object sender, EventArgs e){
            DatabaseUpdate();
        }

        private void Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }
    }
}
