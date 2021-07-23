using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace Saving_Accelerator_Tools2.TemplateSelectors.Validation
{
    public class ANCValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return ValidationResult.ValidResult;

            return value.ToString().Length > 0 && value.ToString().Length < 9
                ? new ValidationResult(false, $"To short ANC number. This ANC have only {value.ToString().Length} characters")
                : ValidationResult.ValidResult;
        }
    }
}
