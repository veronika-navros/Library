using Library.Data.Entities;

namespace Library.Data.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ILibraryContext _context;

    public BookRepository(ILibraryContext context)
    {
        _context = context;
    }

    public List<BookEntity> GetAll()
    {
        return _context.Books.ToList();
    }

    public int Insert(BookEntity entity)
    {
        _context.Books.Add(entity);
        _context.SaveChanges();
        return entity.Id;
    }
}