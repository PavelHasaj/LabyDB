using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB{
    public partial class Form11 : Form{
        public Form11(){
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

            dataGridView1.Columns[0].HeaderText = "Id владельца";
            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Номер телефона";
            dataGridView1.Columns[3].HeaderText = "Номер лицевого счета";
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
            ("SELECT * From Owner", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DeleteEmptyColumns(dataGridView1);
            dataGridView1.Columns[0].HeaderText = "Id_owner";
            dataGridView1.Columns[1].HeaderText = "FIO";
            dataGridView1.Columns[2].HeaderText = "Phone_number";
            dataGridView1.Columns[3].HeaderText = "Driver_license_number";
        }
        private void Form11_Load(object sender, EventArgs e){
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

        private void button7_Click(object sender, EventArgs e){
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

        private void button9_Click(object sender, EventArgs e){
            Ochko();

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            //Создаем рабочую книгу:
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            //Нам доступно редактирование некоторых параметров, в качестве примера изменим ширину столбцов:
            ExcelApp.Columns.ColumnWidth = 15;
            //Задать значение ячейки можно так:
            ExcelApp.Cells[1, 2] = "Владельцы";
            ExcelApp.Cells[2, 1] = "Id владельца";
            ExcelApp.Cells[2, 2] = "ФИО";
            ExcelApp.Cells[2, 3] = "Номер телефона";
            ExcelApp.Cells[2, 4] = "Номер лицевого счета";


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

        private void button5_Click(object sender, EventArgs e){
            Program.form1.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e){
            Form12 form12 = new Form12();
            form12.Show();
            this.Close();
        }

        private void Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }
    }
}
