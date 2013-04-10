using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace Yummy.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class Price : ValidationAttribute
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public override bool IsValid(object value)
        {
            if (MaxPrice == 0)
            {
                MaxPrice = 10000.00M;
            } 

            var price = (decimal)value;
            if (price < this.MinPrice || price > this.MaxPrice)
                return false;

            return true;
        }
    }
}
