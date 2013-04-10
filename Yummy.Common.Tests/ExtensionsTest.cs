using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Yummy.Common.Extensions;

namespace Gummy.Common.Tests
{
    [TestClass]
    public class ExtensionsTest
    {
        [TestMethod]
        public void DateTimeStartOfWeek()
        {
            DateTime dt = new DateTime(2013,2,1);
            var start = dt.StartOfWeek(DayOfWeek.Monday);
            Assert.AreEqual(new DateTime(2013, 1, 28), start);
        }

        [TestMethod]
        public void DateTimeEndOfWeek()
        {
            DateTime dt = new DateTime(2013, 2, 1);
            var end = dt.EndOfWeek(DayOfWeek.Monday);
            Assert.AreEqual(new DateTime(2013, 2, 5), end);
        }

        [TestMethod]
        public void FirstLetterIsVowel()
        {
            string s = "test";
            Assert.IsFalse(s.FirstLetterIsVowel());

            s = "also";
            Assert.IsTrue(s.FirstLetterIsVowel());
        }
    }
}
