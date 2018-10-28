using System;
using System.ServiceProcess;

namespace DcmSMService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                DcmSMService testsvc = new DcmSMService();
                testsvc.TestStartupAndStop(args);
            }
            else
            {
                // Put the body of your old Main method here.
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new DcmSMService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
