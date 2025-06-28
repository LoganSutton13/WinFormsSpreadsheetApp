// <copyright file="CellChangeText.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Cell Change Text command.
    /// </summary>
    public class CellChangeText : IUndoRedoCommand
    {
        private Cell cell;
        private string oldText;
        private string newText;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellChangeText"/> class.
        /// </summary>
        /// <param name="cell">Cell.</param>
        /// <param name="newText">newText.</param>
        public CellChangeText(Cell cell, string newText)
        {
            this.cell = cell;
            this.oldText = cell.Text;
            this.newText = newText;
        }

        /// <summary>
        /// Execute command.
        /// </summary>
        public void Execute()
        {
            this.cell.Text = this.newText;
        }

        /// <summary>
        /// Unexecute command.
        /// </summary>
        public void Unexecute()
        {
            this.cell.Text = this.oldText;
        }
    }
}
