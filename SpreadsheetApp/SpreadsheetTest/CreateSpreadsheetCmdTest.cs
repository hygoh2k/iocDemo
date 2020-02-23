using System;
using Common.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadsheet.Model;

namespace SpreadsheetTest
{
    [TestClass]
    public class CreateSpreadsheetCmdTest
    {
        [TestMethod]
        public void CreateSpreadsheetSimpleTest()
        {
            SpreadsheetCommand cmd = new CreateSpreadsheetCommand("");
            var result = cmd.Execute(null, "3", "4");
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Spreadsheet.Width, 3);
            Assert.AreEqual(result.Spreadsheet.Height, 4);
            Assert.IsInstanceOfType(result.Spreadsheet, typeof(SimpleSpreadsheet));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void CreateSpreadsheetVerifyFailTest()
        {
            SpreadsheetCommand cmd = new CreateSpreadsheetCommand("");
            var result = cmd.Execute(null, "0", "0");
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void CreateSpreadsheetVerifyNegativeFailTest()
        {
            SpreadsheetCommand cmd = new CreateSpreadsheetCommand("");
            var result = cmd.Execute(null, "-1", "-2");
            Assert.IsFalse(result.Success);
        }


        /// <summary>
        /// added invalid argument test
        /// </summary>
        [TestMethod]
        public void CreateSpreadsheetInvalidArgumentTest()
        {
            SpreadsheetCommand cmd = new CreateSpreadsheetCommand("");
            var result = cmd.Execute(null, "3", "4", "5");
            Assert.IsFalse(result.Success);
        }
    }
}
