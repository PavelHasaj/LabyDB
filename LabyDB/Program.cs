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
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(form1);
        }
    }
}
