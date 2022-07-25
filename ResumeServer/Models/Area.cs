using System.ComponentModel.DataAnnotations;

namespace ResumeServer.Models
{
    public class Area : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string Name{ get; set; }
        public int UserId { get; set; }
    }
}
