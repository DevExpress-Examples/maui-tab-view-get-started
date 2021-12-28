<!-- default file list -->
*Files to look at*:

* [MauiProgram.cs](./TabView_GenerateItems/MauiProgram.cs)
* [MainPage.xaml](./TabView_GenerateItems/MainPage.xaml)
* [MainPage.xaml.cs](./TabView_GenerateItems/MainPage.xaml.cs)
* [CarModel.cs](./TabView_GenerateItems/CarModel.cs)
* [CarBrandViewModel.cs](./TabView_GenerateItems/CarBrandViewModel.cs)
* [BoolToColorConverter.cs](./TabView_GenerateItems/BoolToColorConverter.cs)
* [MainViewModel.cs](./TabView_GenerateItems/MainViewModel.cs)
<!-- default file list end -->

# Generate MAUI Tab View Items from a Data Source

This lesson explains how to use the [TabView](http://docs.devexpress.com/MAUI/DevExpress.Maui.Navigation.TabView) component with tab items generated from a data source to create a tab bar that allows users to filter lists. 

1. Install a [.NET MAUI development](https://docs.microsoft.com/en-gb/dotnet/maui/get-started/installation) environment and open the solution in Visual Studio 2022.
2. Register the following NuGet feed in Visual Studio: `https://nuget.devexpress.com/free/api`.  
	If you are an active DevExpress [Universal](https://www.devexpress.com/subscriptions/universal.xml) customer or have registered our [free Xamarin UI controls](https://www.devexpress.com/xamarin/), this MAUI preview will be available in your personal NuGet feed automatically.
4. Restore NuGet packages.  
5. Run the application on an Android or iOS device or emulator.  

<img src="./img/devexpress-maui-tab-view.png"/>

The following step-by-step instructions describe how to create the same application.

## Create a New MAUI Application and Add a Tab View

Create a new .NET MAUI solution in Visual Studio 2022. Refer to the following Microsoft documentation for more information on how to get started with .NET MAUI: [.NET Multi-platform App UI](https://docs.microsoft.com/en-gb/dotnet/maui/).

Register `https://nuget.devexpress.com/free/api` as a package source in Visual Studio, if you are not an active DevExpress [Universal](https://www.devexpress.com/subscriptions/universal.xml) customer or have not yet registered our [free Xamarin UI controls](https://www.devexpress.com/xamarin/).

Install the **DevExpress.Maui.Navigation** package from your NuGet feed.

In the *MauiProgram.cs* file, call the `UseDevExpress` method to register handlers for the [TabView](http://docs.devexpress.com/MAUI/DevExpress.Maui.Navigation.TabView) and other DevExpress controls:

```cs
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using DevExpress.Maui;

namespace TabView_GenerateItems {
    public static class MauiProgram {
        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseDevExpress()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            return builder.Build();
        }
    }
}
```

In the *MainPage.xaml* file, use the *dxn* prefix to declare the **DevExpress.Maui.Navigation** namespace and add a [TabView](http://docs.devexpress.com/MAUI/DevExpress.Maui.Navigation.TabView) object to the main page:

```xaml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:dxn="clr-namespace:DevExpress.Maui.Navigation;assembly=DevExpress.Maui.Navigation"
             x:Class="TabView_GenerateItems.MainPage">
     <dxn:TabView/>
</ContentPage>
```

## Create Models and View Models

Add a **CarModel** class that specifies a data object in the application:

```cs
namespace TabView_GenerateItems {
    public class CarModel {
        public string BrandName { get; }
        public string ModelName { get; }
        public string FullName => $"{BrandName} {ModelName}";

        public CarModel(string brand, string model) {
            this.BrandName = brand;
            this.ModelName = model;
        }
    }
}
```

Create a **CarBrandViewModel** class that defines content for the tab view: car make and corresponding models. This view model will be used to display brands as tabs in the header and matching models in the tab item’s content area:

```cs
using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TabView_GenerateItems {
    public class CarBrandViewModel : INotifyPropertyChanged {
        private bool isSelected = false;
        public string BrandName { get; }
        public IReadOnlyList<CarModel> CarModels { get; }

        // This property is used to change the appearance of a tab depending on its state. 
        public bool IsSelected {
            get { return isSelected; }
            set {
                if (value == isSelected) return;
                isSelected = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CarBrandViewModel(string brandName, IEnumerable<CarModel> carModels) {
            if (String.IsNullOrEmpty(brandName)) {
                this.BrandName = String.Empty;
            }
            else {
                this.BrandName = brandName;
            }
            if (carModels == null) {
                this.CarModels = new List<CarModel>();
            }
            else {
                this.CarModels = carModels.ToList();
            }
        }
        private void RaisePropertyChanged([CallerMemberName] string caller = "") {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler.Invoke(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
```

Create a **MainViewModel** class that defines content for the MainPage (models grouped by make/brand):

```cs
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TabView_GenerateItems {
    public class MainViewModel : INotifyPropertyChanged {
        private const int UnselectedIndex = -1;
        private static readonly IReadOnlyList<CarModel> allCarModels = new List<CarModel> {
            new CarModel("Mercedes-Benz", "SL500 Roadster"),
            new CarModel("Mercedes-Benz", "CLK55 AMG Cabriolet"),
            new CarModel("Mercedes-Benz", "C230 Kompressor Sport Coupe"),
            new CarModel("BMW", "530i"),
            new CarModel("Rolls-Royce", "Corniche"),
            new CarModel("Jaguar", "S-Type 3.0"),
            new CarModel("Cadillac", "Seville"),
            new CarModel("Cadillac", "DeVille"),
            new CarModel("Lexus", "LS430"),
            new CarModel("Lexus", "GS430"),
            new CarModel("Ford", "Ranger FX-4"),
            new CarModel("Dodge", "RAM 1500"),
            new CarModel("GMC", "Siera Quadrasteer"),
            new CarModel("Nissan", "Crew Cab SE"),
            new CarModel("Toyota", "Tacoma S-Runner"),
        };

        public IReadOnlyList<CarBrandViewModel> CarModelsByBrand { get; }

        int selectedIndex = UnselectedIndex;
        public int SelectedIndex {
            get => selectedIndex;
            set {
                if (selectedIndex == value) return;
                if (selectedIndex != UnselectedIndex) {
                    CarModelsByBrand[selectedIndex].IsSelected = false;
                }
                selectedIndex = value;
                if (selectedIndex != UnselectedIndex) {
                    CarModelsByBrand[selectedIndex].IsSelected = true;
                }
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel() {
            List<CarBrandViewModel> carBrandViewModels = new List<CarBrandViewModel>();
            carBrandViewModels.Add(new CarBrandViewModel("All", allCarModels));

            IEnumerable<IGrouping<string, CarModel>> groupedCarModels = 
                                                        allCarModels.GroupBy(v => v.BrandName);
            foreach (IGrouping<string, CarModel> carModelGroup in groupedCarModels) {
                carBrandViewModels.Add(new CarBrandViewModel(carModelGroup.Key, carModelGroup));
            }
            CarModelsByBrand = carBrandViewModels;
        }

        private void RaisePropertyChanged([CallerMemberName] string caller = "") {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler.Invoke(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
```



## Bind the Tab View to View Models

In the MainPage.xaml file:

1. Assign a **MainViewModel** instance to the **ContentPage.BindingContext** property.
2. Bind the TabView’s [ItemsSource](http://docs.devexpress.com/MAUI/DevExpress.Maui.Navigation.TabView.ItemsSource) property to the **CarModelsByBrand** property of the view model, and the [SelectedItemIndex](http://docs.devexpress.com/MAUI/DevExpress.Maui.Navigation.TabView.SelectedItemIndex) property to the view model’s **SelectedIndex** property.
3. Use the [ItemHeaderTemplate](http://docs.devexpress.com/MAUI/DevExpress.Maui.Navigation.TabView.ItemHeaderTemplate) property to define the tab item header’s appearance, and the [ItemTemplate](http://docs.devexpress.com/MAUI/DevExpress.Maui.Navigation.TabView.ItemTemplate) property to specify a view displayed in the tab item’s content area.

```xaml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:dxn="clr-namespace:DevExpress.Maui.Navigation;assembly=DevExpress.Maui.Navigation"
             xmlns:local="clr-namespace:TabView_GenerateItems"
             x:Class="TabView_GenerateItems.MainPage">
    <ContentPage.BindingContext>
        <local:MainViewModel/>
    </ContentPage.BindingContext>
    <dxn:TabView ItemsSource="{Binding CarModelsByBrand}"
                 SelectedItemIndex="{Binding SelectedIndex, Mode=TwoWay}">
        <dxn:TabView.ItemHeaderTemplate>
            <DataTemplate>
                <Label Text="{Binding BrandName}"
                       HorizontalOptions="Center"
                       VerticalOptions="CenterAndExpand"/>
            	
            </DataTemplate>
        </dxn:TabView.ItemHeaderTemplate>
        <dxn:TabView.ItemTemplate>
            <DataTemplate>
                <Grid>
                    <ListView ItemsSource="{Binding CarModels}">
                        <ListView.ItemTemplate>
                            <DataTemplate>
                                <ViewCell>
                                    <Label Padding="5" Text="{Binding FullName}" />
                                </ViewCell>
                            </DataTemplate>
                        </ListView.ItemTemplate>
                    </ListView>
                </Grid>
            </DataTemplate>
        </dxn:TabView.ItemTemplate>
    </dxn:TabView>
</ContentPage>
```

## Customize the Tab View
Configure the appearance of the TabView’s header panel and header items.

Specify the minimum and maximum sizes of items, the spacing between them, and item padding:

```xaml
<dxn:TabView ItemsSource="{Binding CarModelsByBrand}"
             SelectedItemIndex="{Binding SelectedIndex, Mode=TwoWay}"
             ItemHeaderMinWidth="90"
             ItemHeaderMaxWidth="360"
             HeaderPanelItemSpacing="8">
    <dxn:TabView.ItemHeaderTemplate>
        <DataTemplate>
            <Grid>
                <Label Text="{Binding BrandName}"
                       HorizontalOptions="Center"
                       VerticalOptions="CenterAndExpand"
                       Padding="5,0"/>
            </Grid>
        </DataTemplate>
    </dxn:TabView.ItemHeaderTemplate>
    <!-- Other Tab View settings.-->
</dxn:TabView>
```

Use the [TabView.HeaderPanelBackgroundColor](https://docs.devexpress.com/MAUI/DevExpress.Maui.Navigation.TabView.HeaderPanelBackgroundColor) property to specify the background color of the header panel. The `Label.TextColor` property specifies the color of labels in the panel. We use the following converter to convert a Boolean value in the view model to a `Color` structure.


```cs
using System;
using System.ComponentModel;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace TabView_GenerateItems
{
    public class BoolToColorConverter:IValueConverter {
        public Color FalseSource { get; set; }
        public Color TrueSource { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is bool)) {
                return null;
            }
            return (bool)value ? TrueSource : FalseSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
```

```xaml
<ContentPage.Resources>
    <ResourceDictionary>
        <local:BoolToColorConverter x:Key="boolToColorConverter"
                                            DefaultColor="Transparent"
                                            SelectedColor="#40FFFFFF"/>
    </ResourceDictionary>
</ContentPage.Resources>
<dxn:TabView ItemsSource="{Binding CarModelsByBrand}"
             SelectedItemIndex="{Binding SelectedIndex, Mode=TwoWay}"
             ItemHeaderMinWidth="90"
             ItemHeaderMaxWidth="360"
             HeaderPanelItemSpacing="8"
             HeaderPanelBackgroundColor="#1e88e5">
    <dxn:TabView.ItemHeaderTemplate>
        <DataTemplate>
           <Label HorizontalOptions="Center"
               VerticalOptions="CenterAndExpand"
               Text="{Binding BrandName}"
               HorizontalTextAlignment="Center"
               Padding="5,0"
               TextColor="{Binding IsSelected, Converter={StaticResource boolToColorConverter}}"/>
        </DataTemplate>
    </dxn:TabView.ItemHeaderTemplate>
    <!-- Other Tab View settings.-->
</dxn:TabView>
```

Configure also the header panel’s shadow, margin for header items:

```xaml
<dxn:TabView ItemsSource="{Binding CarModelsByBrand}"
             SelectedItemIndex="{Binding SelectedIndex, Mode=TwoWay}"
             ItemHeaderMinWidth="90"
             ItemHeaderMaxWidth="360"
             HeaderPanelItemSpacing="8"
             HeaderPanelBackgroundColor="#1e88e5"
             HeaderPanelShadowHeight="3"
             HeaderPanelShadowRadius="3"
             IsSelectedItemIndicatorVisible="True"
	     SelectedItemIndicatorColor="White">
    <dxn:TabView.ItemHeaderTemplate>
        <DataTemplate>
            <Label HorizontalOptions="Center"
                   VerticalOptions="CenterAndExpand"
                   Text="{Binding BrandName}"
                   HorizontalTextAlignment="Center"
                   Padding="5,0"
                   TextColor="{Binding IsSelected, Converter={StaticResource boolToColorConverter}}"/>
        </DataTemplate>
    </dxn:TabView.ItemHeaderTemplate>
    <!-- Other Tab View settings.-->
</dxn:TabView>
```
