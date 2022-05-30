using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ModeloEntrevistasMovil.ViewModel.Converters
{
    class ClickedEventArgsConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is ClickedEventArgs eventArgs) ? throw new ArgumentException("Este no es un TextChangedEventArgs", "value") : eventArgs.Parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
