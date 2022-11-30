using CipherNoteBook.WPF.State.Navigators;
using CipherNoteBook.WPF.ViewModels.Factories;
using System;
using System.Windows.Input;

namespace CipherNoteBook.WPF.Commands;

public class UpdateCurrentViewModelCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public INavigator _navigator;
    private readonly IViewModelFactory _viewModelFactory;

    public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelFactory viewModelFactory)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
    }

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        if (parameter is not ViewType viewType) return;
        _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
    }
}
