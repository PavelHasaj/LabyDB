using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Office.Interop;

namespace LabyDB
{
    public partial class Form6 : Form {
        //Строка подключения к БД
        SqlConnection connection = new SqlConnection(Program.GetConnectionString());
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SampleDatabaseDataSet dataSet = new SampleDatabaseDataSet();

        //Обновление
        private void DatabaseUpdate() {
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
        //Добавление записи
        void DataAdd(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            SqlCommand comand = new SqlCommand("Insert Into Service Values (@Id, @State_number, @Id_services, @Id_spare_parts, @Ready_date, @Nacenka, @Total_cost)", connection);
            comand.Parameters.AddWithValue("@Id", Convert.ToInt32(textBox1.Text));
            comand.Parameters.AddWithValue("@State_number", comboBox1.Text);
            comand.Parameters.AddWithValue("@Id_services", Convert.ToInt32(comboBox2.Text));
            comand.Parameters.AddWithValue("@Id_spare_parts", Convert.ToInt32(comboBox3.Text));
            comand.Parameters.AddWithValue("@Ready_date", Convert.ToDateTime(dateTimePicker1.Text));
            comand.Parameters.AddWithValue("@Nacenka", Convert.ToInt32(textBox6.Text));
            comand.Parameters.AddWithValue("@Total_cost", Convert.ToInt32(textBox3.Text));

            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();//вызов метода обновления dataGridView
        }
        //складывает наценку + сумма(услуг + цены товара)
        void DataDB(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            SqlCommand comand = new SqlCommand("UPDATE Service SET Total_cost=Total_cost+Nacenka", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }
        private void DataDB2(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand("Update Service SET Total_cost = Services.Cost + Spare_parts.Cost FROM (Service INNER JOIN Services ON Service.Id_services = Services.Id_services LEFT JOIN Spare_parts ON Service.Id_spare_parts = Spare_parts.Id_spare_parts)", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
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
        //Изменение записи
        void DataChange(){
            SqlCommand command = new SqlCommand("Update Service set State_number=@State_number, Id_services=@Id_services, Id_spare_parts=@Id_spare_parts, Ready_date=@Ready_date, Nacenka=@Nacenka Where Id = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            command.Parameters.AddWithValue("@State_number", comboBox1.Text);
            command.Parameters.AddWithValue("@Id_services", Convert.ToInt32(comboBox2.Text));
            command.Parameters.AddWithValue("@Id_spare_parts", Convert.ToInt32(comboBox3.Text));
            command.Parameters.AddWithValue("@Ready_date", Convert.ToDateTime(dateTimePicker1.Text));
            command.Parameters.AddWithValue("@Nacenka", Convert.ToInt32(textBox6.Text));

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
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sampleDatabaseDataSet1.Spare_parts". При необходимости она может быть перемещена или удалена.
            this.spare_partsTableAdapter.Fill(this.sampleDatabaseDataSet1.Spare_parts);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sampleDatabaseDataSet1.Services". При необходимости она может быть перемещена или удалена.
            this.servicesTableAdapter.Fill(this.sampleDatabaseDataSet1.Services);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sampleDatabaseDataSet1.Cars". При необходимости она может быть перемещена или удалена.
            this.carsTableAdapter.Fill(this.sampleDatabaseDataSet1.Cars);

            DatabaseUpdate();
            textBox3.Enabled = false;//недоступность
            textBox3.Visible = false;//скрытие поля ввода

            ToolTip t = new ToolTip();
            t.SetToolTip(button1, "Нажмите чтобы добавить новую запись.");
            t.SetToolTip(button3, "Нажмите чтобы изменить существующую запись.");
            t.SetToolTip(button7, "Нажмите чтобы удалить существующую запись.");
        }
        //Кнопка обновить
        private void button8_Click(object sender, EventArgs e){
            DatabaseUpdate();
        }
        //Кнопка добавить
        private void button1_Click(object sender, EventArgs e){
            DataAdd();
            DataDB2();
            DataDB();
            DatabaseUpdate();
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
            Form4 form4 = new Form4();
            form4.Show();
            this.Close();
        }
        //Кнопка выход на главную
        private void button5_Click(object sender, EventArgs e){
            Form9 form9 = new Form9();
            form9.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e){

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e){
        }

        private void button6_Click(object sender, EventArgs e){
            for (int i = 0; i < dataGridView1.RowCount; i++){
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox2.Text)){
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        private void textBox2_Clear(object sender, EventArgs e){
            textBox2.Clear();
        }

        private void button9_Click(object sender, EventArgs e){
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

        private void button2_Click(object sender, EventArgs e){
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

        private void button10_Click(object sender, EventArgs e){
            DatabaseUpdate();
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("Select * From Service where Total_cost>@Total_cost", connection);
            command_select.Parameters.AddWithValue("@Total_cost", Convert.ToInt32(textBox2.Text));
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void textBox5_TextChanged(object sender, EventArgs e){

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e){
        }
    }
}
