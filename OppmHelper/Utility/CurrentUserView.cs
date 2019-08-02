using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Web;
using NLog;

namespace OppmHelper.Utility
{
    public class CurrentUserView
    {
        /// <summary>
        /// The NLOG Logger for this class.
        /// </summary>
        public static Logger PsLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets or sets the module id.
        /// </summary>
        /// <value>
        /// The module id.
        /// </value>
        public Int32? ModuleId { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>
        /// The name of the module.
        /// </value>
        public String ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the type of the module.
        /// </summary>
        /// <value>
        /// The type of the module.
        /// </value>
        public Int32? ModuleType { get; set; }

        /// <summary>
        /// Gets or sets the module window.
        /// </summary>
        /// <value>
        /// The module window.
        /// </value>
        public String ModuleWindow { get; set; }

        /// <summary>
        /// Gets or sets the item id.
        /// </summary>
        /// <value>
        /// The item id.
        /// </value>
        public Int32? ItemId { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>
        /// The name of the item.
        /// </value>
        public String ItemName { get; set; }

        /// <summary>
        /// Gets or sets the version id.
        /// </summary>
        /// <value>
        /// The version id.
        /// </value>
        public Int32? VersionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the version.
        /// </summary>
        /// <value>
        /// The name of the version.
        /// </value>
        public String VersionName { get; set; }

        /// <summary>
        /// Gets or sets the version date.
        /// </summary>
        /// <value>
        /// The version date.
        /// </value>
        public DateTime? VersionDate { get; set; }

        /// <summary>
        /// Gets or sets the user selections.
        /// </summary>
        /// <value>
        /// The user selections.
        /// </value>
        public CurrentSelections UserSelections { get; set; }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public Boolean IsValid()
        {
            var retVal = false;
            if (ModuleId.HasValue && ItemId.HasValue && VersionId.HasValue)
            {
                if (!ModuleName.IsNullOrEmpty() && !ModuleWindow.IsNullOrEmpty() &&
                    !ItemName.IsNullOrEmpty()) //Currently VersionName is not supported.
                {
                    retVal = true;
                }
            }
            return retVal;
        }

        /// <summary>
        /// Gets the current user view.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>CurrentUserView.</returns>
        public CurrentUserView GetCurrentUserView(HttpRequest httpRequest, Int32 userId)
        {
            PsLogger.Trace("GetCurrentUserView userId = {0}", userId.ToString(CultureInfo.InvariantCulture));
            var retVal = new CurrentUserView { UserSelections = CurrentSelections.GetCurrentSelections(httpRequest.Cookies) };
            try
            {
                if (retVal.UserSelections.CurrentItem.HasValue)
                {
                    retVal.ItemId = retVal.UserSelections.CurrentItem.Value;
                    retVal.VersionId = retVal.UserSelections.AsOfId;
                    retVal.VersionDate = retVal.UserSelections.AsOfDate.ToDate();

                    if (retVal.UserSelections.Module.HasValue)
                    {
                        retVal.ModuleType = retVal.UserSelections.Module.Value;
                        retVal.ModuleWindow = String.Empty;
                        if (retVal.UserSelections.Module == 1)
                        {
                            retVal.ModuleWindow = "todo";
                        }
                        if (retVal.UserSelections.Module == 2)
                        {
                            retVal.ModuleWindow = "map";
                            retVal.ModuleId = retVal.UserSelections.MapId;
                        }
                        if (retVal.UserSelections.Module == 3)
                        {
                            retVal.ModuleWindow = "sc";
                            retVal.ModuleId = retVal.UserSelections.ScId;
                        }
                        if (retVal.UserSelections.Module == 4)
                        {
                            retVal.ModuleWindow = "dash";
                            retVal.ModuleId = retVal.UserSelections.DashboardId;
                        }
                        if (retVal.UserSelections.Module == 5)
                        {
                            retVal.ModuleWindow = "wb";
                            retVal.ModuleId = retVal.ModuleId = retVal.UserSelections.ScId;
                        }
                        if (retVal.UserSelections.Module == 7)
                        {
                            retVal.ModuleWindow = "form";
                            retVal.ModuleId = retVal.UserSelections.FormId;
                        }
                    }

                    if (retVal.ModuleId.HasValue && retVal.ModuleType.HasValue)
                    {
                        //var itemInfo = String.Empty; GetItemInfo(retVal.UserSelections.CurrentItem.Value);
                        retVal.ItemName = String.Empty; //itemInfo.Name;
                        retVal.ModuleName = String.Empty; //psDal.GetModuleName(retVal.ModuleType.Value, retVal.ModuleId.Value);
                    }
                }


            }
            catch (Exception ex)
            {
                PsLogger.Error(ex.ToString()); // Warn -- log it for some who cares.
                retVal = new CurrentUserView { UserSelections = CurrentSelections.GetCurrentSelections(httpRequest.Cookies) };
            }

            PsLogger.Trace("GetCurrentUserView retVal = {0}", retVal.ToString());
            return retVal;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            var reportSettings = GetType();
            var props = reportSettings.GetProperties();
            foreach (PropertyInfo p in props)
            {
                var pName = p.Name;
                var pValue = p.GetValue(this, null);
                sb.AppendFormat("{0} = {1}\n", pName, pValue);
            }
            return sb.ToString();
        }
    }
}