﻿using CipherNoteBook.WPF.State.Navigators;
using System;
using System.Windows.Input;

namespace CipherNoteBook.WPF.Commands;

public class RenavigateCommand : ICommand
{
    private readonly IRenavigator _renavigator;

    public RenavigateCommand(IRenavigator renavigator) => _renavigator = renavigator;

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter) => _renavigator.Renavigate();
}
