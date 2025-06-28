// <copyright file="DivisionNode.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Division Node.
    /// </summary>
    internal class DivisionNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivisionNode"/> class.
        /// </summary>
        /// <param name="left"> Left child.</param>
        /// <param name="right">Right child.</param>
        public DivisionNode(Node left, Node right)
            : base(left, right)
        {
        }

        /// <summary>
        /// Gets Operator representation.
        /// </summary>
        public override string Operator => "/";

        /// <summary>
        /// Gets a value indicating whether associativity is left or right.
        /// </summary>
        public override bool IsLeftAssociative => true;

        /// <summary>
        /// Gets precedence.
        /// </summary>
        public override int Precedence => 2;

        /// <summary>
        /// Evaluates the nodes.
        /// </summary>
        /// <returns>Evaluated value. </returns>
        /// <exception cref="DivideByZeroException">Divide by 0.</exception>
        public override double Evaluate()
        {
            // check for division by 0
            double right = this.Right.Evaluate();
            if (right != 0)
            {
                return this.Left.Evaluate() / right;
            }
            else
            {
                // if dividing by 0, return positive infinity
                return double.PositiveInfinity;
            }
        }
    }
}
