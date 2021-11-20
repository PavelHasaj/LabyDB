﻿using System;
using System.Windows.Forms;

namespace LabyDB {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        //Кнопка клиенты
        private void Form2_open_button(object sender, EventArgs e) {
            Program.form2.Show();
            this.Hide();
        }
        //Кнопка автомобили
        private void button2_Click(object sender, EventArgs e){
            Program.form3.Show();
            this.Hide();
        }
        //Кнопка сервис
        private void button3_Click(object sender, EventArgs e){
            Program.form6.Show();
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
        }
        //Кнопка запчасти
        private void button5_Click(object sender, EventArgs e){
            Program.form5.Show();
            this.Hide();
        }
        //Кнопка услуги
        private void button4_Click(object sender, EventArgs e){
            Program.form4.Show();
            this.Hide();
        }
        //Кнопка выход
        private void button6_Click(object sender, EventArgs e){
            Program.form7.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Program.form8.Show();
            this.Hide();
        }
    }
}
