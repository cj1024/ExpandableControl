using System;
using System.Globalization;
using System.Windows.Data;

namespace ExpandableControl.ValueConverter
{
    class NegativeConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return -(double) value;
            }
            catch (Exception)
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return -(double)value;
            }
            catch (Exception)
            {
                return value;
            }
        }
    }
}
