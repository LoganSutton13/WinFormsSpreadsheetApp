// <copyright file="CellChangeBackgroundColor.cs" company="Logan Sutton 11798384">
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
    /// Cell Change Background Color.
    /// </summary>
    public class CellChangeBackgroundColor : IUndoRedoCommand
    {
        private Cell cell;
        private uint oldColor;
        private uint newColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellChangeBackgroundColor"/> class.
        /// </summary>
        /// <param name="cell">Cell.</param>
        /// <param name="newColor">New color.</param>
        public CellChangeBackgroundColor(Cell cell, uint newColor)
        {
            this.cell = cell;
            this.oldColor = cell.BGColor;
            this.newColor = newColor;
        }

        /// <summary>
        /// Execute command.
        /// </summary>
        public void Execute()
        {
            this.cell.BGColor = this.newColor;
        }

        /// <summary>
        /// Unexecute command.
        /// </summary>
        public void Unexecute()
        {
            this.cell.BGColor = this.oldColor;
        }
    }
}
