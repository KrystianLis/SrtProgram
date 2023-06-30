namespace SrtProgram.Interfaces;

public interface IFileWriter
{
    Task WriteAllLinesAsync(string path, IEnumerable<string> contents);
}