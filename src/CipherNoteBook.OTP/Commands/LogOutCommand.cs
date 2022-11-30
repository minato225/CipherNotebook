using CipherNoteBook.OTP.State.Authenticators;
using CipherNoteBook.OTP.State.Navigators;
using System;
using System.Windows.Input;

namespace CipherNoteBook.OTP.Commands
{
    internal class LogOutCommand : ICommand
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LogOutCommand(IAuthenticator authenticator, IRenavigator renavigator)
        {
            _authenticator = authenticator;
            _renavigator = renavigator;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            _authenticator.Logout();
            _renavigator.Renavigate();
        }
    }
}
