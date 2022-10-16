using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Home
{
    public Home()
    {
        InitializeComponent();
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        switch (btn!.ClassId)
        {
            case "ButtonConnexion":
                await Navigation.PushAsync( new SelectedAccount());
                break;
            case "ButtonNewAccount":
                await Navigation.PushAsync(new AddAccount());
                break;
        }
    }
}