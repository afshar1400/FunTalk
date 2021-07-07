using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunTalk.Application.Jokes.ViewModels
{
    public class JokeVm
    {
        
        public long Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public int LikeCount { get; set; }
        public int CmtCount { get; set; }
        public bool IsPrivate { get; set; } 
        public DateTime Created { get; set; } 
    }
}
