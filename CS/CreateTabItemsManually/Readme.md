<!-- default file list -->
*Files to look at*:

* [MauiProgram.cs](./TabView_CreateItems/MauiProgram.cs)
* [MainPage.xaml](./TabView_CreateItems/MainPage.xaml)
* [MainPage.xaml.cs](./TabView_CreateItems/MainPage.xaml.cs)
<!-- default file list end -->

# Create MAUI Tab View Items Manually

This lesson explains how to use the [TabView](http://docs.devexpress.com/MAUI/DevExpress.Maui.Controls.TabView) component with manually created tab items to implement bottom tab navigation in a .NET MAUI application.

1. Install a [.NET MAUI development](https://docs.microsoft.com/en-gb/dotnet/maui/get-started/installation) environment and open the solution in Visual Studio 2022.
2. Register [your personal NuGet feed](https://nuget.devexpress.com/) in Visual Studio.  
	If you are an active DevExpress [Universal](https://www.devexpress.com/subscriptions/universal.xml) customer or have registered our [free Xamarin UI controls](https://www.devexpress.com/xamarin/), this MAUI preview will be available in your personal NuGet feed automatically.
4. Restore NuGet packages.  
5. Run the application on an Android or iOS device or emulator.  

<img src="./img/devexpress-maui-tab-view.png"/>

The following step-by-step instructions describe how to create the same application.

## Create a New MAUI Application and Add a Tab View

Create a new .NET MAUI solution in Visual Studio 22 Preview. Refer to the following Microsoft documentation for more information on how to get started with .NET MAUI: [.NET Multi-platform App UI](https://docs.microsoft.com/en-gb/dotnet/maui/).

Register [your personal NuGet feed](https://nuget.devexpress.com/) as a package source in Visual Studio, if you are not an active DevExpress [Universal](https://www.devexpress.com/subscriptions/universal.xml) customer or have not yet registered our [free Xamarin UI controls](https://www.devexpress.com/xamarin/).

Install the **DevExpress.Maui.Controls** package from your NuGet feed.

In the *MauiProgram.cs* file, call the **UseDevExpress** method to register handlers for the [TabView](http://docs.devexpress.com/MAUI/DevExpress.Maui.Controls.TabView) and other DevExpress controls:

```cs
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using DevExpress.Maui;

namespace TabView_CreateItems {
    public static class MauiProgram {
        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                })
                .UseDevExpress();
            return builder.Build();
        }
    }
}
```

In the *MainPage.xaml* file, use the *dxn* prefix to declare the **DevExpress.Maui.Controls** namespace and add a [TabView](http://docs.devexpress.com/MAUI/DevExpress.Maui.Controls.TabView) object to the main page:

```xaml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:dxco="clr-namespace:DevExpress.Maui.Controls;assembly=DevExpress.Maui.Controls"
             x:Class="TabView_CreateItems.MainPage">
     <dxco:TabView/>
</ContentPage>
```

## Create Tab Items
Add icons for tabs ([.svg files](./TabView_CreateItems/Resources/Images/)) to the project and set their **Build Action** property to **MauiImage**.

Add [TabViewItem](http://docs.devexpress.com/MAUI/DevExpress.Maui.Controls.TabViewItem) objects to the [TabView.Items](http://docs.devexpress.com/MAUI/DevExpress.Maui.Controls.TabView.Items) collection:

```xaml
<dxco:TabView>
    <dxco:TabViewItem HeaderText="Mail"
                     HeaderIcon="email">
        <dxco:TabViewItem.Content>
            <Grid>
                <Label Text="Mail List Here" 
                       HorizontalOptions="Center" 
                       VerticalOptions="CenterAndExpand"/>
            </Grid>
        </dxco:TabViewItem.Content>
    </dxco:TabViewItem>

    <dxco:TabViewItem HeaderText="Calendar"
                     HeaderIcon="calendar">
        <dxco:TabViewItem.Content>
            <Grid>
                <Label Text="Calendar Here" 
                       HorizontalOptions="Center" 
                       VerticalOptions="CenterAndExpand"/>
            </Grid>
        </dxco:TabViewItem.Content>
    </dxco:TabViewItem>

    <dxco:TabViewItem HeaderText="People"
                     HeaderIcon="people">
        <dxco:TabViewItem.Content>
            <Grid>
                <Label Text="People Here"
                       HorizontalOptions="Center" 
                       VerticalOptions="CenterAndExpand"/>
            </Grid>
        </dxco:TabViewItem.Content>
    </dxco:TabViewItem>
</dxco:TabView>
```

Note that you can skip the **Items** property tags in the XAML markup as this property has a *ContentProperty* attribute.

## Customize the Tab View Appearance

Move the header panel with tabs to the bottom of the page, set up tabs to fill all the available space in the header panel, and hide the selected item indicator:

```xaml
<dxco:TabView HeaderPanelPosition="Bottom"
             ItemHeaderWidth="*"
             IsSelectedItemIndicatorVisible="False">
    <!-- Tab items here. -->
</dxco:TabView>
```

Specify icon and text colors for tabs in the header panel. Use the **ItemHeaderIconColor** and **ItemHeaderTextColor** properties of the [TabView](http://docs.devexpress.com/MAUI/DevExpress.Maui.Controls.TabView) object to specify the same colors for all tab items when they are not selected, and the **SelectedHeaderIconColor** and **SelectedHeaderTextColor** properties of [TabViewItem](http://docs.devexpress.com/MAUI/DevExpress.Maui.Controls.TabViewItem) objects to set icon and text colors for individual tabs in the selected state:

```xaml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="TabView_CreateItems.MainPage"
             xmlns:dxco="clr-namespace:DevExpress.Maui.Controls;assembly=DevExpress.Maui.Controls">
    <ContentPage.Resources>
        <Color x:Key="unselected_grey">#757575</Color>
        <Color x:Key="mail_blue">#1e88e5</Color>
        <Color x:Key="calendar_green">#43a047</Color>
        <Color x:Key="people_red">#e53935</Color>
    </ContentPage.Resources>
    <dxco:TabView HeaderPanelPosition="Bottom"
                 IsSelectedItemIndicatorVisible="False"
                 ItemHeaderWidth="*"
                 ItemHeaderIconColor="{StaticResource unselected_grey}"
                 ItemHeaderTextColor="{StaticResource unselected_grey}">
        <dxco:TabViewItem HeaderText="Mail"
                         HeaderIcon="email"
                         SelectedHeaderIconColor="{StaticResource mail_blue}"
                         SelectedHeaderTextColor="{StaticResource mail_blue}">
            <dxco:TabViewItem.Content>
                <Grid>
                    <Label Text="Mail List Here" 
                           HorizontalOptions="Center" 
                           VerticalOptions="CenterAndExpand"/>
                </Grid>
            </dxco:TabViewItem.Content>
        </dxco:TabViewItem>

        <dxco:TabViewItem HeaderText="Calendar"
                         HeaderIcon="calendar"
                         SelectedHeaderIconColor="{StaticResource calendar_green}"
                         SelectedHeaderTextColor="{StaticResource calendar_green}">
            <dxco:TabViewItem.Content>
                <Grid>
                    <Label Text="Calendar Here" 
                           HorizontalOptions="Center" 
                           VerticalOptions="CenterAndExpand"/>
                </Grid>
            </dxco:TabViewItem.Content>
        </dxco:TabViewItem>

        <dxco:TabViewItem HeaderText="People"
                         HeaderIcon="people"
                         SelectedHeaderIconColor="{StaticResource people_red}"
                         SelectedHeaderTextColor="{StaticResource people_red}">
            <dxco:TabViewItem.Content>
                <Grid>
                    <Label Text="People Here"
                           HorizontalOptions="Center" 
                           VerticalOptions="CenterAndExpand"/>
                </Grid>
            </dxco:TabViewItem.Content>
        </dxco:TabViewItem>
    </dxco:TabView>
</ContentPage>
```
