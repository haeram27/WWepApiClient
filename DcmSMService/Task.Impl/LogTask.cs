using Foundations.Task;
using Foundations.Logger;
using System.Timers;

namespace DcmSMService.Task.Impl
{
    public sealed class LogTask : ScheduleTask
    {
        private const string TAG = "LogTask";

        public LogTask(double interval, bool autoreset) : base(interval, autoreset) {}

        public override void Run(object sender, ElapsedEventArgs args)
        {
            Log.Info(TAG, args.SignalTime.ToUniversalTime().ToString());
        }
    }
}
