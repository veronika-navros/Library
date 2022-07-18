namespace Library.BL.Dtos;

public class BookAuditDto
{
    public Guid Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}