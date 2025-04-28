namespace LearningBotApi.Models;

public class SubTheme
{
    public int Id { get; set; }
    public int ThemeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty; // Для хранения текста .md
    public Theme Theme { get; set; } = null!;
}