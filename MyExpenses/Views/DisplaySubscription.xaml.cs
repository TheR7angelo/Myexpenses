﻿using System;
using MyExpenses.Utils.Database;
using MyExpenses.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplaySubscription
{
    private static SqLite.SubscriptionClass _selected = new();
    public DisplaySubscription()
    {
        InitializeComponent();
        BindingContext = new SubscriptionModel();
    }

    private async void MenuItem_OnClicked(object sender, EventArgs e)
    {
        var button = (ToolbarItem)sender;
        var select = DataGrid.SelectedItem as SqLite.SubscriptionClass;
        if (select is null)
        {
            var msg = button.AutomationId switch
            {
                "modify" => "Impossible de modifier, aucun élement de sélectionner",
                _ => "Erreur inconnue"
            };
            await DisplayAlert("Erreur", msg, "Ok");
            return;
        }

        Console.WriteLine(select.Id);
    }
}