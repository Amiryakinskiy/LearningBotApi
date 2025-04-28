using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearningBotApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BlockNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Additions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ThemeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Additions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Additions_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubThemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ThemeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Link = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubThemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubThemes_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "Id", "BlockNumber", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Тайм-менеджмент" },
                    { 2, 2, "Навык Лидерство" },
                    { 3, 3, "Управление задачами" },
                    { 4, 4, "Управление с командой" }
                });

            migrationBuilder.InsertData(
                table: "Additions",
                columns: new[] { "Id", "Image", "Text", "ThemeId" },
                values: new object[,]
                {
                    { 1, "literature", "Дополнительный материал", 1 },
                    { 2, "test_icon", "Итоговый тест", 1 },
                    { 3, "megaphone_icon", "Аудио-лекция", 1 },
                    { 4, "literature", "Дополнительный материал", 2 },
                    { 5, "test_icon", "Итоговый тест", 2 },
                    { 6, "literature", "Дополнительный материал", 3 },
                    { 7, "test_icon", "Итоговый тест", 3 },
                    { 8, "megaphone_icon", "Аудио-лекция", 3 },
                    { 9, "literature", "Дополнительный материал", 4 },
                    { 10, "megaphone_icon", "Аудио-лекция", 4 }
                });

            migrationBuilder.InsertData(
                table: "SubThemes",
                columns: new[] { "Id", "Content", "Link", "Name", "ThemeId" },
                values: new object[,]
                {
                    { 1, "", "/Timemanagement", "Тайм менеджмент тим лида", 1 },
                    { 2, "", "/Matrix", "Матрица Эйзенхауэра", 1 },
                    { 3, "", "/ABCDE", "МЕТОД Abcde", 1 },
                    { 4, "", "/lider", "Кто такой лидер?", 2 },
                    { 5, "", "/liderStyle", "Стили лидерства", 2 },
                    { 6, "", "/settingTask", "Постановка задач модели", 3 },
                    { 7, "", "/delegate", "Делегирование", 3 },
                    { 8, "", "/workCulture", "Культура совместной работы", 4 },
                    { 9, "", "/teamRole", "Роли в команде", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Additions_ThemeId",
                table: "Additions",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubThemes_ThemeId",
                table: "SubThemes",
                column: "ThemeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Additions");

            migrationBuilder.DropTable(
                name: "SubThemes");

            migrationBuilder.DropTable(
                name: "Themes");
        }
    }
}
