using System;
using System.Collections.Generic;
using System.Linq;
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

    private void Button_OnClicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
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