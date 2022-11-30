using Microsoft.Extensions.Options;

namespace CipherNoteBook.Server.Services.OptionsServices;

public interface IWritableOptions<out T> : IOptions<T> where T : class, new()
{
    void ChangeAppSettingValue(Action<T> applyChanges);
}
