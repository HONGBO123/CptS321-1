/*
 * Nolen Young
 * 11517296
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spreadsheet_Nolen_Young
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetUpDataGridView();
        }

        /// <summary>
        /// stores some constants for use in the program
        /// </summary>
        private class Constants
        {
            public static string[] columnHeaders = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            public static int numberOfRows = 50;
        }

        /// <summary>
        /// initializes the original spreadsheet
        /// </summary>
        private void SetUpDataGridView()
        {
            dataGridView1.Columns.Clear();
            foreach (string colHeader in Constants.columnHeaders)
            {
                dataGridView1.Columns.Add(colHeader, colHeader);
            }

            for (int i = 1; i <= Constants.numberOfRows; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i - 1].HeaderCell.Value = i.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
