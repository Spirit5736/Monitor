using System;

namespace Monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine($"parameter count = {args.Length}");

            //for (int i = 0; i < args.Length; i++)
            //{
            //    Console.WriteLine($"Arg[{i}] = [{args[i]}]");
            //}
            string name = args[0];
            int acceptableLifetime = Convert.ToInt32(args[1]);         
            int frequencyOfVerification = Convert.ToInt32(args[2]);
            Console.WriteLine("идет мониторинг процесса...");
            MonitoringOfProcess monitor = new();
            monitor.KillProcess(name, acceptableLifetime, frequencyOfVerification);
        }
    }
}
