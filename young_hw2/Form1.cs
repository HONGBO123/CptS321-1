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

namespace young_hw2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This Class runs all the code for the assignment
        /// It generates the random numbers and then runs
        /// the 3 algorithms I wrote and displays the results.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // initiate the list and fill with 10000 integers between 0 and 20000 inclusive
            List<int> randomNumbers = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                randomNumbers.Add(random.Next(0, 20001));
            }

            // run all 3 algorithms and display their results to the text box.
            textBox1.Text = "HashSet Method: " + algorithm1(randomNumbers).ToString() + Environment.NewLine +
                "The HashSet Method has a complexity of O(n) time."+ Environment.NewLine +
                "The algorithm passes over the array of random integers 1 time only," + Environment.NewLine +
                "all other operations are O(1) time. So O(n) + O(1) = O(n) time." + Environment.NewLine +
                "O(1) Storage Method: " + algorithm2(randomNumbers).ToString() + Environment.NewLine + 
                "Sorted Method: " + algorithm3(randomNumbers).ToString();
        }

        /// <summary>
        /// Uses a hashset to keep track of all numbers that have been run into already
        /// </summary>
        /// <param name="randomNumbers"></param>
        /// <returns>int count</returns>
        private int algorithm1(List<int> randomNumbers)
        {
            // initialize hash set
            HashSet<int> hashAggregation = new HashSet<int>();

            // try to add each number in randomNumbers to the hashset, if the number already exists it wont be added
            foreach (int i in randomNumbers)
            {
                try
                {
                    hashAggregation.Add(i);
                }
                catch {}
            }
            // return the count
            return hashAggregation.Count;
        }

        /// <summary>
        /// Uses a boolean array of size 20000 to keep track of all possible numbers in the set
        /// if a number exists in randomNumbers then the value at its index in arrayAggregate
        /// will be set to true instead of false
        /// </summary>
        /// <param name="randomNumbers"></param>
        /// <returns>int count</returns>
        private int algorithm2(List<int> randomNumbers)
        {
            bool[] arrayAggregate = new bool[20001];
            int count = 0;
            // set all initial values of the array to false
            for (int i = 0; i < arrayAggregate.Length; i++)
            {
                arrayAggregate[i] = false;
            }
            // set each index found in randomNumbers to true
            foreach (int i in randomNumbers)
            {
                arrayAggregate[i] = true;
            }

            // count the number of true values in arrayAggregate
            foreach (bool i in arrayAggregate)
            {
                if (i)
                {
                    count++;
                }
            }
            // return the count
            return count;
        }

        /// <summary>
        /// This algorithm relies on the list of randomNumbers to be sorted in either ascending or descending order.
        /// It checks to see if each number is the same as the last, if they the number, cur, is different from the last
        /// number, prev, then cur must be a new unique number.
        /// </summary>
        /// <param name="randomNumbers"></param>
        /// <returns></returns>
        private int algorithm3(List<int> randomNumbers)
        {
            randomNumbers.Sort();
            int count = 0, prev = -1;
            foreach (int cur in randomNumbers)
            {
                if (cur != prev)
                {
                    count++;
                }
                prev = cur;
            }
            return count;
        }
    }
}
