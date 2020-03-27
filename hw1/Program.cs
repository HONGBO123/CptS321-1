// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>



namespace hw1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This class runs the program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main function of the program. It calls function to get input from the user and put
        /// it into a binary tree. Then it calls methods on that tree to print the ordered tree itself,
        /// the number of items in the tree, the height of the tree, and the theoretical minimum height of
        /// the tree.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // declares binary tree
            IntBinaryTree tree = new IntBinaryTree();

            //gets user input using the GetLine function.
            Console.WriteLine("Enter a collection of numbers in the range [0, 100], seperated by spaces: ");
            int[] userInput = GetLine();

            //Inserts the numbers the user input into the binary tree
            foreach (int i in userInput)
            {
                tree.insert(i);
            }

            // show the tree inorder.
            tree.display();
            Console.WriteLine();

            // counts and displys the items in the tree
            int treeCount = tree.Count();
            Console.WriteLine("Number of items in tree: " + treeCount);

            // counts and displays the height of the tree
            int height = tree.height(tree.root);
            Console.WriteLine("Height of tree: " + height);

            // displays the theoretical minimum height of the tree.
            Console.WriteLine("Theoretical minimum height of tree: " + Math.Floor(Math.Log(treeCount, 2)));
        }

        /// <summary>
        /// This takes in a line of input from the console and splits the input into tokens
        /// seperated by spaces, and then converts the string list into an intlist
        /// </summary>
        /// <returns>
        /// int [] inputIntegerList
        /// </returns>
        static int[] GetLine()
        {
            string rawInput = Console.ReadLine();
            string[] tokens = rawInput.Split();
            int[] inputIntegerList = Array.ConvertAll(tokens, int.Parse);
            return inputIntegerList;
        }
    }

    /// <summary>
    /// Class IntNode is an object used as the nodes for the binary tree.
    /// This node contains a number as data and two other nodes to form the data structure.
    /// There are included functions for insertion and display.
    /// </summary>
    class IntNode
    {
        private int number;
        public IntNode rightLeaf;
        public IntNode leftLeaf;

        public IntNode(int value)
        {
            number = value;
            rightLeaf = null;
            leftLeaf = null;
        }

        /// <summary>
        /// takes in a node and data to be inserted and then inserts the node into
        /// the proper location based off the data.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="data"></param>
        public void insertData(ref IntNode node, int data)
        {
            if (node == null)
            {
                node = new IntNode(data);

            }
            else if (node.number < data)
            {
                insertData(ref node.rightLeaf, data);
            }

            else if (node.number > data)
            {
                insertData(ref node.leftLeaf, data);
            }
        }

        /// <summary>
        /// Prints the data of the node
        /// </summary>
        /// <param name="n"></param>
        public void display(IntNode n)
        {
            if (n == null)
                return;

            display(n.leftLeaf);
            Console.Write(" " + n.number);
            display(n.rightLeaf);
        }

    }

    /// <summary>
    /// This class contains a binary tree made up of IntNodes.
    /// It contains methods to insert, check if its empty, count the height,
    /// print, and count the number of nodes in the tree.
    /// </summary>
    class IntBinaryTree
    {
        public IntNode root;
        private int count;

        public IntBinaryTree()
        {
            root = null;
            count = 0;
        }

        /// <summary>
        /// checks if the tree is empty
        /// </summary>
        /// <returns>boolean</returns>
        public bool isEmpty()
        {
            return root == null;
        }

        /// <summary>
        /// Inserts new data into the tree
        /// </summary>
        /// <param name="d"></param>
        public void insert(int d)
        {
            if (isEmpty())
            {
                root = new IntNode(d);
            }
            else
            {
                root.insertData(ref root, d);
            }

            count++;
        }

        /// <summary>
        /// counts the height of the tree recursiveley
        /// </summary>
        /// <param name="node"></param>
        /// <returns>int height</returns>
        public int height(IntNode node)
        {
            var result = 0;

            if (node != null)
            {
                result = Math.Max(height(node.leftLeaf), height(node.rightLeaf)) + 1;
            }

            return result;
        }

        /// <summary>
        /// displays the entire tree
        /// </summary>
        public void display()
        {
            if (!isEmpty())
                root.display(root);
        }

        /// <summary>
        /// returns the number of nodes in the tree
        /// </summary>
        /// <returns>int count</returns>
        public int Count()
        {
            return count;
        }
    }
}