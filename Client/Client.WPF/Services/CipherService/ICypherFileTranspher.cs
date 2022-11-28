using Client.Domain.Models;
using System.Threading.Tasks;

namespace Client.WPF.Services.CipherService;

public interface ICypherFileTranspher
{
    Task<string> Decrypt(string encryptTextFile, string sessionKey);
    Task<string> GenOPenKey();
    Task<Message> RequestFile(string textFile);
}
