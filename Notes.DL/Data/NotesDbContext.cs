using Microsoft.EntityFrameworkCore;
using Notes.DL.Data.Entities;
using System.Reflection;

namespace Notes.DL.Data
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext() { }
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) { }

        public virtual DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
