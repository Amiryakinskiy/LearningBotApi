namespace LearningBotApi.Models;

public class Addition
{
    public int Id { get; set; }
    public int ThemeId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public Theme Theme { get; set; } = null!;
}