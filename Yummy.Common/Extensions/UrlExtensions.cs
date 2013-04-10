using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;

namespace Yummy.Common.Extensions
{
    public static class UrlExtensions
    {
        private const string _signedRequestKey = "signed_request";

        public static string FacebookAction(this UrlHelper urlHelper, string action)
        {
            return urlHelper.Action(action, new { signed_request = UrlExtensions.SignedRequest });
        }

        public static string FacebookAction(this UrlHelper urlHelper, string action, string controller = "", string area = "")
        {
            return urlHelper.Action(action, new { area = area, controller = controller, signed_request = UrlExtensions.SignedRequest });
        }

        public static string FacebookAction(this UrlHelper urlHelper, string action, object routeValues)
        {
            string url = urlHelper.Action(action, routeValues);

            if (!url.Contains(_signedRequestKey))
            {
                string signedRequest = UrlExtensions.SignedRequest;
                if (!string.IsNullOrEmpty(signedRequest))
                {
                    return (url.Contains('?')) ?
                        string.Format("{0}&{1}={2}", url, _signedRequestKey, signedRequest) :
                        string.Format("{0}?{1}={2}", url, _signedRequestKey, signedRequest);
                }
            }
            return url;
        }

        private static string SignedRequest
        {
            get
            {
                var ret = HttpContext.Current.Request[_signedRequestKey];
                return ret; 
            }
        }
    }
}