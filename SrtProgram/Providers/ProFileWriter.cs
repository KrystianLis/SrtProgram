using SrtProgram.Interfaces;

namespace SrtProgram.Providers;

public class ProFileWriter : IFileWriter
{
    public async Task WriteAllLinesAsync(string path, IEnumerable<string> contents)
    {
        await File.WriteAllLinesAsync(path, contents);
    }
}