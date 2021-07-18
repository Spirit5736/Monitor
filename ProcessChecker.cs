using System;
using System.Diagnostics;

namespace Monitor
{
    /// <summary>Класс определяет метод расчета жизни процесса</summary>    
    public class ProcessChecker
    {
        public Process Process { get; }

        public int AcceptableLifetime { get; }

        public int FrequencyOfVerification { get; }

        public bool ProcessIsKilled { get; private set; }

        /// <summary>Конструктор класса ProcessChecker</summary>
        ///  <param name="process">экземпляр Process</param>
        ///  <param name="acceptableLifetime">Допустимое время жизни процесса (в минутах)</param>
        ///  <param name="frequencyOfVerification">Частота мониторинга процесса (в минутах)</param>
        public ProcessChecker(Process process, int acceptableLifetime, int frequencyOfVerification)
        {
            Process = process;
            AcceptableLifetime = acceptableLifetime;
            FrequencyOfVerification = frequencyOfVerification;
        }

        /// <summary>Метод производит проверку жизни процесса и "убивает" процесс</summary>    
        public void Check()
        {
            var delta = DateTime.Now - Process.StartTime;
            if (delta.TotalMilliseconds >= AcceptableLifetime * 60000)
            {
                Console.WriteLine($"{Process.ProcessName} был завершен - {DateTime.Now}");
                Process.Kill();
                ProcessIsKilled = true;
            }
        }
    }
}
