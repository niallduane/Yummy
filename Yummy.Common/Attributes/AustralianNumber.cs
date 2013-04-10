using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Yummy.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class AustralianNumber : ValidationAttribute, IClientValidatable
    {
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule()
            {
                ErrorMessage = this.FormatErrorMessage(metadata.DisplayName),
                ValidationType = "AustralianNumber"
            };
        }

        public override bool IsValid(object value)
        {
            string valueString = value.ToString();
            valueString = Regex.Replace(valueString, @"\s", string.Empty);

            return ((valueString.Substring(0, 1) == "0" || valueString.Substring(0, 1) == "6") && valueString.Length >= 10);
        }
    }
}
