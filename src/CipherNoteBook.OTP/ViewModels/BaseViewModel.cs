using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CipherNoteBook.OTP.ViewModels;

public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : BaseViewModel;

public class BaseViewModel : INotifyPropertyChanged
{
    public virtual void Dispose() { }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = default) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
