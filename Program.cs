using RedirectAPI;

internal static class Program
{
    private static WebApplicationBuilder? _builder;
    public static WebApplication? App;
    private static void Main(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);

        _builder.Services.AddControllers();
        _builder.Services.AddEndpointsApiExplorer();
        _builder.Services.AddSwaggerGen();

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