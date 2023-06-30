using SrtProgram.Interfaces;
using SrtProgram.Providers;

ISubtitleReader reader = new SubtitleReader(new ProFileReader());
ISubtitleWriter writer = new SubtitleWriter(new ProFileWriter());
ISubtitleProcessor processor = new SubtitleProcessor(TimeSpan.FromSeconds(5.88));

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

        await writer.WriteAsync(filePath, subtitles);

        Console.WriteLine($"Processed file: {filePath}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}