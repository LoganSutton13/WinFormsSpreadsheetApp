// <copyright file="ConstantNode.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System.Collections.Generic;

    /// <summary>
    /// Constant node.
    /// </summary>
    internal class ConstantNode : Node
    {
        /// <summary>
        /// A constant numerical value.
        /// </summary>
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="value">Double value.</param>
        public ConstantNode(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public double Value { get => this.value; set => this.value = value; }

        /// <summary>
        /// Returns the value associated with this.
        /// </summary>
        /// <returns>Value.</returns>
        public override double Evaluate()
        {
            return this.value;
        }
    }
}
