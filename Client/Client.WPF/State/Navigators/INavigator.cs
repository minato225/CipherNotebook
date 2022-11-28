using Client.WPF.ViewModels;
using System;

namespace Client.WPF.State.Navigators;

public enum ViewType
{
    Login,
    Home
}

public interface INavigator
{
    BaseViewModel CurrentViewModel { get; set; }
    event Action StateChanged;
}
