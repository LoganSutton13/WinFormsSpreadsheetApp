// <copyright file="OperatorNodeFactory.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Operator node factory class.
    /// </summary>
    public class OperatorNodeFactory
    {
        /// <summary>
        /// Dictionary containing found operators using reflection.
        /// </summary>
        private Dictionary<string, Type> operators = new Dictionary<string, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
        /// </summary>
        public OperatorNodeFactory()
        {
            this.TraverseAvailableOperators((op, type) => this.operators.Add(op, type));
        }

        /// <summary>
        /// On operator delegate.
        /// </summary>
        /// <param name="op">Operator symbol.</param>
        /// <param name="type">Type of the operator.</param>
        private delegate void OnOperator(string op, Type type);

        /// <summary>
        /// Gets the precedence for provided operator symbol.
        /// </summary>
        /// <param name="op">Operator symbol.</param>
        /// <returns>Integer precedence value.</returns>
        /// <exception cref="Exception">Operator does not exist.</exception>
        public int GetPrecedence(string op)
        {
            if (this.operators.TryGetValue(op, out Type? value))
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                object operatorNodeObject = System.Activator.CreateInstance(value, new ConstantNode(0), new ConstantNode(0));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                if (operatorNodeObject is OperatorNode)
                {
                    return ((OperatorNode)operatorNodeObject).Precedence;
                }
            }

            throw new Exception("Operator not found.");
        }

        /// <summary>
        /// Determines whether an operator exists in the operator dictionary.
        /// </summary>
        /// <param name="op">Operator symbol.</param>
        /// <returns>True or false.</returns>
        public bool DoesOpExist(string op)
        {
            return this.operators.ContainsKey(op);
        }

        /// <summary>
        /// Creates an operator node.
        /// </summary>
        /// <param name="op">Operator symbol.</param>
        /// <param name="left">Left path.</param>
        /// <param name="right">Right path.</param>
        /// <returns>Operator node.</returns>
        /// <exception cref="Exception">Operator is not handled.</exception>
        public OperatorNode CreateOperatorNode(string op, Node left, Node right)
        {
            if (this.operators.TryGetValue(op, out Type? value))
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                object operatorNodeObject = System.Activator.CreateInstance(value, left, right);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                if (operatorNodeObject is OperatorNode)
                {
                    return (OperatorNode)operatorNodeObject;
                }
            }

            throw new Exception("Unhandled operator");
        }

        /// <summary>
        /// Reflection code provided in class exercise.
        /// </summary>
        /// <param name="onOperator">On operator delegate.</param>
        private void TraverseAvailableOperators(OnOperator onOperator)
        {
            // get the type declaration of OperatorNode
            Type operatorNodeType = typeof(OperatorNode);

            // Iterate over all loaded assemblies:
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // Get all types that inherit from our OperatorNode class using LINQ
                IEnumerable<Type> operatorTypes =
                assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));

                // Iterate over those subclasses of OperatorNode
                foreach (var type in operatorTypes)
                {
                    // for each subclass, retrieve the Operator property

                    // Create an instance of the subclass (assuming it has a parameterless constructor)
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    object instance = Activator.CreateInstance(type, new ConstantNode(0), new ConstantNode(0));

                    if (instance != null)
                    {
                        PropertyInfo operatorField = type.GetProperty("Operator");

                        if (operatorField != null)
                        {
                            // Get the character of the Operator
                            object value = operatorField.GetValue(instance);

                            // If “Operator” property is not static, you will need to create
                            // an instance first and use the following code instead (or similar):
                            // object value = operatorField.GetValue(Activator.CreateInstance(type,
                            // new ConstantNode(0)));
                            if (value is string)
                            {
                                string operatorSymbol = (string)value;

                                // And invoke the function passed as parameter
                                // with the operator symbol and the operator class
                                onOperator(operatorSymbol, type);
                            }
                        }
                    }
                }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }
        }
    }
}