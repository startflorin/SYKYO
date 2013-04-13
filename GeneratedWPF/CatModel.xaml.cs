using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Object.Model;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Data;

namespace GeneratedWPF
{
    public class FormatedBindCell : DataGridTextColumn
{
    public FormatedBindCell()
    {
        //this.Value
    }

    protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
    {
        TextBox textBox = (TextBox)base.GenerateEditingElement(cell, dataItem);

        Button button = new Button { Content = "..." };
        Grid.SetColumn(button, 1);

        return new Grid
        {
            ColumnDefinitions = {
                new ColumnDefinition(),
                new ColumnDefinition { Width = GridLength.Auto },
            },
            Children = {
                textBox,
                button,
            },
        };
    }
        ITableAdaptor tableAdaptor = null;
    // string stringValue = null;

   /* protected override object GetValue(int rowIndex)
    {
        if (string.IsNullOrEmpty(stringValue))
        {
            if (OwningRow.Index < 0)
            {
                return string.Empty;
            }

            tableAdaptor = (CatTableAdaptor)OwningColumn.Tag;

            List<DataRow> poinedRows = tableAdaptor.GetObjectByIDs(new List<decimal>() { (decimal)((DataRowView)OwningRow.DataBoundItem).Row[ColumnIndex + 1] });
            string result = string.Empty;

            foreach (DataRow dr in poinedRows)
            {
                result += ", " + dr["NAME"];
            }
            if (result.Length > 2)
            {
                stringValue = result.Substring(2);
            }
        }
        return stringValue;
    }
        */
    private List<DataRow> GetObjectByIDs(List<decimal> rowIDs)
    {
        return tableAdaptor.GetObjectByIDs(rowIDs);
    }
}


    /// <summary>
    /// Interaktionslogik für CatWindow.xaml
    /// </summary>
    public partial class CatModel : Window
    {

        object originalValues;

        CatList ml;
        Type modelType;
        public CatModel(object commonRecords, Type modelType)
        {
            this.modelType = modelType;
            InitializeComponent();
            GenerateDetailsPage();
            ml = (CatList)commonRecords;
            dataGrid1.ItemsSource = ml;
            dataGrid1.AutoGenerateColumns = false;
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tabControl1.SelectedIndex++;
            originalValues = CloneObject(dataGrid1.SelectedItem);
        }

        #region VIEW DETAILS COMPOSER

