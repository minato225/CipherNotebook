using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.Models;

public class ObservableObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = default) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
