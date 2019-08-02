// ***********************************************************************
// Assembly         : BMcD
// Author           : Gene Greiff
// Created          : 02-06-2015
//
// Last Modified By : Gene Greiff
// Last Modified On : 02-09-2015
// ***********************************************************************
// <copyright file="HelperFunctions.cs" company="NetImpact Strategies Inc.">
//     Copyright (c) NetImpact Strategies Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using NLog;
using OppmHelper.Utility;
using Quartz;
using wsPortfoliosCategory;
using wsPortfoliosCell;
using wsPortfoliosItem;

namespace ItipsWebServices.Utility
{
    /// <summary>
    /// Class HelperFunctions.
    /// </summary>
    public static class HelperFunctions
    {
        /// <summary>
        /// The n logger
        /// </summary>
        public static Logger NLogger = LogManager.GetCurrentClassLogger();


        public static String GetJobTypeClassName(SchedulesJob job)
        {
            var parts = job.Jobtype.Split(',');
            return parts.Length > 0 ? parts[0] : String.Empty;
        }

        public static String GetJobNameWithoutNamespace(String className)
        {
            var parts = className.Split('.');
            return parts.Length > 0 ? parts[parts.Length - 1] : String.Empty;
        }

        public static SchedulesJob GetSchedulesJobByName(String jobName)
        {
            foreach (var job in GlobalVariables.JobSchedules.Job)
            {
                if (job.Name.IsEqualTo(jobName, true)) return job;
            }
            return new SchedulesJob();
        }

        public static JobDataMap GetJobDataByClassName(String className)
        {
            foreach (var job in GlobalVariables.JobSchedules.Job)
            {
                var thisName = GetJobTypeClassName(job);
                if (thisName.IsEqualTo(className, true)) return GetJobDataMap(job);
            }
            return new JobDataMap();
        }

        public static JobDataMap GetJobDataMap(String jobName)
        {
            return GetJobDataMap(GetSchedulesJobByName(jobName));
        }

        public static JobDataMap GetJobDataMap(SchedulesJob job)
        {
            var jobDataMap = new JobDataMap();

            foreach (var jobEntry in job.JobDataMap)
            {
                jobDataMap.Add(jobEntry.Key, jobEntry.Value);
            }

            return jobDataMap;
        }


        public static String DumpJobDataMap(JobDataMap jobDataMap)
        {
            var sb = new StringBuilder();
            foreach (var jobEntry in jobDataMap)
            {
                sb.AppendLine($"Key: {jobEntry.Key} Value: {jobEntry.Value}");
            }

            return sb.ToString();
        }

        public static SchedulesJob GetAlertsSchedule(String alertName, Schedules jobSchedule)
        {
            try
            {
                var jobName = alertName.ExtractBetween("(", ")").Trim();

                foreach (var scheduleJob in jobSchedule.Job)
                {
                    if (scheduleJob.Name.IsNotEqualTo(jobName, true)) continue;
                    return scheduleJob;
                }
            }
            catch (Exception ex)
            {
                NLogger.Error("alertName missing , (jobName) in the name was {0}", alertName);
                NLogger.Trace(ex.Message);
            }
            return new SchedulesJob();
        }

        public static String GetAlertJobType(String alertName, SchedulesJob schedulesJob)
        {
            try
            {
                var jobTypeParts = schedulesJob.Jobtype.Split(',');
                return $"{jobTypeParts[1].Trim()}.{schedulesJob.Name}";
            }
            catch (Exception ex)
            {
                NLogger.Error(ex.Message);
            }

            return String.Empty;
        }


        /// <summary>
        /// Gets the portfolios cell information.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="psPortfoliosCellInfoList">The ps portfolios cell information list.</param>
        /// <returns>wsPortfoliosCell.psPortfoliosCellInfo.</returns>
        public static psPortfoliosCellInfo GetPortfoliosCellInfo(String categoryName, List<psPortfoliosCellInfo> psPortfoliosCellInfoList)
        {
            return psPortfoliosCellInfoList.Find(x => x.CategoryName.IsEqualTo(categoryName, true));
        }

