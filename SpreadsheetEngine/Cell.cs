// <copyright file="Cell.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;
    using System.Runtime.InteropServices.Marshalling;

    /// <summary>
    /// Abstract cell class.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// Value associated with the cell.
        /// </summary>
        protected string value;

        /// <summary>
        /// Represents the row index the cell was inserted into.
        /// </summary>
        private int rowIndex;

        /// <summary>
        /// Represents the column index the cell was inserted into.
        /// </summary>
        private int columnIndex;

        /// <summary>
        /// Text set by the user.
        /// </summary>
        private string text;

        /// <summary>
        /// BG color for cell.
        /// </summary>
        private uint bgColor;

        /// <summary>
        /// Expression tree.
        /// </summary>
        private ExpressionTree.ExpressionTree expTree = new ExpressionTree.ExpressionTree(string.Empty);

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowIndex"> Row index.</param>
        /// <param name="colIndex"> Column index.</param>
        /// <param name="text"> Text.</param>
        public Cell(int rowIndex, int colIndex, string text)
        {
            this.rowIndex = rowIndex;
            this.columnIndex = colIndex;
            this.bgColor = 0xFFFFFFFF;
            if (text != null)
            {
                this.text = text;
            }
            else
            {
                this.text = string.Empty;
            }

            this.value = this.text;
        }

        /// <summary>
        /// ProertyChanged event handler.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets the row index the cell was inserted into.
        /// </summary>
        public int RowIndex { get => this.rowIndex; }

        /// <summary>
        /// Gets the column index the cell was inserted into.
        /// </summary>
        public int ColumnIndex { get => this.columnIndex; }

        /// <summary>
        /// Gets expression tree.
        /// </summary>
        public ExpressionTree.ExpressionTree ExpTree { get => this.expTree; }

        /// <summary>
        /// Gets or sets the string of text associated with each cell.
        /// </summary>
        public string Text
        {
            get => this.text; set
            {
                // NOTE: formerly did not update text if it == value. However,
                // doing this now to trigger property changed event, in the
                // case that the user clicks to update the cell but doesn't
                // change anything.
                if (value == null && this.text != value)
                {
                    return;
                }
                else
                {
                    this.text = value;

                    // The property has changed, notify observers.
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
                }
            }
        }

        /// <summary>
        /// Gets or sets bgColor field.
        /// </summary>
        public uint BGColor
        {
            get => this.bgColor; set
            {
                if (this.BGColor != value)
                {
                    this.bgColor = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BGColor"));
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Gets the value associated with the cell.
        /// If not a formula, it is the same as text.
        /// </summary>
        public string Value
        {
            get => this.value;
        }

        /// <summary>
        /// Referenced property changed.
        /// Invokes Property changed for current cell.
        /// </summary>
        /// <param name="sender"> Sender.</param>
        /// <param name="e">Args.</param>
        public void ReferencedPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // was "Text" before
            this.PropertyChanged?.Invoke(this, e);
        }
    }
}
