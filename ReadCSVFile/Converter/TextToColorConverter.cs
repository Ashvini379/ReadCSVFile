using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ReadCSVFile.Converter
{
    public class TextToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double feature = 0.0;
            if (!string.IsNullOrEmpty(value.ToString()))
                feature = System.Convert.ToDouble(value);
            if(feature >3)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else

            {
                return new SolidColorBrush(Colors.Green);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
