using System;
using System.Collections.Generic;
using MyExpenses.Utils.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplayStore
{
    private static List<SqLite.LieuClass> _dataStore = null!;
    public DisplayStore()
    {
        _dataStore = SqLite.GetAllLieu();
        
        //todo à finir
        
        InitializeComponent();

        FillAddress();
    }

    private void FillAddress()
    {
        foreach (var store in _dataStore)
        {
            var grid = new Grid { HeightRequest = 30 };
            grid.Children.Add(new Label { Text = store.Name });

            var swipe = new SwipeView
            {
                LeftItems = { SwipeBehaviorOnInvoked = SwipeBehaviorOnInvoked.RemainOpen },
                RightItems = { SwipeBehaviorOnInvoked = SwipeBehaviorOnInvoked.Auto }
            };

            var leftItem = new SwipeItem
            {
                Text = "Modifier",
                BackgroundColor = Color.Orange,
                BindingContext = store
            };
            leftItem.Clicked += SwipeModify_OnClicked;

            var rightItem = new SwipeItem
            {
                Text = "Supprimer",
                BackgroundColor = Color.Crimson,
                BindingContext = store
            };
            rightItem.Clicked += SwipeDelete_OnClicked;
            
            swipe.LeftItems.Add(leftItem);
            swipe.RightItems.Add(rightItem);
            swipe.Content = grid;
            
            StackLayoutAddress.Children.Add(swipe);
        }

        var btn = new Button
        {
            Text = "Add"
        };
        btn.Clicked += BtnOnClicked;
        StackLayoutAddress.Children.Add(btn);
    }

    private void SwipeDelete_OnClicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void SwipeModify_OnClicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void BtnOnClicked(object sender, EventArgs e) => Navigation.PushAsync(new AddAddress());
}