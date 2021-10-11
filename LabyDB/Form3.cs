using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LabyDB
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Program.form2.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.form1.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Program.form1.Show();
            this.Hide();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\feedf\source\repos\LabyDB\LabyDB\SampleDatabase.mdf;Integrated Security=True");
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SampleDatabaseDataSet dataSet = new SampleDatabaseDataSet();

        private void DatabaseUpdate()
        {
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand command_select = new SqlCommand("Select * From Abonents", connection);
            dataAdapter.SelectCommand = command_select;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            DatabaseUpdate();
        }

    }
}
