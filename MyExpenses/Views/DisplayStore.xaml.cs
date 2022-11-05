using System;
using System.Collections.Generic;
using System.Linq;
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
        DataStore = SqLite.GetAllStore();

        InitializeComponent();

        AddDisplayStore();
    }

    #region Function

    public void AddDisplayStore(bool reset = false)
    {
        if (reset) StackLayoutStore.Children.Clear();
        foreach (var store in DataStore) AddStoreDisplay(store);
    }
    
    public void AddDisplayStore(SqLite.LieuClass store) => AddStoreDisplay(store);

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
            
        StackLayoutStore.Children.Add(swipe);
    }

    #endregion

    #region Actions

    private async void SwipeDelete_OnClicked(object sender, EventArgs e)
    {
        var lieu = ((SwipeItem)sender).BindingContext as SqLite.LieuClass;
        
        var uses = SqLite.GetAllHistorique().Where(s => s.LieuFk.Equals(lieu!.Id)).ToList();
        if (!uses.Count.Equals(0))
        {
            await DisplayAlert("Erreur", "Imposible de supprimer car ce magasin est en cours d'utilisation", "Ok");
            return;
        }
        
        lieu!.Delete();
        
        var index = DataStore.FindIndex(s => s.Id.Equals(lieu!.Id));
        StackLayoutStore.Children.RemoveAt(index);
        DataStore.RemoveAt(index);
    }

    private void SwipeModify_OnClicked(object sender, EventArgs e)
    {
        var lieu = ((SwipeItem)sender).BindingContext as SqLite.LieuClass;
        Navigation.PushAsync(new AddAddress(this, lieu));
    }

    private void ButtonAddStore_OnClicked(object sender, EventArgs e) => Navigation.PushAsync(new AddAddress(this));

    #endregion
}