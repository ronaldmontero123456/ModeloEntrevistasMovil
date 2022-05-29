using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ModeloEntrevistasMovil.ViewModel.Converters
{
    public class ItemTappedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is TextChangedEventArgs eventArgs) ? throw new ArgumentException("Este no es un TextChangedEventArgs", "value") : eventArgs.NewTextValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
