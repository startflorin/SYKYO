using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Reflection;
using Object.Model;
using Object.Meta;

namespace GeneratedWPF
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ITableAdaptor cta = new MySqlTableAdaptor();
            DialogEraseTable dialog = new DialogEraseTable();
            //dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
            {
                Type[] tt = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "Object.Structure");
                foreach (Type t in tt)
                {
                    cta.DropTable(t.Name);
                }
            }

            CreateForm(CatList.GetInstance(), typeof(Cat));
            CreateForm(CatList.GetInstance(), typeof(Cat));
        }
        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }

        public static bool CreateForm(object list, Type observabletype)
        {
            CatModel f1 = new CatModel(list, observabletype);
            f1.Show();
            return true;
        }
    }
}
