using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace Nerdile.KeepAliveSvc
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        private List<string> Paths { get; set; } = new List<string>();
        private string LogFile { get; set; }

        private void LogMessage(string message)
        {
            using (var writer = File.AppendText(LogFile))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyyMMdd-hhmmss") + ": " + message);
            }
        }

        protected override void OnStart(string[] args)
        {
//            Thread.Sleep(TimeSpan.FromSeconds(10));
            LogFile = System.Configuration.ConfigurationManager.AppSettings["LogFolder"] + @"\" + DateTime.Now.ToString("yyyyMMdd-hhmmss") + ".txt";
            Paths.AddRange(System.Configuration.ConfigurationManager.AppSettings["Paths"].Split(';'));
            timer.Enabled = true;

            LogMessage("Service started with paths: ");
            foreach (var p in Paths)
            {
                LogMessage(p);
            }
            timer_Tick(null, null);
        }

        protected override void OnStop()
        {
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (var p in Paths)
            {
                try
                {
                    LogMessage($"Writing to {p}");
                    using (var file = File.OpenWrite($@"{p}\ping.txt"))
                    {
                        using (var writer = new StreamWriter(file))
                        {
                            writer.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogMessage($"{ex.GetType().FullName}: {ex.Message}: {ex.StackTrace}");
                }
            }
        }
    }
}
