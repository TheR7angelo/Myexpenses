using MyExpenses.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MyExpenses
{
    public partial class AppShell
    {

        public AppShell()
        {
            InitializeComponent();

            ShellTest.Items.Add(new ShellSection{Title = "home", Items = { new ShellContent{Content = new Home()} }});
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Current.GoToAsync("//LoginPage");
        }
    }
}
