using System;
using System.Collections.Generic;
using System.Linq;
using MyExpenses.Utils.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplayCategory
{
    private const string NewCategory = "NewCategory";
    private const string GridName = $"Grid_{NewCategory}";

    private static List<SqLite.CategoryClass> _dataCategory = null!;
    public DisplayCategory()
    {
        _dataCategory = SqLite.GetAllCategory();
        InitializeComponent();

        AddDisplayCategory();
    }

    #region Function

    private void AddDisplayCategory()
    {
        foreach (var category in _dataCategory) AddCategoryDisplay(category);
    }

    private void AddCategoryDisplay(SqLite.CategoryClass category)
    {
        var grid = new Grid(); //{ HeightRequest = 30 };
        grid.Children.Add(new Editor { Text = category.Name, ClassId = $"Editor_{category.Id}", IsVisible = false });
        grid.Children.Add(new Label { Text = category.Name , ClassId = $"Label_{category.Id}"});

        var swipe = new SwipeView
        {
            LeftItems = { SwipeBehaviorOnInvoked = SwipeBehaviorOnInvoked.RemainOpen },
            RightItems = { SwipeBehaviorOnInvoked = SwipeBehaviorOnInvoked.Auto }
        };

        var leftItemModify = new SwipeItem
        {
            Text = "Modifier",
            BackgroundColor = Color.Orange
        };
        leftItemModify.Clicked += SwipeModify_OnClicked;

        var leftItemValid = new SwipeItem
        {
            Text = "Valider",
            IsVisible = false,
            BackgroundColor = Color.ForestGreen,
            BindingContext = category
        };
        leftItemValid.Clicked += SwipeValidModify_OnClicked;

        var rightItemCancel = new SwipeItem
        {
            Text = "Annuler",
            IsVisible = false,
            BackgroundColor = Color.Orange
        };
        rightItemCancel.Clicked += (_, _) => SwipeViewIsVisibleInverse(swipe);
        
        var rightItemDelete = new SwipeItem
        {
            Text = "Supprimer",
            BackgroundColor = Color.Crimson,
            BindingContext = category
        };
        rightItemDelete.Clicked += SwipeDelete_OnClicked;

        swipe.LeftItems.Add(leftItemModify);
        swipe.LeftItems.Add(leftItemValid);
        
        swipe.RightItems.Add(rightItemCancel);
        swipe.RightItems.Add(rightItemDelete);
        swipe.Content = grid;
        
        StackLayoutCategory.Children.Add(swipe);
    }

    private Grid GetGridCategory() => (StackLayoutCategory.Children.Where(s => s.GetType() == typeof(Grid)).ToList()[0] as Grid)!;
    
    private void DisableAddCategory(View grid) => ButtonAddNew.IsEnabled = StackLayoutCategory.Children.Remove(grid);
    
    private static void SwipeViewIsVisibleInverse(SwipeView swipeView)
    {
        swipeView.Close();
        var grid = swipeView.Children[0] as Grid;
        foreach (var element in grid!.Children) element.IsVisible = !element.IsVisible;
        foreach (var swipe in new List<SwipeItems> { swipeView.LeftItems, swipeView.RightItems }.SelectMany(swipes => swipes))
        {
            swipe.IsVisible = !swipe.IsVisible;
        }
    }

    #endregion

    #region Actions

    private void SwipeDelete_OnClicked(object sender, EventArgs e)
    {
        var category = ((SwipeItem)sender).BindingContext as SqLite.CategoryClass;
        category!.Delete();
        
        var index = _dataCategory.FindIndex(s => s.Id.Equals(category!.Id));
        StackLayoutCategory.Children.RemoveAt(index);
        _dataCategory.RemoveAt(index);
    }

    private void SwipeModify_OnClicked(object sender, EventArgs e)
    {
        var swipeItem = (SwipeItem)sender;
        var swipeItems = swipeItem.Parent as SwipeItems;
        var swipeView = (SwipeView)swipeItems!.Parent;

        SwipeViewIsVisibleInverse(swipeView);
    }

    private void SwipeValidModify_OnClicked(object sender, EventArgs e)
    {
        var category = ((SwipeItem)sender).BindingContext as SqLite.CategoryClass;
    }

    private void ButtonAddCategory_OnClicked(object sender, EventArgs e)
    {
        ButtonAddNew.IsEnabled = false;

        var grid = new Grid{ AutomationId = GridName };
        var column0 = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
        var column1 = new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) };
        var column2 = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };

        foreach (var column in new List<ColumnDefinition>{ column0, column1, column2 }) grid.ColumnDefinitions.Add(column);

        var buttonValid = new Button { Text = "Valider", BackgroundColor = Color.ForestGreen };
        buttonValid.Clicked += ButtonValidCategory_OnClicked;
        
        var buttonCancel = new Button { Text = "Annuler", BackgroundColor = Color.Crimson };
        buttonCancel.Clicked += (_, _) => DisableAddCategory(GetGridCategory());
        
        var editor = new Editor { Placeholder = "catégorie ?", PlaceholderColor = Color.LightGray };

        foreach (var element in new List<View>{ buttonValid, editor, buttonCancel }.Select((value, index) => new { index, value }))
        {
            Grid.SetColumn(element.value, element.index);
            grid.Children.Add(element.value);
        }

        StackLayoutCategory.Children.Add(grid);
    }

    private async void ButtonValidCategory_OnClicked(object sender, EventArgs e)
    {
        var grid = GetGridCategory();
        var editor = grid.Children.Where(s => s.GetType() == typeof(Editor)).ToList()[0] as Editor;

        var category = new SqLite.CategoryClass { Name = editor!.Text };
        
        var msg = string.Empty;
        if (editor.Text is null || editor.Text.Equals(string.Empty)) msg = "Le nom de la catégorie ne peut pas étre vide";

        var duplicate = _dataCategory.Where(s => s.Name.Equals(category.Name));
        if (!duplicate.Count().Equals(0)) msg = "Nom de catégorie déja utiliser";

        if (!msg.Equals(string.Empty))
        {
            await DisplayAlert("Erreur", msg, "Ok");
            return;
        }
        
        category.Insert();
        
        DisableAddCategory(grid);
        AddCategoryDisplay(category);
        _dataCategory.Add(category);
    }

    #endregion
}