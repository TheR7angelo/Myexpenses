using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SelectedAccount
{
    private static readonly string _db = Home._db;
    public SelectedAccount()
    {
        InitializeComponent();
        
        var listeDb = Directory.GetFiles(_db, "*.sqlite").Select(Path.GetFileNameWithoutExtension).ToList();

        const int size = 75;
        foreach (var btn in listeDb.Select(db => new Button{ Text = db, ClassId = db, }))
        {
            btn.HeightRequest = size;
            btn.WidthRequest = size;
            btn.Margin = new Thickness(10);
            btn.Clicked += BtnOnClicked;
            FlexLayout.Children.Add(btn);
        }
    }

    private void BtnOnClicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}