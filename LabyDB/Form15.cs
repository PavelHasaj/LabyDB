using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Office.Interop;

namespace LabyDB{
    public partial class Form15 : Form{
        public Form15(){
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
            SqlCommand comand = new SqlCommand("Select * From Service ORDER BY Id ASC", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DeleteEmptyColumns(dataGridView1);
            dataGridView1.Columns[0].HeaderText = "Id";
            dataGridView1.Columns[1].HeaderText = "Гос. номер";
            dataGridView1.Columns[2].HeaderText = "Id услуги";
            dataGridView1.Columns[3].HeaderText = "Id запчасти";
            dataGridView1.Columns[4].HeaderText = "Дата готовности";
            dataGridView1.Columns[5].HeaderText = "Наценка";
            dataGridView1.Columns[6].HeaderText = "Итоговая цена";
        }

        private void button5_Click(object sender, EventArgs e){
            Program.form1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e){
            Form13 form13 = new Form13();
            form13.Show();
            this.Close();
        }

        private void Form15_Load(object sender, EventArgs e){
            DatabaseUpdate();
        }

        public static void DeleteEmptyColumns(DataGridView dataGridView1){
            bool IsColumnEmpty;
            for (int i = 0; i < dataGridView1.Columns.Count; i++){
                IsColumnEmpty = dataGridView1.Rows[0].Cells[i].Value.ToString() == "";
                if (IsColumnEmpty){
                    dataGridView1.Columns.RemoveAt(i);
                    i--;
                }
            }
        }

        private void Ochko(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand
            ("SELECT Service.Id, Service.State_number, Service.Ready_date, Service.Nacenka, Services.Name, Services.Cost, Spare_parts.Name, Spare_parts.Cost, Service.Total_cost " +
            "FROM Service INNER JOIN Services ON Service.Id_services=Services.Id_services LEFT JOIN Spare_parts ON Service.Id_spare_parts=Spare_parts.Id_spare_parts", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DeleteEmptyColumns(dataGridView1);
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "State_number";
            dataGridView1.Columns[2].HeaderText = "Ready_date";
            dataGridView1.Columns[3].HeaderText = "Nacenka";
            dataGridView1.Columns[4].HeaderText = "Total_cost";
            dataGridView1.Columns[5].HeaderText = "Name";
            dataGridView1.Columns[6].HeaderText = "Cost";
            dataGridView1.Columns[7].HeaderText = "Name";
            dataGridView1.Columns[8].HeaderText = "Cost";
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
            SqlCommand command_select = new SqlCommand("Select * From Service where Ready_date>@Ready_date", connection);
            command_select.Parameters.AddWithValue("@Ready_date", Convert.ToDateTime(dateTimePicker1.Text));
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
            SqlCommand command_select = new SqlCommand("Select * From Service where Total_cost>@Total_cost", connection);
            command_select.Parameters.AddWithValue("@Total_cost", Convert.ToInt32(textBox5.Text));
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void button9_Click(object sender, EventArgs e){
            Ochko();

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            //Создаем рабочую книгу:
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            //Нам доступно редактирование некоторых параметров, в качестве примера изменим ширину столбцов:
            ExcelApp.Columns.ColumnWidth = 15;
            //Задать значение ячейки можно так:

            ExcelApp.Cells[1, 5] = "Итоговый отчет";
            ExcelApp.Cells[2, 1] = "ID";
            ExcelApp.Cells[2, 2] = "Гос. номер";
            ExcelApp.Cells[2, 3] = "Дата готовности";
            ExcelApp.Cells[2, 4] = "Наценка";
            ExcelApp.Cells[2, 5] = "Итоговая цена";
            ExcelApp.Cells[2, 6] = "Наименование";
            ExcelApp.Cells[2, 7] = "Цена";
            ExcelApp.Cells[2, 8] = "Наименование";
            ExcelApp.Cells[2, 9] = "Цена";

            ExcelApp.Cells[dataGridView1.Rows.Count + 3, 1] = "Отвественное лицо - Отвественное лицо – “Скопинцев Олег Данилович ";
            ExcelApp.Cells[dataGridView1.Rows.Count + 4, 1] = "Дата выдачи отчета - " + DateTime.Now;

            for (int i = 0; i < dataGridView1.ColumnCount; i++){
                for (int j = 0; j < dataGridView1.RowCount - 1; j++){
                    ExcelApp.Cells[j + 3, i + 1] = (dataGridView1[i, j].Value).ToString();
                }
            }
            ExcelApp.Visible = true;


            DatabaseUpdate();
        }

        private void Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }
    }
}
