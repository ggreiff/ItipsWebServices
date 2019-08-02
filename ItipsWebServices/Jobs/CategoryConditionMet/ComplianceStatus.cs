using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ItipsWebServices.Utility;
using NLog;
using OppmHelper.Utility;
using Oracle.ManagedDataAccess.Client;
using Quartz;
using Quartz.Job;
using wsPortfoliosWebServiceAlert;

namespace ItipsWebServices.Jobs.CategoryConditionMet
{
    public class ComplianceStatus : IJob
    {
        const String GroupName = "CategoryConditionMet";
        const String JobName = "Quartz.Job.SendMailJob";

        public static Logger NLogger = LogManager.GetCurrentClassLogger();


        public void Execute(IJobExecutionContext context)
        {
            try
            {
                NLogger.Trace("Starting ComplianceStatus");

                var eventObj = context.MergedJobDataMap.GetString("EventObj").FromXml<psPortfoliosCategoryConditionMetEventInfo>();
                var statusData = GetDataElements(eventObj, context.MergedJobDataMap);

                var jobDataMap = HelperFunctions.GetJobDataByClassName(JobName);
                jobDataMap.Put("recipient", statusData.PmEmail);
                jobDataMap.Put("subject", $"Alert {statusData.CategoryName} for {statusData.ItemName}");
                jobDataMap.Put("message", BuildMessage(statusData));
                if (statusData.GroupEmails.HasItems()) jobDataMap.Put("cc_recipient", String.Join(",", statusData.GroupEmails));

                if (statusData.PmEmail.IsNullOrEmpty())
                {
                    NLogger.Debug(statusData.ToXml());
                }
                else
                {
                    var newGuid = Guid.NewGuid().ToString();

                    var jobDetail = JobBuilder.Create<SendMailJob>().WithIdentity($"{JobName} {newGuid}", GroupName).SetJobData(jobDataMap).Build();
                    var trigger = TriggerBuilder.Create().WithIdentity($"Trigger {newGuid}", GroupName).StartNow().Build();

                    context.Scheduler.ScheduleJob(jobDetail, trigger);
                    NLogger.Info($"SendEmailJob SendEmailGroup to {statusData.PmEmail}");
                }
            }
            catch (Exception ex)
            {
                NLogger.Error(ex.Message);
            }

            NLogger.Trace("Finished ComplianceStatus");
        }

        private static String BuildMessage(ComplianceStatusData complianceStatusData)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("The {0} for {1} has changed to non-compliant and requires your action.", complianceStatusData.CategoryName, complianceStatusData.ItemName);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine(complianceStatusData.CategoryComment);
            sb.AppendLine();
            sb.AppendLine("Please log into ITIPS at your earliest convenience and address this issue.");
            return sb.ToString();
        }

        public ComplianceStatusData GetDataElements(psPortfoliosCategoryConditionMetEventInfo eventInfo, JobDataMap jobDataMap)
        {
            try
            {
                var connectionString = jobDataMap.GetString("ConnectionString");
                var categoryValues = jobDataMap.GetString("CategoryValues");
                var groupMembers = jobDataMap.GetString("GroupMembers");

                var proSightDal = new ProSightDal(connectionString);

                var complianceStatusData = new ComplianceStatusData
                {
                    CategoryName = eventInfo.CategoryName,
                    ProSightId = eventInfo.ScopeInfo.ProSightID,
                    ItemName = eventInfo.ScopeInfo.Name
                };

                var parameterList = new List<OracleParameter> { new OracleParameter { ParameterName = "portfolioId", Value = complianceStatusData.ProSightId } };
                var dataTable = proSightDal.GetDataTable(categoryValues, parameterList);

                foreach (DataRow dataTableRow in dataTable.Rows)
                {
                    if (dataTableRow.Field<String>("Name").Equals(complianceStatusData.CategoryName))
                    {
                        complianceStatusData.CategoryValue = dataTableRow.Field<String>("Value");
                        continue;
                    }
                    if (dataTableRow.Field<String>("Name").Equals("ITIPS ID")) complianceStatusData.ItipsId = dataTableRow.Field<String>("Value");
                    if (dataTableRow.Field<String>("Name").Equals("P_7 PM Email")) complianceStatusData.PmEmail = dataTableRow.Field<String>("Value");
                    if (dataTableRow.Field<String>("Name").Equals("G51 DITPR Number")) complianceStatusData.DitprId = dataTableRow.Field<String>("Value");
                    if (dataTableRow.Field<String>("Name").StartsWith(complianceStatusData.CategoryName)) complianceStatusData.CategoryComment = dataTableRow.Field<String>("Value");
                }

                //
                // no pm email so log it and bail
                //
                if (complianceStatusData.PmEmail.IsNullOrEmpty())
                {
                    NLogger.Error("Missing a PM Email address");
                    NLogger.Trace(complianceStatusData.ToXml());
                    return complianceStatusData;
                }

                parameterList = new List<OracleParameter> { new OracleParameter { ParameterName = "groupName", Value = $"%{complianceStatusData.ItipsId}%(Edit-Read)" } };
                dataTable = proSightDal.GetDataTable(groupMembers, parameterList);

                foreach (DataRow dataTableRow in dataTable.Rows)
                {
                    if (dataTableRow.Field<Int64>("is_enabled") != 1) continue;
                    var email = dataTableRow.Field<String>("Email");
                    if (email.IsEqualTo(complianceStatusData.PmEmail, true)) continue;
                    complianceStatusData.GroupEmails.Add(email);
                }
                return complianceStatusData;
            }
            catch (Exception ex)
            {
                NLogger.Error(ex.Message);
            }
            return new ComplianceStatusData();
        }
    }

    public class ComplianceStatusData
    {
        public Int32 ProSightId { get; set; }

        public String ItemName { get; set; }

        public String ItipsId { get; set; }

        public String DitprId { get; set; }

        public String CategoryName { get; set; }

        public String CategoryValue { get; set; }

        public String CategoryComment { get; set; }

        public String PmEmail { get; set; }

        public List<String> GroupEmails { get; set; }

        public ComplianceStatusData()
        {
            GroupEmails = new List<String>();

        }

    }
}
