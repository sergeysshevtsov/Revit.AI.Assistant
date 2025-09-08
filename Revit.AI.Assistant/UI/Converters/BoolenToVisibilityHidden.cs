using System.Windows.Data;

namespace Revit.AI.Assistant.UI.Converters;
internal class BooleanToVisibilityHidden : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        bool? s = value as bool?;

        if (s.HasValue && s.Value == true)
            return System.Windows.Visibility.Visible;

        if (s.HasValue && !s.Value)
            return System.Windows.Visibility.Hidden;

        return System.Windows.Visibility.Hidden;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => null;
}

