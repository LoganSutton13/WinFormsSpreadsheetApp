// <copyright file="IUndoRedoCommand.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// Interface IUndoRedoCommand.
    /// </summary>
    public interface IUndoRedoCommand
    {
        /// <summary>
        /// Execute command.
        /// </summary>
        void Execute();

        /// <summary>
        /// Unexecute command.
        /// </summary>
        void Unexecute();
    }
}
