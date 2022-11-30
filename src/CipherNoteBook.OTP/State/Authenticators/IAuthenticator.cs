using CipherNoteBook.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CipherNoteBook.OTP.State.Authenticators;

public interface IAuthenticator
{
    Account CurrentAccount { get; }
    bool IsLoggined { get; }

    event Action StateChanged;

    Task Login(string username, string password);

    void Logout();
}
