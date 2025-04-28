using LearningBotApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningBotApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubThemesController : ControllerBase
{
    private readonly LearningBotContext _context;
    private readonly ILogger<SubThemesController> _logger;

    public SubThemesController(LearningBotContext context, ILogger<SubThemesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("{link}/content")]
    public async Task<ActionResult<string>> GetSubThemeContent(string link)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                _logger.LogWarning("Параметр link пустой или null.");
                return BadRequest("Параметр link не может быть пустым.");
            }

            var normalizedLink = link.StartsWith("/") ? link : "/" + link;
            _logger.LogInformation($"Поиск подтемы с Link: {normalizedLink}");

            var subTheme = await _context.SubThemes
                .FirstOrDefaultAsync(s => s.Link == normalizedLink);

            if (subTheme == null)
            {
                _logger.LogWarning($"Подтема с Link {normalizedLink} не найдена.");
                return NotFound();
            }

            _logger.LogInformation($"Найдена подтема: {subTheme.Name}, Content length: {subTheme.Content?.Length ?? 0}");
            return Ok(subTheme.Content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при получении содержимого подтемы с Link: {link}");
            return StatusCode(500, "Произошла ошибка на сервере.");
        }
    }
}