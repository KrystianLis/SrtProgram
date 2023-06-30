using SrtProgram.Interfaces;
using SrtProgram.Models;

namespace SrtProgram.Providers;

public class SrtProcessor : ISrtProcessor
{
    private readonly TimeSpan _offset;

    public SrtProcessor(TimeSpan offset)
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