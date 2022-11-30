using CipherNoteBook.OTP.ViewModels;
using System;

namespace CipherNoteBook.OTP.State.Navigators;

public class Navigator : INavigator
{
    private BaseViewModel _currentViewModel;
    public BaseViewModel CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel?.Dispose();

            _currentViewModel = value;
            StateChanged?.Invoke();
        }
    }

    public event Action StateChanged;
}
