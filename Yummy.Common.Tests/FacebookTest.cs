using System;
using Yummy.Common.SocialMedia.Facebook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Goby.Web.Custom.Common.Tests
{
    [TestClass]
    public class FacebookTest
    {
        [TestMethod]
        public void CreateLoginUrl()
        {
            var facebook = new Client("1234564");
            string url = facebook.CreateLoginUrl("http://localhost", "email, about_me");
            Debug.WriteLine(url);
            Assert.IsNotNull(url);
        }
    }
}
