using CipherNoteBook.WPF.State.Navigators;
using CipherNoteBook.WPF.ViewModels;

namespace CipherNoteBook.WPF.ViewModels.Factories;

public interface IViewModelFactory
{
    BaseViewModel CreateViewModel(ViewType viewType);
}
