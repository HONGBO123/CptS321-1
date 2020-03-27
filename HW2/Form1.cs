using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender,  EventArgs e)
        {
            // initiate the list and fill with 10000 integers between 0 and 20000
            List<int> randomNumbers = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                randomNumbers.Add(random.Next(0, 20001));
            }


        }
    }
}
