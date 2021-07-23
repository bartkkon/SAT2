using ControlzEx.Theming;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Saving_Accelerator_Tools2.Converters
{
    public class ColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is decimal))
                return 3;

            var dValue = System.Convert.ToDecimal(value);

            return dValue < 0 ? 1 : dValue > 0 ? 2 : 3;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
