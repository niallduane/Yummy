using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Facebook;
using Yummy.Common.Extensions;


namespace Yummy.Common.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class FacebookPageLikedAttribute : FilterAttribute, IActionFilter
    {
        private const string _signedKey = "signed_request";

        public string AppId  { get; set; }
        public string Secret { get; set; }
        public string Url { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Authorisation request submitted.
            if (!string.IsNullOrEmpty(filterContext.HttpContext.Request["status"])) return;

            if (string.IsNullOrEmpty(filterContext.HttpContext.Request[_signedKey]))
            {
                return;
            }
            else
            {
                bool isLiked = false;

                var client = new FacebookClient();
                client.AppId = GetAppId();
                client.AppSecret = GetSecret();
                dynamic obj = client.ParseSignedRequest(filterContext.HttpContext.Request[_signedKey]);

                if (null != obj.page)
                {
                    isLiked = obj.page.liked;
                }

                if ((isLiked) || (null == obj.page))
                {
                    return;
                }
            }

            if (!string.IsNullOrEmpty(this.Action))
            {
                var route = new RouteValueDictionary()
                {
                    { "action", this.Action}
                };
                if (!string.IsNullOrEmpty(this.Controller))
                {
                    route.Add("controller", this.Controller);
                }

                filterContext.Result = new RedirectToRouteResult(route);
                return;
            }
            else if (!string.IsNullOrEmpty(this.Url))
            {
                filterContext.HttpContext.Response.Redirect(this.Url);
                return;
            }

            throw new Exception("Enter Like Url or Route Data for Facebook Like Attribute.");
        }

        private string GetAppId()
        {
            var appId = System.Web.Configuration.WebConfigurationManager.AppSettings["Facebook.AppId"].ToString();
            return (string.IsNullOrEmpty(this.AppId)) ? appId : this.AppId;
        }

        private string GetSecret()
        {
            var secret = System.Web.Configuration.WebConfigurationManager.AppSettings["Facebook.AppSecret"].ToString();
            return (string.IsNullOrEmpty(this.Secret)) ? secret : this.Secret;
        }
    }
}
