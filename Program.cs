using System;
using System.Timers;
using NLog;

namespace Monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = args[0];
            int acceptableLifetime = Convert.ToInt32(args[1]);
            int frequencyOfVerification = Convert.ToInt32(args[2]);
            Console.WriteLine("Идет мониторинг процесса...");
            MonitoringOfProcess monitor = new();
            monitor.KillProcess(name, acceptableLifetime, frequencyOfVerification);
            Console.Read();
        }
    }
}
