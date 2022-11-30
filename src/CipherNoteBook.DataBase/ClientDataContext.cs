using CipherNoteBook.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CipherNoteBook.DataBase;

public sealed class ClientDataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }

    public ClientDataContext(DbContextOptions<ClientDataContext> options)
        : base(options) =>
        Database.EnsureCreated();
}
