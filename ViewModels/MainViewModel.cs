using Avalonia.Controls;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.ViewModels.SplitViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AvaloniaApplication1.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public MainViewModel() 
    {
        Items = new ObservableCollection<ListItemTemplate>(_template);
        SelectedListItem = Items.First(vm=> vm.ModelType == typeof(DashboardPageViewModel));
    }

    [ObservableProperty]
    private bool _isPaneOpen; 

    public ObservableCollection<ListItemTemplate> Items {get;}

    [ObservableProperty]
    private ListItemTemplate _selectedListItem;

    partial void OnSelectedListItemChanged(ListItemTemplate? value)
    {
        if (value is null) return;

        var instance = Activator.CreateInstance(value.ModelType);
        if (instance == null) return;
        CurrentPage = (ViewModelBase)instance;

        //var vm = Design.IsDesignMode
        //    ? Activator.CreateInstance(value.ModelType)
        //    : Ioc.Default.GetService(value.ModelType);

        //if (vm is not ViewModelBase vmb) return;

        //CurrentPage = vmb;


    }



[RelayCommand]
    private void PaneMode()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    [ObservableProperty]
    private ViewModelBase _currentPage = new DashboardPageViewModel();

    private readonly List<ListItemTemplate> _template = [
        new ListItemTemplate(typeof(DashboardPageViewModel), "Home", "Dashboard"),
        new ListItemTemplate(typeof(DistributePageViewModel), "Package 2", "Distribute"),
        new ListItemTemplate(typeof(PurchasePageViewModel), "Shopping Bag", "Purchase"),
        new ListItemTemplate(typeof(RecordsPageViewModel), "analytics", "Records")

        ];

}
