using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace testtask
{
    public class AgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int v = 0;
            int.TryParse(value.ToString(),out v);
            if (v == 0)
                return "";
            if (v % 10 == 1 && v % 100 != 11)
                return value.ToString() + " год";
            if (v % 10 < 5 && v % 10 != 0)
                return value.ToString() + " года";
            return value.ToString() + " лет";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
