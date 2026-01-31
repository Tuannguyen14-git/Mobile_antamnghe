namespace AnTamNghe.Api.Models
{
    public class UserSetting
    {
        public int UserSettingId { get; set; }

        public int UserId { get; set; }
        public string FilterMode { get; set; }
        // Focus / Normal / AllowAll

        public bool AllowRepeatedCalls { get; set; }
        public bool NotifyAfterBlocked { get; set; }

        // Navigation
        public User User { get; set; }
    }
}
