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
    public class FacebookAuthoriseAttribute : FilterAttribute, IActionFilter
    {
        private const string _signedKey = "signed_request",
            _statusKey = "status";
        public string Permissions { get; set; }

        public string AppId  { get; set; }
        public string Secret { get; set; }
        
        public string ErrorPageAction { get; set; }
        public string ErrorPageController { get; set; }
        public string ErrorPageUrl { get; set; }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!string.IsNullOrEmpty(filterContext.HttpContext.Request[_signedKey]))
            {
                var client = new FacebookClient();
                client.AppId = GetAppId();
                client.AppSecret = GetSecret();
                dynamic obj = client.ParseSignedRequest(filterContext.HttpContext.Request[_signedKey]);

                if (obj.page != null)
                {
                    bool isLiked = false;
                    isLiked = obj.page.liked;
                    if (!isLiked) return;
                }
            }

            if (string.IsNullOrEmpty(filterContext.HttpContext.Request[_statusKey]))
            {
                var resp = Resources.Filters.FacebookAuthorise;
                resp = resp.Replace(new Dictionary<string, string>()
                {
                    { "{{appId}}", this.GetAppId() },
                    { "{{permissions}}", GetPermissions() },
                    { "{{url}}", filterContext.HttpContext.Request.Url.ToString()}
                });
                filterContext.HttpContext.Response.Write(resp);
                filterContext.Result = new EmptyResult();
                return;
            }
            else if (filterContext.HttpContext.Request[_statusKey].Equals("popup-blocked"))
            {
                return;
            }
            else if (!filterContext.HttpContext.Request[_statusKey].Equals("connected"))
            {
                if (!string.IsNullOrEmpty(this.ErrorPageAction))
                {
                    var route = new RouteValueDictionary()
                        {
                            { "action", this.ErrorPageAction}
                        };
                    if (!string.IsNullOrEmpty(this.ErrorPageController))
                    {
                        route.Add("controller", this.ErrorPageController);
                    }

                    filterContext.Result = new RedirectToRouteResult(route);
                }
                else if (!string.IsNullOrEmpty(this.ErrorPageUrl))
                {
                    filterContext.HttpContext.Response.Redirect(this.ErrorPageUrl);
                }
            }

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

        private string GetPermissions()
        {
            var permissions = System.Web.Configuration.WebConfigurationManager.AppSettings["Facebook.Scope"].ToString();
            return (string.IsNullOrEmpty(this.Permissions)) ? permissions : this.Permissions;
        }
    }
}
