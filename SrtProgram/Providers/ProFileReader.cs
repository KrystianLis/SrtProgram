using SrtProgram.Interfaces;

namespace SrtProgram.Providers;

public class ProFileReader : IFileReader
{
    public async Task<string[]> ReadAllLinesAsync(string path)
        => await File.ReadAllLinesAsync(path);
}