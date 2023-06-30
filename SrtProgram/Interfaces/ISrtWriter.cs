using SrtProgram.Models;

namespace SrtProgram.Interfaces;

public interface ISrtWriter
{
    Task WriteAsync(string path, List<Subtitle> subtitles);
}