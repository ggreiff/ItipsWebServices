// ***********************************************************************
// Assembly         : BMcD
// Author           : Gene Greiff
// Created          : 02-05-2015
//
// Last Modified By : Gene Greiff
// Last Modified On : 02-09-2015
// ***********************************************************************
// <copyright file="JobScheduler.cs" company="NetImpact Strategies Inc.">
//     Copyright (c) NetImpact Strategies Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Common.Logging.NLog;
using Quartz;
using Quartz.Impl;

namespace ItipsWebServices.Quartz
{
    /// <summary>
    /// Class JobScheduler.
    /// </summary>
    public class JobScheduler
    {
        /// <summary>
        /// Starts the schedular.
        /// </summary>
        /// <returns>IScheduler.</returns>
        public static IScheduler StartSchedular()
        {
            var properties = new Common.Logging.Configuration.NameValueCollection {{"configType", "FILE"}, {"configFile", "~/NLog.config"}};
            Common.Logging.LogManager.Adapter = new NLogLoggerFactoryAdapter(properties);
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            return scheduler;
        }

        /// <summary>
        /// Shutdown the scheduler.
        /// </summary>
        public static void ShutdownScheduler()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Shutdown();
        }
    }
}