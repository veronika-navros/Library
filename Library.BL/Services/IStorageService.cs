namespace Library.BL.Services;

public interface IStorageService
{
    Task<string> Upload(string fileName, byte[] fileData);
}