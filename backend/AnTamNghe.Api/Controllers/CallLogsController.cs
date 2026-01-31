using Microsoft.AspNetCore.Mvc;
using AnTamNghe.Api.Data;
using AnTamNghe.Api.Models;

namespace AnTamNghe.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CallLogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CallLogsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/CallLogs/user/1
        [HttpGet("user/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            var logs = _context.CallLogs
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Id) // ❗ tạm dùng Id
                .ToList();

            return Ok(logs);
        }

        // POST api/CallLogs
        [HttpPost]
        public IActionResult Create(CallLog log)
        {
            _context.CallLogs.Add(log);
            _context.SaveChanges();
            return Ok(log);
        }
    }
}
