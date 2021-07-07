using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FunTalk.Domain.Entity
{
    public class Joke
    {
        public long Id { get; set; }
        [StringLength(500)]
        public string Text { get; set; }
        public string UserId { get; set; }
        public int LikeCount { get; set; }
        public int CmtCount { get; set; }
        public bool IsPrivate { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.UtcNow;

        public virtual IEnumerable<MainCmt> MainCmts { get; set; }
        public virtual IEnumerable<Like> Likes { get; set; }
    }
}
