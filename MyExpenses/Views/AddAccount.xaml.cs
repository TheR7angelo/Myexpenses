using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AddAccount
{
    public AddAccount()
    {
        InitializeComponent();

        const int minWidth = 200;
        const int maxLenght = 20;
        
        foreach (var editor in new List<Editor> { EditorNameAccount })
        {
            editor.WidthRequest = minWidth;
        }
        
        foreach (var entry in new List<Entry>{ EntryPassword, EntryPasswordConfirm })
        {
            entry.WidthRequest = minWidth;
            entry.MaxLength = maxLenght;
        }
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        var password = string.Empty;
        
        if (CheckBoxPassword.IsChecked)
        {
            if (!EntryPassword.Text.Equals(EntryPasswordConfirm.Text))
            {
                await DisplayAlert("Alert", "Les mot de passe ne correspondent pas", "OK");
                return;
            }
            password = EntryPassword.Text;
        }
        await CreateDb(EditorNameAccount.Text, password);
        await DisplayAlert("Alert", "Compte ajouté", "OK");
    }

    private static async Task CreateDb(string name, string password)
    {
        //todo finir
        // pass
    }

    private void CheckBoxPassword_OnCheckedChanged(object sender, CheckedChangedEventArgs e) => StackLayoutPassword.IsVisible = ((CheckBox)sender).IsChecked;
    
    private void EditorNameAccount_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var editor = sender as Editor;
        foreach (var c in new List<string>{"\\", "/", ":", "*", "?", "\"", "<", ">", "|"}.Where(c => editor!.Text.Contains(c)))
        {
            editor!.Text = editor.Text.Replace(c, "");
        }
    }
}