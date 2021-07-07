using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FunTalk.Domain.Entity
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("Joke")]
        public long JokeId { get; set; }
        public Joke Joke { get; set; }
    }
}
