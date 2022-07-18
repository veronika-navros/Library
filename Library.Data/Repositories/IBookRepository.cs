using Library.Data.Entities;

namespace Library.Data.Repositories;

public interface IBookRepository
{
    List<BookEntity> GetAll();
    int Insert(BookEntity entity);
}