using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyExpenses.Utils.Database;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplaySubscription
{
    private static readonly ObservableCollection<SqLite.SubscriptionClass> Subscription = new ();
    public DisplaySubscription()
    {
        try
        {
            InitializeComponent();
            DataGrid.ItemsSource = Subscription;

            SqLite.GetAllSubscription().ForEach(s => Subscription.Add(s));
            
            Subscription.Add(new SqLite.SubscriptionClass
            {
                Id = 1,
                Amount = 5,
                Duration = 3,
                LieuFk = 1,
                PaymentTypeFk = 1,
                Raison = "Test débile qui va disparaitre",
                Recurrence = 1
            });
            Subscription.Add(new SqLite.SubscriptionClass
            {
                Id = 2,
                Amount = 5,
                Duration = 3,
                LieuFk = 1,
                PaymentTypeFk = 1,
                Raison = "Test débile qui va disparaitre",
                Recurrence = 1
            });
            
            Subscription.CollectionChanged += SubscriptionOnCollectionChanged;
        }
        catch (Exception e)
        {
            DisplayAlert("e", e.Message, "a");
        }

    }

    private void SubscriptionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        Console.WriteLine("hey");
    }
}