        /// <summary>
        /// Builds the cell infos.
        /// </summary>
        /// <param name="categoryData">The category data.</param>
        /// <returns>CategoryMapList&lt;psPortfoliosCellInfo&gt;.</returns>
        public static List<psPortfoliosCellInfo> BuildItemCellInfos(Dictionary<String, String> categoryData)
        {
            return categoryData.Keys.Select(key => new psPortfoliosCellInfo { CategoryName = key, CellDisplayValue = categoryData[key] }).ToList();
        }

        /// <summary>
        /// Builds the cell information.
        /// </summary>
        /// <param name="categoryInfo">The category information.</param>
        /// <param name="categoryValue">The category value.</param>
        /// <returns>psPortfoliosCellInfo.</returns>
        public static psPortfoliosCellInfo BuildItemCellInfo(psPortfoliosCategoryInfo categoryInfo, String categoryValue)
        {
            if (categoryInfo.ValueType == psCATEGORY_VALUE_TYPE.CVTYP_DATETIME)
            {
                var dateTimeValue = categoryValue.ToDate();
                if (dateTimeValue.HasValue)
                    categoryValue = dateTimeValue.Value.ToShortDateString();
            }

            if (categoryInfo.ValueType == psCATEGORY_VALUE_TYPE.CVTYP_FLOAT)
            {
                var doubleValue = categoryValue.ToDouble();
                if (doubleValue.HasValue)
                    categoryValue = doubleValue.Value.ToString(CultureInfo.InvariantCulture);
            }

            if (categoryInfo.ValueType == psCATEGORY_VALUE_TYPE.CVTYP_INT)
            {
                var intValue = categoryValue.ToInt();
                if (intValue.HasValue)
                    categoryValue = intValue.Value.ToString(CultureInfo.InvariantCulture);
            }

            return BuildItemCellInfo(categoryInfo.Name, categoryValue);
        }

        /// <summary>
        /// Builds the cell information.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="categoryValue">The category value.</param>
        /// <returns>psPortfoliosCellInfo.</returns>
        public static psPortfoliosCellInfo BuildItemCellInfo(String categoryName, String categoryValue)
        {
            return BuildItemCellInfo(categoryName, categoryValue, String.Empty);
        }

        /// <summary>
        /// Builds the cell information.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="categoryValue">The category value.</param>
        /// <param name="cellAsOf">The cell as of.</param>
        /// <returns>psPortfoliosCellInfo.</returns>
        public static psPortfoliosCellInfo BuildItemCellInfo(String categoryName, String categoryValue, String cellAsOf)
        {
            var retVal = new psPortfoliosCellInfo
            {
                CategoryName = categoryName,
                CellDisplayValue = categoryValue,
                CellAsOf = cellAsOf
            };
            return retVal;
        }

        /// <summary>
        /// Builds the sub item cell infos.
        /// </summary>
        /// <param name="categoryData">The category data.</param>
        /// <returns>List&lt;subItemCell.psPortfoliosCellInfo&gt;.</returns>
        public static List<wsPortfoliosSubItem.psPortfoliosCellInfo> BuildSubItemCellInfos(Dictionary<String, String> categoryData)
        {
            return categoryData.Keys.Select(key => new wsPortfoliosSubItem.psPortfoliosCellInfo { CategoryName = key, CellDisplayValue = categoryData[key] }).ToList();
        }

