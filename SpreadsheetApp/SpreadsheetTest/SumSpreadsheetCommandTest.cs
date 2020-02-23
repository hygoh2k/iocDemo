using System;
using System.Collections.Generic;
using System.Linq;
using Common.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadsheet.Model;
using Spreadsheet.Model.Spreadsheet.Model;

namespace SpreadsheetTest
{
    [TestClass]
    public class SumSpreadsheetCommandTest
    {
        /// <summary>
        /// a simple mock of 3x5 sheet
        /// </summary>
        class MockSheet_3x5 : ISheet
        {
            private List<Tuple<int, int, object>> Values { get; set; }
            public int Width => 3;
            public int Height => 5;

            public MockSheet_3x5()
            {
                this.Values = new List<Tuple<int, int, object>>();
            }


            public bool HasValue(int x, int y)
            {
                return this.Values.Any(item => item.Item1 == x && item.Item2 == y);
            }

            public bool IsWithinRange(int x, int y)
            {
                return this.Width > x && this.Height > y;
            }

            public object this[int x, int y]
            {
                get
                {
                    return this.Values.FirstOrDefault(v => v.Item1 == x && v.Item2 == y).Item3;
                }
                set
                {
                    var foundVal = this.Values.FirstOrDefault(v => v.Item1 == x && v.Item2 == y);

                    if (foundVal != null)
                        this.Values.Remove(foundVal);

                    this.Values.Add(new Tuple<int, int, object>(x, y, value));
                }
            }
        }


        /// <summary>
        /// basic sum test
        /// </summary>
        [TestMethod]
        public void BasicSumTest()
        {
            SumSpreadsheetCommand cmd = new SumSpreadsheetCommand("");
            var mockSheet = new MockSheet_3x5();
            mockSheet[0, 0] = 1;
            mockSheet[0, 1] = 2;
            mockSheet[1, 1] = 3;
            var result = cmd.Execute(mockSheet, "0", "0", "1","1","2","2");
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Spreadsheet[2, 2], "6");
        }

        /// <summary>
        /// out of range test
        /// </summary>
        [TestMethod]
        public void SumOutOfRangeTest()
        {
            SumSpreadsheetCommand cmd = new SumSpreadsheetCommand("");
            var mockSheet = new MockSheet_3x5();
            mockSheet[0, 0] = 1;
            mockSheet[0, 1] = 2;
            mockSheet[1, 1] = 3;
            var result = cmd.Execute(mockSheet, "0", "0", "1", "1", "3", "5");
            Assert.IsFalse(result.Success);
        }

        /// <summary>
        /// invalid argument test
        /// </summary>
        [TestMethod]
        public void SumInvalidArgumentTest()
        {
            SumSpreadsheetCommand cmd = new SumSpreadsheetCommand("");
            var mockSheet = new MockSheet_3x5();
            mockSheet[0, 0] = 1;
            mockSheet[0, 1] = 2;
            mockSheet[1, 1] = 3;
            var result = cmd.Execute(mockSheet, "0", "0", "1", "1");
            Assert.IsFalse(result.Success);
        }

    }
}
