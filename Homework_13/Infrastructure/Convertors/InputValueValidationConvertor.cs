using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Homework_13.Infrastructure.Convertors;

internal class InputValueValidationConvertor : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not InputValueValidationEnum mode) return null;
        return mode switch
        {
            InputValueValidationEnum.Default => new SolidColorBrush(Colors.White),
            InputValueValidationEnum.Disable => new SolidColorBrush(Colors.Silver),
            InputValueValidationEnum.Error => new SolidColorBrush(Colors.Red),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
