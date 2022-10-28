using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microcharts;
using MyExpenses.Utils.Database;
using MyExpenses.Utils.Ressources;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ActiveAccount
{
    private static readonly Style StyleWallet = Utils.Ressources.Style.Button.GetStyle("ButtonWallet");
    public ActiveAccount(string account)
    {
        //to a finir
        InitializeComponent();
        Title = account;

        DisplayWallet();
        
        FlexLayout.ForceLayout();
    }

    public void DisplayWallet()
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
            Style = StyleWallet
        };
        btnNewWallet.Clicked += BtnNewWallet_OnClicked;
        FlexLayout.Children.Add(btnNewWallet);
    }

    private void BtnNewWallet_OnClicked(object sender, EventArgs e) => Navigation.PushAsync(new AddWallet(this));


    private void MakeWallets()
    {
        var entries = new List<ChartEntry>();

        var lstWallet = SqLite.GetAllWallet();

        var lstVTotalByWalletClass = SqLite.GetVTotalByWalletClass();

        foreach (var wallet in lstWallet)
        {
            var value = lstVTotalByWalletClass.Where(s => s.Name.Equals(wallet.Name)).ToList();
            if (!value.Count.Equals(0))
            {
                entries.Add(new ChartEntry(value[0].Remaining)
                {
                    ValueLabel = value[0].Remaining.ToString(CultureInfo.InvariantCulture),
                    ValueLabelColor = SKColor.Parse(wallet.ColorValue),
                    Color = SKColor.Parse(wallet.ColorValue)
                });
            }

            var bitmap = wallet.Image.GetSkBitmap();
            bitmap = bitmap.Resize(new SKImageInfo(39, 39), SKFilterQuality.High);
            var image = bitmap.ParseToImageSource();

            var btn = new Button
            {
                Text = wallet.Name,
                ClassId = wallet.Name,
                FontSize = 10,
                Style = StyleWallet,
                ImageSource = image,
                BackgroundColor = Color.FromHex(wallet.ColorValue),
                ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Bottom, 0.1f),
                BindingContext = wallet
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