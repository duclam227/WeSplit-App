using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GUI_WeSplit.Validation
{
    class NotNullRule : ValidationRule
    {      
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Không thể để trống trường này.");
            if (String.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, "Không thể để trống trường này.");
            }
            return ValidationResult.ValidResult;
        }
    }
}


