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
    /// A spread sheet model that contains cells collection
    /// 
    /// </summary>
    public class SimpleSpreadsheet : ISheet
    {
        //collection of cells that mapped with unique keys
        private IDictionary<int, object> _cells;
        
        /// <summary>
        /// width of the sheet
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// height of the sheet
        /// </summary>
        public int Height { get; private set; }

        public SimpleSpreadsheet(int width, int height)
        {
            _cells = new Dictionary<int, object>();
            Width = width;
            Height = height;
        }


        /// <summary>
        /// Generate a unique hash based on 2 integer values
        /// http://en.wikipedia.org/wiki/pairing_function
        /// </summary>
        /// <param name="x">first value</param>
        /// <param name="y">second value</param>
        /// <returns></returns>
        private int CantorPairing(int x, int y)
        {
            return (int)(0.5 * ((x + y) * (x + y + 1)) + y);
        }

        /// <summary>
        /// check fs the cell in X Y coordinate contains value
        /// </summary>
        /// <param name="x">x axis value</param>
        /// <param name="y">y axis value</param>
        /// <returns></returns>
        public bool HasValue(int x, int y)
        {
            return _cells.ContainsKey(CantorPairing(x, y));
        }

        /// <summary>
        /// check if x y coordinate is within the range
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsWithinRange(int x, int y)
        {
            return (y < this.Height && x < this.Width);
        }


        /// <summary>
        /// get/set value in spreadsheet based on x/y
        /// </summary>
        /// <param name="x">x axis value</param>
        /// <param name="y">y axis value</param>
        /// <returns></returns>
        public object this[int x, int y]
        {
            get
            {
                if (!IsWithinRange(x, y))
                    throw new IndexOutOfRangeException();

                int hash = CantorPairing(x, y);
                return _cells.ContainsKey(hash) ? _cells[hash] : 0;
            }


            set
            {
                if (!IsWithinRange(x, y))
                    throw new IndexOutOfRangeException();

                _cells[CantorPairing(x, y)] = value;
            }
        }

    }

}
