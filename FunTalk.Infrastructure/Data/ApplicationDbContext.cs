using FunTalk.Application.Common.Interface;
using FunTalk.Domain.Entity;
using FunTalk.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunTalk.Infrastructure.Data
{
    public class ApplicationDbContext:IdentityDbContext<AppUser>,IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Joke> Jokes { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<MainCmt> MainCmts { get; set; }
        public DbSet<SubCmt> SubCmts { get; set; }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Like>().HasOne<Joke>(x => x.Joke).WithMany(y => y.Likes).HasForeignKey(x => x.JokeId);
            base.OnModelCreating(builder);
        }
    }
}
