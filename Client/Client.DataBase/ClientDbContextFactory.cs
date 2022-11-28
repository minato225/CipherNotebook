using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Client.DataBase;

public class ClientDbContextFactory
{
    private readonly Action<DbContextOptionsBuilder> _configureDbContext;

    public ClientDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext) => 
        _configureDbContext = configureDbContext;

    public ClientDataContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ClientDataContext>();

        _configureDbContext(options);

        return new ClientDataContext(options.Options);
    }
}
