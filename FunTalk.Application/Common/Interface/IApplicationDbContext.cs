using FunTalk.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunTalk.Application.Common.Interface
{
    public interface IApplicationDbContext
    {
         DbSet<Joke> Jokes { get; set; }
         DbSet<Follow> Follows { get; set; }
         DbSet<Like> Likes  { get; set; }
         DbSet<MainCmt> MainCmts { get; set; }
         DbSet<SubCmt> SubCmts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
