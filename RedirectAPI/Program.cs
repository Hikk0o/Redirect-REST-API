using Microsoft.EntityFrameworkCore;
using RedirectAPI.Data;

namespace RedirectAPI;

internal static class Program
{
    private static WebApplicationBuilder? _builder;
    public static WebApplication? App;
    private static void Main(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);
        // Console.WriteLine(_builder.Configuration.GetConnectionString("RedirectDB"));

        _builder.Services.AddControllers();
        _builder.Services.AddEndpointsApiExplorer();
        _builder.Services.AddSwaggerGen();
        _builder.Services.AddDbContext<ApplicationContext>(
            o => o.UseNpgsql(_builder.Configuration.GetConnectionString("RedirectDB")
            ));
        App = _builder.Build();

        if (App.Environment.IsDevelopment())
        {
            App.UseSwagger();
            App.UseSwaggerUI();
        }
        
        // Redirect.IsDev = App.Environment.IsDevelopment();

        // app.UseHttpsRedirection();
        // app.UseAuthorization();
        App.MapControllers();

        App.Run();
    }
}