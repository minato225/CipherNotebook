using CipherNoteBook.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CipherNoteBook.DataBase.Services;

public class NonQueryDataService<T> where T : DomainObject
{
    private readonly ClientDbContextFactory _contextFactory;

    public NonQueryDataService(ClientDbContextFactory contextFactory) =>
        _contextFactory = contextFactory;

    public async Task<T> Create(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        var createdResult = await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();

        return createdResult.Entity;
    }

    public async Task<T> Update(int id, T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        entity.Id = id;

        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> Delete(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }
}
