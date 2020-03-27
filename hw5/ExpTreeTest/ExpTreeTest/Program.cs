/*
 * Nolen Young, 11517296
 * This program tests the exptree class.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetEngine;

namespace TestExpTree
{
    class TestExpTree
    {
        static void Main(string[] args)
        {
            RunExpTreeTester();
        }

        /// <summary>
        /// menu
        /// </summary>
        /// <param name="current_expression"></param>
        static void PrintMenu(string current_expression)
        {
            Console.WriteLine("---------- MENU ----------");
            Console.WriteLine("Current Expression: {0}", current_expression);
            Console.WriteLine("1. Enter an expression");
            Console.WriteLine("2. Set a variable value");
            Console.WriteLine("3. Evaluate");
            Console.WriteLine("4. Quit");
            Console.Write(">");
        }
        public enum MenuOptions { NEW_EXPR = 1, SET_VAR_VAL, EVAL, QUIT }

        /// <summary>
        /// tests the tree
        /// </summary>
        static void RunExpTreeTester()
        {
            string newExpr = "1+1";
            string menu_option = String.Empty;
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree(newExpr); // initialize expression tree
            int option = 0;
            do
            {
                PrintMenu(newExpr);

                menu_option = Console.ReadLine();

                Int32.TryParse(menu_option, out option);
                switch ((MenuOptions)option)
                {
                    case MenuOptions.NEW_EXPR:
                        Console.Write("Expression: ");
                        newExpr = Console.ReadLine();
                        tree = new CptS321.ExpressionTree(newExpr);
                        break;

                    case MenuOptions.SET_VAR_VAL:
                        string variable_name = String.Empty;
                        string variable_value = String.Empty;

                        Console.Write("Variable name: ");
                        variable_name = Console.ReadLine();    

                        Console.Write("Variable value: ");
                        variable_value = Console.ReadLine();    

                        bool success = Int32.TryParse(variable_value, out int val);
                        if (success)
                        {
                            tree.SetVar(variable_name, val);
                        }
                        else
                        {
                            Console.WriteLine("Invalid");
                        }
                        break;

                    case MenuOptions.EVAL:
                        Console.WriteLine("Result: {0}", tree.Eval());
                        break;
                        
                    case MenuOptions.QUIT:
                        break;

                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
            } while ((MenuOptions)option != MenuOptions.QUIT);
        }
    }
}
