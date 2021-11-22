using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB
{
    public partial class Form4 : Form{
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
        }
        //Добавление записи
        void DataAdd(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            SqlCommand comand = new SqlCommand("Insert Into Services Values (@Id_services, @Name, @Cost)", connection);
            comand.Parameters.AddWithValue("@Id_services", Convert.ToInt64(textBox1.Text));
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
            SqlCommand command = new SqlCommand("Update Services set Name=@Name, Cost=@Cost Where Id_services = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
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
            SqlCommand command = new SqlCommand("Delete From Services where Id_services = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);

            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }
        //Кнопка выход на главную
        private void button5_Click(object sender, EventArgs e){
            Program.form1.Show();
            this.Close(); ;
        }
        //Кнопка назад
        private void button4_Click(object sender, EventArgs e){
            Form5 form5 = new Form5();
            form5.Show();
            this.Close();
        }
        //Кнопка вперед
        private void button6_Click(object sender, EventArgs e){
            Form6  form6= new Form6();
            form6.Show();
            this.Close();
        }
        //Кнопка удалить
        private void button7_Click(object sender, EventArgs e){
            DataDelete();
        }

        //Объявление переменной
        int name;
        int cost;

        private void Form4_Load(object sender, EventArgs e){
            DatabaseUpdate();

            //Услуги
            comboBox1.Items.Add("Авто-электрика");
            comboBox1.Items.Add("Аргонная сварка");
            comboBox1.Items.Add("Замена приводных ремней");
            comboBox1.Items.Add("Замена расходников");
            comboBox1.Items.Add("Замена технических жидкостей");
            comboBox1.Items.Add("Заправка и ремонт кондиционеров");
            comboBox1.Items.Add("Компьютерная диагностика");
            comboBox1.Items.Add("Кузовной ремон");
            comboBox1.Items.Add("Подготовка и продажа новых и б/у автомобилей");
            comboBox1.Items.Add("Развал схождения 3D");
            comboBox1.Items.Add("Регламентное ТО");
            comboBox1.Items.Add("Ремонт автоматических и механических коробок передач");
            comboBox1.Items.Add("Ремонт выхлопной системы");
            comboBox1.Items.Add("Ремонт двигателя");
            comboBox1.Items.Add("Ремонт подвески");
            comboBox1.Items.Add("Ремонт ходовой части, балансировка, развал-схождения");
            comboBox1.Items.Add("Ремонт электрики в машинах");
            comboBox1.Items.Add("Реставрация авто и запчастей");
            comboBox1.Items.Add("Тонировка");
            comboBox1.Items.Add("Установка и обслуживание газового оборудования в автомобилях");
            comboBox1.Items.Add("Другое");

            ToolTip t = new ToolTip();
            t.SetToolTip(button1, "Нажмите чтобы добавить новую запись.");
            t.SetToolTip(button3, "Нажмите чтобы изменить существующую запись.");
            t.SetToolTip(button7, "Нажмите чтобы удалить существующую запись.");
        }
        //Кнопка добавить
        private void button1_Click(object sender, EventArgs e){
            DataAdd();
        }
        //Кнопка изменить
        private void button3_Click(object sender, EventArgs e){
            DataChange();
        }
        //Кнопка обновить
        private void button8_Click(object sender, EventArgs e){
            DatabaseUpdate();
        }

        public Form4(){
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e){
            //Вывод подсказки
            object _dt = null;
            comboBox1.DataSource = _dt;
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;

            //Вывод номера и цены услуги
            if (comboBox1.SelectedIndex == 0){
                name = 1;
                cost = 500;
            }
            else if (comboBox1.SelectedIndex == 1){
                name = 2;
                cost = 1000;
            }
            else if (comboBox1.SelectedIndex == 2){
                name = 3;
                cost = 210;
            }
            else if (comboBox1.SelectedIndex == 3){
                name = 4;
                cost = 1200;
            }
            else if (comboBox1.SelectedIndex == 4){
                name = 5;
                cost = 500;
            }
            else if (comboBox1.SelectedIndex == 5){
                name = 6;
                cost = 4500;
            }
            else if (comboBox1.SelectedIndex == 6){
                name = 7;
                cost = 1000;
            }
            else if (comboBox1.SelectedIndex == 7){
                name = 8;
                cost = 500;
            }
            else if (comboBox1.SelectedIndex == 8){
                name = 9;
                cost = 2500;
            }
            else if (comboBox1.SelectedIndex == 9){
                name = 10;
                cost = 5000;
            }
            else if (comboBox1.SelectedIndex == 10){
                name = 11;
                cost = 5000;
            }
            else if (comboBox1.SelectedIndex == 11){
                name = 12;
                cost = 1000;
            }
            else if (comboBox1.SelectedIndex == 12)
            {
                name = 13;
                cost = 1500;
            }
            else if (comboBox1.SelectedIndex == 13)
            {
                name = 14;
                cost = 1600;
            }
            else if (comboBox1.SelectedIndex == 14)
            {
                name = 15;
                cost = 1300;
            }
            else if (comboBox1.SelectedIndex == 15)
            {
                name = 16;
                cost = 2000;
            }
            else if (comboBox1.SelectedIndex == 16)
            {
                name = 17;
                cost = 3000;
            }
            else if (comboBox1.SelectedIndex == 17)
            {
                name = 18;
                cost = 2500;
            }
            else if (comboBox1.SelectedIndex == 18)
            {
                name = 19;
                cost = 1900;
            }
            else if (comboBox1.SelectedIndex == 19)
            {
                name = 20;
                cost = 2100;
            }
            else if (comboBox1.SelectedIndex == 20)
            {
                name = 21;
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
            SqlCommand command_select = new SqlCommand("Select * From Services where Id_services>@Id_services", connection);
            command_select.Parameters.AddWithValue("@Id_services", Convert.ToInt32(textBox5.Text));
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
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

        private void comboBox1_Click(object sender, EventArgs e)
        {
            //Вывод подсказки
            object _dt = null;
            comboBox1.DataSource = _dt;
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(@"Q:\\otchet", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    xlApp.Cells[i + 3, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                    (xlWorkSheet.Cells[i + 3, j + 1] as Microsoft.Office.Interop.Excel.Range).Font.Bold = true;
                    (xlWorkSheet.Cells[i + 3, j + 1] as Microsoft.Office.Interop.Excel.Range).Font.Size = 13;
                    (xlWorkSheet.Cells[i + 3, j + 1] as Microsoft.Office.Interop.Excel.Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    (xlWorkSheet.Cells[dataGridView1.Rows.Count + 3, j + 1] as Microsoft.Office.Interop.Excel.Range).EntireColumn.AutoFit();
                }
            }
            xlApp.Cells[dataGridView1.Rows.Count + 3, 1] = "Отвественное лицо - Отвественное лицо – “Скопинцев Олег Данилович ";


            xlApp.Cells[dataGridView1.Rows.Count + 4, 1] = "Дата выдачи отчета - " + DateTime.Now;
            (xlWorkSheet.Cells[dataGridView1.Rows.Count + 3, 1] as Microsoft.Office.Interop.Excel.Range).Font.Bold = true;
            (xlWorkSheet.Cells[dataGridView1.Rows.Count + 3, 1] as Microsoft.Office.Interop.Excel.Range).Font.Size = 13;

            (xlWorkSheet.Cells[dataGridView1.Rows.Count + 3, 3] as Microsoft.Office.Interop.Excel.Range).Font.Bold = true;
            (xlWorkSheet.Cells[dataGridView1.Rows.Count + 3, 3] as Microsoft.Office.Interop.Excel.Range).Font.Size = 14;
            (xlWorkSheet.Cells[dataGridView1.Rows.Count + 3, 3] as Microsoft.Office.Interop.Excel.Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
            (xlWorkSheet.Cells[dataGridView1.Rows.Count + 3, 1] as Microsoft.Office.Interop.Excel.Range).EntireColumn.AutoFit();

            (xlWorkSheet.Cells[dataGridView1.Rows.Count + 4, 1] as Microsoft.Office.Interop.Excel.Range).Font.Bold = true;
            (xlWorkSheet.Cells[dataGridView1.Rows.Count + 4, 1] as Microsoft.Office.Interop.Excel.Range).Font.Size = 13;
            (xlWorkSheet.Cells[dataGridView1.Rows.Count + 4, 1] as Microsoft.Office.Interop.Excel.Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
            (xlWorkSheet.Cells[dataGridView1.Rows.Count + 4, 1] as Microsoft.Office.Interop.Excel.Range).EntireColumn.AutoFit();


            xlApp.Visible = true;
            xlApp.UserControl = true;
        }
    }
}
