﻿using Client.WPF.Services.CipherService;
using Client.WPF.ViewModels;
using System.Threading.Tasks;

namespace Client.WPF.Commands;

public class DecryptRequestCommand : AsyncCommandBase
{
    private readonly HomeViewModel _homeViewModel;
    private readonly ICypherFileTranspher _cypherFileTranspher;

    public DecryptRequestCommand(HomeViewModel homeViewModel, ICypherFileTranspher cypherFileTranspher)
    {
        _homeViewModel = homeViewModel;
        _cypherFileTranspher = cypherFileTranspher;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        _homeViewModel.DecryptedFileText = await _cypherFileTranspher.Decrypt(_homeViewModel.EncryptedFileText, _homeViewModel.SessionKey);
    }

    public override bool CanExecute(object parameter) => base.CanExecute(parameter);
}