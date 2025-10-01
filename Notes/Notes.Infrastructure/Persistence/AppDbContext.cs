using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;

namespace Notes.Infrastructure.Persistence
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) {}
        public DbSet<Note> Notes => Set<Note>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<Note>().ToTable("Notes","dbo");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotesDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
