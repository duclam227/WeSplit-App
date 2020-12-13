using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GUI_WeSplit.Validation
{
    class MoneyRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Không thể để trống trường này");
            double money = 0;

            try
            {
                if(((String)value).Length>0)
                    money = Double.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Vui lòng nhập số tiền hợp lệ.");
            }

            if (money < 0)
            {
                return new ValidationResult(false, "Không được là số âm");
            }

            return ValidationResult.ValidResult;
        }
    }
}
