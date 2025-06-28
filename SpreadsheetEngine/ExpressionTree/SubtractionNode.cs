// <copyright file="SubtractionNode.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System.Collections.Generic;

    /// <summary>
    /// Subtraction node.
    /// </summary>
    internal class SubtractionNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractionNode"/> class.
        /// </summary>
        /// <param name="left">Left child.</param>
        /// <param name="right">Right child.</param>
        public SubtractionNode(Node left, Node right)
            : base(left, right)
        {
        }

        /// <summary>
        /// Gets Operator representation.
        /// </summary>
        public override string Operator => "-";

        /// <summary>
        /// Gets a value indicating whether associativity is left or right.
        /// </summary>
        public override bool IsLeftAssociative => true;

        /// <summary>
        /// Gets precedence.
        /// </summary>
        public override int Precedence => 1;

        /// <summary>
        /// Evaluates the nodes.
        /// </summary>
        /// <returns>Evaluated value. </returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() - this.Right.Evaluate();
        }
    }
}
