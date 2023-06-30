using SrtProgram.Models;

namespace SrtProgram.Interfaces;

public interface ISubtitleProcessor
{
    void Process(List<Subtitle> subtitles);
}