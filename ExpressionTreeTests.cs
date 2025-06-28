// <copyright file="ExpressionTreeTests.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>
namespace SpreadsheetTests
{
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using SpreadsheetEngine.ExpressionTree;

    /// <summary>
    /// Tests for the spreadsheet class.
    /// </summary>
    public class ExpressionTreeTests
    {
        /// <summary>
        /// Private field constantTree.
        /// </summary>
        private ExpressionTree constantTree;

        /// <summary>
        /// Sets up for the tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // set the expression in the tree to a constant.
            this.constantTree = new ExpressionTree("1");
        }

        /// <summary>
        /// Tests what happens if we try to evaluate simple addition input.
        /// </summary>
        [Test]
        public void TestEvaluateSimpleInputAdd()
        {
            ExpressionTree expTree = new ExpressionTree("1+5");
            Assert.That(expTree.Evaluate(), Is.EqualTo(6));
        }

        /// <summary>
        /// Tests what happens if we try to evaluate simple subtraction input.
        /// </summary>
        [Test]
        public void TestEvaluateSimpleInputSub()
        {
            ExpressionTree expTree = new ExpressionTree("1-5");
            Assert.That(expTree.Evaluate(), Is.EqualTo(-4));
        }

        /// <summary>
        /// Tests what happens if we try to evaluate simple subtraction input.
        /// </summary>
        [Test]
        public void TestEvaluateSimpleInputDivision()
        {
            ExpressionTree expTree = new ExpressionTree("1/5");
            Assert.That(expTree.Evaluate(), Is.EqualTo(0.2));
        }

        /// <summary>
        /// Tests what happens if we try to evaluate simple subtraction input.
        /// </summary>
        [Test]
        public void TestEvaluateSimpleInputMultiplication()
        {
            ExpressionTree expTree = new ExpressionTree("1*5");
            Assert.That(expTree.Evaluate(), Is.EqualTo(5));
        }

        /// <summary>
        /// Tests what happens when we try to evaluate with complex inputs.
        /// </summary>
        [Test]
        public void TestEvaluateComplexInputAdd()
        {
            ExpressionTree expTree = new ExpressionTree("A1+3+C2+9");
            expTree.SetVariable("A1", 1);
            expTree.SetVariable("C2", 4);
            Assert.That(expTree.Evaluate(), Is.EqualTo(17));
        }

        /// <summary>
        /// Tests what happens when we try to evaluate with complex inputs.
        /// </summary>
        [Test]
        public void TestEvaluateComplexInputSub()
        {
            ExpressionTree expTree = new ExpressionTree("A1-3-C2-9");
            expTree.SetVariable("A1", 1);
            expTree.SetVariable("C2", 4);
            Assert.That(expTree.Evaluate(), Is.EqualTo(-15));
        }

        /// <summary>
        /// Tests what happens when we try to evaluate with complex inputs.
        /// </summary>
        [Test]
        public void TestEvaluateComplexInputDiv()
        {
            ExpressionTree expTree = new ExpressionTree("A1/2/C2/4");
            expTree.SetVariable("A1", 16);
            expTree.SetVariable("C2", 4);
            Assert.That(expTree.Evaluate(), Is.EqualTo(0.5));
        }

        /// <summary>
        /// Tests what happens when we try to evaluate with a division by 0.
        /// </summary>
        [Test]
        public void TestEvaluateDivisionByZero()
        {
            ExpressionTree expTree = new ExpressionTree("A1/2/0/4");
            expTree.SetVariable("A1", 16);
            expTree.SetVariable("C2", 4);
            Assert.That(expTree.Evaluate(), Is.EqualTo(double.PositiveInfinity));
        }

        /// <summary>
        /// Tests what happens when we try to evaluate with complex inputs.
        /// </summary>
        [Test]
        public void TestEvaluateComplexInputMul()
        {
            ExpressionTree expTree = new ExpressionTree("A1*2*C2*0.5");
            expTree.SetVariable("A1", 10);
            expTree.SetVariable("C2", 4);
            Assert.That(expTree.Evaluate(), Is.EqualTo(40));
        }

        /// <summary>
        /// Tests what happens when we evaluate a constant.
        /// </summary>
        [Test]
        public void TestEvaluateConstant()
        {
            ExpressionTree expTree = new ExpressionTree("0.1");
            Assert.That(expTree.Evaluate(), Is.EqualTo(0.1));
        }

        /// <summary>
        /// Tests what happens when we try to evaluate with just an operator input.
        /// </summary>
        [Test]
        public void TestParseExpressionOperatorOnlyInput()
        {
            MethodInfo methodInfo = this.GetMethod("ParseExpression");
            var result = methodInfo.Invoke(this.constantTree, new object[] { "+" });
            Assert.That(result, Is.EqualTo(null));
        }

        /// <summary>
        /// Tests what happens when we try to evaluate with complex inputs.
        /// </summary>
        [Test]
        public void TestParseExpressionEmptyInput()
        {
            MethodInfo methodInfo = this.GetMethod("ParseExpression");
            var result = methodInfo.Invoke(this.constantTree, new object[] { string.Empty });
            Assert.That(result, Is.EqualTo(null));
        }

        /// <summary>
        /// Tests what happens when we try to setting a variable.
        /// </summary>
        [Test]
        public void TestSetVariable()
        {
            ExpressionTree exp = new ExpressionTree("A1+1");
            exp.SetVariable("A1", 2);
            Assert.That(exp.Evaluate(), Is.EqualTo(3));
        }

        /// <summary>
        /// Tests what happens when we try to evaluate an expression
        /// with multiple variables.
        /// </summary>
        [Test]
        public void TestComplexExpression()
        {
            ExpressionTree exp = new ExpressionTree("2 * (A1+1) / 3 - 5.5");
            exp.SetVariable("A1", 2);
            Assert.That(exp.Evaluate(), Is.EqualTo(-3.5));
        }

        /// <summary>
        /// Tests what happens when we try to evaluate an expression
        /// with multiple variables.
        /// </summary>
        [Test]
        public void TestInvalidExpression()
        {
            string expression = "(2+3";
            Assert.Throws<Exception>(() => new ExpressionTree(expression));
        }

        /// <summary>
        /// Tests what happens when we try to evaluate an expression
        /// that is empty.
        /// </summary>
        [Test]
        public void TestEmptyExpression()
        {
            ExpressionTree exp = new ExpressionTree(string.Empty);
            Assert.That(exp.Evaluate(), Is.EqualTo(0));
        }

        /// <summary>
        /// Code copied from in class.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>Method to be tested.</returns>
        private MethodInfo GetMethod(string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
            {
                Assert.Fail("methodName cannot be null or whitespace");
            }

            var method = this.constantTree.GetType().GetMethod(
                methodName, BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            if (method == null)
            {
                Assert.Fail(string.Format("{0} method not found", methodName));
            }

            // disabling warning here as it is not possible for this method to return null.
#pragma warning disable CS8603 // Possible null reference return.
            return method;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}