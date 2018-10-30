using DcmSMService.WebAPIClient;
using Foundations.Task;
using System.Timers;

namespace DcmSMService.Task.Impl
{
    public sealed class GithubWebApiTestTask : ScheduleTask
    {
        private const string TAG = "GithubWebApiTestTask";

        public GithubWebApiTestTask(double interval, bool autoreset) : base(interval, autoreset) { }

        public override void Run(object sender, ElapsedEventArgs args)
        {
            GithubRepositoryRequest.Request();
        }
    }
}