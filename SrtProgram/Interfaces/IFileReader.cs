namespace SrtProgram.Interfaces;

public interface IFileReader
{
    Task<string[]> ReadAllLinesAsync(string path);
}