using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Saving_Accelerator_Tools2.TemplateSelectors
{
    public class GreatorLessValue: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((decimal)value > 0)
                return "GREEN";
            else if ((decimal)value < 0)
                return "RED";
            else if ((decimal)value == 0)
                return "BLACK";


            return "EMPTY";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
