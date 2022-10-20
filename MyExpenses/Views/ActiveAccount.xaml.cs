using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microcharts;
using MyExpenses.Utils.Database;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ActiveAccount
{
    private static Style _styleWallet = new (typeof(Button));
    public ActiveAccount(string account)
    {
        InitializeComponent();
        
        //todo à refaire car pas beau
        _styleWallet.Setters.Add(new Setter{Property = View.MarginProperty, Value = 7});
        _styleWallet.Setters.Add(new Setter{Property = HeightRequestProperty, Value = 50});
        _styleWallet.Setters.Add(new Setter{Property = WidthRequestProperty, Value = 100});
        _styleWallet.Setters.Add(new Setter{Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Center});
        _styleWallet.Setters.Add(new Setter{Property = View.VerticalOptionsProperty, Value = LayoutOptions.Start});
        _styleWallet.Setters.Add(new Setter{Property = Button.BorderColorProperty, Value = Color.Black});
        _styleWallet.Setters.Add(new Setter{Property = Button.BorderWidthProperty, Value = 2});
        _styleWallet.Setters.Add(new Setter{Property = Button.CornerRadiusProperty, Value = 10});
        _styleWallet.Setters.Add(new Setter{Property = BackgroundColorProperty, Value = Color.LightGray});
        
        Title = account;

        DisplayWallet();
        
        FlexLayout.ForceLayout();
    }

    private void DisplayWallet()
    {
        FlexLayout.Children.Clear();

        MakeWallets();
        AddNewWallet();
    }

    private void AddNewWallet()
    {
        var btnNewWallet = new Button
        {
            Text = "+",
            FontSize = 25,
            FontAttributes = FontAttributes.Bold,
            ClassId = "+",
            Style = _styleWallet,
        };
        btnNewWallet.Clicked += BtnNewWallet_OnClicked;
        FlexLayout.Children.Add(btnNewWallet);
    }

    private void BtnNewWallet_OnClicked(object sender, EventArgs e) => Navigation.PushAsync(new AddWallet());

    private void MakeWallets()
    {
        var entries = new List<ChartEntry>();

        var lstWallet = SqLite.GetAllWallet();
        var lstVTotalByWalletClass = SqLite.GetVTotalByWalletClass();

        foreach (var account in lstWallet)
        {
            var value = new SqLite.VTotalByWaletClass();
            if (!lstVTotalByWalletClass.Count.Equals(0)) value = lstVTotalByWalletClass.Where(s => s.Name.Equals(account.Name)).ToList()[0];

            entries.Add(new ChartEntry(value.Remaining)
            {
                ValueLabel = value.Remaining.ToString(CultureInfo.InvariantCulture),
                ValueLabelColor = SKColor.Parse(account.Color),
                Color = SKColor.Parse(account.Color)
            });

            var btn = new Button
            {
                Text = account.Name,
                ClassId = account.Name,
                FontSize = 10,
                Style = _styleWallet,
                BackgroundColor = Color.FromHex(account.Color),
                BindingContext = account
            };
            btn.Clicked += BtnOnClicked;
            FlexLayout.Children.Add(btn);
        }

        LabelNumberWallet.Text = lstWallet.Count.ToString();
        ChartView.Chart = new PieChart { Entries = entries, LabelTextSize = 30 };
    }

    private void BtnOnClicked(object sender, EventArgs e)
    {
        // todo à creuser
        var btn = (Button)sender;
        var i = btn.BindingContext as SqLite.VWalletClass;
        Console.WriteLine(i);
    }
}