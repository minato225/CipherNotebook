using CipherNoteBook.Server.Helper;
using CipherNoteBook.Server.Model;
using CipherNoteBook.Server.Service;

internal class Program
{
    [Obsolete]
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddHttpContextAccessor();

        builder.Services.ConfigureWritable<RsaKeys>(builder.Configuration.GetSection("RsaKeys"));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddTransient<IChipherService, ChipherService>();
        builder.Services.AddTransient<ITextService, TextService>();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}