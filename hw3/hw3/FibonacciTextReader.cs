using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace NotepadApp
{
    class Fibonacci
    {
        /// <summary>
        /// it returns the nth number of the fibonacci sequence.
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        public static BigInteger NthFibonacciNumber(int N)
        {
            BigInteger a = 0, b = 1, c;
            if (N == 1)
            {
                return a;
            }
            else
            {
                for (int i = 3; i <= N; i++)
                {
                    c = a + b;
                    a = b;
                    b = c;
                }
                return b;
            }
        }
    }
    class FibonacciTextReader : System.IO.TextReader
    {
        // these ints keep track of number of lines and the current line the reader is on
        private int nLines;
        private int currentLine = 1;

        /// <summary>
        /// constructor: only takes in number of lines as a parameter
        /// </summary>
        /// <param name="n"></param>
        public FibonacciTextReader(int n)
        {
            nLines = n;
        }

        /// <summary>
        /// Overrides the ReadLine.
        /// </summary>
        /// <returns></returns>
        public override string ReadLine()
        {
            return Fibonacci.NthFibonacciNumber(currentLine).ToString();
        }

        /// <summary>
        /// reads nLines worth of numbers
        /// </summary>
        /// <returns></returns>
        public override string ReadToEnd()
        {
            string numbers = "";
            while (currentLine <= nLines)
            {
                numbers += $"{currentLine}: " + ReadLine();
                if (currentLine != nLines)
                {
                    numbers += Environment.NewLine;
                }
                currentLine++;
            }
            return numbers;
        }
    }
}