namespace LearningBotApi.Models;

public class ThemeDto
{
    public int BlockNumber { get; set; }
    public string Theme { get; set; } = string.Empty;
    public List<SubThemeDto> Chilren { get; set; } = new(); // Оставляем "Chilren" для фронтенда
    public List<AdditionDto> Additions { get; set; } = new();
}

public class SubThemeDto
{
    public string Name { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}

public class AdditionDto
{
    public string Text { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}