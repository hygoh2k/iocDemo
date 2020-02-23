using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadsheet.Model;

namespace SpreadsheetTest
{
    [TestClass]
    public class SimpleSpreadsheetTest
    {
        /// <summary>
        /// normal update test
        /// </summary>
        [TestMethod]
        public void TestNormalUpdate()
        {
            var sheet = new SimpleSpreadsheet(2, 3);
            sheet[0, 0] = 1;
            sheet[0, 1] = 2;
            Assert.AreEqual(sheet[0, 0], 1);
            Assert.AreEqual(sheet[0, 1], 2);
            Assert.AreEqual(sheet[0, 2], 0);

            Assert.AreEqual(sheet[1, 0], 0);
            Assert.AreEqual(sheet[1, 1], 0);
            Assert.AreEqual(sheet[1, 2], 0);

        }

        /// <summary>
        /// index out of range test
        /// </summary>
        [TestMethod]
        public void TestIndexOutOfRange()
        {
            var sheet = new SimpleSpreadsheet(2, 3);
            sheet[0, 0] = 1;
            sheet[0, 1] = 2;

            try
            {
                var result = sheet[2, 0];
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is IndexOutOfRangeException);
            }

            try
            {
                var result = sheet[0, 3];
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is IndexOutOfRangeException);
            }

        }
    }
}
