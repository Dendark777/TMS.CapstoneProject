using System.Collections.Concurrent;

namespace TMS.CapstoneProject.Core.Logging;

public static class TestLogger
{
    private static readonly ConcurrentQueue<string> Buffer = new();

    public static void Info(string message) => Write("INFO", message);

    public static void Warn(string message) => Write("WARN", message);

    public static void Error(string message) => Write("ERROR", message);

    public static IReadOnlyList<string> Snapshot() => Buffer.ToArray();

    private static void Write(string level, string message)
    {
        var line = $"[{DateTime.Now:HH:mm:ss.fff}] [{level}] [T{Environment.CurrentManagedThreadId}] {message}";
        Buffer.Enqueue(line);
        Console.WriteLine(line);
    }
}
