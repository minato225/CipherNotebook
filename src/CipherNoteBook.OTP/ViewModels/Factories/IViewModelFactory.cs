using CipherNoteBook.OTP.State.Navigators;
using CipherNoteBook.OTP.ViewModels;

namespace CipherNoteBook.OTP.ViewModels.Factories;

public interface IViewModelFactory
{
    BaseViewModel CreateViewModel(ViewType viewType);
}
