///-----------------------------------------------------------------
///   ClassName:      ScheduleTask
///   Description:    Run task repeatly with interval
///   Author:         soon_woo_hwang@mcafee.com                    
///   Date:           2018/10/27
///   Notes:          
///   Revision History:
///-----------------------------------------------------------------
///

using Foundations.Logger;
using System;
using System.Timers;

namespace Foundations.Task
{
    public abstract class ScheduleTask
    {
        private const string TAG = "ScheduleTask";

        public const double INTERVAL_SEC = 1000;
        public const double INTERVAL_MIN = INTERVAL_SEC * 60;
        public const double INTERVAL_HOUR = INTERVAL_MIN * 60;
        public const double INTERVAL_DAY = INTERVAL_HOUR * 24;
        public const double INTERVAL_WEEK = INTERVAL_DAY * 7;

        private readonly Timer mTimer;

        //Thread-Safe implementation required
        public abstract void Run(object sender, System.Timers.ElapsedEventArgs args);

        public ScheduleTask(double interval, bool autoreset)
        {
            mTimer = new Timer();
            mTimer.Interval = interval;  //msec
            mTimer.AutoReset = autoreset;
        }

        ~ScheduleTask()
        {
            if (mTimer != null)
            {
                if (mTimer.Enabled)
                {
                    mTimer.Stop();
                }
                mTimer.Dispose();
            }
        }

        /// <summary>
        /// Start ScheduleTask.
        /// 
        ///     var task = new ScheduleTask(ScheduleTask.INTERVAL_SEC);
        ///     task.Start();
        /// </summary>
        public void Start()
        {
            mTimer.Elapsed += new ElapsedEventHandler(this.Run);
            try
            {
                mTimer.Start();
            }
            catch (Exception e)
            {
                Log.Error(TAG, e.Message);
            }
        }

        /// <summary>
        /// Pause a started ScheduleTask
        /// 
        /// usage:
        ///     var task = new ScheduleTask(ScheduleTask.INTERVAL_SEC);
        ///     task.Start();
        ///     task.Pause();
        /// </summary>
        public void Pause()
        {
            if (mTimer.Enabled)
            {
                mTimer.Stop();
            }     
        }

        /// <summary>
        /// Resume a paused ScheduleTask
        /// 
        /// usage:
        ///     var task = new ScheduleTask(ScheduleTask.INTERVAL_SEC);
        ///     task.Start();
        ///     task.Pause();
        ///     task.Resume();
        /// </summary>
        public void Resume()
        {
            if (!mTimer.Enabled)
            {
                mTimer.Start();
            }
        }
    }
}
