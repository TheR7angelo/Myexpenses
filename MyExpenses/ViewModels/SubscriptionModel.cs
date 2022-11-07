﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MyExpenses.Utils.Database;
using Xamarin.Forms;

namespace MyExpenses.ViewModels;

public class SubscriptionModel : INotifyPropertyChanged
{
    private List<SqLite.SubscriptionClass> _subscriptionData;
    private SqLite.SubscriptionClass _selectedSubscription = null!;
    private bool _isRefreshing;

    public SubscriptionModel()
    {
        // _subscriptionData = new ObservableCollection<SqLite.SubscriptionClass>(SqLite.GetAllSubscription());
        _subscriptionData = TestData();
        RefreshCommand = new Command(CmdRefresh);
    }

    private static List<SqLite.SubscriptionClass> TestData()
    {
        var lst = new List<SqLite.SubscriptionClass>(SqLite.GetAllSubscription());
        // var lst = new ObservableCollection<SqLite.SubscriptionClass>();
        lst.Add(new SqLite.SubscriptionClass
        {
            Id = 1,
            Amount = 5,
            Duration = 3,
            LieuFk = 1,
            PaymentTypeFk = 1,
            Raison = "Test débile qui va disparaitre",
            Recurrence = 1
        });
        lst.Add(new SqLite.SubscriptionClass
        {
            Id = 2,
            Amount = 5,
            Duration = 3,
            LieuFk = 1,
            PaymentTypeFk = 1,
            Raison = "Test débile qui va disparaitre",
            Recurrence = 1
        });

        return lst;
    }
    
    public List<SqLite.SubscriptionClass> SubscriptionData
    {
        get => _subscriptionData;
        set
        {
            _subscriptionData = value;
            OnPropertyChanged(nameof(SubscriptionData));
        }
    }
    
    public SqLite.SubscriptionClass SelectedSubscription
    {
        get => _selectedSubscription;
        set
        {
            _selectedSubscription = value;
            OnPropertyChanged(nameof(SelectedSubscription));
        }
    }

    public bool IsRefreshing
    {
        get => _isRefreshing;
        set
        {
            _isRefreshing = value;
            OnPropertyChanged(nameof(IsRefreshing));
        }
    }

    private async void CmdRefresh()
    {
        IsRefreshing = true;
        await Task.Delay(3000);
        IsRefreshing = false;
    }
    
    public ICommand RefreshCommand { get; set; }
    
    public event PropertyChangedEventHandler PropertyChanged = null!;

    private void OnPropertyChanged(string property) => PropertyChanged(this, new PropertyChangedEventArgs(property));
}