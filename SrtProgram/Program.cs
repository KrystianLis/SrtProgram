using SrtProgram.Extensions;
using SrtProgram.Interfaces;
using SrtProgram.Providers;

ISrtReader reader = new SrtReader(new ProFileReader());
ISrtWriter writer = new SrtWriter(new ProFileWriter());
ISrtProcessor processor = new SrtProcessor(TimeSpan.FromSeconds(5.88));

while (true)
{
    Console.Write("Enter the path to the .srt file (or 'exit' to quit): ");
    var filePath = Console.ReadLine();

    if (filePath?.ToLower() == "exit")
    {
        break;
    }

    if (!File.Exists(filePath))
    {
        Console.WriteLine($"File does not exist: {filePath}");
        continue;
    }

    if (Path.GetExtension(filePath).ToLower() != ".srt")
    {
        Console.WriteLine($"Unsupported file type: {filePath}. Please provide a .srt file.");
        continue;
    }

    try
    {
        var subtitles = await reader.ReadAsync(filePath);
        processor.Process(subtitles);

        var equalSeconds = subtitles
            .Where(s => s.StartTime.Milliseconds == 0)
            .ReassignSubtitleNumbers();

        subtitles.RemoveAll(s => s.StartTime.Milliseconds == 0);

        subtitles = subtitles.ReassignSubtitleNumbers();

        string equalSecondsPath = @$"{Path.GetDirectoryName(filePath)}\equalSeconds.srt";

        await writer.WriteAsync(equalSecondsPath, equalSeconds);
        Console.WriteLine($"Subtitles with equal seconds saved to {equalSecondsPath}");

        await writer.WriteAsync(filePath, subtitles);

        Console.WriteLine($"Processed file: {filePath}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}
