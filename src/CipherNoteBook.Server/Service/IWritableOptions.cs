using Microsoft.Extensions.Options;

namespace CipherNoteBook.Server.Service;

public interface IWritableOptions<out T> : IOptions<T> where T : class, new()
{
    void ChangeAppSettingValue(Action<T> applyChanges);
}
