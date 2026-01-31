using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnTamNghe.Api.Data;
using AnTamNghe.Api.Models;

namespace AnTamNghe.Api.Controllers
{
    [ApiController]
    [Route("api/settings")]
    public class SettingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SettingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/settings/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetSetting(int userId)
        {
            var setting = await _context.UserSettings
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (setting == null)
            {
                // auto create nếu chưa có
                setting = new UserSetting
                {
                    UserId = userId,
                    FilterMode = "Allowed"
                };
                _context.UserSettings.Add(setting);
                await _context.SaveChangesAsync();
            }

            return Ok(setting);
        }

        // PUT: api/settings/{userId}
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateFilterMode(
            int userId,
            [FromBody] string filterMode)
        {
            if (!new[] { "Allowed", "Blocked", "Silent" }.Contains(filterMode))
                return BadRequest("Invalid FilterMode");

            var setting = await _context.UserSettings
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (setting == null)
                return NotFound();

            setting.FilterMode = filterMode;
            await _context.SaveChangesAsync();

            return Ok(setting);
        }
    }
}
