using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace LabyDB
{
    public partial class PaymentsFormEdeting : Form
    {
        public PaymentsFormEdeting()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(Program.GetConnectionString());
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SampleDatabaseDataSet dataSet = new SampleDatabaseDataSet();
        // Обновление
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
        private void PaymentsFormEdeting_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sampleDatabaseDataSet1.Abonents". При необходимости она может быть перемещена или удалена.
            this.abonentsTableAdapter.Fill(this.sampleDatabaseDataSet1.Abonents);
            DatabaseUpdate();
        }

        private void button1_Click_1(object sender, EventArgs e)
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

        private void button3_Click_1(object sender, EventArgs e)
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

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Program.paymentsForm.Show();
            PaymentsForm form = new PaymentsForm();
            form.Show();
            this.Close();
        }
    }
}
