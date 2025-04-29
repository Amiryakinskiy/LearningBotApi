using LearningBotApi.Data;
using LearningBotApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigins", // Changed from single to double quotes
        policy =>
        {
            policy
            .AllowAnyOrigin() // Allow any origin
            .AllowAnyMethod() // Allow any HTTP method
            .AllowAnyHeader(); // Allow any header
        });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<LearningBotContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MdFileImporter
builder.Services.AddScoped<MdFileImporter>();
builder.Services.AddHostedService<MdFileImporterHostedService>();

var app = builder.Build();

// Временная проверка содержимого базы данных
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LearningBotContext>();
    var canConnect = await context.Database.CanConnectAsync();
    if (!canConnect)
    {
        Console.WriteLine("Ошибка: Не удалось подключиться к базе данных. Проверьте файл learningbot.db.");
    }
    else
    {
        var subThemes = await context.SubThemes.ToListAsync();
        Console.WriteLine($"Найдено {subThemes.Count} подтем в базе данных:");
        foreach (var subTheme in subThemes)
        {
            Console.WriteLine($"Подтема: {subTheme.Name}, Link: {subTheme.Link}, Content length: {subTheme.Content?.Length ?? 0}");
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAnyOrigins");

app.Run();

public class MdFileImporterHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public MdFileImporterHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var importer = scope.ServiceProvider.GetRequiredService<MdFileImporter>();
        await importer.ImportMdFilesAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}