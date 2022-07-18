using Library.BL.Dtos;

namespace Library.BL.Services;

public interface IBookService
{
    List<BookGetDto> GetAll();
    Task<int> Insert(BookAddDto bookDto);
}