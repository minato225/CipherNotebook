using CipherNoteBook.Domain.Models;
using System.Threading.Tasks;

namespace CipherNoteBook.Domain.Services.CipherService;

public interface ICypherFileTranspher
{
    Task<string> Decrypt(string encryptTextFile, string sessionKey);
    Task<string> GenOPenKey();
    Task<Message> RequestFile(string textFile);
}
