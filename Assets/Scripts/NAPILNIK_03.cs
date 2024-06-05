using System;
using System.IO;

namespace NAPILNIK_03
{
    public interface ILogger
    {
        public void Log(string message);
    }

    public class Pathfinder
    {
        private readonly ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            _logger = logger;
        }

        public void Find(string message)
        {
            _logger.Log(message);
        }
    }

    public class FileLogger : ILogger
    {
        protected string FilePath;

        public FileLogger(string filePath)
        {
            FilePath = filePath;
        }

        public void Log(string message)
        {
            File.WriteAllText(FilePath, message);
        }
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class SecureLogger : ILogger
    {
        private ILogger _logger;
        private DayOfWeek _dayOfWeek;

        public SecureLogger(ILogger logger, DayOfWeek dayOfWeek)
        {
            _logger = logger;
            _dayOfWeek = dayOfWeek;
        }

        public void Log(string message)
        {
            if (DateTime.Now.DayOfWeek == _dayOfWeek)
            {
                _logger.Log(message);
            }
        }
    }

    public class ConsoleAndSecureFileLogger : ILogger
    {
        private ConsoleLogger _consoleLogger;
        private SecureLogger _secureFileLogger;

        public ConsoleAndSecureFileLogger(string filePath, DayOfWeek dayOfWeek)
        {
            _consoleLogger = new ConsoleLogger();
            _secureFileLogger = new SecureLogger(new FileLogger(filePath), dayOfWeek);
        }

        public void Log(string message)
        {
            _consoleLogger.Log(message);
            _secureFileLogger.Log(message);
        }
    }

    private void Test()
    {
        DayOfWeek dayOfWeek = DayOfWeek.Friday;
        string filePath = "C:\\Napilnik\\log";
        string message = "Pathfinding in process...";

        Pathfinder pathfinder1 = new Pathfinder(new FileLogger(filePath));
        Pathfinder pathfinder2 = new Pathfinder(new ConsoleLogger());
        Pathfinder pathfinder3 = new Pathfinder(new SecureLogger(new FileLogger(filePath), dayOfWeek));
        Pathfinder pathfinder4 = new Pathfinder(new SecureLogger(new ConsoleLogger(), dayOfWeek));
        Pathfinder pathfinder5 = new Pathfinder(new ConsoleAndSecureFileLogger(filePath,dayOfWeek));

        pathfinder1.Find(message);
        pathfinder2.Find(message);
        pathfinder3.Find(message);
        pathfinder4.Find(message);
        pathfinder5.Find(message);
    }
}