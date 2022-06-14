using Microsoft.EntityFrameworkCore;
using RedirectAPI.Data;

namespace RedirectAPI;

internal static class Program
{
    private static WebApplicationBuilder _builder = null!;
    private static WebApplication _app = null!;
    private static void Main(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);
        _builder.Services.AddControllers();
        _builder.Services.AddEndpointsApiExplorer();
        _builder.Services.AddSwaggerGen();
        _builder.Services.AddDbContext<ApplicationContext>(
            o => o.UseNpgsql(_builder.Configuration.GetConnectionString("RedirectDB")
            ));
        _app = _builder.Build();

        if (_app.Environment.IsDevelopment())
        {
            _app.UseSwagger();
            _app.UseSwaggerUI();
        }

        _app.UseHttpsRedirection();
        _app.UseAuthorization();
        _app.MapControllers();

        _app.Run();
    }
}