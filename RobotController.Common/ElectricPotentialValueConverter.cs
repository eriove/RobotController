using System;
using System.Globalization;
using UnitsNet;
#if NET46
using System.Windows.Data;
#else
using Xamarin.Forms;
#endif


namespace RobotController.Common
{
    public class ElectricPotentialValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            Type sourceType = value.GetType();
            if (sourceType == typeof(ElectricPotentialDc)
                && targetType == typeof(string))
            {
                return $"{((ElectricPotentialDc) value).VoltsDc} V";
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
