using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Spreadsheet.Model
{
    /// <summary>
    /// this is the text printer class that helps to print out spreadsheet
    /// </summary>
    public class SpreadsheetPrinter : ISheetPrinter
    {

        public void PrintContent(ISheet spreadsheet, TextWriter tw)
        {
            const int cellWidth = 4;

            if (tw == null)
                return;

            for (int h = 0; h < spreadsheet.Width; h++)
            {
                tw.Write(h.ToString());
                int offset = cellWidth - h.ToString().Length;
                for (int c = 0; c < offset; c++)
                    tw.Write(" ");
            }
            tw.WriteLine();

            for (int h = 0; h < spreadsheet.Width * cellWidth; h++)
            {
                tw.Write("=");
            }

            tw.WriteLine();
            for (int y = 0; y < spreadsheet.Height; y++)
            {
                for (int x = 0; x < spreadsheet.Width; x++)
                {
                    if (spreadsheet.HasValue(x, y))
                    {
                        string value = spreadsheet[x, y].ToString();
                        tw.Write(value);
                        int offset = cellWidth - value.Length;
                        for (int c = 0; c < offset; c++)
                            tw.Write(" ");
                    }
                    else
                    {
                        for (int c = 0; c < cellWidth; c++)
                            tw.Write(" ");
                    }
                }

                tw.Write("\n");

                for (int h = 0; h < spreadsheet.Width * cellWidth; h++)
                {
                    tw.Write("-");
                }

                tw.Write("\n");
            }
        }
    }
}
