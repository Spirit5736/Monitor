using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using NLog;

namespace Monitor
{
    /// <summary>Класс реализует методы работы с процессом</summary>
    class MonitoringOfProcess
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private Process[] procs;
        private List<ProcessChecker> _processCheckers;

        /// <summary>Метод производит контроль над указанным процессом и "убивает" его при выполнении условия</summary>
        ///  <param name="name">Имя процесса</param>
        ///  <param name="acceptableLifetime">Допустимое время жизни процесса (в минутах)</param>
        ///  <param name="frequencyOfVerification">Частота мониторинга процесса (в минутах)</param>
        public void KillProcess(string name, int acceptableLifetime, int frequencyOfVerification)
        {
            procs = Process.GetProcessesByName(name);
            _processCheckers = new List<ProcessChecker>();
            foreach (var proc in procs)
            {
                var processInfo = new ProcessChecker(proc, acceptableLifetime, frequencyOfVerification);
                if (!processInfo.ProcessIsKilled)
                    _processCheckers.Add(processInfo);
            }
            var timerCb = new TimerCallback(CheckProcesses);
            var timer = new Timer(timerCb, null, 0, frequencyOfVerification * 60000);
        }

        /// <summary>Метод производит проверку на наличие "не убитых" процессов</summary>
        private void CheckProcesses(object parameter)
        {
            var notKilledProcesses = _processCheckers.Where(x => !x.ProcessIsKilled).ToList();
            foreach (var processChecker in notKilledProcesses)
            {
                processChecker.Check();
            }

            notKilledProcesses = _processCheckers.Where(x => !x.ProcessIsKilled).ToList();
            if (notKilledProcesses.Count == 0)
            {
                Logger.Info("Все найденные процессы завершены");
                Environment.Exit(0);
            }
        }
    }
}

   