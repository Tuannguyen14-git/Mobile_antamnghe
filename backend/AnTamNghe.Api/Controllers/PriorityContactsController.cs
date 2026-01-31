using AnTamNghe.Api.Data;
using AnTamNghe.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnTamNghe.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriorityContactsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PriorityContactsController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET: api/prioritycontacts?userId=1
        // Lấy danh sách danh bạ ưu tiên của 1 user
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int userId)
        {
            var contacts = await _context.PriorityContacts
                .Where(p => p.UserId == userId && p.IsActive)
                .OrderBy(p => p.PriorityLevel)
                .ToListAsync();

            return Ok(contacts);
        }

        // =========================
        // GET: api/prioritycontacts/5
        // Lấy chi tiết 1 số ưu tiên
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _context.PriorityContacts.FindAsync(id);

            if (contact == null)
                return NotFound("Không tìm thấy liên hệ ưu tiên");

            return Ok(contact);
        }

        // =========================
        // POST: api/prioritycontacts
        // Thêm số ưu tiên mới
        // =========================
        [HttpPost]
        public async Task<IActionResult> Create(PriorityContact model)
        {
            // Check trùng số cho cùng user
            bool exists = await _context.PriorityContacts.AnyAsync(p =>
                p.UserId == model.UserId &&
                p.PhoneNumber == model.PhoneNumber &&
                p.IsActive);

            if (exists)
                return BadRequest("Số điện thoại này đã nằm trong danh sách ưu tiên");

            _context.PriorityContacts.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // =========================
        // PUT: api/prioritycontacts/5
        // Cập nhật thông tin số ưu tiên
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PriorityContact model)
        {
            if (id != model.Id)
                return BadRequest("Id không khớp");

            var existing = await _context.PriorityContacts.FindAsync(id);
            if (existing == null)
                return NotFound("Không tìm thấy liên hệ ưu tiên");

            existing.Name = model.Name;
            existing.PhoneNumber = model.PhoneNumber;
            existing.PriorityLevel = model.PriorityLevel;
            existing.IsActive = model.IsActive;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // =========================
        // DELETE: api/prioritycontacts/5
        // Xóa mềm (không xóa DB)
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.PriorityContacts.FindAsync(id);

            if (contact == null)
                return NotFound("Không tìm thấy liên hệ ưu tiên");

            contact.IsActive = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