        /// <summary>
        /// Builds the cell information.
        /// </summary>
        /// <param name="categoryInfo">The category information.</param>
        /// <param name="categoryValue">The category value.</param>
        /// <returns>psPortfoliosCellInfo.</returns>
        public static wsPortfoliosSubItem.psPortfoliosCellInfo BuildSubItemCellInfo(psPortfoliosCategoryInfo categoryInfo, String categoryValue)
        {
            if (categoryInfo.ValueType == psCATEGORY_VALUE_TYPE.CVTYP_DATETIME)
            {
                var dateTimeValue = categoryValue.ToDate();
                if (dateTimeValue.HasValue)
                    categoryValue = dateTimeValue.Value.ToShortDateString();
            }

            if (categoryInfo.ValueType == psCATEGORY_VALUE_TYPE.CVTYP_FLOAT)
            {
                var doubleValue = categoryValue.ToDouble();
                if (doubleValue.HasValue)
                    categoryValue = doubleValue.Value.ToString(CultureInfo.InvariantCulture);
            }

            if (categoryInfo.ValueType == psCATEGORY_VALUE_TYPE.CVTYP_INT)
            {
                var intValue = categoryValue.ToInt();
                if (intValue.HasValue)
                    categoryValue = intValue.Value.ToString(CultureInfo.InvariantCulture);
            }

            return BuildSubItemCellInfo(categoryInfo.Name, categoryValue);
        }

        /// <summary>
        /// Builds the cell information.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="categoryValue">The category value.</param>
        /// <returns>psPortfoliosCellInfo.</returns>
        public static wsPortfoliosSubItem.psPortfoliosCellInfo BuildSubItemCellInfo(String categoryName, String categoryValue)
        {

            return BuildSubItemCellInfo(categoryName, categoryValue, DateTime.Now.ToString("MM/dd/yyyy"));
        }

        /// <summary>
        /// Builds the cell information.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="categoryValue">The category value.</param>
        /// <param name="cellAsOf">The cell as of.</param>
        /// <returns>psPortfoliosCellInfo.</returns>
        public static wsPortfoliosSubItem.psPortfoliosCellInfo BuildSubItemCellInfo(String categoryName, String categoryValue, String cellAsOf)
        {
            var retVal = new wsPortfoliosSubItem.psPortfoliosCellInfo
            {
                CategoryName = categoryName,
                CellDisplayValue = categoryValue,
                CellAsOf = cellAsOf
            };
            return retVal;
        }

        /// <summary>
        /// Gets the name of the excel column.
        /// </summary>
        /// <param name="columnNumber">The column number.</param>
        /// <returns>String.</returns>
        public static String GetExcelColumnName(int columnNumber)
        {
            var dividend = columnNumber;
            var columnName = String.Empty;

            while (dividend > 0)
            {
                var modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }
            return columnName;
        }

        /// <summary>
        /// Enum RgbColor
        /// </summary>
        public enum RgbColor
        {
            /// <summary>
            /// The red
            /// </summary>
            Red,
            /// <summary>
            /// The green
            /// </summary>
            Green,
            /// <summary>
            /// The blue
            /// </summary>
            Blue
        }

        /// <summary>
        /// Gets the color of the RGB.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="rgbColor">Color of the RGB.</param>
        /// <returns>Double.</returns>
        public static Double GetRgbColor(Double color, RgbColor rgbColor)
        {
            if (rgbColor == RgbColor.Red) return color % 256;
            if (rgbColor == RgbColor.Red) return color / (256 % 256);
            if (rgbColor == RgbColor.Red) return color / (65536 % 256);
            return 0;
        }

        /// <summary>
        /// Serializes the item information list.
        /// </summary>
        /// <param name="itemInfoList">The item information list.</param>
        /// <returns>String.</returns>
        public static String SerializeItemInfoList(List<psPortfoliosItemInfo> itemInfoList)
        {
            try
            {
                var sw = new StringWriter();
                var s = new XmlSerializer(itemInfoList.GetType());
                s.Serialize(sw, itemInfoList);
                return sw.ToString();
            }
            catch (Exception ex)
            {
                NLogger.Warn(ex.Message);
                return String.Empty;
            }
        }



