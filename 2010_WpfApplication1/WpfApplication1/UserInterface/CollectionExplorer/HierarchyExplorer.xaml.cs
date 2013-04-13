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
using System.Windows.Shapes;
using DataModel.DL.CodeEntity;
using WpfApplication1.Controller.DocumentManager;
using System.Windows.Markup;
using System.Globalization;

namespace WpfApplication1.UserInterface.CollectionExplorer
{
    /// <summary>
    /// Interaktionslogik für HierarchyExplorer.xaml
    /// </summary>
    public partial class HierarchyExplorer : Window
    {
        public HierarchyExplorer()
        {
            InitializeComponent();
            CleanGrid();
            InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            currentColumnOffset = 0;
            currentRowOffset = 0;
            //dataGrid1.ItemBindingGroup.
            UpdateDataGrid();
        }

        public void UpdateDataSource(EDocument document)
        {
            this.document = document;
            SToken token = document.RootTocken;
            documentVerticalOffset = 0;
            documentHorizontalOffset = 0;
            ResetGridContent();
            Fill(document.RootTocken);
            dataGrid1.Items.Refresh();
        }

        public void Fill(SToken token)
        {
            while (token != null)
            {
                int column = documentHorizontalOffset + currentColumnOffset;
                int row = documentVerticalOffset + currentRowOffset;
                if (column < 10 && column > -1 && row < 10 && row > -1)
                {
                    switch (column)
                    {
                        case 0:
                            ((Item)dataGrid1.Items[row]).C0 = token.TokenString;
                            //DataGridCellInfo cell = new DataGridCellInfo(((Item)dataGrid1.Items[documentVerticalOffset]).C0);
                            //dataGrid1. new SolidColorBrush(Colors.White);
                            //((DataGridRow)dataGrid1.Items[documentVerticalOffset]).BorderBrus
                            break;
                        case 1:
                            ((Item)dataGrid1.Items[row]).C1 = token.TokenString;
                            break;
                        case 2:
                            ((Item)dataGrid1.Items[row]).C2 = token.TokenString;
                            break;
                        case 3:
                            ((Item)dataGrid1.Items[row]).C3 = token.TokenString;
                            break;
                        case 4:
                            ((Item)dataGrid1.Items[row]).C4 = token.TokenString;
                            break;
                        case 5:
                            ((Item)dataGrid1.Items[row]).C5 = token.TokenString;
                            break;
                        case 6:
                            ((Item)dataGrid1.Items[row]).C6 = token.TokenString;
                            break;
                        case 7:
                            ((Item)dataGrid1.Items[row]).C7 = token.TokenString;
                            break;
                        case 8:
                            ((Item)dataGrid1.Items[row]).C8 = token.TokenString;
                            break;
                        case 9:
                            ((Item)dataGrid1.Items[row]).C9 = token.TokenString;
                            break;
                    }
                }
                documentHorizontalOffset++;

                if (token.TokenProperties != null)
                    foreach (SToken tokenlist in token.TokenProperties)
                    {
                        documentVerticalOffset++;
                        Fill(tokenlist);
                        documentVerticalOffset--;
                    }
                foreach (SToken tokenlist in token.TokenChilds)
                {
                    documentVerticalOffset++;
                    Fill(tokenlist);
                    documentVerticalOffset--;
                }
                if (token.TokenAfter != null)
                {
                    token = token.TokenAfter[0];
                }
                else
                {
                    token = null;
                }
            }
        }

        public void UpdateDataGrid()
        {
            // Update headers
            for (int i = 1; i < dataGrid1.Columns.Count; i++)
            {
                dataGrid1.Columns[i].Header = "C" + (currentColumnOffset + i);
            }
            for (int i = 0; i < dataGrid1.Items.Count; i++)
            {
                ((Item)dataGrid1.Items[i]).Num = "R" + (currentRowOffset + i);
            }
            dataGrid1.Items.Refresh();
        }


        private void MoveContent(int incrementRow, int incrementColumn)
        {
            currentColumnOffset += incrementColumn;
            currentRowOffset += incrementRow;
            documentVerticalOffset = 0;
            documentHorizontalOffset = 0;
            ResetGridContent();
            UpdateDataGrid();
            Fill(document.RootTocken);
        }

        private void ResetGridContent()
        {
            dataGrid1.Items.Clear();
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
        }


