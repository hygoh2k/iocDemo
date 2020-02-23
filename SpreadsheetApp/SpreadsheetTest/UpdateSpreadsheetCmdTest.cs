using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Common.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Spreadsheet.Model;

namespace SpreadsheetTest
{
    [TestClass]
    public class UpdateSpreadsheetCmdTest
    {
        /// <summary>
        /// a simple mock of 3x5 sheet
        /// </summary>
        class MockSheet_3x5 : ISheet
        {
            private List<Tuple<int,int,object>> Values { get; set; }
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

            public object this[int x, int y] {
                get
                {
                    return this.Values.FirstOrDefault(v => v.Item1 == x && v.Item2 == y).Item3;
                }
                set
                {
                    var foundVal = this.Values.FirstOrDefault(v => v.Item1 == x && v.Item2 == y);

                    if (foundVal != null)
                        this.Values.Remove(foundVal);

                    this.Values.Add(new Tuple<int, int, object>(x,y,value));
                }
            }
        }


        /// <summary>
        /// check if the value is updated according to x y coordinate
        /// </summary>
        [TestMethod]
        public void UpdateSpreadsheetSimpleTest()
        {
            UpdateSpreadsheetCommand cmd = new UpdateSpreadsheetCommand("");
            var result = cmd.Execute(new MockSheet_3x5(),"1", "2", "3");
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Spreadsheet[1,2],"3");
        }

        /// <summary>
        /// test if the command returns fail for out of range 
        /// </summary>
        [TestMethod]
        public void UpdateSpreadsheetOutOfRangeTest()
        {
            UpdateSpreadsheetCommand cmd = new UpdateSpreadsheetCommand("");
            var result = cmd.Execute(new MockSheet_3x5(), "3", "3", "3");
            Assert.IsFalse(result.Success);
        }

        /// <summary>
        /// added invalid argument test
        /// </summary>
        [TestMethod]
        public void UpdateSpreadInvalidArgumentTest()
        {
            UpdateSpreadsheetCommand cmd = new UpdateSpreadsheetCommand("");
            var result = cmd.Execute(null, "3", "4", "5", "6");
            Assert.IsFalse(result.Success);
        }

    }
}
