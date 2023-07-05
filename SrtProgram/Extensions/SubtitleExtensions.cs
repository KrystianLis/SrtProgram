using SrtProgram.Models;

namespace SrtProgram.Extensions;

public static class SubtitleExtensions
{
    public static List<Subtitle> ReassignSubtitleNumbers(this IEnumerable<Subtitle> subtitles)
    {
        return subtitles.Select((s, i) =>
        {
            s.Number = i + 1;
            return s;
        }).ToList();
    }
}