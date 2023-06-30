using SrtProgram.Models;

namespace SrtProgram.Interfaces;

public interface ISrtProcessor
{
    void Process(List<Subtitle> subtitles);
}