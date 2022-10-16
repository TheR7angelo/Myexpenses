using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SelectedAccount
{
    public SelectedAccount()
    {
        InitializeComponent();
        const int size = 75;
        
        var listeDb = new List<string> { "yolo", "salut", "test", "123", "456", "789" };
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