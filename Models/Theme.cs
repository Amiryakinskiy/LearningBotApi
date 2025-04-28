namespace LearningBotApi.Models;

public class Theme
{
    public int Id { get; set; }
    public int BlockNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<SubTheme> SubThemes { get; set; } = new();
    public List<Addition> Additions { get; set; } = new();
}