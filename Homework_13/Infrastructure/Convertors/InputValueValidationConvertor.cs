using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Homework_13.Infrastructure.Convertors;

internal class InputValueValidationConvertor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is InputValueValidationEnum mode)) return null;
        return mode switch
        {
            InputValueValidationEnum.Default => new SolidColorBrush(Colors.White),
            InputValueValidationEnum.Disable => new SolidColorBrush(Colors.Silver),
            InputValueValidationEnum.Error => new SolidColorBrush(Colors.Red),
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
