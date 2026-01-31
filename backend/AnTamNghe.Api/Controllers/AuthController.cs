using AnTamNghe.Api.Data;
using AnTamNghe.Api.DTOs;
using AnTamNghe.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AnTamNghe.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
                return BadRequest("PhoneNumber is required");

            // 1. Tìm user theo số điện thoại
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);

            // 2. Nếu chưa có → tạo mới
            if (user == null)
            {
                user = new User
                {
                    PhoneNumber = request.PhoneNumber,
                    FullName = request.FullName,
                    IsActive = true
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            // 3. Trả kết quả
            return Ok(new LoginResponse
            {
                UserId = user.UserId,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName
            });
        }
    }
}
