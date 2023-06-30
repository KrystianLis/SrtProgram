using SrtProgram.Models;

namespace SrtProgram.Interfaces;

public interface ISubtitleWriter
{
    Task WriteAsync(string path, List<Subtitle> subtitles);
}