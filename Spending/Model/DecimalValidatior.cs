using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EntityModels
{
    public class DecimalValidatior : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            decimal data;
            bool parsed = decimal.TryParse((string)value, out data);

            if (!parsed)
                return false;

            return true;
        }
    }
}