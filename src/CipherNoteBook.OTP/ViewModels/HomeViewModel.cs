using System;
using System.Windows.Input;
using System.Windows.Threading;
using CipherNoteBook.Domain.Services.OtpService;
using CipherNoteBook.OTP.Commands;
using CipherNoteBook.OTP.OptUtils;
using CipherNoteBook.OTP.State.Authenticators;
using CipherNoteBook.OTP.State.Navigators;

namespace CipherNoteBook.OTP.ViewModels;

public class HomeViewModel : BaseViewModel
{
    public ICommand LogOutCommand { get; }
    public ICommand CopyToClipBoardCommand { get; }

    public readonly IOtpService _otpService;
    private readonly IAuthenticator _authenticator;

    private readonly DispatcherTimer _dispatcherTimer = new(){ Interval = new TimeSpan(0, 0, 0, 1) };

    public HomeViewModel(IOtpService otpService, IAuthenticator authenticator, IRenavigator renavigator)
    {
        _otpService = otpService;
        _authenticator = authenticator;
        LogOutCommand = new LogOutCommand(authenticator, renavigator);
        CopyToClipBoardCommand = new CopyToClipBoardCommand(this);

        _dispatcherTimer.Tick += (sender, args) =>
        {
            OnPropertyChanged(nameof(OtpPinCode));
            OnPropertyChanged(nameof(TimeLeft));
        };

        _dispatcherTimer.Start();
    }

    #region Props

    public string UserName => _authenticator.CurrentAccount.AccountHolder.Username;

    public string OtpPinCode => ToptHelper.TimeBasedOneTimePassword(_authenticator.CurrentAccount.AccountHolder.Email).ToString();

    public int TimeLeft => 30 - (ToptHelper.UnixTime() % 30);

    //public override void Dispose()
    //{
    //    _dispatcherTimer.Stop();
    //    base.Dispose();
    //}

    #endregion
}