﻿using System.Globalization;
using System.Windows;
using QCF.Tools.WpfTools.ConverterBase;

namespace QCF.Tools.WpfTools.Converter
{
	public class InvertedBoolToCollapsedVisibilityConverter : GenericValueConverter<bool, Visibility>
    {
	    protected override Visibility Convert(bool value, CultureInfo culture)
	    {
			return value ? Visibility.Collapsed : Visibility.Visible;
	    }	    
    }
}