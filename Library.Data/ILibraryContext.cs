using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data;

public interface ILibraryContext
{
    DbSet<BookEntity> Books { get; set; }
    int SaveChanges();
}