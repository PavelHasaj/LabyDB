using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

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

        SqlConnection connection = new SqlConnection(Program.GetConnectionString());
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

            SqlCommand comand = new SqlCommand("Insert Into Payments Values (@ID, @AbonentID, @MonthOfPayment, @Tariff, @NumberOfKilowatts)", connection);

            comand.Parameters.AddWithValue("@ID", Convert.ToInt32(textBox9.Text));
            comand.Parameters.AddWithValue("@AbonentID", comboBox1.SelectedValue);
            comand.Parameters.AddWithValue("@MonthOfPayment", Convert.ToDateTime(dateTimePicker1.Text));
            comand.Parameters.AddWithValue("@Tariff", Convert.ToInt32(textBox3.Text));
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
            SqlCommand command = new SqlCommand("Update Payments set AbonentID=@AbonentID, MonthOfPayment=@MonthOfPayment, Tariff=@Tariff, NumberOfKilowatts=@NumberOfKilowatts Where ID = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            command.Parameters.AddWithValue("@AbonentID", comboBox1.SelectedValue);
            command.Parameters.AddWithValue("@MonthOfPayment", Convert.ToDateTime(dateTimePicker1.Text));
            command.Parameters.AddWithValue("@Tariff", Convert.ToDouble(textBox3.Text));
            command.Parameters.AddWithValue("@NumberOfKilowatts", Convert.ToInt32(textBox4.Text));

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }

        // Удаление
        private void button7_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Delete From Payments where ID = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);

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
            Program.DeleteEmptyColumns(dataGridView1);
        }

        //Считает сумму к оплате за месяц
        private void button2_Click(object sender, EventArgs e)
        {
                dataGridView1.DataSource = null;
                dataSet.Clear();
                connection.Open();

                SqlCommand comand = new SqlCommand("Select *, Tariff * NumberOfKilowatts AS Total_cost FROM Payments", connection);
                dataAdapter.SelectCommand = comand;
                dataAdapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
                connection.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("Select * From Payments inner join Abonents on Payments.AbonentID = Abonents.AbonentID where Adress = @Adress", connection);
            command_select.Parameters.AddWithValue("@Adress", comboBox2.SelectedValue);
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("Select * From Payments where NumberOfKilowatts>@NumberOfKilowatts", connection);
            command_select.Parameters.AddWithValue("@NumberOfKilowatts", Convert.ToInt32(textBox7.Text));
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("SELECT Tariff, COUNT(*) as count FROM Payments GROUP BY Tariff", connection);
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
            Program.DeleteEmptyColumns(dataGridView1);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("SELECT AbonentID, SUM(Tariff * NumberOfKilowatts) as sum FROM Payments GROUP BY AbonentID", connection);
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
            Program.DeleteEmptyColumns(dataGridView1);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_Click(object sender, EventArgs e)
        {
            textBox8.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox8.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("Select * From Payments where AbonentID>@AbonentID", connection);
            command_select.Parameters.AddWithValue("@AbonentID", Convert.ToInt32(textBox8.Text));
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }
        private void Ochko()
        {
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand
            ("SELECT * From Payments", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            Program.DeleteEmptyColumns(dataGridView1);

            dataGridView1.Columns[0].HeaderText = "Номер записи";
            dataGridView1.Columns[1].HeaderText = "Номер лицевого счета";
            dataGridView1.Columns[2].HeaderText = "Месяц оплаты";
            dataGridView1.Columns[3].HeaderText = "Тариф";
            dataGridView1.Columns[4].HeaderText = "Количество киловатт";

        }
        private void button14_Click(object sender, EventArgs e)
        {
            Ochko();

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            //Создаем рабочую книгу:
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            //Нам доступно редактирование некоторых параметров, в качестве примера изменим ширину столбцов:
            ExcelApp.Columns.ColumnWidth = 15;
            //Задать значение ячейки можно так:

            ExcelApp.Cells[1, 3] = "Итоговый отчет по оплате";
            ExcelApp.Cells[2, 1] = "Номер записи";
            ExcelApp.Cells[2, 2] = "Номер лицевого счета";
            ExcelApp.Cells[2, 3] = "Месяц оплаты";
            ExcelApp.Cells[2, 4] = "Тариф";
            ExcelApp.Cells[2, 5] = "Количество киловатт";

            ExcelApp.Cells[dataGridView1.Rows.Count + 3, 1] = "Отвественное лицо - Отвественное лицо – “Павел Хасай";
            ExcelApp.Cells[dataGridView1.Rows.Count + 4, 1] = "Дата выдачи отчета - " + DateTime.Now;

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                {
                    ExcelApp.Cells[j + 3, i + 1] = (dataGridView1[i, j].Value).ToString();
                }
            }


            ExcelApp.Cells[1, 3].Font.Bold = true;
            ExcelApp.Cells[1, 3].Font.Size = 13;

            for (int i = 1; i <= 9; i++)
            {
                ExcelApp.Cells[2, i].Font.Bold = true;
                ExcelApp.Cells[2, i].Font.Size = 12;
            }

            ExcelApp.Columns.AutoFit();
            ExcelApp.Visible = true;

            DatabaseUpdate();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            SqlCommand comand = new SqlCommand("Select * FROM Payments WHERE NumberOfKilowatts >= 500", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            SqlCommand comand = new SqlCommand("Select * FROM Payments ORDER BY Tariff", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            SqlCommand comand = new SqlCommand("Select * FROM Payments ORDER BY AbonentID", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void button18_Click(object sender, EventArgs e) {
            dataGridView1.DataSource = null;
            connection.Open();
            SqlCommand comand = new SqlCommand("Select * from Payments Where NumberOfKilowatts >= @min AND NumberOfKilowatts <= @max", connection);
            DataSet ds = new DataSet();
            string[] range = new string[2];
            range = comboBox3.SelectedItem.ToString().Split('-');
            comand.Parameters.AddWithValue("@min", range[0]);
            comand.Parameters.AddWithValue("@max", range[1]);

            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            connection.Close();
        }
    }
}
