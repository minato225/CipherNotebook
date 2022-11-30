using System.Windows.Input;
using CipherNoteBook.Domain.Services.OtpService;
using CipherNoteBook.OTP.Commands;
using CipherNoteBook.OTP.State.Authenticators;
using CipherNoteBook.OTP.State.Navigators;

namespace CipherNoteBook.OTP.ViewModels;

public class HomeViewModel : BaseViewModel
{
    public ICommand LogOutCommand { get; }
    public ICommand CopyToClipBoardCommand { get; }

    public readonly IOtpService _otpService;
    private readonly IAuthenticator _authenticator;

    public HomeViewModel(IOtpService otpService, IAuthenticator authenticator, IRenavigator renavigator)
    {
        _otpService = otpService;
        _authenticator = authenticator;
        LogOutCommand = new LogOutCommand(authenticator, renavigator);
        CopyToClipBoardCommand = new CopyToClipBoardCommand(this);
    }

    #region Props

    public string UserName => _authenticator.CurrentAccount.AccountHolder.Username;

    private string _otpPinCode;
    public string OtpPinCode
    {
        get => _otpPinCode;
        set
        {
            _otpPinCode = value;
            OnPropertyChanged();
        }
    }

    #endregion
}