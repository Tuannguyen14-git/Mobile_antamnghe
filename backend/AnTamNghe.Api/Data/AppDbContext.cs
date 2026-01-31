using Microsoft.EntityFrameworkCore;
using AnTamNghe.Api.Models;

namespace AnTamNghe.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        public DbSet<PriorityContact> PriorityContacts { get; set; }
        public DbSet<CallLog> CallLogs { get; set; }
        public DbSet<SpamNumber> SpamNumbers { get; set; }
    }
}
