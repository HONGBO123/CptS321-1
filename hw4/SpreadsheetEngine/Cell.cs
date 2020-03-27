/*
 * Nolen Young
 * 11517296
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace SpreadsheetEngine
{
    public abstract class AbstractCell : INotifyPropertyChanged
    {
        protected string text = "";
        protected string value = "";
        private readonly string _name = "";
        private readonly int row_index;
        private readonly int col_index;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="r_index"></param>
        /// <param name="c_index"></param>
        public AbstractCell(int r_index, int c_index, string name)
        {
            row_index = r_index;
            col_index = c_index;
            _name = name;
        }

        /// <summary>
        /// Return row index
        /// </summary>
        public int RowIndex
        {
            get
            {
                return row_index;
            }
        }

        /// <summary>
        /// Return column index
        /// </summary>
        public int ColumnIndex
        {
            get
            {
                return col_index;
            }
        }

        /// <summary>
        /// Stores the text of the cell
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (value != text)
                {
                    text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        /// <summary>
        /// Stores the value of the cell
        /// </summary>
        public string Value
        {
            get
            {
                return value;
            }
            internal set 
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged("Value");
                }
            }
        }

        /// <summary>
        /// holds the name
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// handles the property change
        /// </summary>
        /// <param name="prop"></param>
        protected void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class Cell : AbstractCell
    {
        public Cell(int r_index, int c_index, string name) : base(r_index, c_index, name) { }
    }
}

