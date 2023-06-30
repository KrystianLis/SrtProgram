using SrtProgram.Interfaces;
using SrtProgram.Models;

namespace SrtProgram.Providers;

public class SubtitleProcessor : ISubtitleProcessor
{
    private readonly TimeSpan _offset;

    public SubtitleProcessor(TimeSpan offset)
        => _offset = offset;

    public void Process(List<Subtitle> subtitles)
    {
        foreach (var subtitle in subtitles)
        {
            subtitle.StartTime += _offset;
            subtitle.EndTime += _offset;
        }
    }
}