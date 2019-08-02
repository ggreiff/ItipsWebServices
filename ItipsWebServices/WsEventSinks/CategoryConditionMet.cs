using System;
using ItipsWebServices.Utility;
using NLog;
using OppmHelper.Utility;
using Quartz;
using wsPortfoliosWebServiceAlert;

namespace ItipsWebServices.WsEventSinks
{
    public class CategoryConditionMet
    {
        public const String GroupName = "CategoryConditionMet";
        private const String EventObj = "EventObj";

        public static Logger NLogger = LogManager.GetCurrentClassLogger();

        public void Run(psPortfoliosCategoryConditionMetEventInfo eventObj)
        {
            if (eventObj == null)
            {
                NLogger.Error("EventObj is null this is not valid for a CategoryConditionMet web service alert");
                return;
            }
            try
            {
                 NLogger.Trace("Starting CategoryConditionMet");               
                
                // get a scheduler
                if (GlobalVariables.Scheduler == null)
                {
                    NLogger.Error("The Scheduler application variable is null");
                    return;
                }
                
                var job = HelperFunctions.GetAlertsSchedule(eventObj.AlertName, GlobalVariables.JobSchedules);
                if (job.Name.IsNullOrEmpty())
                {
                    NLogger.Warn("Unable to find a job in quartz_jobs.xml for alert {0}", eventObj.AlertName);
                    return;
                }

                var jobDataMap = HelperFunctions.GetJobDataMap(job);
                jobDataMap.Put(EventObj, eventObj.ToXml());
                var jobType = Type.GetType(HelperFunctions.GetAlertJobType(eventObj.AlertName, job));
                if (jobType == null)
                {
                    NLogger.Warn("Unable to find a job of type {0}", HelperFunctions.GetAlertJobType(eventObj.AlertName, job));
                    return;
                }

                var newGuid = Guid.NewGuid().ToString();
                var jobDetail = JobBuilder.Create(jobType).WithIdentity($"{job.Name} {newGuid}", GroupName).SetJobData(jobDataMap).Build();
                var trigger = TriggerBuilder.Create().WithIdentity($"Trigger {newGuid}", GroupName).StartNow().Build();

                GlobalVariables.Scheduler.ScheduleJob(jobDetail, trigger);

                NLogger.Trace("Finished CategoryConditionMet");
            }
            catch (Exception ex)
            {
                NLogger.Error(ex.Message);
            }
        }
    }
}