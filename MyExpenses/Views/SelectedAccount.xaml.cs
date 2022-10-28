using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyExpenses.Utils.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SelectedAccount
{
    private static readonly string Db = Home.Db;
    private static readonly List<string> Dbs = Home.Dbs;
    public SelectedAccount()
    {
        InitializeComponent();
        CreateButton();
    }

    private void CreateButton()
    {
        const int size = 75;
        foreach (var btn in Dbs.Select(db => new Button{ Text = db, ClassId = db, }))
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