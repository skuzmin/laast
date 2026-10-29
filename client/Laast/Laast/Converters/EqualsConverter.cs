using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Laast.Converters;

public class EqualsConverter : IMultiValueConverter
{
    public object Convert(
        IList<object?> values,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        if (values.Count < 2 || values[0] is null || values[1] is null)
        {
            return false;
        }

        return Equals(values[0], values[1]);
    }
}