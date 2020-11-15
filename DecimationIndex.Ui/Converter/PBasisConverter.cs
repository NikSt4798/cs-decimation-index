using System;
using System.Globalization;
using System.Windows.Data;
using DecimationIndex.Core;

namespace DecimationIndex.Ui.Converter
{
	public class PBasisConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!int.TryParse(parameter?.ToString(), out var p) ||
			    !int.TryParse(value?.ToString(), out var v))
				return null;

			return v.GetPBasis(p);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}