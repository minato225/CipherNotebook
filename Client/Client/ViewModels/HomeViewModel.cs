using System.Windows.Input;
using Client.WPF.Commands;
using Client.WPF.Services.CipherService;
using Client.WPF.State.Authenticators;
using Client.WPF.State.Navigators;

namespace Client.WPF.ViewModels;

public class HomeViewModel : BaseViewModel
{
    public ICommand GenerateOpenKeyCommand { get; }
    public ICommand RequestTextFileCommand { get; }
    public ICommand DecryptRequestCommand { get; }
    public ICommand LogOutCommand { get; }

    public readonly ICypherFileTranspher _cypherFileTranspher;
    public string SessionKey;

    public HomeViewModel(ICypherFileTranspher cypherFileTranspher, IAuthenticator authenticator, IRenavigator renavigator)
    {
        _cypherFileTranspher = cypherFileTranspher;

        GenerateOpenKeyCommand = new GenerateOpenKeyCommand(this, cypherFileTranspher);
        RequestTextFileCommand = new RequestTextFileCommand(this, cypherFileTranspher);
        DecryptRequestCommand = new DecryptRequestCommand(this, cypherFileTranspher);
        LogOutCommand = new LogOutCommand(authenticator, renavigator);
    }

    #region Props

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

    private bool _genRsaBtnIsEnabled;
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