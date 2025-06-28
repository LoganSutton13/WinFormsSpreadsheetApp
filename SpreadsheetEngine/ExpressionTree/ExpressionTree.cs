// <copyright file="ExpressionTree.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design.Serialization;
    using System.Linq;
    using System.Reflection.Metadata.Ecma335;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Expression tree class.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// Creates a new instance of operator node factory.
        /// Used for creating operator nodes.
        /// </summary>
        private OperatorNodeFactory opFactory = new OperatorNodeFactory();

        /// <summary>
        /// Look up dictionary for variable values.
        /// </summary>
        private Dictionary<string, double> variableDictionary = new Dictionary<string, double>();

        /// <summary>
        /// List of possible variables.
        /// </summary>
        private List<string> possibleVariables = new List<string>();

        /// <summary>
        /// Private field root.
        /// </summary>
        private Node? root;

        /// <summary>
        /// Private field expression.
        /// </summary>
        private string expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// Expression Tree Constructor.
        /// </summary>
        /// <param name="expression">A string representing an expression.</param>
        public ExpressionTree(string expression)
        {
            this.expression = expression;
            this.root = this.ParseExpression(this.expression);
        }

        /// <summary>
        /// Gets or sets expression.
        /// </summary>
        public string Expression
        {
            get => this.expression; set
            {
                this.expression = value;
                this.variableDictionary.Clear();
                this.possibleVariables.Clear();
                this.root = this.ParseExpression(this.expression);
            }
        }

        /// <summary>
        /// Sets the variable in the dictionary with the corresponding value.
        /// </summary>
        /// <param name="variableName">Name of the variable to store.</param>
        /// <param name="variableValue">Value associated with the variable.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variableDictionary[variableName] = variableValue;
        }

        /// <summary>
        /// Gets the variable names loaded in the expression tree.
        /// </summary>
        /// <returns>A list with all variable names found in the expression tree.</returns>
        public List<string> GetVariableNames()
        {
            return this.possibleVariables;
        }

        /// <summary>
        /// Evaluate the expressssion.
        /// </summary>
        /// <returns> The evaluated expression a double.</returns>
        public double Evaluate()
        {
            if (this.root == null)
            {
                // root is null, return 0
                return 0;
            }

            return this.root.Evaluate();
        }

        /// <summary>
        /// Parses the expression and generates the tree.
        /// </summary>
        /// <param name="expression">Expression to parse.</param>
        /// <returns>Root node with formed tree.</returns>
        private Node? ParseExpression(string expression)
        {
            // is the string empty?
            if (expression == string.Empty)
            {
                return null;
            }

            Node? left = null;
            Node? right = null;

            // get the queue with the postfix notation
            Queue<string> postFixQueue = ParsingTools.ShuntingYard(expression, this.opFactory);
            Stack<Node> stack = new Stack<Node>();

            if (postFixQueue.Contains("(") || postFixQueue.Contains(")"))
            {
                throw new Exception("Parenthesis not matched error.");
            }

            while (postFixQueue.Count > 0)
            {
                string token = postFixQueue.Dequeue();

                double value;
                if (double.TryParse(token, out value))
                {
                    // token is a constant
                    stack.Push(new ConstantNode(value));
                }
                else if (!this.opFactory.DoesOpExist(token))
                {
                    // token is not a constant => variable
                    stack.Push(new VariableNode(token, ref this.variableDictionary));

                    // add the variable to the list of possible variables.
                    // these variables have no definition.
                    if (!this.possibleVariables.Contains(token))
                    {
                        this.possibleVariables.Add(token);
                    }

                    // this.variableDictionary.Add(token, 0);
                }
                else
                {
                    if (stack.Count >= 2)
                    {
                        // token is an operator
                        right = stack.Pop();
                        left = stack.Pop();
                        stack.Push(this.opFactory.CreateOperatorNode(token, left, right));
                    }
                    else // we have a lone operator
                    {
                        return null;
                    }
                }
            }

            return stack.Pop();
        }
    }
}
