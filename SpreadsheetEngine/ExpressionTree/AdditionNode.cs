// <copyright file="AdditionNode.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System.Collections.Generic;

    /// <summary>
    /// Addition node, inherits from OperatorNode.
    /// </summary>
    internal class AdditionNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionNode"/> class.
        /// </summary>
        /// <param name="left">Left child.</param>
        /// <param name="right">Right child.</param>
        public AdditionNode(Node left, Node right)
            : base(left, right)
        {
        }

        /// <summary>
        /// Gets Operator representation.
        /// </summary>
        public override string Operator => "+";

        /// <summary>
        /// Gets a value indicating whether associativity is left or right.
        /// </summary>
        public override bool IsLeftAssociative => true;

        /// <summary>
        /// Gets precedence.
        /// </summary>
        public override int Precedence => 1;

        /// <summary>
        /// Public override of evaluate.
        /// </summary>
        /// <returns>Evaluated children.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() + this.Right.Evaluate();
        }
    }
}
