﻿using System.Globalization;
using System.Windows.Data;

namespace WPFHelper.Converters;

public class IsStringNullOrWhiteSpaceConverter : IValueConverter
{
    #region Convert
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        String.IsNullOrWhiteSpace(value?.ToString());
    #endregion

    #region ConvertBack
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
    #endregion
}