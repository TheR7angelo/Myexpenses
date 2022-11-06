using System;
using System.Collections.Generic;
using System.Linq;
using MyExpenses.Utils.Database;
using Xamarin.Forms;

namespace MyExpenses.Utils.Function;

public class BaseDisplay
{
    private static Type _type = null!;
    
    private static List<ITableDisplay> _data = null!;
    private readonly StackLayout _displayLayout;
    private readonly Button _buttonAdd;
    
    private const string NewCategory = "NewCategory";
    private const string GridName = $"Grid_{NewCategory}";
    
    public BaseDisplay(StackLayout stackLayout, Button buttonAdd, List<ITableDisplay> data, Type type)
    {
        _type = type;
        
        _displayLayout = stackLayout;
        _buttonAdd = buttonAdd;
        _data = data;
        
        AddDisplays();
    }

    #region Function

    private void AddDisplays()
    {
        foreach (var d in _data) AddDisplays(d);
    }

    private void AddDisplays(ITableDisplay obj)
    {
        var grid = new Grid();
        grid.Children.Add(new Editor { Text = obj.Name, ClassId = $"Editor_{obj.Id}", IsVisible = false });
        grid.Children.Add(new Label { Text = obj.Name , ClassId = $"Label_{obj.Id}"});
        
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
            BindingContext = obj
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
            BindingContext = obj
        };
        rightItemDelete.Clicked += SwipeDelete_OnClicked;

        swipe.LeftItems.Add(leftItemModify);
        swipe.LeftItems.Add(leftItemValid);
        
        swipe.RightItems.Add(rightItemCancel);
        swipe.RightItems.Add(rightItemDelete);
        swipe.Content = grid;
        
        _displayLayout.Children.Add(swipe);
    }
    
    private void DisableAddCategory(View grid) => _buttonAdd.IsEnabled = _displayLayout.Children.Remove(grid);
    
    private static async void DisplayAlert(string title, string msg, string cancel) => await Application.Current.MainPage.DisplayAlert(title, msg, cancel);
    private static async void DisplayAlert(string title, string msg, string accept, string cancel) => await Application.Current.MainPage.DisplayAlert(title, msg, accept,cancel);
    
    private static int FindUse(ITableDisplay display) ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    {
        if (_type == typeof(SqLite.CategoryClass)) return SqLite.GetAllHistorique().Where(s => s.TypeCategorieFk.Equals(display.Id)).ToList().Count;
        if (_type == typeof(SqLite.WalletTypeClass)) return SqLite.GetAllTWallet().Where(s => s.TypeCompteFk.Equals(display.Id)).ToList().Count;
        if (_type == typeof(SqLite.PaymentClass)) return SqLite.GetAllHistorique().Where(s => s.TypePayementFk.Equals(display.Id)).ToList().Count;

        return 0;

    }
    
    private Grid GetGridCategory() => (_displayLayout.Children.Where(s => s.GetType() == typeof(Grid)).ToList()[0] as Grid)!;

    private static ITableDisplay? GetNew(string name) /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    {
        if (_type == typeof(SqLite.CategoryClass)) return new SqLite.CategoryClass { Name = name };
        if (_type == typeof(SqLite.WalletTypeClass)) return new SqLite.WalletTypeClass { Name = name };
        if (_type == typeof(SqLite.PaymentClass)) return new SqLite.PaymentClass { Name = name };
        
        return null;
    }
    
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

    #region Action

    public void ButtonAdd_OnClicked(object sender, EventArgs e)
    {
        _buttonAdd.IsEnabled = false;
    
        var grid = new Grid{ AutomationId = GridName };
        var column0 = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
        var column1 = new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) };
        var column2 = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
    
        foreach (var column in new List<ColumnDefinition>{ column0, column1, column2 }) grid.ColumnDefinitions.Add(column);
    
        var buttonValid = new Button { Text = "Valider", BackgroundColor = Color.ForestGreen };
        buttonValid.Clicked += ButtonValid_OnClicked;
        
        var buttonCancel = new Button { Text = "Annuler", BackgroundColor = Color.Crimson };
        buttonCancel.Clicked += (_, _) => DisableAddCategory(GetGridCategory());
        
        var editor = new Editor { Placeholder = "catégorie ?", PlaceholderColor = Color.LightGray };
    
        foreach (var element in new List<View>{ buttonValid, editor, buttonCancel }.Select((value, index) => new { index, value }))
        {
            Grid.SetColumn(element.value, element.index);
            grid.Children.Add(element.value);
        }
    
        _displayLayout.Children.Add(grid);
    }
    
    private void ButtonValid_OnClicked(object sender, EventArgs e)
    {
        var grid = GetGridCategory();
        var editor = grid.Children.Where(s => s.GetType() == typeof(Editor)).ToList()[0] as Editor;

        var obj = GetNew(editor!.Text);

        var msg = string.Empty;
        if (editor.Text is null || editor.Text.Equals(string.Empty)) msg = "Le nom de la catégorie ne peut pas étre vide";
    
        var duplicate = _data.Where(s => s.Name.Equals(obj!.Name));
        if (!duplicate.Count().Equals(0)) msg = "Nom de catégorie déja utiliser";
    
        if (!msg.Equals(string.Empty))
        {
            DisplayAlert("Erreur", msg, "Ok");
            return;
        }
        
        obj!.Insert();
        
        DisableAddCategory(grid);
        AddDisplays(obj!);
        _data.Add(obj!);
    }
    
    private void SwipeDelete_OnClicked(object sender, EventArgs e)
    {
        var obj = ((SwipeItem)sender).BindingContext as ITableDisplay;

        var uses = FindUse(obj!);
        if (!uses.Equals(0))
        {
            DisplayAlert("Erreur", "Imposible de supprimer car cette catégorie est en cours d'utilisation", "Ok");
            return;
        }
        obj!.Delete();
        
        var index = _data.FindIndex(s => s.Id.Equals(obj!.Id));
        _displayLayout.Children.RemoveAt(index);
        _data.RemoveAt(index);
    }
    
    private static void SwipeModify_OnClicked(object sender, EventArgs e)
    {
        var swipeItem = (SwipeItem)sender;
        var swipeItems = swipeItem.Parent as SwipeItems;
        var swipeView = (SwipeView)swipeItems!.Parent;

        SwipeViewIsVisibleInverse(swipeView);
    }
    
    private static void SwipeValidModify_OnClicked(object sender, EventArgs e)
    {
        Label? label = null;
        Editor? editor = null;
        
        var title = "Erreur";
        var msg = "Le nom de la catégorie ne peut pas être null";
        
        var swipeItems = ((SwipeItem)sender).Parent as SwipeItems;
        var swipeView = swipeItems!.Parent as SwipeView;

        var grid = swipeView!.Children[0] as Grid;

        foreach (var element in grid!.Children)
        {
            if (element.GetType() == typeof(Label)) label = (element as Label)!;
            else { editor = (element as Editor)!; }
        }

        if (editor!.Text is not null || !editor.Text!.Equals(string.Empty))
        {
            title = "Réussite";
            msg = "Modification appliquer";
            
            var obj = ((SwipeItem)sender).BindingContext as ITableDisplay;
            obj!.Name = editor.Text;

            obj.Update();
            
            label!.Text = editor.Text;
            
            var index = _data.FindIndex(s => s.Id.Equals(obj.Id));
            _data[index] = obj;
            
            SwipeViewIsVisibleInverse(swipeView);
        }
        
        DisplayAlert(title, msg, "Ok");
    }

    #endregion
}