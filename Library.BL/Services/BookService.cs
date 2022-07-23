using Library.BL.Dtos;
using Library.BL.Messages;
using Library.Data.Entities;
using Library.Data.Repositories;

namespace Library.BL.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IStorageService _storageService;
    private readonly IServiceBusService _serviceBusService;

    private const string QueueName = "book-audit";

    public BookService(IBookRepository bookRepository, IStorageService storageService, IServiceBusService serviceBusService)
    {
        _bookRepository = bookRepository;
        _storageService = storageService;
        _serviceBusService = serviceBusService;
    }

    public List<BookGetDto> GetAll()
    {
        return _bookRepository.GetAll().Select(x => new BookGetDto
        {
            Name = x.Name,
            Author = x.Author,
            Link = x.Link
        }).ToList();
    }

    public async Task<int> Insert(BookAddDto bookDto)
    {
        var link = await _storageService.Upload(bookDto.FileName, bookDto.Content);
        var id = _bookRepository.Insert(new BookEntity
        {
            Name = bookDto.Name,
            Author = bookDto.Author,
            Link = link
        });

        var message = new BookAddedMessage
        {
            TimeStamp = DateTime.UtcNow,
            Name = bookDto.Name,
            Url = link
        };
        //await _serviceBusService.PutIntoQueue(QueueName, message);

        return id;
    }
}