        /// <summary>
        /// Deserializes the item information list.
        /// </summary>
        /// <param name="itemInfoListXml">The item information list XML.</param>
        /// <returns>List&lt;psPortfoliosItemInfo&gt;.</returns>
        public static List<psPortfoliosItemInfo> DeserializeItemInfoList(String itemInfoListXml)
        {
            try
            {
                var xs = new XmlSerializer(typeof(List<psPortfoliosItemInfo>));
                var newList = (List<psPortfoliosItemInfo>)xs.Deserialize(new StringReader(itemInfoListXml));
                return newList;
            }
            catch (Exception ex)
            {
                NLogger.Warn(ex.Message);
                return new List<psPortfoliosItemInfo>();
            }
        }

        /// <summary>
        /// Serializes the XLSX file name list.
        /// </summary>
        /// <param name="xlsxFileNameList">The XLSX file name list.</param>
        /// <returns>String.</returns>
        public static String SerializeXlsxFileNameList(List<String> xlsxFileNameList)
        {
            try
            {
                var sw = new StringWriter();
                var xs = new XmlSerializer(xlsxFileNameList.GetType());
                xs.Serialize(sw, xlsxFileNameList);
                return sw.ToString();
            }
            catch (Exception ex)
            {
                NLogger.Warn(ex.Message);
                return String.Empty;
            }
        }

        /// <summary>
        /// Deserializes the XLSX file name list.
        /// </summary>
        /// <param name="xlsxFileNameListXml">The XLSX file name list XML.</param>
        /// <returns>List&lt;String&gt;.</returns>
        public static List<String> DeserializeXlsxFileNameList(String xlsxFileNameListXml)
        {
            try
            {
                var xs = new XmlSerializer(typeof(List<String>));
                var newList = (List<String>)xs.Deserialize(new StringReader(xlsxFileNameListXml));
                return newList;
            }
            catch (Exception ex)
            {
                NLogger.Warn(ex.Message);
                return new List<String>();
            }
        }

        public static String SerializeProSightEmail(HelperProSightEmail helperProSightEmail)
        {
            try
            {
                var sw = new StringWriter();
                var xs = new XmlSerializer(helperProSightEmail.GetType());
                xs.Serialize(sw, helperProSightEmail);
                return sw.ToString();
            }
            catch (Exception ex)
            {
                NLogger.Warn(ex.Message);
                return String.Empty;
            }
        }

        /// <summary>
        /// Deserializes the XLSX file name list.
        /// </summary>
        /// <param name="helperProSightEmail">The helper pro sight email.</param>
        /// <returns>List&lt;String&gt;.</returns>
        public static HelperProSightEmail DeserializeProSightEmail(String helperProSightEmail)
        {
            try
            {
                var xs = new XmlSerializer(typeof(HelperProSightEmail));
                var newList = (HelperProSightEmail)xs.Deserialize(new StringReader(helperProSightEmail));
                return newList;
            }
            catch (Exception ex)
            {
                NLogger.Warn(ex.Message);
                return new HelperProSightEmail();
            }
        }


        /// <summary>
        /// Builds the name of the item name into unique file name.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns>String.</returns>
        public static String BuildItemNameFileName(this String itemName)
        {
            var cleanName = itemName.CleanFileName();
            if (cleanName.Length <= 150) return cleanName;
            var index = cleanName.LastIndexOf('(');
            
            if (index == -1 && cleanName.Length < 113) return cleanName;
            if (index == -1) return String.Format("{0}_{1}", cleanName.Left(113), Guid.NewGuid());

            var uniquePart = cleanName.Right(cleanName.Length - index);
            var fileName = cleanName.Left(149 - uniquePart.Length);
            return $"{fileName} {uniquePart}";
        }

        /// <summary>
        /// returns an empty string.
        /// </summary>
        /// <returns>String.</returns>
        public static String EmptyString()
        {
            return String.Empty;
        }
    }
}
