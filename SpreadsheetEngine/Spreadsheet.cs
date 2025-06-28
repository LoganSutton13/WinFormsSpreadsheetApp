// <copyright file="Spreadsheet.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net.Security;
    using System.Reflection.Metadata.Ecma335;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;
    using SpreadsheetEngine.ExpressionTree;

    /// <summary>
    /// Spreasheet class.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// 2d array of cells.
        /// </summary>
        private Cell[,] cells;

        private Stack<UndoRedoCollection> undos = new Stack<UndoRedoCollection>();

        private Stack<UndoRedoCollection> redos = new Stack<UndoRedoCollection>();

        private HashSet<Cell> visitedCells = new HashSet<Cell>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="numRows">The number of rows.</param>
        /// <param name="numCols">The number of columns.</param>
        public Spreadsheet(int numRows, int numCols)
        {
            this.cells = this.GenerateSpreadsheet(numRows, numCols);
        }

        /// <summary>
        /// Detects if a cell has been changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        public int RowCount { get => this.cells.GetLength(0); }

        /// <summary>
        /// Gets the number of Columns.
        /// </summary>
        public int ColumnCount { get => this.cells.GetLength(1); }

        /// <summary>
        /// Saves the spreadsheet data to an xml document.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        public void SaveSpreadsheet(StreamWriter stream)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement root = xmlDocument.CreateElement("spreadsheet");
            xmlDocument.AppendChild(root);

            // loop through each cell
            foreach (var cell in this.cells)
            {
                // check if we should write the cell
                if (cell != null && (cell.Text != string.Empty || cell.BGColor != 0xFFFFFFFF))
                {
                    XmlElement cellElement = xmlDocument.CreateElement("cell");
                    cellElement.SetAttribute("name", $"{(char)('A' + cell.ColumnIndex)}{cell.RowIndex + 1}");

                    // set up BG color
                    XmlElement bgColorElement = xmlDocument.CreateElement("bgcolor");
                    bgColorElement.InnerText = cell.BGColor.ToString("X8");
                    cellElement.AppendChild(bgColorElement);

                    // set up text
                    XmlElement textElement = xmlDocument.CreateElement("text");
                    textElement.InnerText = cell.Text;
                    cellElement.AppendChild(textElement);

                    root.AppendChild(cellElement);
                }
            }

            xmlDocument.Save(stream);
        }

        /// <summary>
        /// Loads the spreadsheet from a file.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        public void LoadSpreadsheet(StreamReader stream)
        {
            this.ClearSpreadsheet();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(stream);
            XmlNodeList? nodeList = xmlDocument.SelectNodes("/spreadsheet/cell");

            if (nodeList != null)
            {
                // for each cell "node" in the cell lists
                foreach (XmlNode node in nodeList)
                {
                    if (node.Attributes != null)
                    {
                        // get the name of the cell
                        var attributeName = node.Attributes["name"];

                        if (attributeName != null)
                        {
                            // get the cell to update
                            string name = attributeName.Value;
                            int colIndex = name[0] - 'A';
                            int rowIndex = int.Parse(name.Substring(1)) - 1;

                            Cell? cell = this.GetCell(rowIndex, colIndex);

                            // if the cell isn't null, update its data
                            if (cell != null)
                            {
                                // input the data
                                foreach (XmlNode childNode in node.ChildNodes)
                                {
                                    switch (childNode.Name)
                                    {
                                        case "text":
                                            cell.Text = childNode.InnerText;
                                            break;
                                        case "bgcolor":
                                            cell.BGColor = uint.Parse(childNode.InnerText, System.Globalization.NumberStyles.HexNumber);
                                            break;

                                        // ignore all other possibilities
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            foreach (TextCell cell in this.cells)
            {
                this.EvaluateCellMaster(cell);
            }

            // clear the undo and redo stacks
            this.redos.Clear();
            this.undos.Clear();
        }

        /// <summary>
        /// Adds an undo collection to the undo stack.
        /// </summary>
        /// <param name="undoRedoCollection">Undo collection.</param>
        public void AddUndo(UndoRedoCollection undoRedoCollection)
        {
            this.undos.Push(undoRedoCollection);
            this.redos.Clear();
        }

        /// <summary>
        /// Adds a redo collection to the undo stack.
        /// </summary>
        /// <param name="undoRedoCollection">Redo collection.</param>
        public void AddRedo(UndoRedoCollection undoRedoCollection)
        {
            this.redos.Push(undoRedoCollection);
        }

        /// <summary>
        /// Gets the next undo command title.
        /// </summary>
        /// <returns>Next command title.</returns>
        public string? GetNextUndoCommand()
        {
            if (this.undos.Count == 0)
            {
                return null;
            }

            return this.undos.Peek().Title;
        }

        /// <summary>
        /// Gets the next redo command title.
        /// </summary>
        /// <returns>Next command title.</returns>
        public string? GetNextRedoCommand()
        {
            if (this.redos.Count == 0)
            {
                return null;
            }

            return this.redos.Peek().Title;
        }

        /// <summary>
        /// Executes an undo on the top of the undo stack.
        /// </summary>
        public void ExecuteUndo()
        {
            if (this.undos.Count > 0)
            {
                UndoRedoCollection toExecute = this.undos.Pop();
                toExecute.Unexecute();
                this.redos.Push(toExecute);
            }
        }

        /// <summary>
        /// Execute a redo on the top of the redo stack.
        /// </summary>
        public void ExecuteRedo()
        {
            if (this.redos.Count > 0)
            {
                UndoRedoCollection toExecute = this.redos.Pop();
                toExecute.Execute();
                this.undos.Push(toExecute);
            }
        }

        /// <summary>
        /// Returns the cell at the given index, or null if no cell exists.
        /// </summary>
        /// <param name="rowIndex">The index of the row.</param>
        /// <param name="colIndex">The index of the column.</param>
        /// <returns>A cell or null.</returns>
        public Cell? GetCell(int rowIndex, int colIndex)
        {
            if (rowIndex < this.cells.GetLength(0) && colIndex < this.cells.GetLength(1))
            {
                if (rowIndex >= 0 && colIndex >= 0)
                {
                    return this.cells[rowIndex, colIndex];
                }
            }

            return null;
        }

        /// <summary>
        /// Resets the spreadsheet with default cells.
        /// </summary>
        public void ClearSpreadsheet()
        {
            // resets all cells in the spreadsheet
            foreach (TextCell cell in this.cells)
            {
                cell.Text = string.Empty;
                cell.BGColor = 0xFFFFFFFF;
            }
        }

        /// <summary>
        /// Cell property changed event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void CellPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is TextCell cell && e.PropertyName == "Text")
            {
                // evaluate the cell
                this.EvaluateCellMaster(cell);

                // update the cell text with events
                this.PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Cell"));
            }
            else if (sender is TextCell refcell && e.PropertyName == "Reference")
            {
                this.EvaluateCell(refcell);
                this.PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Cell"));
            }
            else if (sender is TextCell && e.PropertyName == "BGColor")
            {
                this.PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("BGColor"));
            }
            else if (sender is TextCell badCell && e.PropertyName == "CircRef")
            {
                badCell.Value = "#CIRC_REF";
            }
        }

        /// <summary>
        /// Calls EvaluateCell method after clearing hash set.
        /// </summary>
        /// <param name="cell">Cell to evaluate.</param>
        private void EvaluateCellMaster(TextCell cell)
        {
            // clear the hash set
            this.visitedCells.Clear();
            this.EvaluateCell(cell);
        }

        /// <summary>
        /// Determines what the Value attribute of the cell should be.
        /// </summary>
        /// <param name="cell">Cell to evaluate.</param>
        private void EvaluateCell(TextCell cell)
        {
            // check if the text block starts with =
            if (cell.Text.StartsWith('='))
            {
                // check if the cell has already been visited
                if (this.visitedCells.Contains(cell))
                {
                    cell.Value = "#CIRC_REF";

                    foreach (TextCell badCell in this.visitedCells)
                    {
                        char letter = Convert.ToChar(badCell.ColumnIndex);
                        letter += 'A';

                        string num = (badCell.RowIndex + 1).ToString();

                        string varString = letter + num;

                        if (cell.ExpTree.GetVariableNames().Contains(varString))
                        {
                            badCell.Value = "#CIRC_REF";
                        }
                    }

                    return;
                }
                else
                {
                    this.visitedCells.Add(cell);
                }

                string expression = cell.Text.Substring(1);

                // get the old variables
                List<string> prevVariables = cell.ExpTree.GetVariableNames();

                // remove dependency from each referenced variable
                foreach (string var in prevVariables)
                {
                    int rowIndexInt = 0;
                    int colIndexInt = 0;

                    try
                    {
                        this.GetVariableCoordinates(var, ref rowIndexInt, ref colIndexInt);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                        cell.Value = "#BAD_REF";
                    }

                    // get the cells value we need to load into expTree
                    TextCell? referencedCell = this.GetCell(rowIndexInt, colIndexInt) as TextCell;

                    if (referencedCell != null && referencedCell != cell)
                    {
                        // remove the dependency
                        referencedCell.Dependency -= cell.ReferencedPropertyChanged;
                    }
                }

                // update the expression tree
                try
                {
                    cell.ExpTree.Expression = expression;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    cell.Value = "#OP_ERROR";
                    return;
                }

                // and get the variable names
                List<string> variableNames = cell.ExpTree.GetVariableNames();

                foreach (string var in variableNames)
                {
                    int rowIndexInt = 0;
                    int colIndexInt = 0;

                    try
                    {
                        this.GetVariableCoordinates(var, ref rowIndexInt, ref colIndexInt);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                        cell.Value = "#BAD_REF";
                        return;
                    }

                    // get the cells value we need to load into expTree
                    TextCell? referencedCell = this.GetCell(rowIndexInt, colIndexInt) as TextCell;

                    if (referencedCell != null && referencedCell != cell)
                    {
                        // is the value in the referenced cell a double?
                        if (double.TryParse(referencedCell.Value, out double result))
                        {
                            cell.ExpTree.SetVariable(var, result);
                        }

                        // if its not, assign default value of 0
                        else
                        {
                            cell.ExpTree.SetVariable(var, 0);
                        }

                        // if the referenced cell property changes, we need to notify the
                        // referencing cell that we need an update
                        referencedCell.Dependency += cell.ReferencedPropertyChanged;
                    }

                    // does the cell reference itself?
                    if (referencedCell == cell)
                    {
                        cell.Value = "#SELF_REF";
                        return;
                    }
                }

                try
                {
                    cell.Value = cell.ExpTree.Evaluate().ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    cell.Value = "#BAD_REF";
                }
            }
            else
            {
                cell.Value = cell.Text;
            }
        }

        /// <summary>
        /// Gets the coordinates of a variable respective to the cell it is representing.
        /// </summary>
        /// <param name="var">The variable as a string.</param>
        /// <param name="rowIndexInt">RowIndex as an integer reference.</param>
        /// <param name="colIndexInt">ColIndex as an integer reference.</param>
        private void GetVariableCoordinates(string var, ref int rowIndexInt, ref int colIndexInt)
        {
            string rowIndex = string.Empty;
            string colIndex = string.Empty;

            // we need to get the row and column int indices of the var
            for (int i = 0; i < var.Length; i++)
            {
                if (char.IsLetter(var[i]) && colIndex == string.Empty)
                {
                    colIndex += char.ToUpper(var[0]);
                }
                else if (char.IsDigit(var[i]))
                {
                    rowIndex += var[i];
                }
            }

            // get the row index as an integer
            rowIndexInt = int.Parse(rowIndex) - 1;

            // get the col index as an integer
            for (int i = 0; i < colIndex.Length; i++)
            {
                colIndexInt = (int)colIndex[i] - (int)'A';
            }
        }

        /// <summary>
        /// Generates a 2d array of cells.
        /// </summary>
        /// <param name="numRows">Number of rows.</param>
        /// <param name="numCols">Number of columns.</param>
        /// <returns>A 2d array of cells.</returns>
        private Cell[,] GenerateSpreadsheet(int numRows, int numCols)
        {
            if (numRows > 0 && numCols > 0)
            {
                Cell[,] cells = new Cell[numRows, numCols];
                for (int i = 0; i < numRows; i++)
                {
                    for (int j = 0; j < numCols; j++)
                    {
                        cells[i, j] = new TextCell(i, j, string.Empty);
                        cells[i, j].PropertyChanged += this.CellPropertyChanged;
                    }
                }

                return cells;
            }

            return new Cell[0, 0];
        }

        /// <summary>
        /// Text cell, inhertis from cell.
        /// Allows for setting value in cell.
        /// </summary>
        private class TextCell : Cell
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TextCell"/> class.
            /// </summary>
            /// <param name="rowIndex">Row index.</param>
            /// <param name="colIndex">Col index.</param>
            /// <param name="text">Actual text.</param>
            public TextCell(int rowIndex, int colIndex, string text)
                : base(rowIndex, colIndex, text)
            {
            }

            /// <summary>
            /// ProertyChanged dependency event handler.
            /// </summary>
            public event PropertyChangedEventHandler? Dependency = (sender, e) => { };

            /// <summary>
            /// Gets or Sets value in cell. Override for value setter.
            /// </summary>
            public new string Value
            {
                get => this.value; set
                {
                    this.value = value;

                    // the value has changed, trigger to update any referencing cells
                    if (this.value != "#CIRC_REF")
                    {
                        this.Dependency?.Invoke(this, new PropertyChangedEventArgs("Reference"));
                    }
                }
            }
        }
    }
}
