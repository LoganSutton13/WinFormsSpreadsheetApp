// <copyright file="VariableNode.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System.Collections.Generic;

    /// <summary>
    /// Variable node.
    /// </summary>
    internal class VariableNode : Node
    {
        /// <summary>
        /// Variables dictionary.
        /// </summary>
        private Dictionary<string, double> variables;

        /// <summary>
        /// Private field name.
        /// </summary>
        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="name">Name of the variable.</param>
        /// <param name="variables">Dictionary of variables.</param>
        public VariableNode(string name, ref Dictionary<string, double> variables)
        {
            this.variables = variables;
            this.name = name;
        }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get => this.name; set => this.name = value; }

        /// <summary>
        /// Returns the value associated with the variable.
        /// </summary>
        /// <returns>Value.</returns>
        public override double Evaluate()
        {
            if (this.variables.ContainsKey(this.name))
            {
                return this.variables[this.name];
            }
            else
            {
                // the variable doesn't exist, throw exception
                throw new KeyNotFoundException(this.name);
            }
        }
    }
}
