// ***********************************************************************
// Assembly         : OppmHelper
// Author           : ggreiff
// Created          : 02-04-2015
//
// Last Modified By : ggreiff
// Last Modified On : 02-05-2015
// ***********************************************************************
// <copyright file="CurrentSelections.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Reflection;
using System.Text;
using System.Web;
using NLog;

namespace OppmHelper.Utility
{
    /// <summary>
    /// Class CurrentSelections.
    /// </summary>
    public class CurrentSelections
    {
        /// <summary>
        /// The NLOG Logger for this class.
        /// </summary>
        public static Logger PsLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets or sets the item ID.
        /// </summary>
        /// <value>The item ID.</value>
        public Int32? ItemId { get; set; }

        /// <summary>
        /// Gets or sets the sub item ID.
        /// </summary>
        /// <value>The sub item ID.</value>
        public Int32? SubItemId { get; set; }

        /// <summary>
        /// Gets or sets the extended info.
        /// </summary>
        /// <value>The extended info.</value>
        public String ExtendedInfo { get; set; }

        /// <summary>
        /// Gets or sets the map ID.
        /// </summary>
        /// <value>The map ID.</value>
        public Int32? MapId { get; set; }

        /// <summary>
        /// Gets or sets the VPF ID.
        /// </summary>
        /// <value>The VPF ID.</value>
        public Int32? VpfId { get; set; }

        /// <summary>
        /// Gets or sets the sc ID.
        /// </summary>
        /// <value>The sc ID.</value>
        public Int32? ScId { get; set; }

        /// <summary>
        /// Gets or sets the form ID.
        /// </summary>
        /// <value>The form ID.</value>
        public Int32? FormId { get; set; }

        /// <summary>
        /// Gets or sets the form tab ID.
        /// </summary>
        /// <value>The form tab ID.</value>
        public Int32? FormTabId { get; set; }

        /// <summary>
        /// Gets or sets the module.
        /// </summary>
        /// <value>The module.</value>
        public Int32? Module { get; set; }

