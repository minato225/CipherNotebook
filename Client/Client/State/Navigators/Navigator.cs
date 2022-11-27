using Client.Models;
using Client.ViewModels;
using System;

namespace Client.State.Navigators;

public enum ViewType
{
    Login,
    Home
}

public class Navigator : ObservableObject, INavigator
{
    private BaseViewModel _currentViewModel;

    public BaseViewModel CurrentViewModel {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged();
            StateChanged?.Invoke();
        }
    }

    public event Action StateChanged;
}
