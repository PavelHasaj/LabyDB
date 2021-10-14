using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public static string ConnectionString { get; internal set; }

        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(form1);
        }
    }
}
