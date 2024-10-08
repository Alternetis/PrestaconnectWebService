using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PrestaconnectWebService.Converters
{
    public class StringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convertit la chaîne de couleur en objet SolidColorBrush
            if (value != null && value is string colorString)
            {
                if (!string.IsNullOrEmpty(colorString))
                {
                    try
                    {
                        var color = (Color)ColorConverter.ConvertFromString(colorString);
                        return new SolidColorBrush(color);
                    }
                    catch (FormatException)
                    {
                        // Gestion de l'erreur si la couleur n'est pas valide
                        return Brushes.Transparent;
                    }
                }
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
