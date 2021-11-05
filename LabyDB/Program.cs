using System;
using System.Windows.Forms;

namespace LabyDB {
    static class Program {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        public static Form1 form1 = new Form1();
        public static Form2 form2 = new Form2();
        public static Form3 form3 = new Form3();
        public static Form4 form4 = new Form4();
        public static Form5 form5 = new Form5();
        public static Form6 form6 = new Form6();
        public static login form7 = new login();

        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new login());
        }

        public static string GetConnectionString() {
            return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\" + Environment.UserName + @"\source\repos\Auto_repair_shop\LabyDB\LabyDB\SampleDatabase.mdf;Integrated Security=True";
        }
    }
}
