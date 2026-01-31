namespace AnTamNghe.Api.Models
{
    public class CallLog
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string PhoneNumber { get; set; }

        // Allowed | Blocked | Silent
        public string CallResult { get; set; }

        public string? Reason { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now; // ✅ THÊM DÒNG NÀY

        // Navigation
        public User User { get; set; }
    }
}
