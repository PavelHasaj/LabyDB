using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace LabyDB
{
    public partial class PaymentsForm : Form
    {
        public PaymentsForm()
        {
            InitializeComponent();
        }

        private void ToPreviousFormButton_Click(object sender, EventArgs e)
        {
            AbonentsForm form = new AbonentsForm();
            form.Show();
            this.Close();
        }

        private void ToMainFormButton_Click(object sender, EventArgs e)
        {
            Program.mainForm.Show();
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

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sampleDatabaseDataSet1.Abonents". При необходимости она может быть перемещена или удалена.
            this.abonentsTableAdapter.Fill(this.sampleDatabaseDataSet1.Abonents);
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

        private void Ochko2()
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
        private void Ochko3()
        {
            DatabaseUpdate();
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("SELECT Abonents.FullName, Abonents.Adress, Payments.AbonentID, Payments.MonthOfPayment, Payments.Tariff, Payments.NumberOfKilowatts FROM Abonents INNER JOIN Payments ON Abonents.AbonentID = Payments.AbonentID", connection);
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
            Program.DeleteEmptyColumns(dataGridView1);
        }
        private void button14_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand
            ("SELECT Abonents.AbonentID, Payments.Tariff, Payments.NumberOfKilowatts,  Abonents.FullName, SUM(Payments.Tariff * Payments.NumberOfKilowatts) AS sum From Abonents INNER JOIN Payments ON Abonents.AbonentID = Payments.AbonentID GROUP BY Abonents.AbonentID, Abonents.FullName, Payments.NumberOfKilowatts, Payments.Tariff", connection);
            dataAdapter.SelectCommand = comand;

            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            Program.DeleteEmptyColumns(dataGridView1);
            int Sum = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                Sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
            }

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            //Создаем рабочую книгу:
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            //Нам доступно редактирование некоторых параметров, в качестве примера изменим ширину столбцов:
            ExcelApp.Columns.ColumnWidth = 15;
            //Задать значение ячейки можно так:
            ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, 5]].Merge();
            ExcelApp.Cells[1, 1] = "Ведомость оплаты за электроэнергию за месяц";
            ExcelApp.Cells[2, 1] = "Номер лицевого счета";
            ExcelApp.Cells[2, 2] = "Тариф";
            ExcelApp.Cells[2, 3] = "Кол-во киловатт";
            ExcelApp.Cells[2, 4] = "ФИО";
            ExcelApp.Cells[2, 5] = "Сумма к оплате";

            ExcelApp.Range[ExcelApp.Cells[dataGridView1.Rows.Count + 3, 1], ExcelApp.Cells[dataGridView1.Rows.Count + 3, 4]].Merge();
            ExcelApp.Cells[dataGridView1.Rows.Count + 3, 1] = "Итоговая стоимость:";
            ExcelApp.Cells[dataGridView1.Rows.Count + 3, 1].Font.Bold = true;
            ExcelApp.Cells[dataGridView1.Rows.Count + 3, 5] = Sum;

            ExcelApp.Range[ExcelApp.Cells[dataGridView1.Rows.Count + 4, 1], ExcelApp.Cells[dataGridView1.Rows.Count + 4, 5]].Merge();
            ExcelApp.Range[ExcelApp.Cells[dataGridView1.Rows.Count + 5, 1], ExcelApp.Cells[dataGridView1.Rows.Count + 5, 5]].Merge();
            ExcelApp.Cells[dataGridView1.Rows.Count + 4, 1] = "Отвественное лицо – “Павел Хасай";
            ExcelApp.Cells[dataGridView1.Rows.Count + 5, 1] = "Дата выдачи отчета - " + DateTime.Now;

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                {
                    ExcelApp.Cells[j + 3, i + 1] = (dataGridView1[i, j].Value).ToString();
                }
            }


            ExcelApp.Cells[1, 1].Font.Bold = true;
            ExcelApp.Cells[1, 1].Font.Size = 13;

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

        //Увеличен тариф за электроэнергию
        private void Dick()
        {
            SqlCommand command = new SqlCommand("Update Payments set Tariff=Tariff+@Tariff Where ID = ID", connection);

            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            command.Parameters.AddWithValue("@Tariff", Convert.ToInt32(textBox1.Text));

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }
        //Увеличен тариф за электроэнергию кнопка
        private void button19_Click(object sender, EventArgs e)
        {
            Dick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PaymentsFormEdeting form = new PaymentsFormEdeting();
            form.Show();
            this.Close();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            DatabaseUpdate();
            Program.DeleteEmptyColumns(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ochko2();

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            //Создаем рабочую книгу:
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            //Нам доступно редактирование некоторых параметров, в качестве примера изменим ширину столбцов:
            ExcelApp.Columns.ColumnWidth = 15;
            //Задать значение ячейки можно так:
            ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, 2]].Merge();
            ExcelApp.Cells[1, 1] = "Отчетная ведомость группировка тариф:";
            ExcelApp.Cells[2, 1] = "Тариф";
            ExcelApp.Cells[2, 2] = "Кол-во абонентов";

            ExcelApp.Range[ExcelApp.Cells[dataGridView1.Rows.Count + 3, 1], ExcelApp.Cells[dataGridView1.Rows.Count + 3, 6]].Merge();
            ExcelApp.Range[ExcelApp.Cells[dataGridView1.Rows.Count + 4, 1], ExcelApp.Cells[dataGridView1.Rows.Count + 4, 6]].Merge();
            ExcelApp.Cells[dataGridView1.Rows.Count + 3, 1] = "Отвественное лицо – “Павел Хасай";
            ExcelApp.Cells[dataGridView1.Rows.Count + 4, 1] = "Дата выдачи отчета - " + DateTime.Now;

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                {
                    ExcelApp.Cells[j + 3, i + 1] = (dataGridView1[i, j].Value).ToString();
                }
            }


            ExcelApp.Cells[1, 1].Font.Bold = true;
            ExcelApp.Cells[1, 1].Font.Size = 13;

            for (int i = 1; i <= 9; i++)
            {
                ExcelApp.Cells[2, i].Font.Bold = true;
                ExcelApp.Cells[2, i].Font.Size = 12;
            }

            ExcelApp.Columns.AutoFit();
            ExcelApp.Visible = true;

            DatabaseUpdate();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Ochko3();

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            //Создаем рабочую книгу:
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            //Нам доступно редактирование некоторых параметров, в качестве примера изменим ширину столбцов:
            ExcelApp.Columns.ColumnWidth = 15;
            //Задать значение ячейки можно так:
            ExcelApp.Range[ExcelApp.Cells[1, 1], ExcelApp.Cells[1, 6]].Merge();
            ExcelApp.Cells[1, 1] = "Отчетная ведомость группировка тариф:";
            ExcelApp.Cells[2, 1] = "Лицевой счет";
            ExcelApp.Cells[2, 2] = "Месяц оплаты";
            ExcelApp.Cells[2, 3] = "Тариф";
            ExcelApp.Cells[2, 4] = "Кол-во киловат";
            ExcelApp.Cells[2, 5] = "ФИО";
            ExcelApp.Cells[2, 6] = "Адрес";

            ExcelApp.Range[ExcelApp.Cells[dataGridView1.Rows.Count + 3, 1], ExcelApp.Cells[dataGridView1.Rows.Count + 3, 6]].Merge();
            ExcelApp.Range[ExcelApp.Cells[dataGridView1.Rows.Count + 4, 1], ExcelApp.Cells[dataGridView1.Rows.Count + 4, 6]].Merge();
            ExcelApp.Cells[dataGridView1.Rows.Count + 3, 1] = "Отвественное лицо – “Павел Хасай";
            ExcelApp.Cells[dataGridView1.Rows.Count + 4, 1] = "Дата выдачи отчета - " + DateTime.Now;


            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                {
                    ExcelApp.Cells[j + 3, i + 1] = (dataGridView1[i, j].Value).ToString();
                }
            }


            ExcelApp.Cells[1, 1].Font.Bold = true;
            ExcelApp.Cells[1, 1].Font.Size = 13;

            for (int i = 1; i <= 9; i++)
            {
                ExcelApp.Cells[2, i].Font.Bold = true;
                ExcelApp.Cells[2, i].Font.Size = 12;
            }

            ExcelApp.Columns.AutoFit();
            ExcelApp.Visible = true;

            DatabaseUpdate();
        }
    }
}
