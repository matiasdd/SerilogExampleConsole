using System;
using Serilog;
using Serilog.Sinks.File;

namespace SerilogExampleConsole
{
    using static Log;

    static class Log
    {
        public static ILogger Logger { get; }

        static Log()
        {
            Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Minute)
                .CreateLogger();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Logger.Information("Hello World!");
            Console.WriteLine("Hello World!");
            Logger.Debug("I just said \"Hello World!\"");
            Logger.Error(new Exception("I failed after \"Hello World!\""), "Exception");

            var someClass = new SomeClass();
            someClass.Run();

            Logger.Information("Goodbye!");
        }
    }

    class SomeClass
    {
        ILogger contextLogger = Logger.ForContext(typeof(SomeClass));
        
        public void Run()
        {
            contextLogger.Debug($"Entering {nameof(Run)}");
            contextLogger.Information($"Running {nameof(Run)}");
            contextLogger.Debug($"Exiting {nameof(Run)}");
        }
    }
}
