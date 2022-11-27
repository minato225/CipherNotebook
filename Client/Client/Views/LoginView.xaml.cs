using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Views;

/// <summary>
/// Interaction logic for LoginView.xaml
/// </summary>
public partial class LoginView : UserControl
{
    public static readonly DependencyProperty LoginCommandProperty = DependencyProperty.Register(
        nameof(LoginCommand),
        typeof(ICommand),
        typeof(LoginView),
        new PropertyMetadata(null));

    public ICommand LoginCommand
    {
        get { return (ICommand)GetValue(LoginCommandProperty); }
        set { SetValue(LoginCommandProperty, value); }
    }

    public LoginView() => InitializeComponent();

    private void Login_Click(object sender, System.Windows.RoutedEventArgs e) => 
        LoginCommand?.Execute(pbPassword.Password);
}
