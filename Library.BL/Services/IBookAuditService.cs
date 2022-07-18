using Library.BL.Dtos;

namespace Library.BL.Services;

public interface IBookAuditService
{
    Task<IEnumerable<BookAuditDto>?> GetAll();
}