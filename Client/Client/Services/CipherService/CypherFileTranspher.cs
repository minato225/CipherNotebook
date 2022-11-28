using Client.DataBase.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.WPF.Services.CipherService;

public class CypherFileTranspher : ICypherFileTranspher
{
    private readonly HttpClient _httpClient;

    public CypherFileTranspher(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<string> Decrypt(string encryptTextFile, string sessionKey)
    {
        var privateKey = await File.ReadAllTextAsync("PrivateKey.txt");
        return CipherHelper.DecryptSessionKey(encryptTextFile, privateKey, sessionKey);
    }

    public async Task<string> GenOPenKey()
    {
        var openKey = CipherHelper.GenerateRsaOpenKey();

        try
        {
            await _httpClient.PostAsJsonAsync($@"/GetOpenRsaKey", openKey);
        }
        catch (Exception)
        {
            return await Task.FromResult("Bad Request");
        }

        return await Task.FromResult(openKey);
    }

    public async Task<Message> RequestFile(string textFile)
    {
        var response = new Message();
        try
        {
            var fileName = $"{textFile}.txt";
            var res = await _httpClient.GetFromJsonAsync<Message>($@"/GetFileText?fileName={fileName}");
            response = res;
        }
        catch (Exception er)
        {
            response.ErrorMessage = $"Bad Request:{er.Message}";
            return response;
        }

        return response;
    }
}
