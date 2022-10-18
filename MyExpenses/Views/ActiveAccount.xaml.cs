using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        FlexLayout.Children.Clear();
        
        var entries = new List<ChartEntry>();
        
        var lstAccount = SqLite.GetAllAccount();
        var lstVTotalByAccountClass = SqLite.GetVTotalByAccountClass();
        
        foreach (var account in lstAccount)
        {
            // Fonctionne mais faut atttribuée la couleur du compte au schema
            var value = new SqLite.VTotalByAccountClass();
            if (!lstVTotalByAccountClass.Count.Equals(0))
            {
                value = lstVTotalByAccountClass.Where(s => s.Name.Equals(account.Name)).ToList()[0];
            }

            entries.Add(new ChartEntry(value.Remaining)
            {
                ValueLabel = value.Remaining.ToString(CultureInfo.InvariantCulture),
                ValueLabelColor = SKColor.Parse(account.Color),
                Color = SKColor.Parse(account.Color)
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
                BackgroundColor = Color.FromHex(account.Color)
            };
            FlexLayout.Children.Add(btn);
        }

        LabelNumberWallet.Text = lstAccount.Count.ToString();
        ChartView.Chart = new PieChart{Entries = entries ,LabelTextSize = 30};
    }
    
    private static StackLayout LegendLabel(SqLite.TAccountClass account)
    {
        return new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
            Children =
            {
                new Rectangle{WidthRequest = 17, HeightRequest = 17, Background = new SolidColorBrush(Color.FromHex(account.Color))},
                new Label{Text = account.Name, TextColor = Color.Black}
            }
        };
    }
}