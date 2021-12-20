using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB {
    public partial class AbonentsForm : Form {
        public AbonentsForm() {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(Program.GetConnectionString());
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SampleDatabaseDataSet dataSet = new SampleDatabaseDataSet();

        private void DatabaseUpdate() {
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("Select * From Abonents", connection);
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        void DataAdd() {
            //добавление записи
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            SqlCommand comand = new SqlCommand("Insert Into Abonents Values (@Nomer_licevogo_cheta, @FIO, @Adres)", connection);
            comand.Parameters.AddWithValue("@Nomer_licevogo_cheta", textBox1.Text);
            comand.Parameters.AddWithValue("@FIO", textBox2.Text);
            comand.Parameters.AddWithValue("@Adres", textBox3.Text);

            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();//вызов метода обновления dataGridView
        }

        void DataChange() {
            SqlCommand command = new SqlCommand("Update Abonents set FullName=@FullName, Adress=@Adress Where AbonentID = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            command.Parameters.AddWithValue("@FullName", textBox2.Text);
            command.Parameters.AddWithValue("@Adress", textBox3.Text);

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }

        void DataDelete() {
            SqlCommand command = new SqlCommand("Delete From Abonents where AbonentID = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);

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
        }

        private void add_button(object sender, EventArgs e) {
            DataAdd();
        }

        private void data_change_button(object sender, EventArgs e) {
            DataChange();
        }

        private void data_delete_button(object sender, EventArgs e) {
            DialogResult result = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DataDelete();
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PaymentsForm form = new PaymentsForm();
            form.Show();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.mainForm.Show();
            this.Hide();
        }

        private void textBox_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox4.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DatabaseUpdate();
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("Select * From Abonents where AbonentID>@AbonentID", connection);
            command_select.Parameters.AddWithValue("@AbonentID", Convert.ToInt32(textBox4.Text));
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
            ("SELECT * From Abonents", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            Program.DeleteEmptyColumns(dataGridView1);
            dataGridView1.Columns[0].HeaderText = "ID абонента";
            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Адресс";
        }
        private void button9_Click(object sender, EventArgs e)
        {
            Ochko();

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            //Создаем рабочую книгу:
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            //Нам доступно редактирование некоторых параметров, в качестве примера изменим ширину столбцов:
            ExcelApp.Columns.ColumnWidth = 15;
            //Задать значение ячейки можно так:

            ExcelApp.Cells[1, 2] = "Итоговый отчет по абонентам";
            ExcelApp.Cells[2, 1] = "ID абонента";
            ExcelApp.Cells[2, 2] = "ФИО";
            ExcelApp.Cells[2, 3] = "Адресс";

            ExcelApp.Cells[dataGridView1.Rows.Count + 3, 1] = "Отвественное лицо - Отвественное лицо – “Павел Хасай";
            ExcelApp.Cells[dataGridView1.Rows.Count + 4, 1] = "Дата выдачи отчета - " + DateTime.Now;

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                {
                    ExcelApp.Cells[j + 3, i + 1] = (dataGridView1[i, j].Value).ToString();
                }
            }


            ExcelApp.Cells[1, 2].Font.Bold = true;
            ExcelApp.Cells[1, 2].Font.Size = 13;

            for (int i = 1; i <= 9; i++)
            {
                ExcelApp.Cells[2, i].Font.Bold = true;
                ExcelApp.Cells[2, i].Font.Size = 12;
            }

            ExcelApp.Columns.AutoFit();
            ExcelApp.Visible = true;

            DatabaseUpdate();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            SqlCommand comand = new SqlCommand("Select * FROM Abonents ORDER BY FullName", connection);
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

            SqlCommand comand = new SqlCommand("Select * FROM Abonents ORDER BY AbonentID", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }
    }
}
