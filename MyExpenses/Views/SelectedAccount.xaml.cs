using System;
using System.IO;
using System.Linq;
using Microcharts;
using MyExpenses.Utils.Database;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SelectedAccount
{
    private static readonly string Db = Home.Db;
    public SelectedAccount()
    {
        InitializeComponent();

        
        
        // todo a finir
        var entrie = new[]
        {
            new ChartEntry(212)
            {
                Label = "test",
                ValueLabel = "212",
                Color = SKColors.SkyBlue
            },
            new ChartEntry(213)
            {
                Label = "test2",
                ValueLabel = "213",
                Color = SKColors.Bisque
            },
        };
        
        ChartView.Chart = new PieChart{Entries = entrie ,LabelTextSize = 30};

        CreateButton();
    }

    private void CreateButton()
    {
        var listeDb = Directory.GetFiles(Db, "*.sqlite").Select(Path.GetFileNameWithoutExtension).ToList();

        const int size = 75;
        foreach (var btn in listeDb.Select(db => new Button{ Text = db, ClassId = db, }))
        {
            btn.HeightRequest = size;
            btn.WidthRequest = size;
            btn.Margin = new Thickness(10);
            btn.Clicked += Btn_OnClicked;
            FlexLayout.Children.Add(btn);
            
        }
    }

    private void Btn_OnClicked(object sender, EventArgs e)
    {
        // todo password ?
        var account = ((Button)sender).ClassId;
        SqLite.Initialized(account, "");

        Navigation.PushAsync(new ActiveAccount(account));
    }
}