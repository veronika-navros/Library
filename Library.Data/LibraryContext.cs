using Library.Data.Entities;
using Library.Data.Mappings;
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
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}