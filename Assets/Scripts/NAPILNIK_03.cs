using System;
using UnityEngine;

public class NAPILNIK_02 : MonoBehaviour
{
    public class Pathfinder
    {
        private ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            _logger = logger;
        }

        public void Find()
        {
            print("Finding path...");
        }
    }

    public class FileLogger : ILogger
    {
        public void Log(string message)
        {
            print("Log in file");
        }
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            print("Log in console");
        }
    }

    public class FridayFileLogger : ILogger
    {
        public void Log(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                print($"{DateTime.Now} Log in file");
            }
        }
    }

    public class FridayConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                print($"{DateTime.Now} Log in console");
            }
        }
    }

    public class ConsoleAndFridayFileLogger : ILogger
    {
        public void Log(string message)
        {
            print("Log in console");

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                print($"{DateTime.Now} Log in file");
            }
        }
    }

    private void Start()
    {
        Pathfinder pathfinder1 = new Pathfinder(new FileLogger());
        Pathfinder pathfinder2 = new Pathfinder(new ConsoleLogger());
        Pathfinder pathfinder3 = new Pathfinder(new FridayFileLogger());
        Pathfinder pathfinder4 = new Pathfinder(new FridayConsoleLogger());
        Pathfinder pathfinder5 = new Pathfinder(new ConsoleAndFridayFileLogger());

        pathfinder1.Find();
        pathfinder2.Find();
        pathfinder3.Find();
        pathfinder4.Find();
        pathfinder5.Find();
    }
}