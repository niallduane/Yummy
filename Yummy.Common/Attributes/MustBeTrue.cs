using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Yummy.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MustBeTrue : RequiredAttribute, IClientValidatable
    {
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = this.FormatErrorMessage(metadata.DisplayName),
                ValidationType = "MustBeTrue"
            };
            yield return rule;
        }

        public override bool IsValid(object value)
        {
            return value != null && value is bool && (bool)value;
        }
    }
}
