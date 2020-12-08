using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_WeSplit.Validation
{
    class NotNullRule : ValidationRule
    {
        double money = 0;

            try
            {
                if(((String) value).Length>0)
                    money = Double.Parse((String) value);
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
