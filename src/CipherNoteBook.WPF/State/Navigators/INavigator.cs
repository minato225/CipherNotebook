using CipherNoteBook.WPF.ViewModels;
using System;

namespace CipherNoteBook.WPF.State.Navigators;

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
