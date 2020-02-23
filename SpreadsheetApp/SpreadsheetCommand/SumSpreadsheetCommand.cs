using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Model;

    namespace Spreadsheet.Model
    {

        /// <summary>
        /// Sum up the cell values in the specified range
        /// </summary>
        public class SumSpreadsheetCommand : SpreadsheetCommand
        {
            public SumSpreadsheetCommand(string cmdKey)
            {
                CommandKey = cmdKey;
            }

            /// <summary>
            /// Sum up the cell values in the specified range
            /// </summary>
            /// <param name="input">input spreadsheet</param>
            /// <param name="arguments">calculate the sum from cells (fromX, fromY) to (toX, toY)
            /// value will be updated to the cell (targetX, targetY)
            /// [fromX, fromY, toX, toY, targetX, targetY]
            /// </param>
            /// <returns></returns>
            public override SpreadsheetCommandResult Execute(ISheet input, params string[] arguments)
            {
                var result = new SpreadsheetCommandResult();

                int fromX, fromY, toX, toY, targetX, targetY;

                result.Success = false;

                //start the checking
                if (arguments.Length != 6)
                {
                    result.ErrorMessage = "argument must be 6 integers";

                }
                else if (!int.TryParse(arguments[0], out fromX))
                {
                    result.ErrorMessage = "fromX must a integer value";
                }
                else if (!int.TryParse(arguments[1], out fromY))
                {
                    result.ErrorMessage = "fromY must a integer values";
                }
                else if (!int.TryParse(arguments[2], out toX))
                {
                    result.ErrorMessage = "toX must a integer value";
                }
                else if (!int.TryParse(arguments[3], out toY))
                {
                    result.ErrorMessage = "toY must a integer value";
                }
                else if (!int.TryParse(arguments[4], out targetX))
                {
                    result.ErrorMessage = "targetX must a integer value";
                }
                else if (!int.TryParse(arguments[5], out targetY))
                {
                    result.ErrorMessage = "targetY must a integer value";
                }
                else if (fromX < 0 || fromY < 0 || toX >= input.Width || toY >= input.Height || targetX < 0 || targetY < 0 || targetX >= input.Width || targetY >= input.Height)
                {
                    result.ErrorMessage = "selectionout of range";
                }
                else
                {
                    //passes all validation
                    //execute the Sum operation
                    result.Success = true;
                    float sum = 0f;
                    for (int x = fromX; x <= toX; x++)
                    {
                        for (int y = fromY; y <= toY; y++)
                        {
                            float value;
                            if (input.HasValue(x, y) 
                                && float.TryParse(input[x, y].ToString(), out value))
                            {
                                sum += value;
                            }
                        }

                    }

                    input[targetX, targetY] = sum.ToString();
                    result.Spreadsheet = input;
                }
                return result;
            }
        }
    }

}