        private void CleanGrid()
        {
            dataGrid1.Columns.Clear();
            DataGridTextColumn cH = new DataGridTextColumn();
            cH.Header = "";
            cH.Binding = new Binding("Num");
            cH.Width = 110;
            dataGrid1.Columns.Add(cH);
            DataGridTextColumn c0 = new DataGridTextColumn();
            c0.Header = "c0";
            c0.Width = 110;
            c0.Binding = new Binding("C0");
            dataGrid1.Columns.Add(c0);
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "C1";
            c1.Width = 110;
            c1.Binding = new Binding("C1");
            dataGrid1.Columns.Add(c1);
            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "C2";
            c2.Width = 110;
            c2.Binding = new Binding("C2");
            dataGrid1.Columns.Add(c2);
            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "C3";
            c3.Width = 110;
            c3.Binding = new Binding("C3");
            dataGrid1.Columns.Add(c3);
            DataGridTextColumn c4 = new DataGridTextColumn();
            c4.Header = "C4";
            c4.Width = 110;
            c4.Binding = new Binding("C4");
            dataGrid1.Columns.Add(c4);
            DataGridTextColumn c5 = new DataGridTextColumn();
            c5.Header = "C5";
            c5.Width = 110;
            c5.Binding = new Binding("C5");
            dataGrid1.Columns.Add(c5);
            DataGridTextColumn c6 = new DataGridTextColumn();
            c6.Header = "C6";
            c6.Width = 110;
            c6.Binding = new Binding("C6");
            dataGrid1.Columns.Add(c6);
            DataGridTextColumn c7 = new DataGridTextColumn();
            c7.Header = "C7";
            c7.Width = 110;
            c7.Binding = new Binding("C7");
            dataGrid1.Columns.Add(c7);
            DataGridTextColumn c8 = new DataGridTextColumn();
            c8.Header = "C8";
            c8.Width = 110;
            c8.Binding = new Binding("C8");
            dataGrid1.Columns.Add(c8);
            DataGridTextColumn c9 = new DataGridTextColumn();
            c9.Header = "C9";
            c9.Width = 110;
            c9.Binding = new Binding("C9");
            dataGrid1.Columns.Add(c9);
            //Item currentItem = new Item();
            //currentItem.
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
            dataGrid1.Items.Add(new Item() { Num = "R1", C0 = "", C1 = "", C2 = "", C3 = "", C4 = "", C5 = "", C6 = "", C7 = "", C8 = "", C9 = "" });
        
        }

        public class Item
        {
            public string Num { get; set; }
            public string C0 { get; set; }
            public string C1 { get; set; }
            public string C2 { get; set; }
            public string C3 { get; set; }
            public string C4 { get; set; }
            public string C5 { get; set; }
            public string C6 { get; set; }
            public string C7 { get; set; }
            public string C8 { get; set; }
            public string C9 { get; set; }
        }

        #region VARIABLES

        EDocument document = null;

        bool holeScreen = false;
        SToken currentToken = new SToken();

        int currentRowOffset = 0;
        int currentColumnOffset = 0;

        int documentVerticalOffset = 0;
        int documentHorizontalOffset = 0;

        #endregion VARIABLES

        #region NAVIGATION

        #region NAVIGATION EVENTS

        private void MouveUpButton_Click(object sender, RoutedEventArgs e)
        {
            holeScreen = true;
            MouveContentDoun();
        }

        private void MouveDownButton_Click(object sender, RoutedEventArgs e)
        {
            holeScreen = true;
            MouveContentUp();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            holeScreen = true;
            MouveContentRight();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            holeScreen = true;
            MouveContentLeft();
        }

        #endregion NAVIGATION EVENTS

        #region NAVIGATION HANDLERS

        private void MouveContentDoun()
        {
            MoveContent(-1, 0);
        }

        private void MouveContentUp()
        {
            MoveContent(1, 0);
        }

        private void MouveContentLeft()
        {
            MoveContent(0, 1);
        }

        private void MouveContentRight()
        {
            MoveContent(0, -1);
        }

        #endregion NAVIGATION HANDLERS

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion NAVIGATION
    }

    public class FooToColorConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new FooToColorConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int foo = (int)value;
            return
              foo == 1 ? Brushes.Blue :
              foo == 2 ? Brushes.Red :
              foo == 3 ? Brushes.Yellow :
              foo > 3 ? Brushes.Green :
                Brushes.Transparent;  // For foo<1
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
