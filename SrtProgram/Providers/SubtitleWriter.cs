using SrtProgram.Interfaces;
using SrtProgram.Models;

namespace SrtProgram.Providers;

public class SubtitleWriter : ISubtitleWriter
{
    private readonly IFileWriter _fileWriter;

    public SubtitleWriter(IFileWriter fileWriter)
        => _fileWriter = fileWriter;

    public async Task WriteAsync(string path, List<Subtitle> subtitles)
    {
        var lines = new List<string>();
        foreach (var subtitle in subtitles)
        {
            lines.Add(subtitle.Number.ToString());
            lines.Add($"{subtitle.StartTime:hh\\:mm\\:ss\\,fff} --> {subtitle.EndTime:hh\\:mm\\:ss\\,fff}");
            lines.AddRange(subtitle.Lines);
            lines.Add(string.Empty);
        }

        await _fileWriter.WriteAllLinesAsync(path, lines);
    }
}