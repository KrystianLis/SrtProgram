using SrtProgram.Models;

namespace SrtProgram.Interfaces;

public interface ISubtitleReader
{
    Task<List<Subtitle>> ReadAsync(string path);
}