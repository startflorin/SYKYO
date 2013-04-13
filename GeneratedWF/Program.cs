using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using Object.Model;
using System.Reflection;

namespace GeneratedWF
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GeneratedWF.Console console = new Console();
            console.Show();

            //CatModel cats = new CatModel();
            ITableAdaptor cta = new MySqlTableAdaptor();

            if (DialogResult.Yes == MessageBox.Show("DropTables ?", "?", MessageBoxButtons.YesNo))
            {
                Type[] tt = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "WindowsFormsApplication1.Model");
                foreach (Type t in tt)
                {
                    cta.DropTable(t.Name);
                }
            }

            CreateForm(typeof(Cat));
            Application.Run();
        }

        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }

        public static bool CreateForm(Type tableName)
        {
            Form1 f1 = new Form1(tableName);
            f1.Show();
            return true;
        }
    }
}
