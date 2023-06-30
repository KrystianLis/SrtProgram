using SrtProgram.Models;

namespace SrtProgram.Interfaces;

public interface ISrtReader
{
    Task<List<Subtitle>> ReadAsync(string path);
}