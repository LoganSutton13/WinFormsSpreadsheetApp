// <copyright file="ParsingTools.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Shunting yard algorithm.
    /// </summary>
    public class ParsingTools
    {
        /// <summary>
        /// Uses the shuting yard algorithm to parse with precedence.
        /// </summary>
        /// <param name="tokens"> List of string tokens. </param>
        /// <param name="factory"> Has information on operator nodes. </param>
        /// <returns> A queue with the postfix notation of the strings. </returns>
        public static Queue<string> ShuntingYard(List<string> tokens, OperatorNodeFactory factory)
        {
            // queue where we will store the postfixed structure
            Queue<string> outputQueue = new Queue<string>();

            // stack where we will store operators and parenthesis
            Stack<string> opStack = new Stack<string>();

            // analyze each token
            foreach (string token in tokens)
            {
                // check for number, if number enqueue in output queue
                if (double.TryParse(token, out _) || char.IsLetter(token[0]))
                {
                    // Token is a number or a variable, add it to the output queue
                    outputQueue.Enqueue(token);
                }

                // check for ( parenthesis (open parenthesis)
                else if (token == "(")
                {
                    // we have an open parenthesis, put it on the op stack
                    opStack.Push(token);
                }

                // if we hit a closing parenthesis...
                else if (token == ")")
                {
                    // search for the corresponding open parenthesis
                    while (opStack.Peek() != "(")
                    {
                        // enqueue everything that is inbetween the parenthesis
                        outputQueue.Enqueue(opStack.Pop());
                    }

                    // pop the remnant open parenthesis
                    opStack.Pop();
                }

                // check for operators
                else if (factory.DoesOpExist(token))
                {
                    // while opstack is not empty, and the next thing on the stack is
                    // an operator, and the operator has higher or = precedence to the current op
                    while (opStack.Count > 0 && factory.DoesOpExist(opStack.Peek())
                        && factory.GetPrecedence(opStack.Peek()) >= factory.GetPrecedence(token))
                    {
                        // enqueue the operator that we pop from stack
                        outputQueue.Enqueue(opStack.Pop());
                    }

                    // push the current operator
                    opStack.Push(token);
                }
            }

            // pop the remaining operators off the stack
            while (opStack.Count > 0)
            {
                outputQueue.Enqueue(opStack.Pop());
            }

            // return the output
            return outputQueue;
        }

        /// <summary>
        /// Shunting yard algorithm.
        /// </summary>
        /// <param name="expression"> String of expression. </param>
        /// <param name="factory"> Has information on operatornodes. </param>
        /// <returns> Postfix queue. </returns>
        public static Queue<string> ShuntingYard(string expression, OperatorNodeFactory factory)
        {
            return ShuntingYard(TokenizeExpression(expression, factory), factory);
        }

        /// <summary>
        /// Tokenizes an expression.
        /// </summary>
        /// <param name="exp">Expression.</param>
        /// <param name="factory">Has information on operator nodes.</param>
        /// <returns>List containging tokens.</returns>
        /// <exception cref="Exception">If character not recoginzes, throws and exception.</exception>
        public static List<string> TokenizeExpression(string exp, OperatorNodeFactory factory)
        {
            List<string> tokens = new List<string>();
            int i = 0;
            while (i < exp.Length)
            {
                // handle whitespace
                if (char.IsWhiteSpace(exp[i]))
                {
                    i++;
                    continue;
                }

                // handle numbers
                else if (char.IsDigit(exp[i]) || exp[i] == '.')
                {
                    string num = string.Empty;
                    while (i < exp.Length && (char.IsDigit(exp[i]) || exp[i] == '.'))
                    {
                        num += exp[i];
                        i++;
                    }

                    tokens.Add(num);
                    continue;
                }

                // handle variables
                else if (char.IsLetter(exp[i]))
                {
                    string var = string.Empty;
                    while (i < exp.Length && char.IsLetterOrDigit(exp[i]))
                    {
                        var += exp[i];
                        i++;
                    }

                    tokens.Add(var);
                }

                // handle operators and parenthesis
                else if (factory.DoesOpExist(char.ToString(exp[i])) || exp[i] == ')' || exp[i] == '(')
                {
                    tokens.Add(char.ToString(exp[i]));
                    i++;
                }
                else
                {
                    throw new Exception("Character not recognized when tokenizing.");
                }
            }

            return tokens;
        }
    }
}
