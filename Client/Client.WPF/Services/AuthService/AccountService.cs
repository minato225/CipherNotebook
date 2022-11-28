﻿using Client.DataBase;
using Client.Domain.Models;
using Client.WPF.Services.AuthService.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.WPF.Services.AuthService;

public class AccountDataService : IAccountService
{
    private readonly ClientDbContextFactory _contextFactory;
    private readonly NonQueryDataService<Account> _nonQueryDataService;

    public AccountDataService(ClientDbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
        _nonQueryDataService = new NonQueryDataService<Account>(contextFactory);
    }

    public async Task<Account> Create(Account entity) =>
        await _nonQueryDataService.Create(entity);

    public async Task<bool> Delete(int id) =>
        await _nonQueryDataService.Delete(id);

    public async Task<Account> Get(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.Accounts
            .Include(a => a.AccountHolder)
            .FirstOrDefaultAsync(e => e.Id == id);
        return entity;
    }

    public async Task<IEnumerable<Account>> GetAll()
    {
        using var context = _contextFactory.CreateDbContext();
        var entities = await context.Accounts
            .Include(a => a.AccountHolder)
            .ToListAsync();
        return entities;
    }

    public async Task<Account> GetByEmail(string email)
    {
        using var context = _contextFactory.CreateDbContext();
        var accounts = await context.Accounts
            .Include(a => a.AccountHolder)
            .FirstOrDefaultAsync(a => a.AccountHolder.Email == email);

        return accounts;
    }

    public async Task<Account> GetByUsername(string username)
    {
        using var context = _contextFactory.CreateDbContext();
        var account = await context.Accounts
            .Include(a => a.AccountHolder)
            .FirstOrDefaultAsync(a => a.AccountHolder.Username == username);

        return account;
    }

    public async Task<Account> Update(int id, Account entity) =>
        await _nonQueryDataService.Update(id, entity);
}