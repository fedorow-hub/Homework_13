using System;
using System.Globalization;
using System.Windows.Data;

namespace Homework_13.Infrastructure.Convertors
{
    [ValueConversion(typeof(decimal[]), typeof(string))]
    internal class RateCourceMultiConvertor : MultiConvertor
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var icon = (decimal)values[0] >= (decimal)values[1] ? "Solid_SortUp" : "Solid_SortDown";
            return icon;
        }
    }
}
