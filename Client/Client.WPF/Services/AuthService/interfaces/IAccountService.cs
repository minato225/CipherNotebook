using Client.Domain.Models;
using System.Threading.Tasks;

namespace Client.WPF.Services.AuthService.interfaces;

public interface IAccountService : IDataService<Account>
{
    Task<Account> GetByUsername(string username);
    Task<Account> GetByEmail(string email);
}
