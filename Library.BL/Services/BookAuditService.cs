using Library.BL.Dtos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Library.BL.Services;

public class BookAuditService : IBookAuditService
{
    private readonly string _bookAuditUrl;
    private readonly string _functionKey;
    private readonly ILogger _logger;

    public BookAuditService(string bookAuditUrl, string functionKey)
    {
        _bookAuditUrl = bookAuditUrl;
        _functionKey = functionKey;
    }

    public async Task<IEnumerable<BookAuditDto>?> GetAll()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, _bookAuditUrl);
        request.Headers.Add("x-functions-key", _functionKey);

        var response = await client.SendAsync(request);
        var responseString = response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IEnumerable<BookAuditDto>>(responseString.Result);
    }
}