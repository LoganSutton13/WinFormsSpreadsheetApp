// <copyright file="ParsingTests.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>
namespace SpreadsheetTests
{
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using NUnit.Framework.Constraints;
    using SpreadsheetEngine.ExpressionTree;

    /// <summary>
    /// Tests for the spreadsheet class.
    /// </summary>
    public class ParsingTests
    {
        private OperatorNodeFactory opFactory = new OperatorNodeFactory();

        /// <summary>
        /// Sets up for the tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Tests what happens if we try tokenizing a simple input.
        /// </summary>
        [Test]
        public void TestTokenizeExpressionSimple()
        {
            List<string> expected = ["A1", "+", "A2", "/", "10"];

            List<string> tokens = ParsingTools.TokenizeExpression("A1+A2/10", this.opFactory);
            Assert.That(tokens, Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests what happens if we try tokenizing empty string.
        /// </summary>
        [Test]
        public void TestTokenizeExpressionEmpty()
        {
            List<string> expected = [];

            List<string> tokens = ParsingTools.TokenizeExpression(string.Empty, this.opFactory);
            Assert.That(tokens, Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests what happens if we try tokenizing a simple input.
        /// </summary>
        [Test]
        public void TestTokenizeExpressionNumOnly()
        {
            List<string> expected = ["(", "1", "+", "2", ")", "/", "20", "*", "100.1", "-", "7"];

            List<string> tokens = ParsingTools.TokenizeExpression("(1+2)/20*100.1-7", this.opFactory);
            Assert.That(tokens, Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests what happens if we try Shunting a simple input.
        /// </summary>
        [Test]
        public void TestShuntingSimple()
        {
            List<string> tokens = ["A", "*", "B", "+", "C"];
            Queue<string> expected = new Queue<string>(new List<string> { "A", "B", "*", "C", "+" });
            Queue<string> actual = ParsingTools.ShuntingYard(tokens, this.opFactory);
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests what happens if we try Shunting an empty input.
        /// </summary>
        [Test]
        public void TestShuntingEmpty()
        {
            List<string> tokens = new List<string>();
            Queue<string> expected = new Queue<string>(new List<string> { });
            Queue<string> actual = ParsingTools.ShuntingYard(tokens, this.opFactory);
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests what happens if we try tokenizing a simple input.
        /// </summary>
        [Test]
        public void TestShuntingComplex()
        {
            List<string> tokens = ["A", "*", "(", "B", "+", "C", "*", "D", ")", "+", "E"];
            Queue<string> expected = new Queue<string>(new List<string> { "A", "B", "C", "D", "*", "+", "*", "E", "+" });
            Queue<string> actual = ParsingTools.ShuntingYard(tokens, this.opFactory);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}