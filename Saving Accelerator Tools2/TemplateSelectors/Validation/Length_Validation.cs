using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace Saving_Accelerator_Tools2.TemplateSelectors.Validation
{
    public class Length_Validation : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public Length_Validation()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString().Length < Max && value.ToString().Length > Min)
            {
                return new ValidationResult(false, $"To short");
            }
            return ValidationResult.ValidResult;
        }
    }
}
