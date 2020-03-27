using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CptS321
{
    public class ExpressionTree
    {
        private TreeNode _root = null;

        /// <summary>
        /// constructor, puts together the tree
        /// </summary>
        /// <param name="expression"></param>
        public ExpressionTree(string expression)
        {
            expression = expression.Replace(" ", String.Empty);    // Remove spaces from expression
            try
            {
                _root = ConstructTree(InfixToPostfix(expression));
            }
            catch
            {
                throw;              // propagate it up to UI layer
            }
        }

        /// <summary>
        /// Using the factory, we construct the tree.
        /// the string is tokenized then each element is inserted into the tree.
        /// the factory determines the type of each element and inserts the
        /// right node for each element.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private TreeNode ConstructTree(string expression)
        {
            var list = Tokenize(expression);

            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNodeFactory treeNodeFactory = new ConcreteTreeNodeFactory();
            foreach (string token in list) // iterate through each node
            {
                TreeNode tree = treeNodeFactory.FactoryMethod(token); // create a node using the factory
                switch (tree) // determine the treeNodes type
                {
                    case OperatorNode opnode:
                        switch (opnode)
                        {
                            case BinaryOperatorNode bopNode:
                                BinaryOperatorNode binaryOperator = bopNode as BinaryOperatorNode;
                                TreeNode right = stack.Pop();
                                TreeNode left = stack.Pop();
                                binaryOperator.left = left;
                                binaryOperator.right = right;          
                                stack.Push(binaryOperator);                                        
                                break;
                            default:
                                break;
                        }
                        break;

                    case ValueNode vnode:
                        stack.Push(tree);
                        break;
                    default:
                        break;
                }
            }
            return stack.Pop();
        }

        /// <summary>
        /// this takes an input string and tokenizes it
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private List<string> Tokenize(string expression)
        {
            string @pattern = @"[\d]+\.?[\d]*|[A-Za-z]+[0-9]+|[-/\+\*\(\)]";
            Regex r = new Regex(@pattern);
            MatchCollection matchList = Regex.Matches(expression, @pattern);
            return matchList.Cast<Match>().Select(match => match.Value).ToList();
        }

        /// <summary>
        /// This is based off the shunting yard algorithm
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string InfixToPostfix(string expression)
        {
            HashSet<char> operators = new HashSet<char>(new char[] { '+', '-', '*', '/' });
            Dictionary<char, int> precedence = new Dictionary<char, int>
            {
                ['('] = 0,
                ['+'] = 1,
                ['-'] = 1,
                ['*'] = 2,
                ['/'] = 2,
                [')'] = 10
            };

            var list = Tokenize(expression);
            Queue<string> output_list = new Queue<string>(list.Capacity);
            Stack<char> stack = new Stack<char>();
            foreach (string tok in list)
            {
                if (int.TryParse(tok, out int int_result) || double.TryParse(tok, out double dec_result)
                    || Regex.Match(tok, @"[A-Za-z]+[0-9]+").Success)
                {
                    output_list.Enqueue(tok);
                }
                else // operator
                {
                    if (operators.Contains(tok[0]))
                    {
                        while (stack.Count != 0 && precedence[stack.Peek()] > precedence[tok[0]])
                        {
                            output_list.Enqueue(stack.Pop().ToString());
                        }
                        stack.Push(tok[0]);
                    }
                    else if (tok.StartsWith("("))
                    {
                        stack.Push(tok[0]);
                    }
                    else if (tok.StartsWith(")"))
                    {
                        while (stack.Peek() != '(')
                        {
                            output_list.Enqueue(stack.Pop().ToString());
                        }
                        stack.Pop();
                    }
                }
            }

            while (stack.Count > 0)
            {
                if (stack.Peek() != '(' || stack.Peek() != ')')
                    output_list.Enqueue(stack.Pop().ToString());
            }
            return string.Join(" ", output_list.ToArray());
        }

        /// <summary>
        /// evaluates the expression tree using the overloaded eval operators
        /// </summary>
        /// <returns></returns>
        public double Eval()
        {
            return _root.Eval();
        }
    }

    /// <summary>
    /// contains the overloadable evaluation funtion
    /// </summary>
    internal abstract class TreeNode
    {
        public abstract double Eval();
    }
    
    /// <summary>
    /// the node to contain values, either doubles or ints
    /// </summary>
    internal class ValueNode : TreeNode
    {
        private int? _int;
        private double? _double;

        /// <summary>
        /// constructor for int values
        /// </summary>
        /// <param name="value"></param>
        public ValueNode(int value)
        {
            _int = value;
        }

        /// <summary>
        /// constructor for double values
        /// </summary>
        /// <param name="value"></param>
        public ValueNode(double value)
        {
            _double = value;
        }


        /// <summary>
        /// simply returns the value stored in the node
        /// </summary>
        /// <returns></returns>
        public override double Eval()
        {
            if (_int.HasValue) return _int.Value;
            else return _double.Value;
        }
    }

    /// <summary>
    /// the overloadable class for operator nodes
    /// </summary>
    internal abstract class OperatorNode : TreeNode
    {
        public override abstract double Eval();
    }


    /// <summary>
    /// abstract binary operator node
    /// </summary>
    internal abstract class BinaryOperatorNode : OperatorNode
    {
        public TreeNode left;
        public TreeNode right;

        public BinaryOperatorNode()
        {
            left = null;
            right = null;
        }
        public override abstract double Eval();
    }


    /// <summary>
    /// add
    /// </summary>
    internal class AddNode : BinaryOperatorNode
    {
        public override double Eval()
        {
            return this.left.Eval() + this.right.Eval();
        }
    }

    /// <summary>
    /// subtract
    /// </summary>
    internal class SubtractNode : BinaryOperatorNode
    {
        public override double Eval()
        {
            return this.left.Eval() - this.right.Eval();
        }
    }


    /// <summary>
    /// multiply
    /// </summary>
    internal class MultiplyNode : BinaryOperatorNode
    {
        public override double Eval()
        {
            return this.left.Eval() * this.right.Eval();
        }
    }

    /// <summary>
    /// divide
    /// </summary>
    internal class DivideNode : BinaryOperatorNode
    {
        public override double Eval()
        {
            return this.left.Eval() / this.right.Eval();
        }
    }

    internal abstract class OpNodeFactory
    {
        public abstract OperatorNode FactoryMethod(string op);
    }

    /// <summary>
    /// Factory for the operation nodes
    /// </summary>
    internal class ConcreteOpNodeFactory : OpNodeFactory
    {
        public override OperatorNode FactoryMethod(string op)
        {
            switch (op)
            {
                case "+": return new AddNode();
                case "-": return new SubtractNode();
                case "*": return new MultiplyNode();
                case "/": return new DivideNode();
                default: return null;
            }
        }
    }

    internal abstract class TreeNodeFactory
    {
        public abstract TreeNode FactoryMethod(string expression);
    }

    /// <summary>
    /// factory for tree nodes
    /// </summary>
    internal class ConcreteTreeNodeFactory : TreeNodeFactory
    {
        public override TreeNode FactoryMethod(string expression)
        {
            OpNodeFactory opNodeFactory = new ConcreteOpNodeFactory();
            OperatorNode @operator = opNodeFactory.FactoryMethod(expression);
            if (@operator != null)
            {
                return @operator;
            }
            else
            {
                bool int_success = Int32.TryParse(expression, out int int_result),
                double_success = Double.TryParse(expression, out double double_result);
                if (int_success)
                {
                    return new ValueNode(int_result);
                }
                else
                {
                    return new ValueNode(double_result);
                }
            }
        }
    }
}

