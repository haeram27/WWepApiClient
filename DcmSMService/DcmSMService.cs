using Foundations.Logger;
using Foundations.Task;
using DcmSMService.Task.Impl;
using System.ServiceProcess;


namespace DcmSMService
{
    public partial class DcmSMService : ServiceBase
    {
        const string TAG = "DcmSMService";

        public DcmSMService()
        {
            InitializeComponent();
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            //var task = new LogTask(ScheduleTask.INTERVAL_SEC, true);
            var task = new GithubWebApiTestTask(ScheduleTask.INTERVAL_SEC, false);
            task.Start();
            System.Threading.Thread.Sleep(1000 * 60);
            this.OnStop();
        }

        protected override void OnStart(string[] args)
        {
            Log.Info(TAG, "OnStart");
        }

        protected override void OnPause()
        {
            Log.Info(TAG, "OnPause");
        }

        protected override void OnContinue()
        {
            Log.Info(TAG, "OnContinue");
        }

        protected override void OnStop()
        {
            Log.Info(TAG, "OnStop");
        }

        protected override void OnShutdown()
        {
            Log.Info(TAG, "OnShutdown");
        }
    }
}
