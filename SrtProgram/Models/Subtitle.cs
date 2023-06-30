namespace SrtProgram.Models;

public class Subtitle
{
    public int Number { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public List<string> Lines { get; } = new();
}