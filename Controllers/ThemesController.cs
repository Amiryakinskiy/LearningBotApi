using LearningBotApi.Data;
using LearningBotApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningBotApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ThemesController : ControllerBase
{
    private readonly LearningBotContext _context;

    public ThemesController(LearningBotContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ThemeDto>>> GetThemes()
    {
        var themes = await _context.Themes
            .Include(t => t.SubThemes)
            .Include(t => t.Additions)
            .Select(t => new ThemeDto
            {
                BlockNumber = t.BlockNumber,
                Theme = t.Name,
                Chilren = t.SubThemes.Select(s => new SubThemeDto
                {
                    Name = s.Name,
                    Link = s.Link
                }).ToList(),
                Additions = t.Additions.Select(a => new AdditionDto
                {
                    Text = a.Text,
                    Image = a.Image
                }).ToList()
            })
            .ToListAsync();

        return Ok(themes);
    }
}