using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace CipherNoteBook.DataBase;

public class ClientDbContextFactory : IDesignTimeDbContextFactory<ClientDataContext>
{
    private readonly Action<DbContextOptionsBuilder> _configureDbContext;

    public ClientDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext) =>
        _configureDbContext = configureDbContext;

    public ClientDbContextFactory()
    {

    }

    public ClientDataContext CreateDbContext(string[] args = default)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ClientDataContext>();
        //_configureDbContext(optionsBuilder);

        optionsBuilder.UseSqlite("Data Source=CypherNoteBook.db");

        return new ClientDataContext(optionsBuilder.Options);
    }
}
