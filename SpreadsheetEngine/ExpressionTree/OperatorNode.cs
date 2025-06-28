// <copyright file="OperatorNode.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    /// <summary>
    /// Operator Node class.
    /// </summary>
    public abstract class OperatorNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// Creates a new instance of OperatorNode.
        /// </summary>
        /// <param name="left">Left child.</param>
        /// <param name="right">Right child.</param>
        public OperatorNode(Node left, Node right)
        {
            this.Left = left;
            this.Right = right;
        }

        /// <summary>
        /// Gets the operator name. Operator string representation.
        /// </summary>
        public abstract string Operator { get; }

        /// <summary>
        /// Gets a value indicating whether the associativity is left or right.
        /// </summary>
        public abstract bool IsLeftAssociative { get; }

        /// <summary>
        /// Gets the precedence value of the operator.
        /// </summary>
        public abstract int Precedence { get; }

        /// <summary>
        /// Gets or sets left child property.
        /// </summary>
        public Node Left { get; set; }

        /// <summary>
        /// Gets or sets right child property.
        /// </summary>
        public Node Right { get; set; }
    }
}
