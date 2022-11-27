﻿using Client.State.Navigators;
using System;

namespace Client.ViewModels.Factories;

internal class ViewModelFactory : IViewModelFactory
{
    private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
    private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;

    public ViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel,CreateViewModel<LoginViewModel> createLoginViewModel)
    {
        _createHomeViewModel = createHomeViewModel;
        _createLoginViewModel = createLoginViewModel;
    }

    public BaseViewModel CreateViewModel(ViewType viewType) => viewType switch
    {
        ViewType.Login => _createLoginViewModel(),
        ViewType.Home => _createHomeViewModel(),
        _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType"),
    };
}