        private void GenerateDetailsPage()
        {
            DataTable dataTable = new DataTable();
            System.Reflection.MemberInfo[] memberi = ((Type)modelType).UnderlyingSystemType.GetMembers(); ;
            foreach (System.Reflection.MemberInfo member in memberi)
            {
                //if (member.MemberType == System.Reflection.MemberTypes.Field || member.MemberType == System.Reflection.MemberTypes.Property)
                if (member.MemberType == System.Reflection.MemberTypes.Property)
                {
                    System.Reflection.PropertyInfo propertyInfo = (System.Reflection.PropertyInfo)member;
                    if (member.ReflectedType.Name == member.DeclaringType.Name)
                    {
                        string name = member.Name;
                        if (name.Length > 29)
                        {
                            name = name.Substring(0, 29);
                        }

                        if (!new string[] { "String" }.Contains(propertyInfo.PropertyType.Name))
                        //if (!automaticColumns.Contains(member.Name.ToUpper()))
                        {
                            dataTable.Columns.Add(new DataColumn(name, typeof(decimal)));
                        }
                        else
                        {
                            dataTable.Columns.Add(new DataColumn(name, typeof(string)));
                        }
                    }
                }
            }

            //this.tabItemDetails.SuspendLayout();
            int position = 28;
            for (int i = 1; i < dataTable.Columns.Count; i++)
            {
                if (dataTable.Columns[i].DataType == typeof(decimal))
                {

                    ItemSelector dataWrapper = null;
                    //object objo = ((Type)modelType.recordItem).UnderlyingSystemType.GetProperty(dataTable.Columns[i].ColumnName);
                    //if (objo != null)
                    //{
                    //    dataWrapper = new DataWraper(commonRecords, ((PropertyInfo)objo).PropertyType);//.MemberType.GetType();

                    dataWrapper = new ItemSelector(HeadList.GetInstance(), typeof(Head));//((PropertyInfo)objo).PropertyType, bindingSource1, errorProvider1);//.MemberType.GetType();
                    //    dataGridViewColumn.Tag = new CatTableAdaptor(((PropertyInfo)objo).PropertyType);
                    //}
                    
                    Binding binding = new Binding();
                    //Binding RelativeSource={RelativeSource FindAncestor,AncestorType={x:Type Controls:MyUserControl}},Path=MainMenuImageSource
                    //binding.RelativeSource = new RelativeSource();
                    //binding.RelativeSource.Mode = RelativeSourceMode.FindAncestor;
                    //5binding.RelativeSource.Mode = RelativeSourceMode.FindAncestor;
                    
                    binding.ElementName = "dataGrid1";
                    binding.Path = new PropertyPath("SelectedItem.HeadP");
                    binding.Mode = BindingMode.TwoWay;
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    dataWrapper.CustomSetBinding(ItemSelector.ID, binding);
                    dataWrapper.comboBox1.SelectionChanged += new SelectionChangedEventHandler(comboBox1_SelectionChanged);

                    this.DetailsPanel.Children.Add(dataWrapper);

                    FormatedBindCell formatedBindCell = new FormatedBindCell();
                    formatedBindCell = new FormatedBindCell();
                    formatedBindCell.Binding = new Binding(dataTable.Columns[i].ColumnName+".Name");
                    dataGrid1.Columns.Add(formatedBindCell);

                }
                else
                {

                    
                    Button button1 = new Button();
                    Button button2 = new Button();
                    TextBox textBox1 = new TextBox();
                    Label label1 = new Label();


                    //label1.AutoSize = true;
                    //label1.Location = new System.Drawing.Point(6, position + 6);
                    label1.Name = dataTable.Columns[i].ColumnName + "Label";
                    //label1.Size = new System.Drawing.Size(46, 13);
                    //label1.TabIndex = 0;
                    label1.Content = dataTable.Columns[i].ColumnName; ;
                    // 
                    // textBox1
                    // 
                    //textBox1.Location = new System.Drawing.Point(80, position + 3);
                    textBox1.Name = dataTable.Columns[i].ColumnName + "TextBox";
                    //textBox1.Size = new System.Drawing.Size(111, 20);
                    textBox1.TabIndex = 1;

                    Binding binding = new Binding();
                    binding.ElementName = "dataGrid1";
                    binding.Path = new PropertyPath("SelectedItem." + dataTable.Columns[i].ColumnName);
                    binding.Mode = BindingMode.TwoWay;
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    textBox1.SetBinding(TextBox.TextProperty, binding);
                    //textBox1.SetBinding("Text", dataTable.Columns[i].ColumnName);
                    this.DetailsPanel.Children.Add(textBox1);
                    this.DetailsPanel.Children.Add(label1);

                    DataGridTextColumn dataGridTextColumn = new DataGridTextColumn();
                    dataGridTextColumn = new FormatedBindCell();
                    dataGridTextColumn.Binding = new Binding(dataTable.Columns[i].ColumnName);
                    dataGrid1.Columns.Add(dataGridTextColumn);



                }



                position += 28;
            }


            //this.tabItemDetails.ResumeLayout(false);
            //this.tabItemDetails.PerformLayout();
        }

        void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((Cat)dataGrid1.SelectedItem).HeadP =  (Head)((ComboBox)sender).SelectedItem;
            //throw new NotImplementedException();
        }

        #endregion VIEW DETAILS COMPOSER

        #region BUTTON CLICK

