using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PineApple;

namespace MissionTests
{
    [TestClass]
    public class OperatorsTest
    {
        // Tests sur l'opérateur < de MDate
        [TestMethod]
        public void OperatorIsStrictlyLessThan_WithALessThanB()
        {
            MDate A = new MDate(14, 16, 20);
            MDate B = new MDate(21, 14, 20);
            bool actual = A < B;
            Assert.IsTrue(actual, "Date A is not strictly before date B");
        }
        [TestMethod]
        public void OperatorIsStrictlyLessThan_WithAEqualsB()
        {
            MDate A = new MDate(151, 14, 30);
            MDate B = new MDate(151, 14, 30);
            bool actual = A < B;
            Assert.IsFalse(actual, "Date A is not strictly before date B");
        }
        [TestMethod]
        public void OperatorIsStrictlyLessThan_WithAGreaterThanB()
        {
            MDate A = new MDate(230, 7, 0);
            MDate B = new MDate(229, 2, 50);
            bool actual = A < B;
            Assert.IsFalse(actual, "Date A is not strictly before date B");
        }




        // Tests sur l'opérateur > de MDate
        [TestMethod]
        public void OperatorIsStrictlyGreaterThan_WithALessThanB()
        {
            MDate A = new MDate(13, 14, 0);
            MDate B = new MDate(25, 8, 10);
            bool actual = A > B;
            Assert.IsFalse(actual, "Date A is not strictly after date B");
        }
        [TestMethod]
        public void OperatorIsStrictlyGreaterThan_WithAEqualsB()
        {
            MDate A = new MDate(1, 15, 30);
            MDate B = new MDate(1, 15, 30);
            bool actual = A > B;
            Assert.IsFalse(actual, "Date A is not strictly after date B");
        }
        [TestMethod]
        public void OperatorIsStrictlyGreaterThan_WithAGreaterThanB()
        {
            MDate A = new MDate(400, 7, 0);
            MDate B = new MDate(200, 17, 0);
            bool actual = A > B;
            Assert.IsTrue(actual, "Date A is not strictly after date B");
        }




        // Tests sur l'opérateur >= de MDate
        [TestMethod]
        public void OperatorIsGreaterThan_WithALessThanB()
        {
            MDate A = new MDate(2, 17, 0);
            MDate B = new MDate(2, 18, 0);
            bool actual = A >= B;
            Assert.IsFalse(actual, "Date A is not after date B");
        }
        [TestMethod]
        public void OperatorIsGreaterThan_WithAEqualsB()
        {
            MDate A = new MDate(314, 16, 0);
            MDate B = new MDate(314, 16, 0);
            bool actual = A >= B;
            Assert.IsTrue(actual, "Date A is not after date B");
        }
        [TestMethod]
        public void OperatorIsGreaterThan_WithAGreaterThanB()
        {
            MDate A = new MDate(450, 2, 0);
            MDate B = new MDate(449, 4, 0);
            bool actual = A >= B;
            Assert.IsTrue(actual, "Date A is not after date B");
        }




        // Tests sur l'opérateur <= de MDate
        [TestMethod]
        public void OperatorIsLessThan_WithALessThanB()
        {
            MDate A = new MDate(189, 5, 40);
            MDate B = new MDate(212, 12, 0);
            bool actual = A <= B;
            Assert.IsTrue(actual, "Date A is not before date B");
        }
        [TestMethod]
        public void OperatorIsLessThan_WithAEqualsB()
        {
            MDate A = new MDate(77, 1, 10);
            MDate B = new MDate(77, 1, 10);
            bool actual = A <= B;
            Assert.IsTrue(actual, "Date A is not before date B");
        }
        [TestMethod]
        public void OperatorIsLessThan_WithAGreaterThanB()
        {
            MDate A = new MDate(499, 19, 30);
            MDate B = new MDate(1, 7, 0);
            bool actual = A <= B;
            Assert.IsFalse(actual, "Date A is not before date B");
        }
    }
}
