using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spreadsheet_Logan_Sutton
{
    /// <summary>
    /// Form class.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            InitializeDataGrid();
        }

        /// <summary>
        /// Initializes the UI data grid.
        /// </summary>
        private void InitializeDataGrid()
        {
            this.dataGridView1.Columns.Clear();

            // for each letter in the alphabet, add a column
            for (int i = 0; i < 26; i++)
            {
                char c = (char)('A' + i);
                string colName = "Column" + c;
                string colHeader = string.Empty + c;
                this.dataGridView1.Columns.Add(colName, colHeader);
            }

            // for each number 1-50, add a row
            for (int i = 1; i <= 50; i++)
            {
                string rowName = "Row" + i.ToString();
                int rowIndex = this.dataGridView1.Rows.Add();

                // set the header value
                this.dataGridView1.Rows[rowIndex].HeaderCell.Value = i.ToString();
            }
        }
    }
}
