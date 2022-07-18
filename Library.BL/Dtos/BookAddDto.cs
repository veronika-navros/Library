namespace Library.BL.Dtos;

public class BookAddDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string FileName { get; set; }
    public byte[] Content { get; set; }
}