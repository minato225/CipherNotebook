using CipherNoteBook.OTP.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace CipherNoteBook.OTP.Commands;

public class CopyToClipBoardCommand : ICommand
{
    private readonly HomeViewModel _homeViewModel;

    public CopyToClipBoardCommand(HomeViewModel homeViewModel) => _homeViewModel = homeViewModel;

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter) => Clipboard.SetText(_homeViewModel.OtpPinCode);
}
