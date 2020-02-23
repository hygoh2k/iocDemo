using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    /// <summary>
    /// a basic spreadsheet command result
    /// </summary>
    public class SpreadsheetCommandResult : ICommandResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public ISheet Spreadsheet { get; set; }
    }

    /// <summary>
    /// a base class for spreadsheet command
    /// </summary>
    public abstract class SpreadsheetCommand : ICommand<SpreadsheetCommandResult, ISheet>
    {
        public string CommandKey { get; set; }
        public abstract SpreadsheetCommandResult Execute(ISheet input, params string[] arguments);
    }
}
