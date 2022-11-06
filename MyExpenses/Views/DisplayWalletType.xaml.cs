using System;
using System.Collections.Generic;
using System.Linq;
using MyExpenses.Utils.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplayWalletType
{
    private const string NewCategory = "NewCategory";
    private const string GridName = $"Grid_{NewCategory}";
    
    private static List<SqLite.WalletTypeClass> _dataAccountType = null!;
    
    public DisplayWalletType()
    {
        _dataAccountType = SqLite.GetAllWalletType();
        
        InitializeComponent();

        AddDisplayWalletType();
    }
    
    #region Function

    private void AddDisplayWalletType()
    {
        foreach (var walletType in _dataAccountType) AddWalletTypeDisplay(walletType);
    }

    private void AddWalletTypeDisplay(SqLite.WalletTypeClass walletType)
    {
        var grid = new Grid();
        grid.Children.Add(new Editor { Text = walletType.Name, ClassId = $"Editor_{walletType.Id}", IsVisible = false });
        grid.Children.Add(new Label { Text = walletType.Name , ClassId = $"Label_{walletType.Id}"});

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
            BindingContext = walletType
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
            BindingContext = walletType
        };
        rightItemDelete.Clicked += SwipeDelete_OnClicked;

        swipe.LeftItems.Add(leftItemModify);
        swipe.LeftItems.Add(leftItemValid);
        
        swipe.RightItems.Add(rightItemCancel);
        swipe.RightItems.Add(rightItemDelete);
        swipe.Content = grid;
        
        StackLayoutAccountType.Children.Add(swipe);
    }

    private Grid GetGridWalletType() => (StackLayoutAccountType.Children.Where(s => s.GetType() == typeof(Grid)).ToList()[0] as Grid)!;
    
    private void DisableAddCategory(View grid) => ButtonAddNew.IsEnabled = StackLayoutAccountType.Children.Remove(grid);
    
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

    private async void SwipeDelete_OnClicked(object sender, EventArgs e)
    {
        var walletTypeClass = ((SwipeItem)sender).BindingContext as SqLite.WalletTypeClass;

        var uses = SqLite.GetAllTWallet().Where(s => s.TypeCompteFk.Equals(walletTypeClass!.Id)).ToList();
        if (!uses.Count.Equals(0))
        {
            await DisplayAlert("Erreur", "Imposible de supprimer car cette catégorie est en cours d'utilisation", "Ok");
            return;
        }
        
        walletTypeClass!.Delete();
        
        var index = _dataAccountType.FindIndex(s => s.Id.Equals(walletTypeClass!.Id));
        StackLayoutAccountType.Children.RemoveAt(index);
        _dataAccountType.RemoveAt(index);
    }

    private static void SwipeModify_OnClicked(object sender, EventArgs e)
    {
        var swipeItem = (SwipeItem)sender;
        var swipeItems = swipeItem.Parent as SwipeItems;
        var swipeView = (SwipeView)swipeItems!.Parent;

        SwipeViewIsVisibleInverse(swipeView);
    }

    private async void SwipeValidModify_OnClicked(object sender, EventArgs e)
    {
        Label? label = null;
        Editor? editor = null;
        
        var title = "Erreur";
        var msg = "Le nom du type de compte ne peut pas être null";
        
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
            
            var walletTypeClass = ((SwipeItem)sender).BindingContext as SqLite.WalletTypeClass;
            walletTypeClass!.Name = editor.Text;

            walletTypeClass.Update();
            
            label!.Text = editor.Text;
            
            var index = _dataAccountType.FindIndex(s => s.Id.Equals(walletTypeClass.Id));
            _dataAccountType[index] = walletTypeClass;
            
            SwipeViewIsVisibleInverse(swipeView);
        }
        
        await DisplayAlert(title, msg, "Ok");
    }

    private void ButtonAddWalletType_OnClicked(object sender, EventArgs e)
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
        buttonCancel.Clicked += (_, _) => DisableAddCategory(GetGridWalletType());
        
        var editor = new Editor { Placeholder = "Type de compte ?", PlaceholderColor = Color.LightGray };

        foreach (var element in new List<View>{ buttonValid, editor, buttonCancel }.Select((value, index) => new { index, value }))
        {
            Grid.SetColumn(element.value, element.index);
            grid.Children.Add(element.value);
        }

        StackLayoutAccountType.Children.Add(grid);
    }

    private async void ButtonValidCategory_OnClicked(object sender, EventArgs e)
    {
        var grid = GetGridWalletType();
        var editor = grid.Children.Where(s => s.GetType() == typeof(Editor)).ToList()[0] as Editor;

        var walletTypeClass = new SqLite.WalletTypeClass { Name = editor!.Text };
        
        var msg = string.Empty;
        if (editor.Text is null || editor.Text.Equals(string.Empty)) msg = "Le nom de la catégorie ne peut pas étre vide";

        var duplicate = _dataAccountType.Where(s => s.Name.Equals(walletTypeClass.Name));
        if (!duplicate.Count().Equals(0)) msg = "Nom de catégorie déja utiliser";

        if (!msg.Equals(string.Empty))
        {
            await DisplayAlert("Erreur", msg, "Ok");
            return;
        }
        
        walletTypeClass.Insert();
        
        DisableAddCategory(grid);
        AddWalletTypeDisplay(walletTypeClass);
        _dataAccountType.Add(walletTypeClass);
    }

    #endregion
}