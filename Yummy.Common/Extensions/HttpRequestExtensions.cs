using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Yummy.Common.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetServerUrl(this HttpRequestBase request)
        {
            return request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath;
        }

        public static string GetServerUrl(this HttpRequest request)
        {
            return request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath;
        }
    }
}
