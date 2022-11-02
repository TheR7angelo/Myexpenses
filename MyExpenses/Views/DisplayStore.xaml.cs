using System;
using System.Collections.Generic;
using MyExpenses.Utils.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplayStore
{
    public static List<SqLite.LieuClass> DataStore = null!;
    public DisplayStore()
    {
        DataStore = SqLite.GetAllLieu();
        
        //todo à finir
        
        InitializeComponent();

        FillAddress();
    }

    public void FillAddress(SqLite.LieuClass store) => AddStoreDisplay(store);
    
    private void FillAddress()
    {
        foreach (var store in DataStore) AddStoreDisplay(store);
    }

    private void AddStoreDisplay(SqLite.LieuClass store)
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

    private void SwipeDelete_OnClicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void SwipeModify_OnClicked(object sender, EventArgs e)
    {
        var swipeItem = (SwipeItem)sender;
        var lieu = swipeItem.BindingContext as SqLite.LieuClass;
        Navigation.PushAsync(new AddAddress(this, lieu));
    }

    private void ButtonAddStore_OnClicked(object sender, EventArgs e) => Navigation.PushAsync(new AddAddress(this));
}