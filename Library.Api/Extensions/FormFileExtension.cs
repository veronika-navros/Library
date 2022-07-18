namespace Library.Api.Extensions;

public static class FormFileExtension
{
    public static byte[] ToByteArray(this IFormFile formFile)
    {
        if (formFile.Length > 0)
        {
            using var ms = new MemoryStream();
            formFile.CopyTo(ms);
            return ms.ToArray();
        }
        return Array.Empty<byte>();
    }
}