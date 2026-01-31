namespace AnTamNghe.Api.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; } = true; // ✅ THÊM DÒNG NÀY

        // Navigation
        public UserSetting UserSetting { get; set; }
        public ICollection<PriorityContact> PriorityContacts { get; set; }
        public ICollection<CallLog> CallLogs { get; set; }
    }
}
