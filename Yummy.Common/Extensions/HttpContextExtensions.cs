using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Yummy.Common.Extensions
{
    public static class HttpContextExtensions
    {
        public static bool IsMobile(this HttpRequestBase context)
        {
            return (context.Browser.IsMobileDevice || context.UserAgent.IndexOf(
                "android", StringComparison.OrdinalIgnoreCase) >= 0 || context.UserAgent.IndexOf(
                "nokia", StringComparison.OrdinalIgnoreCase) >= 0 || context.UserAgent.IndexOf(
                "iphone", StringComparison.OrdinalIgnoreCase) >= 0);   
        }
    }
}
