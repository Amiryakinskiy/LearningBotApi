using LearningBotApi.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace LearningBotApi.Services;

public class MdFileImporter
{
    private readonly LearningBotContext _context;
    private readonly ILogger<MdFileImporter> _logger;

    public MdFileImporter(LearningBotContext context, ILogger<MdFileImporter> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task ImportMdFilesAsync()
    {
        try
        {
            // Проверяем, пустое ли поле Content у подтем
            var subThemes = await _context.SubThemes.ToListAsync();
            if (subThemes.All(st => !string.IsNullOrEmpty(st.Content)))
            {
                _logger.LogInformation("Все подтемы уже содержат содержимое. Импорт не требуется.");
                return;
            }

            // Путь к папке md_files
            var mdFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "md_files");
            if (!Directory.Exists(mdFilesPath))
            {
                _logger.LogWarning("Папка md_files не найдена! Создайте папку и поместите .md-файлы.");
                return;
            }

            foreach (var subTheme in subThemes)
            {
                var fileName = subTheme.Link.TrimStart('/') + ".md";
                var mdFilePath = Path.Combine(mdFilesPath, fileName);

                if (File.Exists(mdFilePath))
                {
                    var content = await File.ReadAllTextAsync(mdFilePath);
                    subTheme.Content = content;
                    _logger.LogInformation($"Импортирован файл {fileName} для подтемы {subTheme.Name}");
                }
                else
                {
                    _logger.LogWarning($"Файл {fileName} не найден!");
                }
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Импорт .md-файлов завершен!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при импорте .md-файлов.");
        }
    }
}