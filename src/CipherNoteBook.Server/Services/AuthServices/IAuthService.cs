using CipherNoteBook.Domain.Models;
using CipherNoteBook.Server.Models.AuthModels;

namespace CipherNoteBook.Server.Services.AuthServices;

public interface IAuthService
{
    AuthenticateResponse Register(User model, string ipAddress);
    AuthenticateResponse Login(LoginRequestModel model, string ipAddress);
}