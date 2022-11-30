using CipherNoteBook.OTP.ViewModels;
using System;

namespace CipherNoteBook.OTP.State.Navigators;

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
