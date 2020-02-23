using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Spreadsheet.Model
{
    /// <summary>
    /// the command that creates spreadsheet instance
    /// </summary>
    public class CreateSpreadsheetCommand : SpreadsheetCommand
    {
        public CreateSpreadsheetCommand(string cmdKey)
        {
            CommandKey = cmdKey;
        }

        /// <summary>
        /// creates a spreadsheet instance with width x height
        /// </summary>
        /// <param name="input">can be null</param>
        /// <param name="arguments">takes in array of integer: [width, height]</param>
        /// <returns></returns>
        public override SpreadsheetCommandResult Execute(ISheet input, params string[] arguments)
        {
            var result = new SpreadsheetCommandResult();
            int width, height;

            if (arguments.Length != 2)
            {
                result.ErrorMessage = "Invalid Arguments";
                result.Success = false;
            }

            else if (int.TryParse(arguments[0], out width)
                     && int.TryParse(arguments[1], out height))
            {
                if (width >= 1 && height >= 1)
                {
                    result.Spreadsheet = new SimpleSpreadsheet(width, height);
                    result.Success = true;
                }
                else
                {
                    result.ErrorMessage = "width and height must be greater than 0";
                    result.Success = false;
                }

            }
            else
            {
                result.ErrorMessage = "width and height must be integers greater than 0";
                result.Success = false;
            }


            return result;
        }
    }
}
