using Library.Api.Models;
using Library.BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;


[ApiController]
[Route("api/audit")]
public class AuditController : ControllerBase
{
    private readonly IBookAuditService _bookAuditService;

    public AuditController(IBookAuditService bookAuditService)
    {
        _bookAuditService = bookAuditService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var bookAudit = await _bookAuditService.GetAll();
        return Ok(bookAudit?.Select(x => new BookAuditModel
        {
            Id = x.Id,
            TimeStamp = x.TimeStamp,
            Name = x.Name,
            Url = x.Url
        }));
    }
}