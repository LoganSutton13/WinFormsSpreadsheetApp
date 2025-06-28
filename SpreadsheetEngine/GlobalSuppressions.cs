// <copyright file="GlobalSuppressions.cs" company="Logan Sutton 11798384">
// Copyright (c) Logan Sutton 11798384. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "Do not need to prefix handler with this.", Scope = "member", Target = "~M:SpreadsheetEngine.Spreadsheet.GenerateSpreadsheet(System.Int32,System.Int32)~SpreadsheetEngine.Cell[,]")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "This field needs to be protected.", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.value")]