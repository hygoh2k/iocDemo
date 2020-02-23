using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public interface ISheetPrinter
    {
        void PrintContent(ISheet spreadsheet, TextWriter tw);
    }
}
