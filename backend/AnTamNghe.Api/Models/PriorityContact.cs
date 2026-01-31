using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnTamNghe.Api.Models
{
    public class PriorityContact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Name { get; set; }

        // Mức độ ưu tiên: 1 = cao nhất
        public int PriorityLevel { get; set; } = 1;

        public bool IsActive { get; set; } = true;
    }
}
