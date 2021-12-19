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
        public static MainForm mainForm = new MainForm();
        public static AbonentsForm abonentsForm = new AbonentsForm();
        public static PaymentsForm paymentsForm = new PaymentsForm();

        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(mainForm);
        }

        public static string GetConnectionString() {
            return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\" + Environment.UserName + @"\Source\Repos\PavelHasaj\LabyDB\LabyDB\SampleDatabase.mdf;Integrated Security=True";
        }

        //Метод удаления пустых столбцов
        public static void DeleteEmptyColumns(DataGridView dataGridView1)
        {
            bool IsColumnEmpty;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                IsColumnEmpty = dataGridView1.Rows[0].Cells[i].Value == null || dataGridView1.Rows[0].Cells[i].Value.ToString() == "";
                if (IsColumnEmpty)
                {
                    dataGridView1.Columns.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
