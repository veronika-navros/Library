using Library.Api.Extensions;
using Library.Api.Models;
using Library.BL.Dtos;
using Library.BL.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/book")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        Log.Information("Log test");
        return Ok(_bookService.GetAll().Select(x => new BookViewModel
        {
            Title = x.Name,
            Author = x.Author,
            Link = x.Link
        }));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] BookAddModel model)
    {
        await _bookService.Insert(new BookAddDto
        {
            Name = model.Title,
            Author = model.Author,
            FileName = model.Book.FileName,
            Content = model.Book.ToByteArray()
        });
        return Created("", model);
    }
}