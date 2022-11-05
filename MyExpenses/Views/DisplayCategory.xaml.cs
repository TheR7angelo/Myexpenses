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

    public void AddDisplayCategory(bool reset = false)
    {
        if (reset) StackLayoutCategory.Children.Clear();
        foreach (var category in _dataCategory) AddCategoryDisplay(category);
    }

    public void AddDisplayStore(SqLite.CategoryClass category) => AddCategoryDisplay(category);
    
    private void AddCategoryDisplay(SqLite.CategoryClass category)
    {
        var grid = new Grid { HeightRequest = 30 };
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
            BackgroundColor = Color.Orange,
            BindingContext = category
        };
        leftItemModify.Clicked += SwipeModify_OnClicked;

        var lefItemValid = new SwipeItem
        {
            Text = "Valider",
            IsVisible = false,
            BackgroundColor = Color.ForestGreen,
            BindingContext = category
        };
        lefItemValid.Clicked += SwipeValid_OnClicked;

        var rightItem = new SwipeItem
        {
            Text = "Supprimer",
            BackgroundColor = Color.Crimson,
            BindingContext = category
        };
        rightItem.Clicked += SwipeDelete_OnClicked;

        swipe.LeftItems.Add(leftItemModify);
        swipe.RightItems.Add(rightItem);
        swipe.Content = grid;
        
        StackLayoutCategory.Children.Add(swipe);
    }

    #endregion

    #region Actions

    private void SwipeDelete_OnClicked(object sender, EventArgs e)
    {
        // var lieu = ((SwipeItem)sender).BindingContext as SqLite.CategoryClass;
        // lieu!.Delete();
        //
        // var index = _dataCategory.FindIndex(s => s.Id.Equals(lieu!.Id));
        // StackLayoutCategory.Children.RemoveAt(index);
        // _dataCategory.RemoveAt(index);
    }

    private void SwipeModify_OnClicked(object sender, EventArgs e)
    {
        var category = ((SwipeItem)sender).BindingContext as SqLite.CategoryClass;
        
        //Navigation.PushAsync(new AddCategory(this, category));
    }

    private void SwipeValid_OnClicked(object sender, EventArgs e)
    {
        // pass
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

        if (editor!.Text is null || editor.Text.Equals(string.Empty))
        {
            await DisplayAlert("Erreur", "Le nom de la catégorie ne peut pas étre vide", "Ok");
            return;
        }
    
        var category = new SqLite.CategoryClass { Name = editor.Text };
        category.Insert();
        
        DisableAddCategory(grid);
        AddCategoryDisplay(category);
    }

    private Grid GetGridCategory() => (StackLayoutCategory.Children.Where(s => s.GetType() == typeof(Grid)).ToList()[0] as Grid)!;
    private void DisableAddCategory(View grid) => ButtonAddNew.IsEnabled = StackLayoutCategory.Children.Remove(grid);

    #endregion
}