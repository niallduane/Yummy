using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Yummy.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MaxFileSize : ValidationAttribute, IClientValidatable
    {
        private int _bytes = 3145728; //3Mb

        public MaxFileSize() { }

        public MaxFileSize(int bytes)
        {
            _bytes = bytes;
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file != null)
            {
                return file.ContentLength <= _bytes;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(metadata.DisplayName),
                ValidationType = "maxfilesize"
            };
            rule.ValidationParameters["bytes"] = this._bytes;
            yield return rule;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, FormatFileSize(_bytes));
        }

        private string FormatFileSize(int bytes)
        {
            if ((double)bytes / (double)(1024 * 1024) > 1)
            {
                return string.Format("{0:0.##}MB", ((double)bytes / (double)(1024 * 1024)));
            }

            if ((double)bytes / (double)1024 > 1)
            {
                return string.Format("{0:0.##}KB", ((double)bytes / (double)1024));
            }

            return string.Format("{0}B", bytes);
        }
    }
}
