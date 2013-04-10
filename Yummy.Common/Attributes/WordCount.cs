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
    public class WordCount : ValidationAttribute, IClientValidatable
    {
        public int WordLimit { get; set; }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule()
            {
                ErrorMessage = this.FormatErrorMessage(metadata.DisplayName),
                ValidationType = "WordCount"
            };
        }

        public override bool IsValid(object value)
        {
            WordLimit = (WordLimit == 0) ? 25 : WordLimit;

            MatchCollection collection = Regex.Matches(value.ToString(), @"[\S]+");
            return (collection.Count > WordLimit) ? false : true;
        }
    }
}
