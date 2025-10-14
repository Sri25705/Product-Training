using System;
using System.IO;

public interface ILogger
{
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message);
}

public class ConsoleLogger : ILogger
{
    public void LogInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Info:{message}");
        Console.ResetColor();
    }
    public void LogWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"WARNING:{message}");
        Console.ResetColor();
    }
    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error:{message}");
        Console.ResetColor();
    }
}

public class FileLogger : ILogger
{
    private readonly string filepath = Path.Combine(Environment.CurrentDirectory, "log.txt");

    public void LogInfo(string message)
    {
        LogToFile($"Info:{message}");
    }
    public void LogWarning(string message)
    {
        LogToFile($"Warning:{message}");
    }

    public void LogError(string message)
    {
        LogToFile($"Error:{message}");
    }
    private void LogToFile(string message)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(filepath, append: true))
            {
                sw.WriteLine($"{DateTime.Now}:{message}");
                sw.Flush();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"File logging failed:{ex.Message}");
        }
    }

    public void DisplayLogs()
    {
        Console.WriteLine("Log file path");
        if (File.Exists(filepath))
        {
            using (StreamReader reader = new StreamReader(filepath))
            {
                string line;
                while((line=reader.ReadLine())!=null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        else
        {
            Console.WriteLine("log file not found");
        }
    }
}

public class AppService
{
    private readonly ILogger logger;
    public AppService(ILogger logger)
    {
        this.logger = logger;
    }
    public void Run()
    {
        logger.LogInfo("Application started");
        logger.LogWarning("Detected");
        logger.LogError("Error");
    }
}
public class Program
    {
        public static void Main()
    {
        ILogger logger;

        Console.WriteLine("choose");
        Console.WriteLine("1. Console Logegr");
        Console.WriteLine("2. File Logger");
        Console.WriteLine("Enter your choice");
        string choice = Console.ReadLine();

        if (choice == "2")
            logger = new FileLogger();
        else
            logger = new ConsoleLogger();

        AppService app = new AppService(logger);
        app.Run();

        if(logger is FileLogger fileLogger)
        {
            fileLogger.DisplayLogs();
        }

        Console.WriteLine("completed");
    }
}