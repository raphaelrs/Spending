using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EntityModels
{
    public class TimeSpamValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (((TimeSpan)value).Days > 0)
                {
                    return false;
                } 
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
