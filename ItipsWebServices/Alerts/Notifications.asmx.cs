using System;
using System.Linq;
using System.Web.Services;
using ItipsWebServices.Utility;
using ItipsWebServices.WsEventSinks;
using NLog;
using OppmHelper.Utility;
using Quartz;
using Quartz.Impl;
using Quartz.Job;
using wsPortfoliosWebServiceAlert;



// ReSharper disable InconsistentNaming

namespace ItipsWebServices.Alerts
{

    [WebService(Namespace = "http://prosight.com/wsdl/7.0/EventSink/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Notifications
    {

        public static Logger NLogger = LogManager.GetCurrentClassLogger();

        public Notifications()
        {
            var i = 0;
            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        public String HeartBeat()
        {
            NLogger.Info("HeartBeat");
            return $"HeartBeat {DateTime.Now}";
        }

        [WebMethod]
        public void TestEmail()
        {
            try
            {
                var scheduledJob = JobBuilder.Create<SendMailJob>().WithIdentity("SendEmailJob", "SendEmailGroup").Build();

                var sendMailJob = HelperFunctions.GetSchedulesJobByName("SendEmailJob");


               
                    foreach (var jobDataEntry in sendMailJob.JobDataMap)
                    {
                        scheduledJob.JobDataMap.Add(jobDataEntry.Key, jobDataEntry.Value);
                    }
                    
                
                scheduledJob.JobDataMap.Add("message", "This is the test email");
                NLogger.Trace(HelperFunctions.DumpJobDataMap(scheduledJob.JobDataMap));
                
                // Trigger the job to run now, and then every 40 seconds
                var trigger = TriggerBuilder
                    .Create()
                    .WithIdentity("SendEmailTrigger", "SendEmailGroup")
                    .StartNow()
                    .Build();

                GlobalVariables.Scheduler.ScheduleJob(scheduledJob, trigger);

                var toEmail = scheduledJob.JobDataMap.ToList().Find(x => x.Key.IsEqualTo("recipient", true));
                NLogger.Info($"SendEmailJob SendEmailGroup to {toEmail.Value}");
            }
            catch (Exception ex)
            {
                NLogger.Error(ex.Message);
            }
        }


        private void showEventProperties(psPortfoliosEventInfo eventObj)
        {
            NLogger.Trace("Alert Name: " + eventObj.AlertName +
                "\nAlert Guid: " + eventObj.AlertGuid +
                "\nEvent Date Time: " + eventObj.EventDateTime +
                "\nOwner Id: " + eventObj.OwnerId +
                "\nOwner Login: " + eventObj.OwnerLogin +
                "\nScope Prosight Id: " + eventObj.ScopeInfo.ProSightID +
                "\nScope Name: " + eventObj.ScopeInfo.Name +
                "\nScope Portfolio Type: " + eventObj.ScopeInfo.PortfolioType +
                "\nScope UCI ID: " + eventObj.ScopeInfo.UCI);
        }

        [WebMethod]
        public void itemAddedToPortfolio(psPortfoliosItemAddedToPortfolioEventInfo eventObj)
        {
            showEventProperties(eventObj);
            NLogger.Trace("AddedCreated: " + eventObj.AddedCreated +
                "\nAdded Item Prosight Id: " + eventObj.AddedItemInfo.ProSightID +
                "\nAdded Item Name: " + eventObj.AddedItemInfo.Name +
                "\nAdded Item Type: " + eventObj.AddedItemInfo.PortfolioType +
                "\nAdded Item UCI ID: " + eventObj.AddedItemInfo.UCI);
        }

        [WebMethod]
        public void categoryConditionMet(psPortfoliosCategoryConditionMetEventInfo eventObj)
        {
            var categoryConditionMet = new CategoryConditionMet();
            categoryConditionMet.Run(eventObj);
        }

        [WebMethod]
        public void multipleCategoryConditionMet(psPortfoliosMultipleCategoryConditionsMetEventInfo eventObj)
        {

            showEventProperties(eventObj);
        }

        [WebMethod]
        public void phaseModified(psPortfoliosPhaseModifiedEventInfo eventObj)
        {
            showEventProperties(eventObj);
            NLogger.Trace("Phase Name: " + eventObj.PhaseName);
            for (var i = 0; i < eventObj.PhaseModifications.Length; ++i)
            {
                switch (eventObj.PhaseModifications[i].Field)
                {
                    case psPHASE_FIELD.PF_HEALTH:
                        NLogger.Trace("Modification " + i +
                            ", Phase Field: " + eventObj.PhaseModifications[i].Field +
                            ", Previous Value: " + ((psINDICATOR)Int32.Parse(eventObj.PhaseModifications[i].PrevValue)) +
                            ", Current Value: " + ((psINDICATOR)Int32.Parse(eventObj.PhaseModifications[i].CurValue)));
                        break;
                    case psPHASE_FIELD.PF_STATUS:
                        NLogger.Trace("Modification " + i +
                            ", Phase Field: " + eventObj.PhaseModifications[i].Field +
                            ", Previous Value: " + ((psPHASE_STATUS)Int32.Parse(eventObj.PhaseModifications[i].PrevValue)) +
                            ", Current Value: " + ((psPHASE_STATUS)Int32.Parse(eventObj.PhaseModifications[i].CurValue)));
                        break;
                    default:
                        NLogger.Trace("Modification " + i +
                            ", Phase Field: " + eventObj.PhaseModifications[i].Field +
                            ", Previous Value: " + eventObj.PhaseModifications[i].PrevValue +
                            ", Current Value: " + eventObj.PhaseModifications[i].CurValue);
                        break;
                }
            }
        }

        [WebMethod]
        public void deliverableModified(psPortfoliosDeliverableModifiedEventInfo eventObj)
        {
            showEventProperties(eventObj);
            NLogger.Trace("Deliverable Name: " + eventObj.DeliverableName +
                "\nPhase Name: " + eventObj.PhaseName +
                "\nEvent Cause: " + eventObj.EventCause);
            for (int i = 0; i < eventObj.DeliverableModifications.Length; ++i)
            {
                NLogger.Trace("Modification " + i +
                    ", Deliverable Field: " + eventObj.DeliverableModifications[i].Field +
                    ", Previous Value: " + eventObj.DeliverableModifications[i].PrevValue +
                    ", Current Value: " + eventObj.DeliverableModifications[i].CurValue);
            }
        }

        [WebMethod]
        public void actionItemModified(psPortfoliosActionItemModifiedEventInfo eventObj)
        {
            showEventProperties(eventObj);
            NLogger.Trace("Action Item Name: " + eventObj.ActionItemName +
                "\nEventCause: " + eventObj.EventCause);
            for (int i = 0; i < eventObj.ActionItemModifications.Length; ++i)
            {
                NLogger.Trace("Modification " + i +
                    ", Action Item Field: " + eventObj.ActionItemModifications[i].Field +
                    ", Previous Value: " + eventObj.ActionItemModifications[i].PrevValue +
                    ", Current Value: " + eventObj.ActionItemModifications[i].CurValue);
            }
        }

        [WebMethod]
        public psINDICATOR defaultpsINDICATOR()
        {
            return psINDICATOR.IND_NONE;
        }

        [WebMethod]
        public psPHASE_STATUS defaultpsPHASE_STATUS()
        {
            return psPHASE_STATUS.PHSTS_NONE;
        }
    }
}
