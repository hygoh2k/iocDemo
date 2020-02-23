using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Spreadsheet.Model
{

    /// <summary>
    /// spreadsheet command that add/update value in specified cell
    /// </summary>
    public class UpdateSpreadsheetCommand : SpreadsheetCommand
    {
        public UpdateSpreadsheetCommand(string cmdKey)
        {
            CommandKey = cmdKey;
        }

        /// <summary>
        /// update the spreadsheet with specific x/y coordinate
        /// </summary>
        /// <param name="input">input spreadsheet</param>
        /// <param name="arguments">x/y coordinate and value: [x,y,value]</param>
        /// <returns>updated spreadsheet object, error code, execution status</returns>
        public override SpreadsheetCommandResult Execute(ISheet input, params string[] arguments)
        {
            var result = new SpreadsheetCommandResult();
            int x, y;
            float value;

            if (arguments.Length != 3)
            {
                result.ErrorMessage = "Invalid Arguments";
                result.Success = false;
            }
            else if (input == null)
            {
                result.ErrorMessage = "No Spreadsheet is provided";
                result.Success = false;
            }
            else if (int.TryParse(arguments[0], out x)
                     && int.TryParse(arguments[1], out y)
                     && float.TryParse(arguments[2], out value)
            )
            {
                if (!input.IsWithinRange(x, y))
                {
                    result.ErrorMessage = "Index out of range";
                    result.Success = false;
                }
                else
                {
                    input[x, y] = value.ToString();
                    result.Spreadsheet = input;
                    result.Success = true;
                }


            }
            else
            {
                result.ErrorMessage = "X and Y must be integers, Value must be float value";
                result.Success = false;
            }


            return result;
        }
    }
}