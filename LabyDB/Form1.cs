using System;
using System.Windows.Forms;

namespace LabyDB {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        //Кнопка клиенты
        private void Form2_open_button(object sender, EventArgs e) {
            Form11 form11 = new Form11();
            form11.Show();
            this.Hide();
        }
        //Кнопка автомобили
        private void button2_Click(object sender, EventArgs e){
            Form12 form12 = new Form12();
            form12.Show();
            this.Hide();
        }
        //Кнопка сервис
        private void button3_Click(object sender, EventArgs e){
            Form15 form15 = new Form15();
            form15.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e){
            ToolTip t = new ToolTip();
            t.SetToolTip(button1, "Переход во вкладку для добавления владельца машины.");
            t.SetToolTip(button2, "Переход во вкладку для добавления автомобиля.");
            t.SetToolTip(button3, "Переход во вкладку для подсчета итоговой суммы.");
            t.SetToolTip(button4, "Переход во вкладку для добавления услуги.");
            t.SetToolTip(button5, "Переход во вкладку для добавление запчасти.");
            t.SetToolTip(button6, "Выход из приложения.");
            t.SetToolTip(button7, "Переход во вкладку для добавления нового пользователя.");
            t.SetToolTip(button8, "Переход во вкладку настройки.");

            MessageBox.Show("Внимание! Функции добавления, редактирования и удаления недоступны через администратора. Войдите через обычного пользователя для применения этих возможностей.");
        }
        //Кнопка запчасти
        private void button5_Click(object sender, EventArgs e){
            Form14 form14 = new Form14();
            form14.Show();
            this.Hide();
        }
        //Кнопка услуги
        private void button4_Click(object sender, EventArgs e){
            Form13 form13 = new Form13();
            form13.Show();
            this.Hide();
        }
        //Кнопка выход
        private void button6_Click(object sender, EventArgs e){
            login form7 = new login();
            form7.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();
            form10.Show();
            this.Hide();
        }
    }
}
