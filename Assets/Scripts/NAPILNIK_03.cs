using System;
using System.Collections.Generic;
using System.IO;

namespace NAPILNIK_03
{
    public interface ILogger
    {
        public void Log(string message);
    }

    public class Pathfinder
    {
        private List<ILogger> _loggers;

        public Pathfinder(List<ILogger> loggers)
        {
            _loggers = loggers;
        }

        public void Find(string message)
        {
            foreach (ILogger logger in _loggers)
            {
                logger.Log(message);
            }
        }
    }

    public class FileLogger : ILogger
    {
        private string _filePath;

        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void Log(string message)
        {
            File.WriteAllText(_filePath, message);
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
    /*
    private void Test()
    {
        DayOfWeek dayOfWeek = DayOfWeek.Friday;
        string filePath = "C:\\Napilnik\\log";
        string message = "Pathfinding in process...";

        Pathfinder pathfinder1 = new Pathfinder(new List<ILogger>() { new FileLogger(filePath) });
        Pathfinder pathfinder2 = new Pathfinder(new List<ILogger>() { new ConsoleLogger()});
        Pathfinder pathfinder3 = new Pathfinder(new List<ILogger>() { new SecureLogger(new FileLogger(filePath), dayOfWeek) });
        Pathfinder pathfinder4 = new Pathfinder(new List<ILogger>() { new SecureLogger(new ConsoleLogger(), dayOfWeek) });
        Pathfinder pathfinder5 = new Pathfinder(new List<ILogger>() { new ConsoleLogger(), new SecureLogger(new FileLogger(filePath), dayOfWeek) });

        pathfinder1.Find(message);
        pathfinder2.Find(message);
        pathfinder3.Find(message);
        pathfinder4.Find(message);
        pathfinder5.Find(message);
    }
    */
}