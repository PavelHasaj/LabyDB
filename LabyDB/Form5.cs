using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace LabyDB
{
    public partial class Form5 : Form{
        //Строка подключения к БД
        SqlConnection connection = new SqlConnection(Program.GetConnectionString());
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SampleDatabaseDataSet dataSet = new SampleDatabaseDataSet();
        //Обновление
        private void DatabaseUpdate(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand("Select * From Spare_parts ORDER BY Id_spare_parts ASC", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            dataGridView1.Columns[0].HeaderText = "Id запчасти";
            dataGridView1.Columns[1].HeaderText = "Наименование";
            dataGridView1.Columns[2].HeaderText = "Цена";

            Program.DeleteEmptyColumns(dataGridView1);
        }
        //Добавление записи
        void DataAdd(){
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            SqlCommand comand = new SqlCommand("Insert Into Spare_parts Values (@Id_spare_parts, @Name, @Cost)", connection);
            comand.Parameters.AddWithValue("@Id_spare_parts", Convert.ToInt32(textBox1.Text));
            comand.Parameters.AddWithValue("@Name", comboBox1.Text);
            comand.Parameters.AddWithValue("@Cost", Convert.ToInt32(textBox3.Text));

            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();//вызов метода обновления dataGridView
        }
        //Изменение записи
        void DataChange(){
            SqlCommand command = new SqlCommand("Update Spare_parts set Name=@Name, Cost=@Cost Where Id_spare_parts = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);
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
            SqlCommand command = new SqlCommand("Delete From Spare_parts where Id_spare_parts = " + dataGridView1[0, dataGridView1.CurrentRow.Index].Value, connection);

            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            DatabaseUpdate();
        }

        //Объявление переменной
        int name;
        int cost;

        public Form5(){
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e){
            DatabaseUpdate();

            //Запчасти
            comboBox1.Items.Add("Ароматизатор Елочка Аромат ванили Car-Freshner U1P-10105-RUSS");
            comboBox1.Items.Add("Ароматизатор Елочка Бабл гам Car-Freshner U1P-10348-RUSS");
            comboBox1.Items.Add("Ароматизатор Елочка Новая машина Car-Freshner U1P-10189-RUSS");
            comboBox1.Items.Add("Ароматизатор Елочка Пина колада Car-Freshner U1P-10967-RUSS");
            comboBox1.Items.Add("Ароматизатор Елочка Черный лед США 10155");
            comboBox1.Items.Add("Ароматизатор меловой SPIRIT REFILL самурай EIKOSHA");
            comboBox1.Items.Add("Ароматизатор меловой SPIRIT REFILL свежесть EIKOSHA");
            comboBox1.Items.Add("Ароматизатор подвесной, французский парфюм №6 Sexy aromatique");
            comboBox1.Items.Add("Ароматизатор подвесной, французский парфюм №17 Egoïste platine");
            comboBox1.Items.Add("Ароматизатор подвесной, французский парфюм №18 Esprit de légende");
            comboBox1.Items.Add("Базовый комплект ароматизации воздуха в салоне BMW Natural Air");
            comboBox1.Items.Add("Герметик REINZOSIL 70мл универсальный");
            comboBox1.Items.Add("Долговременная дизельная присадка");
            comboBox1.Items.Add("Коврики в салон AUTOPROFI синие");
            comboBox1.Items.Add("Масло моторное Mobil Super 3000 X1, 4л");
            comboBox1.Items.Add("Масляный фильтр Mann-Filter");
            comboBox1.Items.Add("Мастер-смазка «ВАЛЕРА», 400мл");
            comboBox1.Items.Add("Многофункциональная очищающая присадка в бензин СУПРОТЕК А-Прохим SGA (СГА), 2х50 мл");
            comboBox1.Items.Add("Оплетка SKYWAY Corset M Черная экокожа");
            comboBox1.Items.Add("Оплетка на рулевое колесо AVS GL-165M-B Size M натуральная кожа 37-39 см. черный");
            comboBox1.Items.Add("Оплетка на рулевое колесо M 38см натуральная кожа черная A0501005");
            comboBox1.Items.Add("Оплетка на рулевое колесо M 38см натуральная кожа черная A0501016");
            comboBox1.Items.Add("Очиститель инжекторов В бензин на 40-60 л");
            comboBox1.Items.Add("Очиститель инжекторов быстрого действия на 60 л");
            comboBox1.Items.Add("Очиститель форсунок и деталей топливной системы Injection System Purge на 1л");
            comboBox1.Items.Add("Присадка в топливо");
            comboBox1.Items.Add("Промывка инжекторной системы бензинового двигателя ML101 Euro на 1 л");
            comboBox1.Items.Add("Промывка инжекторной системы бензинового двигателя ML101 на 1 л");
            comboBox1.Items.Add("Промывочное масло Лукойл на 4л");
            comboBox1.Items.Add("Свеча зажигания Denso 3120");
            comboBox1.Items.Add("Свеча накаливания");
            comboBox1.Items.Add("Смазка ШРУС-триподный, 90мл стик-пакет");
            comboBox1.Items.Add("Смазка для направляющих суппорта МС 1630, 5г стик-пакет");
            comboBox1.Items.Add("Смазка медная, 520мл");
            comboBox1.Items.Add("Смазка пластичная для направляющих, 25гр");
            comboBox1.Items.Add("Сменный картридж BMW Natural Air с ароматом Sparkling Raindrops");
            comboBox1.Items.Add("Средство универсальное WD-40® для тысячи применений на работе и в быту 100мл");
            comboBox1.Items.Add("Средство универсальное WD-40® для тысячи применений на работе и в быту 200мл");
            comboBox1.Items.Add("Фильтр воздушный");
            comboBox1.Items.Add("Фильтр масляный");
            comboBox1.Items.Add("Фильтр салона, пылевой");
            comboBox1.Items.Add("Фильтр салона, угольный");
            comboBox1.Items.Add("Фильтр топливный");
            comboBox1.Items.Add("Щётка стеклоочистителя, задняя");
            comboBox1.Items.Add("Щётки стеклоочистителя, комплект");
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
        //Кнопка удалить
        private void button7_Click(object sender, EventArgs e){
            DataDelete();
        }
        //Кнопка обновить
        private void button8_Click(object sender, EventArgs e){
            DatabaseUpdate();
        }
        //Кнопка выход на главную
        private void button5_Click(object sender, EventArgs e){
            Program.form9.Show();
            this.Close();
        }
        //Кнопка назад
        private void button4_Click(object sender, EventArgs e){
            Form3 form3 = new Form3();
            form3.Show();
            this.Close();
        }
        //Кнопка вперед
        private void button6_Click(object sender, EventArgs e){
            Form4 form4 = new Form4();
            form4.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e){
            //Вывод подсказки
            object _dt = null;
            comboBox1.DataSource = _dt;
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;

            //Вывод номера и цены запчасти
            if (comboBox1.SelectedIndex == 0){
                name = 1;
                cost = 82;
            }
            else if (comboBox1.SelectedIndex == 1){
                name = 2;
                cost = 54;
            }
            else if (comboBox1.SelectedIndex == 2){
                name = 3;
                cost = 111;
            }
            else if (comboBox1.SelectedIndex == 3){
                name = 4;
                cost = 150;
            }
            else if (comboBox1.SelectedIndex == 4){
                name = 5;
                cost = 96;
            }
            else if (comboBox1.SelectedIndex == 5){
                name = 6;
                cost = 58;
            }
            else if (comboBox1.SelectedIndex == 6){
                name = 7;
                cost = 90;
            }
            else if (comboBox1.SelectedIndex == 7){
                name = 8;
                cost = 68;
            }
            else if (comboBox1.SelectedIndex == 8)
            {
                name = 9;
                cost = 94;
            }
            else if (comboBox1.SelectedIndex == 9)
            {
                name = 10;
                cost = 60;
            }
            else if (comboBox1.SelectedIndex == 10)
            {
                name = 11;
                cost = 576;
            }
            else if (comboBox1.SelectedIndex == 11)
            {
                name = 12;
                cost = 731;
            }
            else if (comboBox1.SelectedIndex == 12)
            {
                name = 13;
                cost = 494;
            }
            else if (comboBox1.SelectedIndex == 13)
            {
                name = 14;
                cost = 173;
            }
            else if (comboBox1.SelectedIndex == 14)
            {
                name = 15;
                cost = 498;
            }
            else if (comboBox1.SelectedIndex == 15)
            {
                name = 16;
                cost = 414;
            }
            else if (comboBox1.SelectedIndex == 16)
            {
                name = 17;
                cost = 534;
            }
            else if (comboBox1.SelectedIndex == 17)
            {
                name = 18;
                cost = 453;
            }
            else if (comboBox1.SelectedIndex == 18)
            {
                name = 19;
                cost = 865;
            }
            else if (comboBox1.SelectedIndex == 19)
            {
                name = 20;
                cost = 364;
            }
            else if (comboBox1.SelectedIndex == 20)
            {
                name = 21;
                cost = 964;
            }
            else if (comboBox1.SelectedIndex == 21)
            {
                name = 22;
                cost = 457;
            }
            else if (comboBox1.SelectedIndex == 22)
            {
                name = 23;
                cost = 257;
            }
            else if (comboBox1.SelectedIndex == 23)
            {
                name = 24;
                cost = 653;
            }
            else if (comboBox1.SelectedIndex == 24)
            {
                name = 25;
                cost = 342;
            }
            else if (comboBox1.SelectedIndex == 25)
            {
                name = 26;
                cost = 2345;
            }
            else if (comboBox1.SelectedIndex == 26)
            {
                name = 27;
                cost = 753;
            }
            else if (comboBox1.SelectedIndex == 27)
            {
                name = 28;
                cost = 2532;
            }
            else if (comboBox1.SelectedIndex == 28)
            {
                name = 29;
                cost = 643;
            }
            else if (comboBox1.SelectedIndex == 29)
            {
                name = 30;
                cost = 343;
            }
            else if (comboBox1.SelectedIndex == 30)
            {
                name = 31;
                cost = 121;
            }
            else if (comboBox1.SelectedIndex == 31)
            {
                name = 32;
                cost = 432;
            }
            else if (comboBox1.SelectedIndex == 32)
            {
                name = 33;
                cost = 870;
            }
            else if (comboBox1.SelectedIndex == 33)
            {
                name = 34;
                cost = 2567;
            }
            else if (comboBox1.SelectedIndex == 34)
            {
                name = 35;
                cost = 2893;
            }
            else if (comboBox1.SelectedIndex == 35)
            {
                name = 36;
                cost = 2086;
            }
            else if (comboBox1.SelectedIndex == 36)
            {
                name = 37;
                cost = 2589;
            }
            else if (comboBox1.SelectedIndex == 37)
            {
                name = 38;
                cost = 354;
            }
            else if (comboBox1.SelectedIndex == 38)
            {
                name = 39;
                cost = 876;
            }
            else if (comboBox1.SelectedIndex == 39)
            {
                name = 40;
                cost = 577;
            }
            else if (comboBox1.SelectedIndex == 40)
            {
                name = 41;
                cost = 468;
            }
            else if (comboBox1.SelectedIndex == 41)
            {
                name = 42;
                cost = 853;
            }
            else if (comboBox1.SelectedIndex == 42)
            {
                name = 43;
                cost = 572;
            }
            else if (comboBox1.SelectedIndex == 43)
            {
                name = 44;
                cost = 950;
            }
            else if (comboBox1.SelectedIndex == 44)
            {
                name = 45;
                cost = 876;
            }
            else if (comboBox1.SelectedIndex == 45)
            {
                name = 46;
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
            SqlCommand command_select = new SqlCommand("Select * From Spare_parts where Id_spare_parts>@Id_spare_parts", connection);
            command_select.Parameters.AddWithValue("@Id_spare_parts", Convert.ToInt32(textBox5.Text));
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
            SqlCommand command_select = new SqlCommand("Select * From Spare_parts where Cost>@Cost", connection);
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

       
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {            
            DatabaseUpdate();
            dataGridView1.DataSource = null;
            dataSet.Clear();
            connection.Open();
            SqlCommand comand = new SqlCommand("SELECT Spare_parts.Id_spare_parts, Spare_parts.Name, COUNT (Service.Id) AS count FROM Service, Spare_parts WHERE Spare_parts.Id_spare_parts = Service.Id_spare_parts GROUP BY Spare_parts.Id_spare_parts, Spare_parts.Name ORDER BY count DESC", connection);
            dataAdapter.SelectCommand = comand;
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();

            dataGridView1.Columns[0].HeaderText = "Id запчасти";
            dataGridView1.Columns[1].HeaderText = "Наименование";
            dataGridView1.Columns[3].HeaderText = "Кол-во заказов";

            Program.DeleteEmptyColumns(dataGridView1);
        }
    }
}
