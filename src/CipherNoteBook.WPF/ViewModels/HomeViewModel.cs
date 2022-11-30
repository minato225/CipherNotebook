using System.Windows.Input;
using CipherNoteBook.Domain.Services.CipherService;
using CipherNoteBook.WPF.State.Authenticators;
using CipherNoteBook.WPF.State.Navigators;
using CipherNoteBook.WPF.Commands;

namespace CipherNoteBook.WPF.ViewModels;

public class HomeViewModel : BaseViewModel
{
    public ICommand GenerateOpenKeyCommand { get; }
    public ICommand RequestTextFileCommand { get; }
    public ICommand DecryptRequestCommand { get; }
    public ICommand LogOutCommand { get; }

    public readonly ICypherFileTranspher _cypherFileTranspher;
    private readonly IAuthenticator _authenticator;
    public string SessionKey;

    public HomeViewModel(ICypherFileTranspher cypherFileTranspher, IAuthenticator authenticator, IRenavigator renavigator)
    {
        _cypherFileTranspher = cypherFileTranspher;
        _authenticator = authenticator;
        GenerateOpenKeyCommand = new GenerateOpenKeyCommand(this, cypherFileTranspher);
        RequestTextFileCommand = new RequestTextFileCommand(this, cypherFileTranspher);
        DecryptRequestCommand = new DecryptRequestCommand(this, cypherFileTranspher);
        LogOutCommand = new LogOutCommand(authenticator, renavigator);
    }

    #region Props

    public string UserName => _authenticator.CurrentAccount.AccountHolder.Username;

    private string _openKey;
    public string OpenKey
    {
        get => _openKey;
        set
        {
            _openKey = value;
            OnPropertyChanged();
        }
    }

    private string _fileName;
    public string FileName
    {
        get => _fileName;
        set
        {
            _fileName = value;
            OnPropertyChanged();
        }
    }

    private string _decryptedFileText = "Empty...";
    public string DecryptedFileText
    {
        get => _decryptedFileText;
        set
        {
            _decryptedFileText = value;
            OnPropertyChanged();
        }
    }

    private string _encryptedFileText = "Empty...";
    public string EncryptedFileText
    {
        get => _encryptedFileText;
        set
        {
            _encryptedFileText = value;
            OnPropertyChanged();
        }
    }

    private bool _genRsaBtnIsEnabled = true;
    public bool GenRsaBtnIsEnabled
    {
        get => _genRsaBtnIsEnabled;
        set
        {
            _genRsaBtnIsEnabled = value;
            OnPropertyChanged();
        }
    }

    private bool _enterFileNamePanelIsEnabled;
    public bool EnterFileNamePanelIsEnabled
    {
        get => _enterFileNamePanelIsEnabled;
        set
        {
            _enterFileNamePanelIsEnabled = value;
            OnPropertyChanged();
        }
    }

    private bool _decryptBtnIsEnabled;
    public bool DecryptBtnIsEnabled
    {
        get => _decryptBtnIsEnabled;
        set
        {
            _decryptBtnIsEnabled = value;
            OnPropertyChanged();
        }
    }

    #endregion
}