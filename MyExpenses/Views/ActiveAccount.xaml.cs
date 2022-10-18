﻿using System;
using System.Collections;
using System.Collections.Generic;
using Microcharts;
using Microcharts.Forms;
using MyExpenses.Utils.Database;
using SkiaSharp;
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
        var entries = new List<ChartEntry>();
        var rand = new Random();
        
        
        FlexLayout.Children.Clear();
        var lst = SqLite.GetAllAccount();
        foreach (var account in lst)
        {
            // a tester
            entries.Add(new ChartEntry(rand.Next(10, 255))
            {
                Label = account.Name,
                Color = SKColors.LightBlue
            });
            
            var btn = new Button
            {
                Text = account.Name,
                ClassId = account.Name,
                Style = FindByName("WalletButton") as Style,
                Margin = new Thickness(7),
                // BackgroundColor = account.Color is not null ? Color.FromHex(account.Color) : Color.Bisque
                BackgroundColor = Color.Chocolate
            };
            FlexLayout.Children.Add(btn);
        }

        LabelNumberWallet.Text = lst.Count.ToString();
        ChartView.Chart = new PieChart{Entries = entries ,LabelTextSize = 30};
    }
    
    private void AddAccountDb(List<string> accountDb)
    {
        
    }
}