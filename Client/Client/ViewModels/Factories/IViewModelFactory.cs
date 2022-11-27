using Client.State.Navigators;

namespace Client.ViewModels.Factories;

public interface IViewModelFactory
{
    BaseViewModel CreateViewModel(ViewType viewType);
}
