﻿using CipherNoteBook.Domain.Models;

namespace CipherNoteBook.Domain.Services.AuthService.interfaces;

public interface IAccountService : IDataService<Account>
{
    Task<Account> GetByUsername(string username);
    Task<Account> GetByEmail(string email);
}
