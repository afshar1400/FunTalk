using System.ComponentModel.DataAnnotations;

namespace FunTalk.Domain.Entity
{

    public class Follow
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string UserId { get; set; }
        [StringLength(255)]
        public string peopleId { get; set; }
    }
}
