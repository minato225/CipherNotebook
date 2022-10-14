using Microsoft.Extensions.Options;
using System;

namespace Server.Service
{
    public interface IWritableOptions<out T> : IOptions<T> where T : class, new()
    {
        void ChangeAppSettingValue(Action<T> applyChanges);
    }
}
