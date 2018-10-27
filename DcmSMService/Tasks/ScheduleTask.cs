///-----------------------------------------------------------------
///   ClassName:      ConfigHelper
///   Description:    Run task repeatly with interval
///   Author:         soon_woo_hwang@mcafee.com                    
///   Date:           2018/10/27
///   Notes:          
///   Revision History:
///-----------------------------------------------------------------
///

using DcmSMService.Utils;
using System;
using System.Timers;

namespace DcmSMService.Tasks
{
    public abstract class ScheduleTask
    {
        private const string TAG = "ScheduleTask";

        public const uint INTERVAL_SEC = 1000;
        public const uint INTERVAL_MIN = INTERVAL_SEC * 60;
        public const uint INTERVAL_HOUR = INTERVAL_MIN * 60;
        public const uint INTERVAL_DAY = INTERVAL_HOUR * 24;
        public const uint INTERVAL_WEEK = INTERVAL_DAY * 7;

        private readonly Timer mTimer;

        //Thread-Safe implementation required
        public abstract void Run(object sender, System.Timers.ElapsedEventArgs args);

        public ScheduleTask(uint interval)
        {
            mTimer = new Timer();
            mTimer.Interval = interval;  //msec
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

        public void Pause()
        {
            if (mTimer.Enabled)
            {
                mTimer.Stop();
            }     
        }

        public void Resume()
        {
            if (!mTimer.Enabled)
            {
                mTimer.Start();
            }
        }
    }
}
