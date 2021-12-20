using DWR_API.Helpers;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Timers;

namespace Rabby2_ModbusProtocol_WindowsService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();

        //*************************************************************************************************************
        public Service1()
        {
            InitializeComponent();
        }

        //*************************************************************************************************************

        //public void onDebug()
        //{
        //    this.OnStart(null);
        //}

        //*************************************************************************************************************

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);
            int timeMinuteIntervalToCheck = Convert.ToInt32(Utilities.RegEdit.getTimerMinute()) * 60000;
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = Convert.ToInt32(timeMinuteIntervalToCheck); //number in milisecinds  
            timer.Enabled = true;
        }

        //*************************************************************************************************************

        protected override void OnStop()
        {
            string strPath = AppDomain.CurrentDomain.BaseDirectory + "Log.txt";
            File.AppendAllLines(strPath, new[] { "Stop time: " + DateTime.Now.ToString() });
        }

        //*************************************************************************************************************

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            string servicePath = Utilities.RegEdit.getSPath();

            bool isRunning = Process.GetProcessesByName("javaw").Any();
            //WriteToFile(DateTime.Now.ToString() + ":Starting javaw.exe" + ", javaw status:" + isRunning);
            if (!isRunning)
            {
                Process myProcess = new Process();
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = "javaw";
                myProcess.StartInfo.Arguments = "-jar " + servicePath;
                myProcess.Start();
            }         
        }

        //*************************************************************************************************************

        private void WriteToFile(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(message);
                }

            }
        }

        //*************************************************************************************************************

    }
}