        private void actionButtonNew_Click(object sender, RoutedEventArgs e)
        {
            
            HeadList mlH = HeadList.GetInstance(); ;
            object newItemH1 = new Head();
            ((Head)newItemH1).Name = "1a";
            ((Head)newItemH1).ID = 1;
            mlH.Add(newItemH1);

            object newItemH2 = new Head();
            ((Head)newItemH2).Name = "2b";
            ((Head)newItemH2).ID = 2;
            mlH.Add(newItemH2);
            
            object newItem = GetNewObject(modelType);
            ((Cat)newItem).Name = "est";
            ((Cat)newItem).HeadP = (Head)newItemH2;
            ml.Add(newItem);
            dataGrid1.SelectedIndex = dataGrid1.Items.Count-2;
            SetNewModeOfForm();

        }

        private void actionButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SetInitialModeOfForm();
        }

        private void actionButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem == null)
            {
                return;
            }
            if (((Cat)dataGrid1.SelectedItem).ID > 0)
            {
                ml.Remove(dataGrid1.SelectedItem);
            }
            else
            {
                dataGrid1.SelectedItem = originalValues;
            }
            SetInitialModeOfForm();
        }

        private void actionButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem == null)
            {
                return;
            }
            ml.Remove(dataGrid1.SelectedItem);
        }

        private void actionButtonExit_Click(object sender, RoutedEventArgs e)
        {
        }

        private void actionButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem == null)
            {
                return;
            }
            object newItem = CloneObject(dataGrid1.SelectedItem);
            ((Cat)newItem).ID = -1;
            ml.Add(newItem);
            SetNewModeOfForm();
        }
 
        #endregion BUTTON CLICK

        #region INTERFACE STATES
        private void SetNoSelectionModeOfForm()
        {
            actionButtonNew.Visibility = System.Windows.Visibility.Visible;
            actionButtonCopy.Visibility = System.Windows.Visibility.Hidden;
            actionButtonCancel.Visibility = System.Windows.Visibility.Hidden;
            actionButtonSave.Visibility = System.Windows.Visibility.Hidden;
            actionButtonDelete.Visibility = System.Windows.Visibility.Hidden;
        }
        private void SetMultiselectModeOfForm()
        {
            actionButtonNew.Visibility = System.Windows.Visibility.Visible;
            actionButtonCopy.Visibility = System.Windows.Visibility.Hidden;
            actionButtonCancel.Visibility = System.Windows.Visibility.Hidden;
            actionButtonSave.Visibility = System.Windows.Visibility.Hidden;
            actionButtonDelete.Visibility = System.Windows.Visibility.Visible;
        }
        private void SetNewModeOfForm()
        {
            actionButtonNew.Visibility = System.Windows.Visibility.Hidden;
            actionButtonCopy.Visibility = System.Windows.Visibility.Hidden;
            actionButtonCancel.Visibility = System.Windows.Visibility.Visible;
            actionButtonSave.Visibility = System.Windows.Visibility.Visible;
            actionButtonDelete.Visibility = System.Windows.Visibility.Hidden;

            tabControl1.SelectedItem = tabItemDetails;
            tabItemOverview.IsEnabled = false;
            //tabControl1.Refresh();
        }
        private void SetInitialModeOfForm()
        {
            actionButtonNew.Visibility = System.Windows.Visibility.Visible;
            actionButtonCopy.Visibility = System.Windows.Visibility.Visible;
            actionButtonCancel.Visibility = System.Windows.Visibility.Hidden;
            actionButtonSave.Visibility = System.Windows.Visibility.Hidden;
            actionButtonDelete.Visibility = System.Windows.Visibility.Visible;

            tabItemOverview.IsEnabled = true;
            tabControl1.SelectedItem = tabItemOverview;
            //tabControl1.Refresh();
        }
        #endregion INTERFACE STATES

        public static object CloneObject(object obj)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter(null,
                     new StreamingContext(StreamingContextStates.Clone));
                binaryFormatter.Serialize(memStream, obj);
                memStream.Seek(0, SeekOrigin.Begin);
                return binaryFormatter.Deserialize(memStream);
            }
        }

        public static object GetNewObject(Type t)
        {
            return t.GetConstructor(new Type[] { }).Invoke(new object[] { });
        }

    }
}
