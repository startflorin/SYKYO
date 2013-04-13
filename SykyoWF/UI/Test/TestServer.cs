using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.BL;
using WindowsFormsApplication1.DL;
using DataPersistency.DL.ServerAccess;
using DataPersistency.DL.CommenAccess.ObjectsFromNumbers;

namespace WindowsFormsApplication1.UI
{
    /// <summary>
    /// Test the functionality of the server access interface
    /// </summary>
    public partial class TestServer : Form
    {
        
        /// <summary>
        /// Server access variable
        /// </summary>
        ServerAccessInterface serverAccess;

        /// <summary>
        /// Test server window constructor
        /// </summary>
        public TestServer()
        {
            InitializeComponent();
            if (serverAccess == null)
            {
                if (SymbolCollection.ServerAccess == null)
                {

                }
                else
                {
                    serverAccess = SymbolCollection.ServerAccess;
                }
            }
        }

        /// <summary>
        /// Evaluation started
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EvaluateButton_Click(object sender, EventArgs e)
        {
            Evaluate();
        }

        /// <summary>
        /// Start form evaluation
        /// </summary>
        private void Evaluate()
        {
            if (!string.IsNullOrEmpty(IDTextBox.Text.Trim()) && !string.IsNullOrEmpty(NameTextBox.Text.Trim()))
            {

                // If both SymbolName and SymbolID fields are filled create alias

                SymbolID ID = new SymbolID();
                int.TryParse(IDTextBox.Text.Trim(), out ID.Location.A);
                List<SymbolID> ReturnedElements = serverAccess.CreateSymbolAlias(NameTextBox.Text.Trim(), ID.Location.A);
                Display(ReturnedElements);
            }
            else if (!string.IsNullOrEmpty(IDTextBox.Text))
            {

                // If only SymbolID filled find SymbolName

                List<SymbolID> ReturnedElements = new List<SymbolID>();
                SymbolID symbol = new SymbolID();
                int.TryParse(IDTextBox.Text.Trim(), out symbol.Location.A);
                symbol.Names = serverAccess.GetSymbolNamesByID(symbol, 0);
                ReturnedElements.Add(symbol);
                Display(ReturnedElements);
            }
            else
            {
                // If only SymbolName filled find SymbolID

                List<SymbolID> ReturnedElements = new List<SymbolID>();
                if (!string.IsNullOrEmpty(NameTextBox.Text.Trim()))
                {
                    Name = NameTextBox.Text.Trim();
                    if (DuplicateCheckBox.Checked)
                    {
                        ReturnedElements = serverAccess.CreateSymbolByName(Name, true); // Create anyway
                    }
                    else if (CreateCheckBox.Checked)
                    {
                        ReturnedElements = serverAccess.CreateSymbolByName(Name, false); // Create if needed
                    }
                    else
                    {
                        ReturnedElements = serverAccess.GetSymbolsByName(Name, 0, 0); // Check for existence
                    }
                }
                Display(ReturnedElements);
            }
            
        }

        /// <summary>
        /// Display a list of SymbolNames
        /// </summary>
        /// <param name="ReturnedElements"></param>
        private void Display(List<string> ReturnedElements)
        {
            string displayed = "";
            foreach (string element in ReturnedElements)
            {
                displayed += "[ " + IDTextBox.Text.Trim() + " . 0 . 0 . 0 ] " + element + "\n\r";
            }
            ResultRichTextBox.Text = displayed;
            CountTextBox.Text = ReturnedElements.Count.ToString();
        }

        /// <summary>
        /// Display a list of SymbolIDs
        /// </summary>
        /// <param name="ReturnedElements"></param>
        private void Display(List<SymbolID> ReturnedElements)
        {
            string displayed = "";
            foreach (SymbolID element in ReturnedElements)
            {
                string ElementString = element.ToString() + "\n";
                displayed += ElementString;
                foreach (string name in element.Names)
                {
                    displayed += "" + name + "\n";
                }
                displayed += "================\n\r";
            }
            ResultRichTextBox.Text = displayed;
            CountTextBox.Text = ReturnedElements.Count.ToString();
        }

        private void CreateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            serverAccess.AcceptSymbols = CreateCheckBox.Checked;
        }

        private void DuplicateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DuplicateCheckBox.Checked)
            {
                CreateCheckBox.Checked = true;
            }
        }
    }
}

