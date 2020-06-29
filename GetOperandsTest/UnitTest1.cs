using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalcLibrary;

namespace GetOperandsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CalculateTestMethod()
        {
            String[] a = Calc.GetOperands("23+4,5");
            Assert.AreEqual("23", a[0]);
            Assert.AreEqual("4,5", a[1]);
        }

        [TestMethod]
        public void OperationTestMethod()
        {
            String[] a = Calc.GetOperation("-23+4,5+-4.5--23");
            Assert.AreEqual("+", a[0]);
            Assert.AreEqual("+", a[1]);
            Assert.AreEqual("-", a[2]);
        }

        [TestMethod]
        public void ResultTestMethod()
        {
            Assert.AreEqual("27,5", Calc.DoubleOperation["+"](23, 4.5).ToString());
            string result = Calc.DoOperation("23+4,5");
            Assert.AreEqual("27,5", result);
        }
    }
}
