using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Object.Model;

namespace GeneratedWPF
{
    /// <summary>
    /// Interaktionslogik für ItemSelector.xaml
    /// </summary>
    public partial class ItemSelector : UserControl
    {
        Head selected = new Head();
        //public readonly static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ItemSelector), new PropertyMetadata(OnTextChanged));
        public readonly static DependencyProperty ID = DependencyProperty.Register("SelectedHeadID", typeof(decimal), typeof(ItemSelector), new PropertyMetadata(OnTextChanged));
 
        static void OnTextChanged(DependencyObject obj,
        DependencyPropertyChangedEventArgs args)
        {
            if (TextChanged != null)
            TextChanged(obj);
        }
 
        public delegate void TextChangeHandler(object sender);
        public static TextChangeHandler TextChanged;
 
        public Head SelectedId
        {
            get {
                return (Head)this.GetValue(ID);
            }
            set {
                this.SetValue(ID, value);
            }
        }

        HeadList ml = new HeadList();
        

        public ItemSelector(object commonRecords, Type modelType)
        {
            InitializeComponent();
            ml = (HeadList)commonRecords;
            TextChanged += new TextChangeHandler((object sender) =>
            {
                //Prevent forcing changes to other instances of the user control
                if (this == ((ItemSelector)sender))
                {
                   // this.textBox1.Text = string.Empty + this.SelectedId.ID;
                   // this.textBox2.Text = string.Empty + this.SelectedId.ID;
                   // this.comboBox1.SelectedItem = this.SelectedId;
                }
            });

            this.textBox1.TextChanged +=new TextChangedEventHandler(textBox1_TextChanged); 
            //new EditValueChangedEventHandler((object sender, EditValueChangedEventArgs e) =>
            //{
            //    this.Text = textBox1.Text;
            //});
            Binding binding = new Binding();
            //Binding RelativeSource={RelativeSource FindAncestor,AncestorType={x:Type Controls:MyUserControl}},Path=MainMenuImageSource
            //binding.RelativeSource = new RelativeSource();
            //binding.RelativeSource.Mode = RelativeSourceMode.FindAncestor;
            //binding.RelativeSource.AncestorType = new 
            //binding.RelativeSource.Mode = RelativeSourceMode.FindAncestor;
            /*binding.ElementName = "dataGrid1";
            binding.Path = new PropertyPath("SelectedItem.Name");
            binding.Mode = BindingMode.TwoWay;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            textBox1.SetBinding(TextBox.TextProperty, binding);
             * */
           // comboBox1.SelectedValuePath = "Head.Name";
            //comboBox1.SelectedItem = "Head.Name";
            //comboBox1.Name="combo";Binding Path=SelectedItem.Company,
            //ElementName=lstPersons, Mode=Default

            comboBox1.ItemsSource = ml;
            comboBox1.DisplayMemberPath="Name";
            comboBox1.SelectedValuePath="ID";
            Binding binding2 = new Binding("ID");
            ///binding.ElementName = "textBox1";
            binding2.Mode = BindingMode.TwoWay;
            comboBox1.SelectedValue = binding2;

            Binding binding3 = new Binding();
            //comboBox1.SelectedValue = binding3;
            BindingExpression be = textBox1.GetBindingExpression(TextBox.TextProperty);

        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal id = -1;
            decimal.TryParse(textBox1.Text.Trim(), out id);
            this.SelectedId.ID = id;
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            TabItem ti = (TabItem)((System.Windows.Controls.TabControl)((System.Windows.Controls.TabItem)((System.Windows.Controls.StackPanel)this.Parent).Parent).Parent).Items[0];
        }

        public BindingExpressionBase CustomSetBinding(DependencyProperty dp, BindingBase bindingg)
        {
            //Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type local:View}}, Path=Name
            Binding binding = new Binding();
            //RelativeSource rs = new RelativeSource( RelativeSourceMode.FindAncestor, typeof(Window), 2);
            //binding.RelativeSource = rs;
            //binding.ElementName
            binding.Path = new PropertyPath("Name");
            binding.Mode = BindingMode.TwoWay;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            textBox1.SetBinding(TextBox.TextProperty, binding);
            
            Binding binding2 = new Binding();
            //binding2.ElementName = "comboBox1";
            binding2.Path = new PropertyPath("SelectedHeadID");
            //binding2.ElementName = "self";
            //binding2.Path = new PropertyPath("SelectedId");
            binding2.Mode = BindingMode.TwoWay;
            binding2.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            textBox2.SetBinding(TextBox.TextProperty, binding2);
            return null;
            //comboBox1.sel
        }
    }
}
