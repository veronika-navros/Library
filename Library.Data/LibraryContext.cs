using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data;

public class LibraryContext : DbContext, ILibraryContext
{
    public LibraryContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BookEntity> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookEntity>().ToTable("Book");
        base.OnModelCreating(modelBuilder);
    }
}