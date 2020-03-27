/*
 * Nolen Young
 * 11517296
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SpreadsheetEngine
{
    class Spreadsheet
    {
        private Cell[,] spreadsheet;
        private readonly int row_dim = 0;
        private readonly int col_dim = 1;
        public event PropertyChangedEventHandler PropertyChanged;
        private static string[] columnHeaders = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        Dictionary<char, int> headerLookUp = new Dictionary<char, int>()
        {
            {'A', 1},
            {'B', 2},
            {'C', 3},
            {'D', 4},
            {'E', 5},
            {'F', 6},
            {'G', 7},
            {'H', 8},
            {'I', 9},
            {'J', 10},
            {'K', 11},
            {'L', 12},
            {'M', 13},
            {'N', 14},
            {'O', 15},
            {'P', 16},
            {'Q', 17},
            {'R', 18},
            {'S', 19},
            {'T', 20},
            {'U', 21},
            {'V', 22},
            {'W', 23},
            {'X', 24},
            {'Y', 25},
            {'Z', 26},
        };
        private Dictionary<AbstractCell, HashSet<AbstractCell>> dependencies;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="num_rows"></param>
        /// <param name="num_cols"></param>
        public Spreadsheet(int num_rows, int num_cols)
        {
            spreadsheet = new Cell[num_rows, num_cols];
            for (int i = 0; i < num_rows; ++i)
            {
                for (int j = 0; j < num_cols; ++j)
                {
                    spreadsheet[i, j] = new Cell(i, j, columnHeaders[j] + (i + 1).ToString());    // 2D Array
                    spreadsheet[i, j].PropertyChanged += new PropertyChangedEventHandler(OnCellPropertyChanged);    // Make A handler
                }
            }
        }

        /// <summary>
        /// Remove the cell from the dependencies 
        /// </summary>
        /// <param name="cell"></param>
        private void RemoveDependencies(AbstractCell cell)
        {
            if (dependencies.ContainsKey(cell))     // If the cell had any dependencies,
            {
                dependencies[cell].Clear();         // then remove them
            }
            dependencies.Remove(cell);        // Remove key
        }

        /// <summary>
        /// Updates all other cell values when one cell changes
        /// </summary>
        /// <param name="cell"></param>
        private void CascadingEffect(AbstractCell cell)
        {
             foreach (AbstractCell key in dependencies.Keys)       // For every cell that has dependencies
             {
                if (dependencies[key].Contains(cell))       // If "cell" is in a hashset (mapped to by key), then "key" needs to be reevaluated since "cell"'s value changed
                {
                    DetermineCellValue(key);
                }
             }
        }

        /// <summary>
        /// figures out what the value of the cell should be
        /// </summary>
        /// <param name="cell"></param>
        private void DetermineCellValue(AbstractCell cell)
        {
            if (cell.Text.Length == 0)
            {
                cell.Value = "";
            }
            else if (cell.Text.StartsWith("="))
            {
                cell.Value = GetCell(headerLookUp[cell.Text[2]], Convert.ToInt32(cell.Text[3])).Value;
            }
            else
            {
                cell.Value = cell.Text;
            }
        }

        /// <summary>
        /// when the cell changes value, handles it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            AbstractCell c = sender as AbstractCell;
            switch (e.PropertyName)
            {
                case "Text":
                    RemoveDependencies(c);
                    DetermineCellValue(c);
                    PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));    // Pass along event to whoever uses this class
                    break;
                case "Value":
                    CascadingEffect(c);
                    PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));    // Pass along event to whoever uses this class
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// returns an abstract cell at the specified location in the spreadsheet
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>abstract cell</returns>
        public AbstractCell GetCell(int row, int col)
        {
            if (row >= spreadsheet.GetLowerBound(row_dim) && row <= spreadsheet.GetUpperBound(row_dim) && col >= spreadsheet.GetLowerBound(col_dim) && col <= spreadsheet.GetUpperBound(col_dim))
            {
                return spreadsheet[row, col];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// returns number of columns
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return spreadsheet.GetLength(col_dim);
            }
        }

        /// <summary>
        /// returns number of rows
        /// </summary>
        public int RowCount
        {
            get
            {
                return spreadsheet.GetLength(row_dim);
            }
        }
    }
}
