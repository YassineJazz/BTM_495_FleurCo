public static class Logger
{
    // ANSI escape codes for colors
    private const string ResetColor = "\u001b[0m";
    private const string GreenColor = "\u001b[32m";
    private const string RedColor = "\u001b[31m";
    private const string YellowColor = "\u001b[33m";

    public static void Success(string message)
    {
        Console.WriteLine($"\n{GreenColor}✅ {message}{ResetColor}");
    }

    public static void Error(string message)
    {
        Console.WriteLine($"\n{RedColor}❌ {message}{ResetColor}");
    }
    public static void Warning(string message)
    {
        Console.WriteLine($"\n{YellowColor}⚠️ {message}{ResetColor}");
    }
}