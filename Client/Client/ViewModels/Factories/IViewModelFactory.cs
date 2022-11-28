using Client.WPF.State.Navigators;
using Client.WPF.ViewModels;

namespace Client.WPF.ViewModels.Factories;

public interface IViewModelFactory
{
    BaseViewModel CreateViewModel(ViewType viewType);
}
