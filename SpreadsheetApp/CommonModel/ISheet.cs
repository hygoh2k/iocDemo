using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    /// <summary>
    /// interface that represents the Spreadsheet
    /// </summary>
    public interface ISheet
    {
        int Width { get; }
        int Height { get; }
        bool HasValue(int x, int y);
        bool IsWithinRange(int x, int y);
        object this[int x, int y] { get; set; }
    }
}