        /// <summary>
        /// Gets or sets the current item.
        /// </summary>
        /// <value>The current item.</value>
        public Int32? CurrentItem { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public String User { get; set; }

        /// <summary>
        /// Gets or sets the locale.
        /// </summary>
        /// <value>The locale.</value>
        public String Locale { get; set; }

        /// <summary>
        /// Gets or sets the dashboard ID.
        /// </summary>
        /// <value>The dashboard ID.</value>
        public Int32? DashboardId { get; set; }

        /// <summary>
        /// Gets or sets the dashboard tab ID.
        /// </summary>
        /// <value>The dashboard tab ID.</value>
        public Int32? DashboardTabId { get; set; }

        /// <summary>
        /// Gets or sets as of ID.
        /// </summary>
        /// <value>As of ID.</value>
        public Int32? AsOfId { get; set; }

        /// <summary>
        /// Gets or sets as of date.
        /// </summary>
        /// <value>As of date.</value>
        public String AsOfDate { get; set; }

        /// <summary>
        /// Gets or sets the display language.
        /// </summary>
        /// <value>The display language.</value>
        public String DisplayLanguage { get; set; }

        /// <summary>
        /// Gets or sets the error MSG.
        /// </summary>
        /// <value>The error MSG.</value>
        public String ErrorMsg { get; set; }

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <value>The name of the module.</value>
        public String ModuleName
        {
            get
            {
                if (!Module.HasValue) return @"UNKNOWN";
                switch (Module.Value)
                {
                    case -5: return @"LAST_USED";
                    case -1: return @"NONE";
                    case 1: return @"TODO";
                    case 2: return @"INVESTOR";
                    case 3: return @"SCORECARD";
                    case 4: return @"DASHBOARD";
                    case 5: return @"WORKBOOK";
                    case 7: return @"FORMS";
                    case 8: return @"SETUP";
                    case 9: return @"PACKAGER_IMPORT";
                    case 10: return @"PACKAGER_EXPORT";
                    case 11: return @"PERIODIC_UPDATE_OF_QBP";
                    case 12: return @"IMPORT_EXTRACTED_DATA";
                    case 13: return @"UPGRADE_REPORT";
                    case 14: return @"TASKS_SCHEDULER";
                    case 15: return @"LICENSE_UPDATE";
                    case 16: return @"ADMIN";
                    case 18: return @"FUNCTION";
                    case 19: return @"CREATE_IMPORT_CATEGORIES";
                    case 20: return @"WORKFLOW_EDITOR";
                    case 21: return @"SNAPSHOT";
                    case 22: return @"APPLY_TO_ALL";
                    case 23: return @"ADMIN_ON_ALL_OBJECTS";
                    case 24: return @"USAGE_REPORT";
                    case 25: return @"DATABASE_CLEANUP";
                    case 26: return @"SEND_MAIL";
                    case 27: return @"UPLOAD_DOCUMENTS";
                    case 28: return @"CREATE_ALERTS";
                    case 29: return @"ALERTS_ACCESS_ALL_DATA";
                    case 30: return @"ENTIRE_SYSTEM_ALERTS";
                    case 31: return @"GLOBALS_CONFIG";
                    case 32: return @"PASTE_MULTIPLE_CELLS";
                    case 33: return @"USE_DATA_AS_OF_CONTROL";
                    case 34: return @"WORKFLOW_START";
                    case 35: return @"WORKFLOW_CAN_MANAGE_INSTANCES";
                    case 36: return @"WORKFLOW_CAN_SET_ACCESS_ALL_DATA";
                    default: return "UNKNOWN";
                }
            }
        }

        /// <summary>
        /// Gets the current selections.
        /// </summary>
        /// <param name="cookeiCollection">The cookei collection.</param>
        /// <returns>CurrentSelections.</returns>
        public static CurrentSelections GetCurrentSelections(HttpCookieCollection cookeiCollection)
        {
            var retVal = new CurrentSelections();
            try
            {
                var psCookies = cookeiCollection;
                var httpCookie = psCookies[@"currentSelections"];
                if (httpCookie != null)
                {
                    var cookieValue = HttpUtility.UrlDecode(httpCookie.Value);
                    PsLogger.Trace("CurrentSelectionsCookie = {0}", cookieValue);
                    if (!String.IsNullOrEmpty(cookieValue))
                    {
                        var cookieArray = cookieValue.Split(new[] { ',' });

                        retVal.ItemId = cookieArray[0].ToInt();
                        retVal.SubItemId = cookieArray[1].ToInt();
                        retVal.ExtendedInfo = cookieArray[2];
                        retVal.MapId = cookieArray[3].ToInt();
                        retVal.VpfId = cookieArray[4].ToInt();
                        retVal.ScId = cookieArray[5].ToInt();
                        retVal.FormId = cookieArray[6].ToInt();
                        retVal.FormTabId = cookieArray[7].ToInt();
                        retVal.Module = cookieArray[8].ToInt();
                        retVal.CurrentItem = cookieArray[9].ToInt();
                        retVal.User = cookieArray[10];
                        retVal.Locale = cookieArray[11];
                        retVal.DashboardId = cookieArray[12].ToInt();
                        retVal.DashboardTabId = cookieArray[13].ToInt();
                        retVal.AsOfId = cookieArray[14].ToInt();
                        retVal.AsOfDate = cookieArray[15];
                        retVal.DisplayLanguage = cookieArray[16];
                    }
                }
            }
            catch (Exception ex)
            {
                PsLogger.Error(ex.Message);
                PsLogger.Error(ex.InnerException);
                retVal = new CurrentSelections();
            }

            PsLogger.Trace("CurrentSelections\n{0}", retVal.ToString());
            return retVal;
        }

        /// <summary>
        /// Gets the tidnpssbmyaftn.
        /// </summary>
        /// <param name="cookeiCollection">The cookei collection.</param>
        /// <returns>String.</returns>
        public static String GetTidnpssbmyaftn(HttpCookieCollection cookeiCollection)
        {
            var retVal = String.Empty;
            try
            {
                var psCookies = cookeiCollection;
                var httpCookie = psCookies[@"TIDNPSSBMYAFTN"];
                if (httpCookie != null)
                {
                    var cookieValue = httpCookie.Value;
                    if (!String.IsNullOrEmpty(cookieValue))
                    {
                        retVal = HttpUtility.UrlDecode(cookieValue);
                    }
                }
            }
            catch (Exception ex)
            {
                PsLogger.Error(ex.Message);
                retVal = String.Empty;
            }

            PsLogger.Trace("GetTidnpssbmyaftn\n{0}", retVal);
            return retVal;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder("\n");
            Type reportSettings = GetType();
            PropertyInfo[] props = reportSettings.GetProperties();
            foreach (PropertyInfo p in props)
            {
                var pName = p.Name;
                var pValue = p.GetValue(this, null);
                sb.AppendFormat("{0}= {1}\n", pName, pValue);
            }
            return sb.ToString();
        }
    }



}
