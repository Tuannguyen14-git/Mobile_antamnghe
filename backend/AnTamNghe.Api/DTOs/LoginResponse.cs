namespace AnTamNghe.Api.DTOs
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? FullName { get; set; }
    }
}
