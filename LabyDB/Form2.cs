using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace LabyDB {
    public partial class Form2 : Form {
        public Form2() {
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
        //Добавление записи
        void DataAdd(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand("Insert Into Owner Values (@Id_owner, @FIO, @Phone_number, @Driver_license_number)", connection);
            comand.Parameters.AddWithValue("@Id_owner", Convert.ToInt32(textBox1.Text));
            comand.Parameters.AddWithValue("@FIO", comboBox1.Text);
            comand.Parameters.AddWithValue("@Phone_number", textBox3.Text);
            comand.Parameters.AddWithValue("@Driver_license_number", textBox4.Text);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
            DatabaseUpdate();//вызов метода обновления dataGridView
        }
        //Изменение записи
        void DataChange(){
            SqlCommand command = new SqlCommand("Update Owner set FIO=@FIO, Phone_number=@Phone_number, Driver_license_number=@Driver_license_number Where Id_owner = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            command.Parameters.AddWithValue("@FIO", comboBox1.Text);
            command.Parameters.AddWithValue("@Phone_number", textBox3.Text);
            command.Parameters.AddWithValue("@Driver_license_number", textBox4.Text);
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
            DatabaseUpdate();
        }
        //Удаление записи
        void DataDelete(){
            SqlCommand command = new SqlCommand("Delete From Owner where Id_owner = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
            DatabaseUpdate();
        }

        private void Form1_Load(object sender, EventArgs e) {

            DatabaseUpdate();

            //Имена
            comboBox1.Items.Add("Август");
            comboBox1.Items.Add("Августин");
            comboBox1.Items.Add("Авраам");
            comboBox1.Items.Add("Аврора");
            comboBox1.Items.Add("Агата");
            comboBox1.Items.Add("Агафон");
            comboBox1.Items.Add("Агнесса");
            comboBox1.Items.Add("Агния");
            comboBox1.Items.Add("Ада");
            comboBox1.Items.Add("Аделаида");
            comboBox1.Items.Add("Аделина");
            comboBox1.Items.Add("Адонис");
            comboBox1.Items.Add("Акайо");
            comboBox1.Items.Add("Акулина");
            comboBox1.Items.Add("Алан");
            comboBox1.Items.Add("Алевтина");
            comboBox1.Items.Add("Александр");
            comboBox1.Items.Add("Александра");
            comboBox1.Items.Add("Алексей");
            comboBox1.Items.Add("Алена");
            comboBox1.Items.Add("Алина");
            comboBox1.Items.Add("Алиса");
            comboBox1.Items.Add("Алла");
            comboBox1.Items.Add("Алсу");
            comboBox1.Items.Add("Альберт");
            comboBox1.Items.Add("Альбина");
            comboBox1.Items.Add("Альфия");
            comboBox1.Items.Add("Альфред");
            comboBox1.Items.Add("Анастасия");
            comboBox1.Items.Add("Анатолий");
            comboBox1.Items.Add("Ангелина");
            comboBox1.Items.Add("Андрей");
            comboBox1.Items.Add("Анжела");
            comboBox1.Items.Add("Анжелика");
            comboBox1.Items.Add("Анна");
            comboBox1.Items.Add("Антон");
            comboBox1.Items.Add("Антонина");
            comboBox1.Items.Add("Арина");
            comboBox1.Items.Add("Аркадий");
            comboBox1.Items.Add("Арсен");
            comboBox1.Items.Add("Арсений");
            comboBox1.Items.Add("Артем");
            comboBox1.Items.Add("Артемий");
            comboBox1.Items.Add("Артур");
            comboBox1.Items.Add("Архип");
            comboBox1.Items.Add("Ася");
            comboBox1.Items.Add("Богдан");
            comboBox1.Items.Add("Борис");
            comboBox1.Items.Add("Борислав");
            comboBox1.Items.Add("Бронислав");
            comboBox1.Items.Add("Бронислава");
            comboBox1.Items.Add("Булат");
            comboBox1.Items.Add("Вадим");
            comboBox1.Items.Add("Валентин");
            comboBox1.Items.Add("Валентина");
            comboBox1.Items.Add("Валерий");
            comboBox1.Items.Add("Валерия");
            comboBox1.Items.Add("Ванда");
            comboBox1.Items.Add("Варвара");
            comboBox1.Items.Add("Василий");
            comboBox1.Items.Add("Василиса");
            comboBox1.Items.Add("Вера");
            comboBox1.Items.Add("Вероника");
            comboBox1.Items.Add("Виктор");
            comboBox1.Items.Add("Виктория");
            comboBox1.Items.Add("Виолетта");
            comboBox1.Items.Add("Виссарион");
            comboBox1.Items.Add("Вита");
            comboBox1.Items.Add("Виталий");
            comboBox1.Items.Add("Влад");
            comboBox1.Items.Add("Владимир");
            comboBox1.Items.Add("Владислав");
            comboBox1.Items.Add("Владислава");
            comboBox1.Items.Add("Вольдемар");
            comboBox1.Items.Add("Всеволод");
            comboBox1.Items.Add("Вячеслав");
            comboBox1.Items.Add("Гавриил");
            comboBox1.Items.Add("Галина");
            comboBox1.Items.Add("Геннадий");
            comboBox1.Items.Add("Георгий");
            comboBox1.Items.Add("Герман");
            comboBox1.Items.Add("Глеб");
            comboBox1.Items.Add("Глория");
            comboBox1.Items.Add("Даниил");
            comboBox1.Items.Add("Дарина");
            comboBox1.Items.Add("Дарья");
            comboBox1.Items.Add("Денис");
            comboBox1.Items.Add("Диана");
            comboBox1.Items.Add("Дина");
            comboBox1.Items.Add("Динара");
            comboBox1.Items.Add("Дмитрий");
            comboBox1.Items.Add("Дора");
            comboBox1.Items.Add("Ева");
            comboBox1.Items.Add("Евгений");
            comboBox1.Items.Add("Евгения");
            comboBox1.Items.Add("Егор");
            comboBox1.Items.Add("Екатерина");
            comboBox1.Items.Add("Елена");
            comboBox1.Items.Add("Елизавета");
            comboBox1.Items.Add("Елисей");
            comboBox1.Items.Add("Есения");
            comboBox1.Items.Add("Ефим");
            comboBox1.Items.Add("Ефрем");
            comboBox1.Items.Add("Ефросинья");
            comboBox1.Items.Add("Жанна");
            comboBox1.Items.Add("Зинаида");
            comboBox1.Items.Add("Зиновий");
            comboBox1.Items.Add("Злата");
            comboBox1.Items.Add("Зоя");
            comboBox1.Items.Add("Иван");
            comboBox1.Items.Add("Игнатий");
            comboBox1.Items.Add("Игорь,");
            comboBox1.Items.Add("Изабелла");
            comboBox1.Items.Add("Илья");
            comboBox1.Items.Add("Инна");
            comboBox1.Items.Add("Иосиф");
            comboBox1.Items.Add("Ирина");
            comboBox1.Items.Add("Карина");
            comboBox1.Items.Add("Каролина");
            comboBox1.Items.Add("Кирилл");
            comboBox1.Items.Add("Константин");
            comboBox1.Items.Add("Кристина");
            comboBox1.Items.Add("Ксения");
            comboBox1.Items.Add("Кузьма");
            comboBox1.Items.Add("Лев");
            comboBox1.Items.Add("Леонид");
            comboBox1.Items.Add("Лидия");
            comboBox1.Items.Add("Лилия");
            comboBox1.Items.Add("Любовь");
            comboBox1.Items.Add("Людмила");
            comboBox1.Items.Add("Макар");
            comboBox1.Items.Add("Максим");
            comboBox1.Items.Add("Маргарита");
            comboBox1.Items.Add("Марина");
            comboBox1.Items.Add("Мария");
            comboBox1.Items.Add("Марк");
            comboBox1.Items.Add("Матвей");
            comboBox1.Items.Add("Милана");
            comboBox1.Items.Add("ТМилена");
            comboBox1.Items.Add("Мирослава");
            comboBox1.Items.Add("Митрофан");
            comboBox1.Items.Add("Михаил");
            comboBox1.Items.Add("Надежда");
            comboBox1.Items.Add("Назар");
            comboBox1.Items.Add("Наталия");
            comboBox1.Items.Add("Наталья");
            comboBox1.Items.Add("Ника");
            comboBox1.Items.Add("Никита");
            comboBox1.Items.Add("Николай");
            comboBox1.Items.Add("Оксана");
            comboBox1.Items.Add("Олег");
            comboBox1.Items.Add("Олеся");
            comboBox1.Items.Add("Оливер");
            comboBox1.Items.Add("Оливия");
            comboBox1.Items.Add("Ольга");
            comboBox1.Items.Add("Павел");
            comboBox1.Items.Add("Петр");
            comboBox1.Items.Add("Платон");
            comboBox1.Items.Add("Полина");
            comboBox1.Items.Add("Прохор");
            comboBox1.Items.Add("Раиса");
            comboBox1.Items.Add("Регина");
            comboBox1.Items.Add("Ренат");
            comboBox1.Items.Add("Рената");
            comboBox1.Items.Add("Ринат");
            comboBox1.Items.Add("Роберт");
            comboBox1.Items.Add("Родион");
            comboBox1.Items.Add("Роза");
            comboBox1.Items.Add("Роман");
            comboBox1.Items.Add("Ростислав");
            comboBox1.Items.Add("Руслан");
            comboBox1.Items.Add("Рэн");
            comboBox1.Items.Add("Сабина");
            comboBox1.Items.Add("Савва");
            comboBox1.Items.Add("Савелий");
            comboBox1.Items.Add("Светлана");
            comboBox1.Items.Add("Святослав");
            comboBox1.Items.Add("Севастьян");
            comboBox1.Items.Add("Семен");
            comboBox1.Items.Add("Сергей");
            comboBox1.Items.Add("Снежана");
            comboBox1.Items.Add("София");
            comboBox1.Items.Add("Софья");
            comboBox1.Items.Add("Станислав");
            comboBox1.Items.Add("Тамара");
            comboBox1.Items.Add("Тарас");
            comboBox1.Items.Add("Татьяна");
            comboBox1.Items.Add("Теодор");
            comboBox1.Items.Add("Тимофей");
            comboBox1.Items.Add("Тимур");
            comboBox1.Items.Add("Тихон");
            comboBox1.Items.Add("Трофим");
            comboBox1.Items.Add("Ульяна");
            comboBox1.Items.Add("Урсула");
            comboBox1.Items.Add("Федор");
            comboBox1.Items.Add("Федот");
            comboBox1.Items.Add("Филат");
            comboBox1.Items.Add("Эдуард");
            comboBox1.Items.Add("Элеонора,");
            comboBox1.Items.Add("Элина");
            comboBox1.Items.Add("Эльвира");
            comboBox1.Items.Add("Эльдар");
            comboBox1.Items.Add("Юлий");
            comboBox1.Items.Add("Юлия");
            comboBox1.Items.Add("Юрий");
            comboBox1.Items.Add("Яков");
            comboBox1.Items.Add("Ян");
            comboBox1.Items.Add("Яна");
            comboBox1.Items.Add("Ярослав");

            ToolTip t = new ToolTip();
            t.SetToolTip(button1, "Нажмите чтобы добавить новую запись.");
            t.SetToolTip(button2, "Нажмите чтобы изменить существующую запись.");
            t.SetToolTip(button3, "Нажмите чтобы удалить существующую запись.");
        }
        //Кнопка добавить
        private void add_button(object sender, EventArgs e) {
            DataAdd();

            DatabaseUpdate();
        }
        //Кнопка изменить
        private void data_change_button(object sender, EventArgs e) {
            DataChange();
        }
        //Кнопка удалить
        private void data_delete_button(object sender, EventArgs e) {
            DataDelete();
        }
        //Кнопка вперед
        private void button6_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Close();
        }
        //Кнопка обновить
        private void button8_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
        }
        //Кнопка выход на главную
        private void button5_Click(object sender, EventArgs e)
        {
            Program.form1.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void button9_Click(object sender, EventArgs e)
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
