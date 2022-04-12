using BlogMvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogMvc.Data
{

    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Post>().Ignore(x => x.Comentario);
        }
    }
}