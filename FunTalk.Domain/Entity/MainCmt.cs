using System.Collections.Generic;

namespace FunTalk.Domain.Entity
{
    public class MainCmt : Cmt
    {
        public IEnumerable<SubCmt> SubCmts { get; set; }
    }
}
