// <copyright file="ParserAppWrapper.cs" company="Logan Sutton, ID: 11798384">
// Copyright (c) Logan Sutton, ID: 11798384. All rights reserved.
// </copyright>

namespace StringParserConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using SpreadsheetEngine.ExpressionTree;

    /// <summary>
    /// Wrapper app for console demo.
    /// </summary>
    public class ParserAppWrapper
    {
        /// <summary>
        /// Expression tree private field.
        /// </summary>
        private ExpressionTree expressionTree;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserAppWrapper"/> class.
        /// </summary>
        public ParserAppWrapper()
        {
            this.expressionTree = new ExpressionTree("A1+B2+C3");
        }

        /// <summary>
        /// Runs the simple console app.
        /// </summary>
        /// <returns>0 when done.</returns>
        public int RunApp()
        {
            while (true)
            {
                this.PrintMenu();
                if (int.TryParse(Console.ReadLine(), out var choice))
                {
                    if (choice > 0 && choice < 5)
                    {
                        if (choice == 4)
                        {
                            return 0;
                        }

                        this.EvaluateChoice(choice);
                    }
                }
            }
        }

        /// <summary>
        /// Prints the menu to the console.
        /// </summary>
        private void PrintMenu()
        {
            Console.WriteLine("Menu " + $"(Current Expresssion = {this.expressionTree.Expression})");
            Console.WriteLine("1 - Enter a new expression");
            Console.WriteLine("2 - Set a variable value");
            Console.WriteLine("3 - Evaluate tree");
            Console.WriteLine("4 - Quit");
        }

        /// <summary>
        /// Evaluates the users choice.
        /// </summary>
        /// <param name="choice">The users menu choice.</param>
        private void EvaluateChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    this.PromptNewExpression();
                    break;
                case 2:
                    this.SetVariableValue();
                    break;
                case 3:
                    this.EvaluateExpression();
                    break;
                default: break;
            }
        }

        /// <summary>
        /// Prompts for a new expression and sets the expression.
        /// </summary>
        private void PromptNewExpression()
        {
            Console.WriteLine("Enter the new expression: ");
            string? exp = Console.ReadLine();
            if (exp != null)
            {
                exp = exp.Replace('\n', '\0');
                this.expressionTree.Expression = exp;
            }
        }

        /// <summary>
        /// Allows the user to set a variable value.
        /// </summary>
        private void SetVariableValue()
        {
            Console.WriteLine("Enter the variable name: ");
            string? variable = Console.ReadLine();
            if (variable != null)
            {
                variable = variable.Replace('\n', '\0');
            }

            Console.WriteLine("Enter the variable value: ");
            string? stringValue = Console.ReadLine();
            double doubleValue = 0.0;

            // try to parse the double value assigned to the variable
            if (double.TryParse(stringValue, out doubleValue) && variable != null)
            {
                this.expressionTree.SetVariable(variable, doubleValue);
            }
        }

        /// <summary>
        /// Evaluates the expression.
        /// </summary>
        private void EvaluateExpression()
        {
            Console.WriteLine("The evaluated expression is: " + this.expressionTree.Evaluate());
        }
    }
}
