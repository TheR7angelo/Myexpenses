using System;
using System.Collections.Generic;
using MyExpenses.Utils.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplayCategory
{
    private static List<SqLite.CategoryClass> _dataCategory = null!;
    public DisplayCategory()
    {
        _dataCategory = SqLite.GetAllCategory();
        InitializeComponent();

        AddDisplayCategory();
    }

    #region Function

    public void AddDisplayCategory(bool reset = false)
    {
        if (reset) StackLayoutCategory.Children.Clear();
        foreach (var category in _dataCategory) AddCategoryDisplay(category);
    }

    public void AddDisplayStore(SqLite.CategoryClass category) => AddCategoryDisplay(category);
    
    private void AddCategoryDisplay(SqLite.CategoryClass category)
    {
        var grid = new Grid { HeightRequest = 30 };
        grid.Children.Add(new Label { Text = category.Name });

        var swipe = new SwipeView
        {
            LeftItems = { SwipeBehaviorOnInvoked = SwipeBehaviorOnInvoked.RemainOpen },
            RightItems = { SwipeBehaviorOnInvoked = SwipeBehaviorOnInvoked.Auto }
        };

        var leftItem = new SwipeItem
        {
            Text = "Modifier",
            BackgroundColor = Color.Orange,
            BindingContext = category
        };
        leftItem.Clicked += SwipeModify_OnClicked;

        var rightItem = new SwipeItem
        {
            Text = "Supprimer",
            BackgroundColor = Color.Crimson,
            BindingContext = category
        };
        rightItem.Clicked += SwipeDelete_OnClicked;

        swipe.LeftItems.Add(leftItem);
        swipe.RightItems.Add(rightItem);
        swipe.Content = grid;
        
        StackLayoutCategory.Children.Add(swipe);
    }

    #endregion

    #region Actions

    private void SwipeDelete_OnClicked(object sender, EventArgs e)
    {
        var lieu = ((SwipeItem)sender).BindingContext as SqLite.LieuClass;
        lieu!.Delete();
        
        var index = _dataCategory.FindIndex(s => s.Id.Equals(lieu!.Id));
        StackLayoutCategory.Children.RemoveAt(index);
        _dataCategory.RemoveAt(index);
    }

    private void SwipeModify_OnClicked(object sender, EventArgs e)
    {
        var category = ((SwipeItem)sender).BindingContext as SqLite.CategoryClass;
        Navigation.PushAsync(new AddCategory(this, category));
    }

    private void ButtonAddCategory_OnClicked(object sender, EventArgs e) => Navigation.PushAsync(new AddCategory(this));

    #endregion
}