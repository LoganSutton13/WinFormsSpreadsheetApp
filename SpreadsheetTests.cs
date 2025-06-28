// <copyright file="SpreadsheetTests.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

namespace SpreadsheetTests
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using NuGet.Frameworks;
    using NUnit.Framework.Constraints;
    using SpreadsheetEngine;

    /// <summary>
    /// Tests for the spreadsheet class.
    /// </summary>
    public class SpreadsheetTests
    {
        /// <summary>
        /// Spreadsheet to test.
        /// </summary>
        private SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(5, 5);

        /// <summary>
        /// Sets up for the tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.spreadsheet = new SpreadsheetEngine.Spreadsheet(5, 5);
        }

        /// <summary>
        /// Tests that clear spreadsheet actually clears the spreadsheet.
        /// </summary>
        [Test]
        public void TestClearSpreadsheet()
        {
            Spreadsheet spreadsheet = new Spreadsheet(10, 10);
            Cell? cell = spreadsheet.GetCell(0, 0);
            Assert.IsNotNull(cell);
            if (cell != null)
            {
                cell.Text = "=1";
            }

            Assert.That(cell.Value, Is.EqualTo("1"));
            spreadsheet.ClearSpreadsheet();
            cell = spreadsheet.GetCell(0, 0);
            Assert.IsNotNull(cell);
            Assert.That(cell.Text, Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Tests the loading functionality of the spreadsheet.
        /// </summary>
        [Test]
        public void TestLoadSpreadsheet()
        {
            /*
            In order to test loading of the spreadsheet, we would need to
            load a properly formatted xml file from the machine, and then
            ensure that the files contents were accurately displayed on the
            spreadsheet.
             */
            Assert.Pass();
        }

        /// <summary>
        /// Tests the saving functionality of the spreadsheet.
        /// </summary>
        [Test]
        public void TestSaveSpreadsheet()
        {
            /*
            In order to test saving of the spreadsheet, we would need
            to input some data into the spreadsheet, attempt to use
            the saving functionality, and then verify the contents of
            the file we wrote to.
             */
            Assert.Pass();
        }

        /// <summary>
        /// Tests what happens if we query for a cell that is out of bounds.
        /// </summary>
        [Test]
        public void TestGetCellOutOfBounds()
        {
            Assert.That(this.spreadsheet.GetCell(6, 6), Is.EqualTo(null));
        }

        /// <summary>
        /// Test that GetCell() returns a cell when in bounds.
        /// </summary>
        [Test]
        public void TestGetCellInBounds()
        {
            var actualValue = this.spreadsheet.GetCell(0, 0);
            Assert.IsInstanceOf<SpreadsheetEngine.Cell>(actualValue, "The cell at (0,0) should be of type Cell.");
        }

        /// <summary>
        /// Tests whether invalid operators are handled. Should not crash.
        /// </summary>
        [Test]
        public void TestInvalidOperatorWhileEvaluatingIsHandled()
        {
            SpreadsheetEngine.Cell? testCell = this.spreadsheet.GetCell(2, 2);
            if (testCell != null)
            {
                testCell.Text = "=4 * 9 $ 7";
                string expected = "#OP_ERROR";
                Assert.That(expected, Is.EqualTo(testCell.Value));
            }
        }

        /// <summary>
        /// Tests Value setter.
        /// </summary>
        [Test]
        public void TestValueSetter()
        {
            string expectedValue = "100";
            SpreadsheetEngine.Cell? testCell = this.spreadsheet.GetCell(0, 1);
            SpreadsheetEngine.Cell? resultCell = this.spreadsheet.GetCell(0, 0);
            if (testCell != null && resultCell != null)
            {
                testCell.Text = expectedValue;
                resultCell.Text = "=B1";
                Assert.That(resultCell.Value, Is.EqualTo(expectedValue));
                Assert.That(testCell.Value, Is.EqualTo(expectedValue));
            }
            else
            {
                Assert.Fail(string.Format("1 or more cells are null."));
            }
        }

        /// <summary>
        /// Comment analysis for EvaluateCell().
        /// </summary>
        [Test]
        public void TestEvaluateCell()
        {
            /*
            Evaluate cell depends on GetCell and the Value setter property to funciton correclty.
            In order to test this, we'd need to get the spreadsheet in a state where we have some
            cells already loaded with data, and then we'd need to set a known cell to have a formula
            value corresponding back another cell with data. We could then compare the two values of
            the cells. If they're equal, the test passes. If not, it fails.
            */
            Assert.Pass();
        }

        /// <summary>
        /// Tests to see if the proper size array of cells was generated correctlly.
        /// </summary>
        [Test]
        public void TestGenerateSpreadsheet()
        {
            MethodInfo methodInfo = this.GetMethod("GenerateSpreadsheet");
            int expected = 3;
            var array = methodInfo.Invoke(this.spreadsheet, new object[] { 3, 3 });
            if (array != null)
            {
                SpreadsheetEngine.Cell[,] result = (SpreadsheetEngine.Cell[,])array;
                Assert.That(result.GetLength(0), Is.EqualTo(expected));
                Assert.That(result.GetLength(1), Is.EqualTo(expected));
            }
        }

        /// <summary>
        /// Tests GenerateSpreadsheet with invalid input.
        /// Expecting array of size 0,0.
        /// </summary>
        [Test]
        public void TestGenerateSpreadsheetInvalidInput()
        {
            MethodInfo methodInfo = this.GetMethod("GenerateSpreadsheet");
            int expected = 0;
            var array = methodInfo.Invoke(this.spreadsheet, new object[] { -1, -1 });
            if (array != null)
            {
                SpreadsheetEngine.Cell[,] result = (SpreadsheetEngine.Cell[,])array;
                Assert.That(result.GetLength(0), Is.EqualTo(expected));
                Assert.That(result.GetLength(1), Is.EqualTo(expected));
            }
        }

        /// <summary>
        /// Tests the undo last action stack.
        /// </summary>
        [Test]
        public void TestUndoLastAction()
        {
            /*
            In order to test this, we would need to put the form in a state where an undo action
            is possible. Since the form uses the command collection and spreadsheet methods to
            control the stack, we can't test this with a spreadsheet object. Once the form is in
            the desired state, we would then make a call to undo the last action, and check if the
            action updated the spreadsheet accordingly.
            */
            Assert.Pass();
        }

        /// <summary>
        /// Tests the redo last action stack.
        /// </summary>
        [Test]
        public void TestRedoLastAction()
        {
            /*
            In order to test this, we would need to put the form in a state where a redo action
            is possible. Since the form uses the command collection and spreadsheet methods to
            control the stack, we can't test this with a spreadsheet object. Once the form is in
            the desired state, we would then make a call to redo the last action, and check if the
            action updated the spreadsheet accordingly.
            */

            Assert.Pass();
        }

        /// <summary>
        /// Tests that adding to the undo stack (and peeking) was succesful.
        /// </summary>
        [Test]
        public void TestAddToUndoStack()
        {
            SpreadsheetEngine.Spreadsheet sheet = new SpreadsheetEngine.Spreadsheet(5, 5);
            UndoRedoCollection col = new SpreadsheetEngine.UndoRedoCollection("test");
            sheet.AddUndo(new SpreadsheetEngine.UndoRedoCollection("test"));
            Assert.That(sheet.GetNextUndoCommand(), Is.EqualTo(col.Title));
        }

        /// <summary>
        /// Tests that adding to the undo stack (and peeking) was succesful.
        /// </summary>
        [Test]
        public void TestAddToRedoStack()
        {
            SpreadsheetEngine.Spreadsheet sheet = new SpreadsheetEngine.Spreadsheet(5, 5);
            UndoRedoCollection col = new SpreadsheetEngine.UndoRedoCollection("test");
            sheet.AddRedo(new SpreadsheetEngine.UndoRedoCollection("test"));
            Assert.That(sheet.GetNextRedoCommand(), Is.EqualTo(col.Title));
        }

        /// <summary>
        /// Tests that adding to the undo stack edge case returned null.
        /// </summary>
        [Test]
        public void TestEmptyGetUndoStack()
        {
            SpreadsheetEngine.Spreadsheet sheet = new SpreadsheetEngine.Spreadsheet(5, 5);
            Assert.That(sheet.GetNextUndoCommand(), Is.EqualTo(null));
        }

        /// <summary>
        /// Tests that adding to the redo stack edge case returned null.
        /// </summary>
        [Test]
        public void TestEmptyGetRedoStack()
        {
            SpreadsheetEngine.Spreadsheet sheet = new SpreadsheetEngine.Spreadsheet(5, 5);
            Assert.That(sheet.GetNextRedoCommand(), Is.EqualTo(null));
        }

        /// <summary>
        /// Tests for a self reference, and that the proper result is displayed.
        /// </summary>
        [Test]
        public void TestSelfReference()
        {
            SpreadsheetEngine.Spreadsheet sheet = new SpreadsheetEngine.Spreadsheet(5, 5);
            Cell? selfRefCell = sheet.GetCell(0, 0);
            string expected = "#SELF_REF";
            Assert.NotNull(selfRefCell);
            selfRefCell.Text = "=A1";
            Assert.That(selfRefCell.Value, Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests for a bad reference, and that the proper result is displayed.
        /// </summary>
        [Test]
        public void TestBadReference()
        {
            SpreadsheetEngine.Spreadsheet sheet = new SpreadsheetEngine.Spreadsheet(26, 26);
            Cell? selfRefCell = sheet.GetCell(0, 0);
            string expected = "#BAD_REF";
            Assert.NotNull(selfRefCell);
            selfRefCell.Text = "=Z1234";
            Assert.That(selfRefCell.Value, Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests for a bad reference but the cell exists, and that the proper result is displayed.
        /// </summary>
        [Test]
        public void TestBadReferenceOfExistingCell()
        {
            SpreadsheetEngine.Spreadsheet sheet = new SpreadsheetEngine.Spreadsheet(5, 5);
            Cell? selfRefCell = sheet.GetCell(0, 0);
            string expected = "0";
            Assert.NotNull(selfRefCell);
            selfRefCell.Text = "=B4";
            Assert.That(selfRefCell.Value, Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests for circular reference, and that the proper result is displayed.
        /// </summary>
        [Test]
        public void TestCircularReference()
        {
            SpreadsheetEngine.Spreadsheet sheet = new SpreadsheetEngine.Spreadsheet(5, 5);

            Cell? A1 = sheet.GetCell(0, 0);
            Cell? B1 = sheet.GetCell(1, 0);
            Cell? B2 = sheet.GetCell(1, 1);
            Cell? A2 = sheet.GetCell(0, 1);

            string expected = "#CIRC_REF";
            Assert.NotNull(A1);
            Assert.NotNull(B1);
            Assert.NotNull(B2);
            Assert.NotNull(A2);

            A1.Text = "=B1";
            B1.Text = "=B2";
            B2.Text = "=A2";
            A2.Text = "=A1";

            Assert.That(A1.Value, Is.EqualTo(expected));
            Assert.That(A2.Value, Is.EqualTo(expected));
            Assert.That(B1.Value, Is.EqualTo(expected));
            Assert.That(B2.Value, Is.EqualTo(expected));
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

            var method = this.spreadsheet.GetType().GetMethod(
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