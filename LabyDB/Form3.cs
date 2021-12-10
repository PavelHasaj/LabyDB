using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB
{
    public partial class Form3 : Form{
        //Строка подключения к БД
        SqlConnection connection = new SqlConnection(Program.GetConnectionString());
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SampleDatabaseDataSet dataSet = new SampleDatabaseDataSet();
        //Обновление
        private void DatabaseUpdate(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand("Select * From Cars ORDER BY Id_owner ASC", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            dataGridView1.Columns[0].HeaderText = "Id владельца";
            dataGridView1.Columns[1].HeaderText = "Гос. номер";
            dataGridView1.Columns[2].HeaderText = "Марка машины";
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
            ("Select * From Cars", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DeleteEmptyColumns(dataGridView1);
            dataGridView1.Columns[0].HeaderText = "Id_owner";
            dataGridView1.Columns[1].HeaderText = "State_number";
            dataGridView1.Columns[2].HeaderText = "Car_brand";
        }
        //Добавление записи
        void DataAdd(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            SqlCommand comand = new SqlCommand("Insert Into Cars Values (@Id_owner, @State_number, @Car_brand)", connection);
            comand.Parameters.AddWithValue("@Id_owner", Convert.ToInt64(textBox1.Text));
            comand.Parameters.AddWithValue("@State_number", textBox2.Text);
            comand.Parameters.AddWithValue("@Car_brand", comboBox1.Text);

            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();//вызов метода обновления dataGridView
        }
        //Изменение записи
        void DataChange(){
            SqlCommand command = new SqlCommand("Update Cars set State_number=@State_number, Car_brand=@Car_brand Where Id_owner = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            command.Parameters.AddWithValue("@State_number", textBox2.Text);
            command.Parameters.AddWithValue("@Car_brand", comboBox1.Text);

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }
        //Удаление записи
        void DataDelete(){
            SqlCommand command = new SqlCommand("Delete From Cars where Id_owner = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);

            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }

        //Кнопка добавить
        private void button1_Click(object sender, EventArgs e){
            DataAdd();

            DatabaseUpdate();
            }

        private void Form3_Load(object sender, EventArgs e){
            DatabaseUpdate();

            //Марки машины
            comboBox1.Items.Add("Audi");
            comboBox1.Items.Add("BMW");
            comboBox1.Items.Add("Cadillac");
            comboBox1.Items.Add("Chevrolet");
            comboBox1.Items.Add("Citroen");
            comboBox1.Items.Add("Daewoo");
            comboBox1.Items.Add("Fiat");
            comboBox1.Items.Add("Ford");
            comboBox1.Items.Add("Ford usa");
            comboBox1.Items.Add("Honda");
            comboBox1.Items.Add("Hummer");
            comboBox1.Items.Add("Hyundai");
            comboBox1.Items.Add("Infiniti");
            comboBox1.Items.Add("Jaguar");
            comboBox1.Items.Add("Jeep");
            comboBox1.Items.Add("Kia");
            comboBox1.Items.Add("Land rover");
            comboBox1.Items.Add("Lexus");
            comboBox1.Items.Add("Mazda");
            comboBox1.Items.Add("Mercedes");
            comboBox1.Items.Add("Mitsubishi");
            comboBox1.Items.Add("Nissan");
            comboBox1.Items.Add("Opel");
            comboBox1.Items.Add("Peugeot");
            comboBox1.Items.Add("Renault");
            comboBox1.Items.Add("Seat");
            comboBox1.Items.Add("Skoda");
            comboBox1.Items.Add("SsangYong");
            comboBox1.Items.Add("Subaru");
            comboBox1.Items.Add("Suzuki");
            comboBox1.Items.Add("Toyota");
            comboBox1.Items.Add("Volkswagen");
            comboBox1.Items.Add("Volvo");
            comboBox1.Items.Add("ЗАЗ");
            comboBox1.Items.Add("ТагАЗ");

            ToolTip t = new ToolTip();
            t.SetToolTip(button1, "Нажмите чтобы добавить новую запись.");
            t.SetToolTip(button3, "Нажмите чтобы изменить существующую запись.");
            t.SetToolTip(button7, "Нажмите чтобы удалить существующую запись.");
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

        public Form3(){
            InitializeComponent();
        }

        //Кнопка назад
        private void button4_Click(object sender, EventArgs e){
            Form2 form2 = new Form2();
            form2.Show();
            this.Close();
        }

        //Кнопка выход на главную
        private void button5_Click(object sender, EventArgs e){
            Program.form9.Show();
            this.Close();
        }

        //Кнопка вперед
        private void button6_Click(object sender, EventArgs e){
            Form5 form5 = new Form5();
            form5.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e){
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
            SqlCommand command_select = new SqlCommand("Select * From Cars where Id_owner>@Id_owner", connection);
            command_select.Parameters.AddWithValue("@Id_owner", Convert.ToInt32(textBox5.Text));
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void Dobavit(object sender, MouseEventArgs e)
        {
            
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            //Вывод подсказки
            object _dt = null;
            comboBox1.DataSource = _dt;
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Ochko();

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            //Создаем рабочую книгу:
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            //Нам доступно редактирование некоторых параметров, в качестве примера изменим ширину столбцов:
            ExcelApp.Columns.ColumnWidth = 15;
            //Задать значение ячейки можно так:
            ExcelApp.Cells[1, 2] = "Автомобили";
            ExcelApp.Cells[2, 1] = "Id владельца";
            ExcelApp.Cells[2, 2] = "Гос. номер";
            ExcelApp.Cells[2, 3] = "Марка машины";

            ExcelApp.Cells[dataGridView1.Rows.Count + 3, 1] = "Отвественное лицо - Отвественное лицо – “Скопинцев Олег Данилович ";
            ExcelApp.Cells[dataGridView1.Rows.Count + 4, 1] = "Дата выдачи отчета - " + DateTime.Now;

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                {
                    ExcelApp.Cells[j + 3, i + 1] = (dataGridView1[i, j].Value).ToString();
                }
            }

            ExcelApp.Cells[1, 2].Font.Bold = true;
            ExcelApp.Cells[1, 2].Font.Size = 14;
            ExcelApp.Cells[8, 1].Font.Bold = true;
            ExcelApp.Cells[8, 1].Font.Size = 14;
            ExcelApp.Cells[9, 1].Font.Bold = true;
            ExcelApp.Cells[9, 1].Font.Size = 14;

            for (int i = 1; i <= 9; i++)
            {
                ExcelApp.Cells[2, i].Font.Bold = true;
                ExcelApp.Cells[2, i].Font.Size = 14;
            }

            ExcelApp.Columns.AutoFit();
            ExcelApp.Visible = true;


            DatabaseUpdate();
        }
    }
}
