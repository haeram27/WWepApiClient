using Foundations.Task;
using Foundations.Logger;
using System.Timers;

namespace DcmSMService.Task.Impl
{
    public sealed class LogTask : ScheduleTask
    {
        private const string TAG = "LogTask";

        public LogTask(uint interval) : base(interval) {}

        public override void Run(object sender, ElapsedEventArgs args)
        {
            Log.Info(TAG, args.SignalTime.ToUniversalTime().ToString());
        }
    }
}
