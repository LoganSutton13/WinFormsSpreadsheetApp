// <copyright file="UndoRedoCollection.cs" company="Logan Sutton 11798384">
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
    /// UndoRedoCollection class.
    /// </summary>
    public class UndoRedoCollection
    {
        private List<IUndoRedoCommand> commands;

        private string title;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoRedoCollection"/> class.
        /// </summary>
        /// <param name="title">Title of the command.</param>
        public UndoRedoCollection(string title)
        {
            this.commands = new List<IUndoRedoCommand>();
            this.title = title;
        }

        /// <summary>
        /// Gets the title of the command collection.
        /// </summary>
        public string Title
        {
            get { return this.title; }
        }

        /// <summary>
        /// Adds a command to the list.
        /// </summary>
        /// <param name="command">Command.</param>
        public void AddCommand(IUndoRedoCommand command)
        {
            this.commands.Add(command);
        }

        /// <summary>
        /// Execute all commands in list.
        /// </summary>
        public void Execute()
        {
            foreach (var command in this.commands)
            {
                command.Execute();
            }
        }

        /// <summary>
        /// Unexecute all commands in list.
        /// </summary>
        public void Unexecute()
        {
            for (int i = this.commands.Count - 1; i >= 0; i--)
            {
                this.commands[i].Unexecute();
            }
        }
    }
}
