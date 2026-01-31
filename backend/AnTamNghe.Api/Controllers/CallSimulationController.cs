using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnTamNghe.Api.Data;
using AnTamNghe.Api.Models;

namespace AnTamNghe.Api.Controllers
{
    [ApiController]
    [Route("api/call-sim")]
    public class CallSimulationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CallSimulationController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/call-sim
        [HttpPost]
        public async Task<IActionResult> SimulateCall(
            int userId,
            string phoneNumber)
        {
            // 1️⃣ Lấy FilterMode
            var setting = await _context.UserSettings
                .FirstOrDefaultAsync(x => x.UserId == userId);

            var filterMode = setting?.FilterMode ?? "Allowed";

            // 2️⃣ Kiểm tra PriorityContacts
            var isPriority = await _context.PriorityContacts.AnyAsync(x =>
                x.UserId == userId &&
                x.PhoneNumber == phoneNumber &&
                x.IsActive
            );

            // 3️⃣ Quyết định kết quả
            string result;
            string reason;

            if (filterMode == "Allowed")
            {
                result = "Allowed";
                reason = "FilterMode cho phép tất cả";
            }
            else if (isPriority)
            {
                result = "Allowed";
                reason = "Số trong danh bạ ưu tiên";
            }
            else if (filterMode == "Blocked")
            {
                result = "Blocked";
                reason = "Chặn số lạ";
            }
            else // Silent
            {
                result = "Silent";
                reason = "Im lặng số lạ";
            }

            // 4️⃣ Ghi CallLog
            var log = new CallLog
            {
                UserId = userId,
                PhoneNumber = phoneNumber,
                CallResult = result,
                Reason = reason
            };

            _context.CallLogs.Add(log);
            await _context.SaveChangesAsync();

            return Ok(log);
        }

        // GET: api/call-sim/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetLogs(int userId)
        {
            var logs = await _context.CallLogs
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(logs);
        }
    }
}
