// <copyright file="Node.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System.Collections.Generic;

    /// <summary>
    /// Abstract class Node.
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Evaluates the node.
        /// </summary>
        /// <returns> A double value representing the evaluation. </returns>
        public abstract double Evaluate();
    }
}
