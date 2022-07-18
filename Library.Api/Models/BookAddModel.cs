using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Models;

public class BookAddModel
{
    [FromForm(Name="title")]
    public string Title { get; set; }
    
    [FromForm(Name="author")]
    public string Author { get; set; }
    
    [FromForm(Name="book")]
    public IFormFile Book { get; set; }
}