using CipherNoteBook.DataBase;
using CipherNoteBook.Domain.Services.AuthService.interfaces;
using CipherNoteBook.Server.AuthUtils;
using CipherNoteBook.Server.Helpers;
using CipherNoteBook.Server.Models.SecurityModels;
using CipherNoteBook.Server.Services.AuthServices;
using CipherNoteBook.Server.Services.SecurityServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CipherNoteBook.DataBase.Services;
using CipherNoteBook.Server.Models.AuthModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using CipherNoteBook.Domain.Services.OtpService;

internal class Program
{
    [Obsolete]
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddHttpContextAccessor();

        // DB
        var connectionString = builder.Configuration.GetConnectionString("sqlite");
        void configureDbContext(DbContextOptionsBuilder o) => o.UseSqlite(connectionString);

        builder.Services.AddDbContext<ClientDataContext>(configureDbContext);
        builder.Services.AddSingleton(new ClientDbContextFactory(configureDbContext));

        //ConfigureWritable
        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
        builder.Services.ConfigureWritable<RsaKeys>(builder.Configuration.GetSection("RsaKeys"));

        //Controllers
        builder.Services.AddCors();
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.IgnoreNullValues = true;
        });
        builder.Services.AddEndpointsApiExplorer();

        //Services DI
        builder.Services.AddTransient<IChipherService, ChipherService>();
        builder.Services.AddTransient<ITextService, TextService>();
        builder.Services.AddScoped<IJwtUtils, JwtUtils>();
        builder.Services.AddScoped<IAccountService, AccountDataService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IOtpService, OtpService>();

        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                        "Enter 'Bearer' [space] and then your token in the text input below.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(x => x.SetIsOriginAllowed(_ => true)
                .AllowAnyMethod().AllowAnyHeader()
                .AllowCredentials());

        app.UseAuthorization();
        app.UseMiddleware<JwtMiddleware>();

        app.MapControllers();

        app.Run();
    }
}