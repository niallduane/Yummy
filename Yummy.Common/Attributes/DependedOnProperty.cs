using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Yummy.Common.Attributes
{
    /// <summary>
    /// Use when another property/field should be filled before validation occurs on property.
    /// </summary>
    public class DependedOnProperty : ValidationAttribute, IClientValidatable
    {
        private string _dependantPropertyName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependantPropertyName"></param>
        public DependedOnProperty(string dependantPropertyName)
        {
            _dependantPropertyName = dependantPropertyName;
        }

        /// <summary>
        /// field is required if another field is populated
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (FieldIsRequired(validationContext))
            {
                if (string.IsNullOrEmpty(value as string))
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Get date to compare with
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        private bool FieldIsRequired(ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_dependantPropertyName);
            if (propertyInfo != null)
            {
                var secondValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);
                return !string.IsNullOrEmpty(secondValue as string);
            }
            return false;
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
                ValidationType = "dependantrequired"
            };
            rule.ValidationParameters["dependantproperty"] = this._dependantPropertyName;
            yield return rule;
        }
    }
}
