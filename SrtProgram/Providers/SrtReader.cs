using System.Text.RegularExpressions;
using SrtProgram.Interfaces;
using SrtProgram.Models;

namespace SrtProgram.Providers;

public class SrtReader : ISrtReader
{
    private readonly IFileReader _fileReader;

    public SrtReader(IFileReader fileReader)
        => _fileReader = fileReader;

    public async Task<List<Subtitle>> ReadAsync(string path)
    {
        var lines = await _fileReader.ReadAllLinesAsync(path);
        var regex = new Regex(@"(\d{2}):(\d{2}):(\d{2}),(\d{3})\s-->\s(\d{2}):(\d{2}):(\d{2}),(\d{3})");
        var subtitles = new List<Subtitle>();
        Subtitle subtitle = null!;

        foreach (var line in lines)
        {
            if (subtitle == null && int.TryParse(line, out var number))
            {
                subtitle = new Subtitle
                {
                    Number = number
                };
            }
            else if (subtitle != null && regex.IsMatch(line))
            {
                var match = regex.Match(line);
                if (match.Groups.Count != 9)
                {
                    throw new FormatException("Invalid timestamp format.");
                }

                subtitle.StartTime = ParseTime(match.Groups);
                subtitle.EndTime = ParseTime(match.Groups, 5);
            }
            else if (subtitle != null && string.IsNullOrWhiteSpace(line))
            {
                if (subtitle.StartTime == default || subtitle.EndTime == default)
                {
                    throw new FormatException("Missing timestamp for a subtitle.");
                }

                if (subtitle.Lines.Count == 0)
                {
                    throw new FormatException("Missing text for a subtitle.");
                }

                subtitles.Add(subtitle);
                subtitle = null!;
            }
            else
            {
                subtitle?.Lines.Add(line);
            }
        }

        return subtitles;
    }

    private TimeSpan ParseTime(GroupCollection groups, int offset = 1)
    {
        var hours = int.Parse(groups[offset].Value);
        var minutes = int.Parse(groups[offset + 1].Value);
        var seconds = int.Parse(groups[offset + 2].Value);
        var milliseconds = int.Parse(groups[offset + 3].Value);

        return new TimeSpan(0, hours, minutes, seconds, milliseconds);
    }
}