using System;
using System.Diagnostics;
using System.Threading;

namespace Monitor
{
    /// <summary>Класс реализует метод работы с процессом</summary>
    class MonitoringOfProcess
    {
        protected Process[] procs;

        /// <summary>Метод производит контроль над указанным процессом и "убивает" его при выполнении условия</summary>
        ///  <param name="name">Имя процесса</param>
        ///  <param name="acceptableLifetime">Допустимое время жизни процесса (в минутах)</param>
        ///  <param name="frequencyOfVerification">Частота мониторинга процесса (в минутах)</param>
        public void KillProcess(string name, int acceptableLifetime, int frequencyOfVerification)
        {
            procs = Process.GetProcessesByName(name);
            int checking = frequencyOfVerification * 60000;
            
            foreach (Process proc in procs)
            {
                while (proc.StartTime.AddMinutes(acceptableLifetime) > DateTime.Now)
                {
                    Thread.Sleep(checking);
                }

                int i = 0;
                while (i != procs.Length)
                {
                    procs[i].Kill();
                    i++;
                }
                Console.WriteLine("Готово");
            }
        }  
    }
}
