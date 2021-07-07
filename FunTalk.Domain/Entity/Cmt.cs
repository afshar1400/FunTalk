using System;
using System.ComponentModel.DataAnnotations;

namespace FunTalk.Domain.Entity
{
    public class Cmt
    {
        public int Id { get; set; }
        public long JokeId { get; set; }
        public Joke Joke { get; set; }
        [StringLength(255)]
        public string Comment { get; set; }
        [StringLength(255)]
        public string UserId { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
