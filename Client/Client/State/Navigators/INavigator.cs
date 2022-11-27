using Client.ViewModels;
using System;
using System.Windows.Input;

namespace Client.State.Navigators;

public interface INavigator
{
    BaseViewModel CurrentViewModel { get; set; }
    event Action StateChanged;
}
