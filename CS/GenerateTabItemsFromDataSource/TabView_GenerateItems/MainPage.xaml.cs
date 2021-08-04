using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace TabView_GenerateItems {
	public partial class MainPage : ContentPage {
		public MainPage() {
			InitializeComponent();
		}
	}

    class IsSelectedToColorConverter : IValueConverter {
        public Color DefaultColor { get; set; }
        public Color SelectedColor { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is bool boolValue)) return DefaultColor;
            return (boolValue) ? SelectedColor : DefaultColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
