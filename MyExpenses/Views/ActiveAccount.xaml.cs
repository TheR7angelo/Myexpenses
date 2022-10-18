using System;
using System.Collections.Generic;
using Microcharts;
using MyExpenses.Utils.Database;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rectangle = Xamarin.Forms.Shapes.Rectangle;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ActiveAccount
{
    public ActiveAccount(string account)
    {
        InitializeComponent();
        Title = account;

        DisplayWallet();
        

        // foreach (var te in new List<string>{"hey", "hey","hey","hey","hey","hey","hey","hey","hey","hey","hey","hey","hey"})
        // {
        //     var btn = new Button() { Text = te, HeightRequest = 75, WidthRequest = 75, Margin = new Thickness(10) };
        //     FlexLayout.Children.Add(btn);
        // }
        FlexLayout.ForceLayout();
    }

    public void DisplayWallet()
    {
        var entries = new List<ChartEntry>();
        var rand = new Random();
        
        
        FlexLayout.Children.Clear();
        var lst = SqLite.GetAllAccount();
        foreach (var account in lst)
        {
            // Fonctionne mais faut atttribuée la couleur du compte au schema
            entries.Add(new ChartEntry(rand.Next(10, 255))
            {
                // Label = account.Name,
                Color = SKColors.Chocolate
            });
            
            ListViewAccount.Children.Add(LegendLabel(account));
            
            var btn = new Button
            {
                Text = account.Name,
                ClassId = account.Name,
                // Style = FindByName("WalletButton") as Style,
                Margin = new Thickness(7),
                HeightRequest = 75,
                WidthRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                BorderColor = Color.Black,
                BorderWidth = 2,
                CornerRadius = 10,
                // BackgroundColor = account.Color is not null ? Color.FromHex(account.Color) : Color.Bisque
                BackgroundColor = Color.Chocolate
            };
            FlexLayout.Children.Add(btn);
        }

        LabelNumberWallet.Text = lst.Count.ToString();
        ChartView.Chart = new PieChart{Entries = entries ,LabelTextSize = 30};
    }
    
    private static StackLayout LegendLabel(SqLite.TCompteClass account)
    {
        return new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
            Children =
            {
                new Rectangle{WidthRequest = 17, HeightRequest = 17, Background = new SolidColorBrush(Color.Chocolate)},
                new Label{Text = account.Name, TextColor = Color.Black}
            }
        };
    }
}