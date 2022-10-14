using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string URI = @"https://localhost:44341/CipherText";

        private string _key = string.Empty;
        public MainWindow() => InitializeComponent();

        private async void GetFileText_ButtonClick(object sender, RoutedEventArgs e)
        {
            using var client = new HttpClient();
            try
            {
                var fileName = $"{FileNameTextBox.Text}.txt";

                var response = await client.GetFromJsonAsync<Message>($@"{URI}/GetFileText?fileName={fileName}");
                if (response is null)
                {
                    EncyptedFileText.Text = "Null";
                    return;
                }

                if (string.IsNullOrEmpty(response.ErrorMessage))
                {
                    DecryptBtn.IsEnabled = true;
                    _key = response.EncryptedSessionKey;
                    EncyptedFileText.Text = response?.EncryptedFileText;
                    return;
                }

                EncyptedFileText.Text = response.ErrorMessage;
            }
            catch (Exception er)
            {
                EncyptedFileText.Text = "Bad Request:" + er.Message;
            }
        }

        private async void GenOPenKey_ButtonClick(object sender, RoutedEventArgs e)
        {
            var openKey = CipherHelper.GenerateRsaOpenKey();

            using var client = new HttpClient();
            try
            {
                await client.PostAsJsonAsync($@"{URI}/GetOpenRsaKey", openKey);
            }
            catch (Exception)
            {
                RsaOpenKeyText.Text = "Bad Request";
            }

            GenRsaBtn.IsEnabled = false;
            EnterFileNamePanel.IsEnabled = true;
            RsaOpenKeyText.Text = openKey;
        }

        private void Decrypt_ButtonClick(object sender, RoutedEventArgs e)
        {
            var privateKey = File.ReadAllText("PrivateKey.txt");
            var text = CipherHelper.DecryptSessionKey(EncyptedFileText.Text, privateKey, _key);

            DecryptedFileText.Text = text;
        }
    }
}
