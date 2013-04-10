using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Yummy.Common.Encryption;

namespace Gummy.Common.Tests
{
    [TestClass]
    public class EncryptionTest
    {
        [TestMethod]
        public void StringEncypt1()
        {
            string i = "test";
            string hash = i.Encrypt();

            Assert.AreNotEqual(hash, ("test1").Encrypt());

            Assert.AreEqual(hash, ("test").Encrypt());
        }

        [TestMethod]
        public void StringEncypt2()
        {
            string i = "test", salt = "mySalt";
            string hash = i.Encrypt(salt);

            Assert.AreNotEqual(hash, ("test1").Encrypt(salt));

            Assert.AreNotEqual(hash, i.Encrypt(""));

            Assert.AreEqual(hash, ("test").Encrypt(salt));
        }
    }
}
