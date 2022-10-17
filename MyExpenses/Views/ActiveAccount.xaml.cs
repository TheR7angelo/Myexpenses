using System.Collections.Generic;
using MyExpenses.Utils.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        var lst = SqLite.GetAllAccount();
        foreach (var account in lst)
        {
            var btn = new Button();
            btn.Text = account.Nom;
            btn.HeightRequest = 75;
            btn.WidthRequest = 125;
            btn.FontSize = 12;
            btn.Margin = new Thickness(7);
            btn.BorderColor = Color.Black;
            btn.BorderWidth = 1;
            btn.CornerRadius = 3;

            btn.BackgroundColor = account.Color is not null ? Color.FromHex(account.Color) : Color.Bisque;
            
            FlexLayout.Children.Add(btn);
        }

        LabelNumberWallet.Text = lst.Count.ToString();
    }
    
    private void AddAccountDb(List<string> accountDb)
    {
        
    }
}