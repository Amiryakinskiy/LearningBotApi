using Microsoft.EntityFrameworkCore;
using LearningBotApi.Models;

namespace LearningBotApi.Data;

public class LearningBotContext : DbContext
{
    public DbSet<Theme> Themes { get; set; }
    public DbSet<SubTheme> SubThemes { get; set; }
    public DbSet<Addition> Additions { get; set; }

    public LearningBotContext(DbContextOptions<LearningBotContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Theme>()
            .HasMany(t => t.SubThemes)
            .WithOne(s => s.Theme)
            .HasForeignKey(s => s.ThemeId);

        modelBuilder.Entity<Theme>()
            .HasMany(t => t.Additions)
            .WithOne(a => a.Theme)
            .HasForeignKey(a => a.ThemeId);

        // Начальные данные (на основе JSON)
        modelBuilder.Entity<Theme>().HasData(
            new Theme { Id = 1, BlockNumber = 1, Name = "Тайм-менеджмент" },
            new Theme { Id = 2, BlockNumber = 2, Name = "Навык Лидерство" },
            new Theme { Id = 3, BlockNumber = 3, Name = "Управление задачами" },
            new Theme { Id = 4, BlockNumber = 4, Name = "Управление с командой" }
        );

        modelBuilder.Entity<SubTheme>().HasData(
            new SubTheme { Id = 1, ThemeId = 1, Name = "Тайм менеджмент тим лида", Link = "/Timemanagement", Content = "" },
            new SubTheme { Id = 2, ThemeId = 1, Name = "Матрица Эйзенхауэра", Link = "/Matrix", Content = "" },
            new SubTheme { Id = 3, ThemeId = 1, Name = "МЕТОД Abcde", Link = "/ABCDE", Content = "" },
            new SubTheme { Id = 4, ThemeId = 2, Name = "Кто такой лидер?", Link = "/lider", Content = "" },
            new SubTheme { Id = 5, ThemeId = 2, Name = "Стили лидерства", Link = "/liderStyle", Content = "" },
            new SubTheme { Id = 6, ThemeId = 3, Name = "Постановка задач модели", Link = "/settingTask", Content = "" },
            new SubTheme { Id = 7, ThemeId = 3, Name = "Делегирование", Link = "/delegate", Content = "" },
            new SubTheme { Id = 8, ThemeId = 4, Name = "Культура совместной работы", Link = "/workCulture", Content = "" },
            new SubTheme { Id = 9, ThemeId = 4, Name = "Роли в команде", Link = "/teamRole", Content = "" }
        );

        modelBuilder.Entity<Addition>().HasData(
            new Addition { Id = 1, ThemeId = 1, Text = "Дополнительный материал", Image = "literature" },
            new Addition { Id = 2, ThemeId = 1, Text = "Итоговый тест", Image = "test_icon" },
            new Addition { Id = 3, ThemeId = 1, Text = "Аудио-лекция", Image = "megaphone_icon" },
            new Addition { Id = 4, ThemeId = 2, Text = "Дополнительный материал", Image = "literature" },
            new Addition { Id = 5, ThemeId = 2, Text = "Итоговый тест", Image = "test_icon" },
            new Addition { Id = 6, ThemeId = 3, Text = "Дополнительный материал", Image = "literature" },
            new Addition { Id = 7, ThemeId = 3, Text = "Итоговый тест", Image = "test_icon" },
            new Addition { Id = 8, ThemeId = 3, Text = "Аудио-лекция", Image = "megaphone_icon" },
            new Addition { Id = 9, ThemeId = 4, Text = "Дополнительный материал", Image = "literature" },
            new Addition { Id = 10, ThemeId = 4, Text = "Аудио-лекция", Image = "megaphone_icon" }
        );
    }
}