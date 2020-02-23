using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Spreadsheet.Model
{
    /// <summary>
    /// a container that stores a collection of spreadsheet command list
    /// </summary>
    public class SpreadsheetCmdCollection
    {
        public IReadOnlyDictionary<string, SpreadsheetCommand> CommandCollection { get; private set; }

        public SpreadsheetCmdCollection(SpreadsheetCommand[] cmdList)
        {
            Dictionary<string, SpreadsheetCommand> cmdCollection = new Dictionary<string, SpreadsheetCommand>();
            foreach (var cmd in cmdList)
            {
                if (cmdCollection.ContainsKey(cmd.CommandKey))
                {
                    throw new DuplicateNameException(string.Format("Duplicated key {0} detected in commands", cmd.CommandKey));
                }

                cmdCollection[cmd.CommandKey] = cmd;
            }

            CommandCollection = new ReadOnlyDictionary<string, SpreadsheetCommand>(cmdCollection);

        }
    }
}
