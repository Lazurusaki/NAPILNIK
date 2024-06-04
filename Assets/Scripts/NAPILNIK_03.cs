using System;
using System.IO;
using UnityEngine;

public class NAPILNIK_03 : MonoBehaviour
{
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
        protected string _filePath;

        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        public virtual void Log(string message)
        {
            File.WriteAllText(_filePath,message);
        }
    }

    public class ConsoleLogger : ILogger
    {
        public virtual void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class FridayFileLogger : FileLogger, ILogger
    {
        public FridayFileLogger(string filePath) : base(filePath)
        {
        }

        public override void Log(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                File.WriteAllText(_filePath, message);
            }
        }
    }

    public class FridayConsoleLogger : ConsoleLogger, ILogger
    {
        public override void Log(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                Console.WriteLine(DateTime.Now + message);
            }
        }
    }

    public class ConsoleAndFridayFileLogger : ILogger
    {
        private ConsoleLogger consoleLogger;
        private FridayFileLogger fridayFileLogger;

        public ConsoleAndFridayFileLogger(string filePath)
        {
            consoleLogger = new ConsoleLogger();
            fridayFileLogger = new FridayFileLogger(filePath);
        }

        public void Log(string message)
        {
            consoleLogger.Log(message);
            fridayFileLogger.Log(message);      
        }
    }

    private void Start()
    {
        string filePath = "C:\\Napilnik\\log";
        string message = "Pathfinding in process...";

        Pathfinder pathfinder1 = new Pathfinder(new FileLogger(filePath));
        Pathfinder pathfinder2 = new Pathfinder(new ConsoleLogger());
        Pathfinder pathfinder3 = new Pathfinder(new FridayFileLogger(filePath));
        Pathfinder pathfinder4 = new Pathfinder(new FridayConsoleLogger());
        Pathfinder pathfinder5 = new Pathfinder(new ConsoleAndFridayFileLogger(filePath));

        pathfinder1.Find(message);
        pathfinder2.Find(message);
        pathfinder3.Find(message);
        pathfinder4.Find(message);
        pathfinder5.Find(message);
    }
}